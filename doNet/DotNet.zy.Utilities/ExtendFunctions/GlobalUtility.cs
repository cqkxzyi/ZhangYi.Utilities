/******************************************
 * Description: 系统全局通用方法
 ******************************************/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace System
{
    public static class GlobalUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this Object obj) where T : class
        {
            return obj as T;
        }

        /// <summary>
        /// List中是否有该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Boolean IsIn<T>(this T obj, IEnumerable<T> collection)
        {
            return collection.Contains(obj);
        }

        /// <summary>
        /// 判断该类型是否实现了指定接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tinterface"></param>
        /// <returns></returns>
        public static Boolean IsImplemented(this Type type, Type tinterface)
        {
            if (tinterface.IsInterface)
            {
                return type.GetInterfaces().Contains(tinterface);
            }
            return false;
        }

        /// <summary>
        /// 创建对象的深度克隆，对象类型必须标记为可序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 转换对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this Object model) where T : new()
        {
            if (model == null) return default(T);
            var targetType = typeof(T);
            var sourceType = model.GetType();

            return (T)model.ConvertTo(targetType);
        }

        /// <summary>
        /// 转换对象
        /// </summary>
        /// <param name="model"></param>
        /// <param name="targetType"></param>
        /// <param name="deeprank"></param>
        /// <returns></returns>
        private static Object ConvertTo(this Object model, Type targetType, Int32 deeprank = 0)
        {
            if (deeprank > 10 || model == null) return null;

            var sourceType = model.GetType();
            if (sourceType == targetType) return model;
            //if (sourceType.Name.Contains("Proxy")) return model;

            if (sourceType.IsImplemented(typeof(IList)))
            {
                var elementtype = targetType.GenericTypeArguments.FirstOrDefault();
                Object list;
                if (elementtype != null)
                {
                    Type generic = typeof(List<>).MakeGenericType(elementtype);
                    list = Activator.CreateInstance(generic) as IList;
                }
                else list = targetType.Assembly.CreateInstance(targetType.FullName);

                try
                {
                    foreach (var item in (IList)model)
                    {
                        list.As<IList>().Add(item.ConvertTo(elementtype, deeprank + 1));
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return list;
            }

            Object res = targetType.Assembly.CreateInstance(targetType.FullName);

            foreach (var propertity in targetType.GetProperties())
            {
                if (!propertity.CanWrite) continue;

                try
                {
                    var proname = propertity.Name;
                    var sourcePropertity = sourceType.GetProperty(proname);
                    if (sourcePropertity == null) continue;
                    var sourcevalue = sourcePropertity.GetValue(model);
                    if (sourcevalue == null) continue;
                    if (sourcePropertity.PropertyType != propertity.PropertyType)
                    {
                        if (sourcePropertity.PropertyType.IsValueType)
                        {
                            propertity.SetValue(res, sourcevalue);
                            continue;
                        }
                        sourcevalue = sourcevalue.ConvertTo(propertity.PropertyType, deeprank + 1);
                        if (sourcevalue == null) continue;
                    }
                    propertity.SetValue(res, sourcevalue);
                }
                catch (TargetInvocationException ex)
                {
                    return null;
                }
                catch (ArgumentException ex)
                {
                    return null;
                }
            }
            return res;
        }

        /// <summary>
        /// 尝试把字符串转换成枚举形式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"> </param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(this String str) where T : struct, IConvertible
        {
            T t;
            return Enum.TryParse(str, true, out t) ? t : default(T);
        }

        /// <summary>
        /// 将 String 转换为 Guid
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns></returns>
        public static Guid ToGuid(this String str)
        {
            if (String.IsNullOrEmpty(str)) return Guid.Empty;

            Guid obj;
            Guid.TryParse(str.Trim(), out obj);
            return obj;
        }

        /// <summary>
        /// 将 String 转换为 Guid
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>s
        public static Guid ToGuid(this String str,Guid defaultValue)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;

            Guid obj;
            return !Guid.TryParse(str.Trim(), out obj) ? defaultValue : obj;
        }
        /// <summary>
        /// 将 String 转换为 Int32
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">转换错误返回值，默认为-1【可选参数】</param>
        /// <returns></returns>
        public static Int32 ToInt32(this String str, Int32 defaultValue = -1)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;

            Int32 obj;
            return Int32.TryParse(str.Trim(), out obj) ? obj : defaultValue;
        }

        /// <summary>
        /// 将 String 转换为 Double
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">转换错误返回值，默认为-1【可选参数】</param>
        /// <returns></returns>
        public static Double ToDouble(this String str, Double defaultValue = -1)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;
            Double obj;
            return Double.TryParse(str.Trim(), out obj) ? obj : defaultValue;
        }
        /// <summary>
        /// 将 String 转换为 float
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">转换错误返回值，默认为-1【可选参数】</param>
        /// <returns></returns>
        public static float ToFloat(this String str, float defaultValue = -1)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;
            float obj;
            return float.TryParse(str.Trim(), out obj) ? obj : defaultValue;
        }
        /// <summary>
        /// 将 String 转换为 DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this String str)
        {
            if (String.IsNullOrEmpty(str)) return DateTimeHelper.GetDefaultTime();

            DateTime obj;
            return DateTime.TryParse(str.Trim(), out obj) ? obj : DateTimeHelper.GetDefaultTime();
        }

        #region JsonSerializer
        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string GetJson<T>(this T obj)
        {
            var timeFormat = new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss"};

            return JsonConvert.SerializeObject(obj,Formatting.Indented,timeFormat);
        }

        /// <summary>
        /// 把JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="szJson">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T ParseFormJson<T>(this string szJson)
        {
            return JsonConvert.DeserializeObject<T>(szJson);
        }
        #endregion

        /// <summary>
        /// 获取类型的默认值，等同于default(type)的返回值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object GetDefaultValue(this Type type)
        {
            return Assembly.GetAssembly(type).CreateInstance(type.FullName);
        }

        /// <summary>
        /// 复制对象属性
        /// </summary>
        /// <typeparam name="T">赋值对象</typeparam>
        /// <param name="model">当前对象</param>
        /// <param name="source">值来源对象</param>
        public static void CloneProperties<T>(this T model, Object source)
        {
            var type = typeof (T);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name == "ID") continue;
                var sourcePpts = source.GetType().GetProperty(propertyInfo.Name);
                if (sourcePpts == null)
                    continue;
                if (sourcePpts.PropertyType != propertyInfo.PropertyType) continue;
                var value = sourcePpts.GetValue(source);
                if (propertyInfo.PropertyType == typeof (String))
                {
                    if (value == null)
                        value = String.Empty;
                    value = value.ToString().Trim();
                }
                propertyInfo.SetValue(model, value);
            }
        }

        /// <summary>
        /// 复制对象属性
        /// </summary>
        /// <typeparam name="TIn">赋值对象</typeparam>
        /// <typeparam name="TOut">值来源对象</typeparam>
        /// <param name="model"></param>
        /// <param name="source"></param>
        public static void CloneProperties<TIn, TOut>(this TIn model, TOut source)
        {
            var type = typeof(TIn);
            foreach (var propertyInfo in type.GetProperties())
            {
                var sourcePpts = typeof(TIn).GetProperty(propertyInfo.Name);
                if (sourcePpts == null)
                    continue;
                if (propertyInfo.PropertyType != typeof(String) && sourcePpts.PropertyType != propertyInfo.PropertyType) continue;

                var value = sourcePpts.GetValue(source);
                if (propertyInfo.PropertyType == typeof(String))
                {
                    if (value == null)
                        value = String.Empty;
                    if (sourcePpts.PropertyType != typeof(String))
                        value = value.ToString();
                    value = value.ToString().Trim();
                }
                propertyInfo.SetValue(model, value);
            }
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public class DateTimeHelper
    {
        public static DateTime GetDefaultTime()
        {
            return DateTime.Parse("1/1/1753 12:00:00");//SQL数据库允许的最小时间值
        }
    }
}
