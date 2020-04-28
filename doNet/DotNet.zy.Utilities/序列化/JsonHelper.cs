using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace DotNet.zy.Utilities
{
    public static class JsonHelper1
    {
        #region 方式一JsonSerializer(用到了Newtonsoft.Json日期格式能正确转换)

        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string NewtonsoftGetJson<T>(this T obj)
        {
            var timeFormat = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

            return JsonConvert.SerializeObject(obj, Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 把JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="szJson">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T NewtonsoftGetEntity<T>(this string szJson)
        {
            return JsonConvert.DeserializeObject<T>(szJson);
        }
        #endregion

        #region System.Runtime.Serialization.Json方式序列化
        /// <summary>  
        /// JSON序列化  
        /// </summary>  
        public static string JsonSerializer<T>(this T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串  
            string p = @"///Date/((/d+)/+/d+/)///"; /*////Date/((([/+/-]/d+)|(/d+))[/+/-]/d+/)////*/
            MatchEvaluator matchEvaluator = new MatchEvaluator(_ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string _ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(this string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"\d{4}-\d{2}-\d{2}(\s\d{2}:\d{2}:\d{2})?";
            MatchEvaluator matchEvaluator = new MatchEvaluator(_ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string _ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = (DateTime)m.Groups[0].Value.ToDateTime();
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);

            return result;
        }
        #endregion



        #region 将指定串值转换JSON格式(很简单)
        /// <summary>
        /// 将指定串值转换JSON格式
        /// </summary>
        /// <param name="name">Key值</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        public static string StringToJson(string name, string value)
        {
            return "{\"" + name + ":\"" + value + "\"}";
        }
        /// <summary>
        /// 将指定串值转换JSON格式
        /// </summary>
        /// <param name="name">Key值</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        public static string StringToJson(string name, int value)
        {
            return "{\"" + name + "\":" + value.ToString() + "}";
        }
        #endregion

        #region 泛型列表转换为JSON(带名称key)
        /// <summary>
        /// 泛型列表转换为JSON(带名称key)
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <param name="ClassName">属性名称</param>
        /// <returns></returns>
        public static string IListToJSON<T>(IList<T> list, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            foreach (T t in list)
            {
                sb.Append(ModelToJSON(t) + ",");
            }

            string _temp = sb.ToString().TrimEnd(',');
            _temp += "]}";
            return _temp;
        }
        #endregion

        #region 泛型列表转换为JSON
        /// <summary>
        /// 泛型列表转换为JSON
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public static string IList2JSON<T>(IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (T t in list)
            {
                sb.Append(ModelToJSON(t) + ",");
            }

            string _temp = sb.ToString().TrimEnd(',');
            _temp += "]";
            return _temp;
        }
        #endregion

        #region 数据实体类转换为JSON
        /// <summary>
        /// 数据实体类转换为JSON
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="t">数据实体类</param>
        /// <returns></returns>
        public static string ModelToJSON<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            string json = "";
            if (t != null)
            {
                sb.Append("{");
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    sb.Append("\"" + pi.Name.ToString().ToLower() + "\"");
                    sb.Append(":");
                    sb.Append("\"" + pi.GetValue(t, null).ToString().Replace("\"", "“").Replace("'", "‘").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<", "＜").Replace(">", "＞") + "\"");
                    sb.Append(",");
                }

                json = sb.ToString().TrimEnd(',');
                json += "}";
            }

            return json;
        }
        #endregion

        #region 将数组转换为JSON格式的字符串
        /// <summary>
        /// 将数组转换为JSON格式的字符串
        /// </summary>
        /// <typeparam name="T">数据类型，如string,int ...</typeparam>
        /// <param name="list">泛型list</param>
        /// <param name="propertyname">JSON的类名</param>
        /// <returns></returns>
        public static string ArrayToJSON<T>(List<T> list, string propertyname)
        {
            StringBuilder sb = new StringBuilder();
            if (list.Count > 0)
            {
                sb.Append("[{\"");
                sb.Append(propertyname);
                sb.Append("\":[");

                foreach (T t in list)
                {
                    sb.Append("\"");
                    sb.Append(t.ToString());
                    sb.Append("\",");
                }

                string _temp = sb.ToString();
                _temp = _temp.TrimEnd(',');

                _temp += "]}]";

                return _temp;
            }
            else
                return "";
        }

        public static string ArrayToJSON(string[] strs, string var)
        {
            return var + "=" + ArrayToJSON(strs);
        }

        public static string ArrayToJSON(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.AppendFormat("'{0}':'{1}',", i + 1, strs[i]);
            }
            if (sb.Length > 0)
                return "{" + sb.ToString().TrimEnd(',') + "}";
            return "";
        }

        #endregion

        #region 将json的时间字符串转换成DateTime
        /// <summary>
        /// 将json的时间字符串转换成DateTime
        /// </summary>将json的时间字符串转换成DateTime
        /// <param name="jsonDate"></param>
        /// <returns></returns>
        public static DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }
        #endregion

        #region Datatable转换为Json
        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtName"></param>
        /// <returns></returns>
        public static string DataTableToJSON(DataTable dt, string dtName)
        {
            string s = DataTableToJSONJquery(dt);
            s = "{\"" + dtName + "\":" + s + "}";
            return s;
        }

        public static string DataTableToJSONJquery(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{");
                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", dt.Columns[n].ColumnName.ToLower(), dt.Rows[i][n].ToString().Replace("\"", "“").Replace("'", "‘").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<", "＜").Replace(">", "＞"));
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1).Insert(0, "[").Append("]");
                return sb.ToString();
            }
            return "";
        }
        #endregion

        #region 将某行转换成JSon
        /// <summary>
        /// 将某行转换成JSon
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string DataRowToJSON(DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            if (row == null)
                return "";
            sb.Append("{");
            for (int n = 0; n < row.Table.Columns.Count; n++)
            {
                sb.AppendFormat("\"{0}\":\"{1}\",", row.Table.Columns[n].ColumnName.ToLower(), row[n].ToString().Replace("\"", "“").Replace("'", "‘").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<", "＜").Replace(">", "＞"));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
        #endregion



        #region 获取指定表的数据，并转换为jqgrid 的JSON格式。适用于sql2000 以上版本
        /// <summary>
        /// 获取指定表的数据，并转换为jqgrid 的JSON格式。适用于sql2000 以上版本
        /// </summary>
        /// <param name="pageindex">当前第几页</param>
        /// <param name="pagesize">每页记录条数</param>
        /// <param name="orderfield">排序字段 如：id asc,name desc</param>
        /// <param name="key">主键</param>
        /// <param name="where">筛选条件</param>
        /// <param name="tbname">表或视图名</param>
        /// <returns></returns>
        public static string GetJsonforjQgrid(int pageindex, int pagesize, string orderfield, string key, string where, string tbname)
        {
            return GetJsonforjQgrid("*", pageindex, pagesize, orderfield, key, where, tbname);
        }

        /// <summary>
        /// 获取指定表的数据，并转换为jqgrid 的JSON格式。适用于sql2000 以上版本
        /// </summary>
        /// <param name="fields">要选取的列，以逗号隔开</param>
        /// <param name="pageindex">当前第几页</param>
        /// <param name="pagesize">每页记录条数</param>
        /// <param name="orderfield">排序</param>
        /// <param name="key">关键字</param>
        /// <param name="where">条件</param>
        /// <param name="tbname">表名或视图名</param>
        /// <returns></returns>
        public static string GetJsonforjQgrid(string fields, int pageindex, int pagesize, string orderfield, string key, string where, string tbname)
        {
            int recordcount = 0;


            DataTable dt = SqlEasy.GetDataByPager2000(fields, tbname, where, orderfield, key, pageindex, pagesize, out recordcount);
            int pagecount = SqlEasy.GetDataPages(pagesize, recordcount);
            //string json = DataTableToJSONJquery(dt);
            string json = JsonConvert.SerializeObject(dt);

            string s = "{\"totalpages\":\"" + pagecount.ToString() + "\",\"currpage\":\"" + pageindex.ToString() + "\",\"totalrecords\":\"" + recordcount.ToString() + "\",\"griddata\":}";

            json = s.Insert(s.Length - 1, json);

            return json;
        }
        #endregion

        #region 获取指定表的数据，并转换为jqgrid 的JSON格式。适用于sql2005 以上版本
        /// <summary>
        /// 获取指定表的数据，并转换为jqgrid 的JSON格式。适用于sql2005 以上版本
        /// </summary>
        /// <param name="pageindex">当前第几页</param>
        /// <param name="pagesize">每页记录条数</param>
        /// <param name="order">排序</param>
        /// <param name="where">条件</param>
        /// <param name="tbname">表名或视图名</param>
        /// <returns></returns>
        public static string GetJsonforjQgrid(int pageindex, int pagesize, string order, string where, string tbname)
        {
            return GetJsonforjQgrid("*", pageindex, pagesize, order, where, tbname);
        }

        /// <summary>
        /// 获取指定表的数据，并转换为jqgrid 的JSON格式。适用于sql2005 以上版本
        /// </summary>
        /// <param name="fields">要选取的列，以逗号隔开</param>
        /// <param name="pageindex">当前第几页</param>
        /// <param name="pagesize">每页记录条数</param>
        /// <param name="order">排序</param>
        /// <param name="where">条件</param>
        /// <param name="tbname">表名或视图名</param>
        /// <returns></returns>
        public static string GetJsonforjQgrid(string fields, int pageindex, int pagesize, string order, string where, string tbname)
        {
            int recordcount = SqlEasy.GetRecordCount(tbname, where);
            int pagecount = SqlEasy.GetDataPages(pagesize, recordcount);

            DataTable dt = SqlEasy.GetDataByPager2005(fields, tbname, where, order, pageindex, pagesize);

            string json = JsonHelper1.DataTableToJSON(dt, "griddata");

            string s = "\"totalpages\":\"" + pagecount.ToString() + "\",\"currpage\":\"" + pageindex.ToString() + "\",\"totalrecords\":\"" + recordcount.ToString() + "\",";

            json = json.Insert(1, s);

            return json;
        }
        #endregion



        #region 获取easyui datagrid 所需要的JSON数据
        /// <summary>
        /// 获取easyui datagrid 所需要的JSON数据
        /// </summary>
        /// <param name="pageindex">第几页</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="keyfield">主键字段名</param>
        /// <param name="where">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public static string GetJsonForEasyuiDatagrid(int pageindex, int pagesize, string keyfield, string where, string sort, string tablename)
        {
            int recordcount = 0;

            DataTable dt = SqlEasy.GetDataByPager2000("*", tablename, where, sort, keyfield, pageindex, pagesize, out recordcount);

            string s = "{\"total\":" + recordcount.ToString() + ",\"rows\":" + JsonConvert.SerializeObject(dt) + "}";
            return s;
        }
        #endregion

    }
}
