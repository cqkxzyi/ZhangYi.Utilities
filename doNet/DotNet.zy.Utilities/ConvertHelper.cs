/** 处理数据类型转换，数制转换、编码转换相关的类
 * 
 * 开发部门：IT 
 * 程序负责：zhangyi
 * 其他联系：重庆市、深圳市
 * Email：kxyi-lover@163.com
 * MSN：10011
 * QQ:284124391
 * 声明：仅限于您自己使用，不得进行商业传播，违者必究！
 * */
using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 处理数据类型转换，数制转换、编码转换相关的类
    /// </summary>    
    public sealed class ConvertHelper
    {
        #region 将Object类型数据安全转换为String类型数据

        /// <summary>
        /// 将Object类型数据安全转换为String类型数据
        /// </summary>
        /// <param name="objStr">Object类型数据</param>
        /// <returns>返回String类型数据</returns>
        public static string CastString(object objStr)
        {
            string strResult = string.Empty;

            if (null == objStr)
            {
                return "";
            }

            try
            {
                strResult = objStr.ToString();
            }
            catch { }

            return strResult;
        }

        #endregion

        #region 将Object类型数据安全转换为Int类型数据

        /// <summary>
        /// 将Object类型数据安全转换为Int类型数据
        /// </summary>
        /// <param name="objInt">Object类型数据</param>
        /// <returns>返回Int类型数据（转换失败，默认值为0）</returns>
        public static int CastInt(object objInt)
        {
            return SafeCastInt(objInt, 0);
        }

        /// <summary>
        /// 将Object类型数据安全转换为Int类型数据
        /// </summary>
        /// <param name="objInt">Object类型数据</param>
        /// <param name="nDefault">转换失败默认值</param>
        /// <returns>返回Int类型数据</returns>
        public static int SafeCastInt(object objInt, int nDefault)
        {
            int nValue = nDefault;

            try
            {
                nValue = Int32.Parse(objInt.ToString());
            }
            catch (Exception)
            {
                nValue = nDefault;
            }

            return nValue;
        }

        /// <summary>
        /// create user:houjing
        /// create date:2012-08-04
        /// create desc:将枚举转换为整型数据
        /// 将Object类型数据安全转换为Int类型数据
        /// </summary>
        /// <param name="objInt">Object类型数据</param>
        /// <param name="nDefault">转换失败默认值</param>
        /// <returns>返回Int类型数据</returns>
        public static int SafeCastIntForEnum(object objInt)
        {
            int nValue = 0;

            try
            {
                nValue = (int)objInt;
            }
            catch (Exception)
            {
            }

            return nValue;
        }
        #endregion

        #region 将Object类型数据安全转换为Bool类型数据

        /// <summary>
        /// 将Object类型数据安全转换为Bool类型数据
        /// </summary>
        /// <param name="objBool">Object类型数据</param>
        /// <returns>返回Bool类型数据（转换失败，默认值为'False'）</returns>
        public static bool CastBool(object objBool)
        {
            return SafeCastBool(objBool, false);
        }

        /// <summary>
        /// 将Object类型数据安全转换为Bool类型数据
        /// </summary>
        /// <param name="objBool">Object类型数据</param>
        /// <param name="bDefault">转换失败默认值</param>
        /// <returns>返回Bool类型数据</returns>
        public static bool SafeCastBool(object objBool, bool bDefault)
        {
            bool bValue = bDefault;

            try
            {
                bValue = bool.Parse(objBool.ToString());
            }
            catch (Exception)
            {
                bValue = bDefault;
            }

            return bValue;
        }

        #endregion

        #region 将Object类型数据安全转换为Float类型数据

        /// <summary>
        /// 将Object类型数据安全转换为Float类型数据
        /// </summary>
        /// <param name="objFloat">Object类型数据</param>
        /// <returns>返回Float类型数据（转换失败，默认值为0）</returns>
        public static float CastFloat(object objFloat)
        {
            return SafeCastFloat(objFloat, 0);
        }

        /// <summary>
        /// 将Object类型数据安全转换为Float类型数据
        /// </summary>
        /// <param name="objFloat">Object类型数据</param>
        /// <param name="fDefault">转换失败默认值</param>
        /// <returns>返回Float类型数据</returns>
        public static float SafeCastFloat(object objFloat, float fDefault)
        {
            float fValue = fDefault;

            try
            {
                fValue = float.Parse(objFloat.ToString());
            }
            catch (Exception)
            {
                fValue = fDefault;
            }

            return fValue;
        }

        #endregion

        #region 将Object类型数据安全转换为Double类型数据

        /// <summary>
        /// 将Object类型数据安全转换为Double类型数据
        /// </summary>
        /// <param name="objDouble">Object类型数据</param>
        /// <returns>返回Double类型数据（转换失败，默认值为0）</returns>
        public static double CastDouble(object objDouble)
        {
            return SafeCastDouble(objDouble, 0);
        }

        /// <summary>
        /// 将Object类型数据安全转换为Double类型数据
        /// </summary>
        /// <param name="objDouble">Object类型数据</param>
        /// <param name="dDefault">转换失败默认值</param>
        /// <returns>返回Double类型数据</returns>
        public static double SafeCastDouble(object objDouble, double dDefault)
        {
            double dValue = dDefault;

            try
            {
                dValue = double.Parse(objDouble.ToString());
            }
            catch (Exception)
            {
                dValue = dDefault;
            }

            return dValue;
        }

        #endregion

        #region 将Object类型数据安全转换为Long类型数据

        /// <summary>
        /// 将Object类型数据安全转换为Long类型数据
        /// </summary>
        /// <param name="objLong">Object类型数据</param>
        /// <returns>返回Long类型数据（转换失败，默认值为0）</returns>
        public static long CastLong(object objLong)
        {
            return SafeCastLong(objLong, 0);
        }

        /// <summary>
        /// 将Object类型数据安全转换为Long类型数据
        /// </summary>
        /// <param name="objLong">Object类型数据</param>
        /// <param name="dDefault">转换失败默认值</param>
        /// <returns>返回Long类型数据</returns>
        public static long SafeCastLong(object objLong, long dDefault)
        {
            long lValue = dDefault;

            try
            {
                lValue = long.Parse(objLong.ToString());
            }
            catch (Exception)
            {
                lValue = dDefault;
            }

            return lValue;
        }

        #endregion

        #region 将Object类型数据安全转换为DateTime类型数据

        /// <summary>
        /// 将Object类型数据安全转换为DateTime类型数据
        /// </summary>
        /// <param name="objDt">Object类型数据</param>
        /// <returns>返回DateTime类型数据</returns>
        public static DateTime SafeCastDateTime(object objDt)
        {
            DateTime dtValue = DateTime.MinValue;

            try
            {
                dtValue = DateTime.Parse(objDt.ToString());
            }
            catch { }

            return dtValue;
        }
        /// <summary>
        /// 将Object类型数据安全转换为DateTime类型数据
        /// </summary>
        /// <param name="objDt"></param>
        /// <param name="defaultDate"></param>
        /// <returns></returns>
        public static DateTime SafeCastDateTime(object objDt, DateTime defaultDate)
        {
            DateTime dtValue = DateTime.MinValue;

            try
            {
                dtValue = DateTime.Parse(objDt.ToString());
            }
            catch { }

            if (dtValue == DateTime.MinValue)
            {
                dtValue = defaultDate;
            }

            return dtValue;
        }
        #endregion

        #region 将Object类型数据安全转换为Decimal类型数据

        /// <summary>
        /// 将Object类型数据安全转换为Decimal类型数据
        /// </summary>
        /// <param name="objInt">Object类型数据</param>
        /// <returns>返回Decimal类型数据（转换失败，默认值为0）</returns>
        public static decimal CastDecimal(object objInt)
        {
            return CastDecimal(objInt, 0);
        }

        /// <summary>
        /// 将Object类型数据安全转换为Decimal类型数据
        /// </summary>
        /// <param name="objInt">Object类型数据</param>
        /// <param name="nDefault">转换失败默认值</param>
        /// <returns>返回Decimal类型数据</returns>
        public static decimal CastDecimal(object objInt, decimal nDefault)
        {
            decimal nValue = nDefault;

            try
            {
                nValue = decimal.Parse(objInt.ToString());
            }
            catch (Exception)
            {
                nValue = nDefault;
            }

            return nValue;
        }

        #endregion



        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }
        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion

        #region byte[]转换成字string
        /// <summary>
        /// byte[]转换成字string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ByteArrayToHexString(byte[] data)
        {
            //方法1
            //StringBuilder sb = new StringBuilder(data.Length * 3);
            //foreach (byte b in data)
            //   sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            //return sb.ToString().ToUpper();

            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

            //方法2
            string str = System.Text.Encoding.ASCII.GetString(data, 0, data.Length);
            byte[] inputBytes = System.Convert.FromBase64String(str);
        }
        #endregion

        #region string转换成byte[]
        /// <summary>
        /// string转换成byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] HexStringToByteArray(string s)
        {
            //方法1
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;

            //方法2
            //byte[] imageStr2 = System.Text.Encoding.ASCII.GetBytes(s); 
        }
        #endregion

        #region 移除SQL敏感词
        /// <summary>
        /// 移除SQL敏感词 返回新字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceSQLFilter(string str)
        {
            //移除空格
            str = str.Trim();

            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            //string[] pattern = { "'", "--", "select", "insert", "delete", "from", "count\\(", "drop table", "update", "truncate", "asc\\(", "mid\\(", "char\\(", "xp_cmdshell", "exec   master", "netlocalgroup administrators", "net user", "or", "and" };
            string[] pattern = { "'" };
            for (int i = 0; i < pattern.Length; i++)
            {
                str = str.Replace(pattern[i].ToString(), "");
            }
            return str;
        }
        #endregion

        #region 分解数据表
        ///<summary>
        /// 分解数据表
        ///</summary>
        ///<param name="originalTab">需要分解的表</param>
        ///<param name="rowsNum">每个表包含的数据量</param>
        ///<returns></returns>
        public static DataSet SplitDataTable(DataTable originalTab, int rowsNum)
        {
            try
            {
                //获取所需创建的表数量
                int tableNum = originalTab.Rows.Count / rowsNum;
                //获取数据余数
                int remainder = originalTab.Rows.Count % rowsNum;
                DataSet ds = new DataSet();
                //如果只需要创建1个表，直接将原始表存入DataSet
                if (tableNum == 0)
                {
                    ds.Tables.Add(originalTab.Copy());
                }
                else
                {
                    DataTable[] tableSlice = new DataTable[tableNum];
                    //Save orginal columns into new table. 
                    for (int c = 0; c < tableNum; c++)
                    {
                        tableSlice[c] = new DataTable();
                        foreach (DataColumn dc in originalTab.Columns)
                        {
                            tableSlice[c].Columns.Add(dc.ColumnName, dc.DataType);
                        }
                    }
                    //Import Rows
                    for (int i = 0; i < tableNum; i++)
                    {
                        // if the current table is not the last one
                        if (i != tableNum - 1)
                        {
                            for (int j = i * rowsNum; j < ((i + 1) * rowsNum); j++)
                            {
                                tableSlice[i].ImportRow(originalTab.Rows[j]);
                            }
                        }
                        else
                        {
                            for (int k = i * rowsNum; k < ((i + 1) * rowsNum + remainder); k++)
                            {
                                tableSlice[i].ImportRow(originalTab.Rows[k]);
                            }
                        }
                    }

                    foreach (DataTable dt in tableSlice)
                    {
                        ds.Tables.Add(dt);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteLine("分解数据表出错");
                throw ex;
            }
        }
        #endregion

        #region 把数组string[]按照分隔符转换成string
        /// <summary>
        /// 把数组string[]按照分隔符转换成string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetArrayStr(string[] list, string speater = ",")
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                if (i == list.Length - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 序列、反序列化Xml文件
        public static string ToXml<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder output = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                serializer.Serialize(writer, item);
                return output.ToString();
            }
        }

        public static object XmlDeserialize(Type type, string filename)
        {
            object obj2 = null;
            if (!File.Exists(filename))
            {
                return obj2;
            }
            using (StreamReader reader = new StreamReader(filename, Encoding.Default))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }
        }

        public static void XmlSerialize(object obj, string filename)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                new XmlSerializer(obj.GetType()).Serialize((Stream)stream, obj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        #endregion


        #region  dataTable某列转换为字符串
        /// <summary>
        /// dataTable某列转换为字符
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="name">列名</param>
        /// <returns>字段值数组</returns>
        public static string FieldToString(DataTable dataTable, string name)
        {
            int rowCount = 0;
            string stringList = "";

            if (dataTable == null)
            {
                return "";
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!string.IsNullOrEmpty(dataRow[name].ToString()))
                {
                    rowCount++;
                    stringList += dataRow[name].ToString() + ",";
                }
            }
            if (rowCount > 0)
            {
                stringList = stringList.TrimEnd(',');
            }
            return stringList;
        }
        #endregion

        #region  dataTable某列转换为字符串
        /// <summary>
        /// dataTable某列转换为字符
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="name">列名</param>
        /// <returns>字段值数组</returns>
        public static List<String> FieldToList(DataTable dataTable, string name)
        {
            int rowCount = 0;
            List<String> strList = new List<String>();

            if (dataTable == null)
            {
                return null;
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!string.IsNullOrEmpty(dataRow[name].ToString()))
                {
                    rowCount++;
                    strList.Add(dataRow[name].ToString());
                }
            }
            return strList;
        }
        #endregion


        #region Dictionary<>转换成字符串
        /// <summary>
        /// Dictionary<>转换成字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayValueStr(Dictionary<int, int> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kvp in list)
            {
                sb.Append(kvp.Value + ",");
            }
            if (list.Count > 0)
            {
                return StringHelper.DelLastComma(sb.ToString());
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region Dictionary<object, object>转String
        /// <summary>
        /// Dictionary<object, object>转String
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string DictionaryToQueryString(Dictionary<object, object> dic)
        {
            string result = string.Empty;
            if (dic != null && dic.Count > 0)
            {
                foreach (KeyValuePair<object, object> kvp in dic)
                {
                    if (result.Length == 0)
                    {
                        result = result + kvp.Key.ToString().Trim() + "=" + kvp.Value.ToString();
                    }
                    else
                    {
                        result = result + "&" + (kvp.Key).ToString() + "=" + (kvp.Value).ToString(); ;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 将List<T>转换成DataTable,如果列表为空将返回空的DataTable结构
        /// <summary>
        /// 将List<T>转换成DataTable,如果列表为空将返回空的DataTable结构
        /// </summary>
        /// <typeparam name="T">要转换的数据类型</typeparam>
        /// <param name="entityList">实体对象列表</param> 
        public static DataTable EntityListToDataTable<T>(List<T> entityList)
        {
            DataTable dt = new DataTable();

            //取类型T所有Propertie
            Type entityType = typeof(T);
            PropertyInfo[] entityProperties = entityType.GetProperties();
            Type colType = null;
            foreach (PropertyInfo propInfo in entityProperties)
            {

                if (propInfo.PropertyType.IsGenericType)
                {
                    colType = Nullable.GetUnderlyingType(propInfo.PropertyType);
                }
                else
                {
                    colType = propInfo.PropertyType;
                }

                if (colType.FullName.StartsWith("System"))
                {
                    dt.Columns.Add(propInfo.Name, colType);
                }
            }

            if (entityList != null && entityList.Count > 0)
            {
                foreach (T entity in entityList)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (PropertyInfo propInfo in entityProperties)
                    {
                        if (dt.Columns.Contains(propInfo.Name))
                        {
                            object objValue = propInfo.GetValue(entity, null);
                            newRow[propInfo.Name] = objValue == null ? DBNull.Value : objValue;
                        }
                    }
                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }
        #endregion

        #region 将对象转换成Dictionary<String, Object>
        /// <summary>
        /// 将对象转换成Dictionary<String, Object>
        /// </summary>  
        /// <param name="o"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> ToMap(Object o)
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }
            return map;
        }
        #endregion

        #region 将DataTable转换成List<T>
        /// <summary>
        /// 将DataTable转换成List<T>
        /// </summary>
        /// <typeparam name="T">实体对象的类型</typeparam>
        /// <param name="dt">要转换的DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToEntityList<T>(DataTable dt)
        {
            List<T> entiyList = new List<T>();

            Type entityType = typeof(T);
            PropertyInfo[] entityProperties = entityType.GetProperties();

            foreach (DataRow row in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>();

                foreach (PropertyInfo propInfo in entityProperties)
                {
                    if (dt.Columns.Contains(propInfo.Name))
                    {
                        if (!row.IsNull(propInfo.Name))
                        {
                            propInfo.SetValue(entity, row[propInfo.Name], null);
                        }
                    }
                }

                entiyList.Add(entity);
            }

            return entiyList;
        }
        #endregion

        #region 复制对象
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="srcObject"></param>
        /// <returns></returns>
        public static Object DeepClone(Object srcObject)
        {
            //定义内存流
            MemoryStream ms = new MemoryStream();
            //定义二进制流
            IFormatter bf = new BinaryFormatter();
            //序列化
            bf.Serialize(ms, srcObject);
            //重置指针到起始位置，以备反序列化
            ms.Position = 0;
            //返回反序列化的深克隆对象
            return bf.Deserialize(ms);
        }
        #endregion


    }

    /// <summary>
    /// 复制对象（不同对象间的复制）
    /// </summary>
    public static class ModelCopier
    {
        public static void CopyCollection<T>(IEnumerable<T> from, ICollection<T> to)
        {
            if (from == null || to == null || to.IsReadOnly)
            {
                return;
            }

            to.Clear();
            foreach (T element in from)
            {
                to.Add(element);
            }
        }

        public static void CopyModel(object from, object to)
        {
            if (from == null || to == null)
            {
                return;
            }

            PropertyDescriptorCollection fromProperties = TypeDescriptor.GetProperties(from);
            PropertyDescriptorCollection toProperties = TypeDescriptor.GetProperties(to);

            foreach (PropertyDescriptor fromProperty in fromProperties)
            {
                PropertyDescriptor toProperty = toProperties.Find(fromProperty.Name, true /* ignoreCase */);
                if (toProperty != null && !toProperty.IsReadOnly)
                {
                    // Can from.Property reference just be assigned directly to to.Property reference?
                    bool isDirectlyAssignable = toProperty.PropertyType.IsAssignableFrom(fromProperty.PropertyType);
                    // Is from.Property just the nullable form of to.Property?
                    bool liftedValueType = (isDirectlyAssignable) ? false : (Nullable.GetUnderlyingType(fromProperty.PropertyType) == toProperty.PropertyType);

                    if (isDirectlyAssignable || liftedValueType)
                    {
                        object fromValue = fromProperty.GetValue(from);
                        if (isDirectlyAssignable || (fromValue != null && liftedValueType))
                        {
                            toProperty.SetValue(to, fromValue);
                        }
                    }
                }
            }
        }

    }
}
