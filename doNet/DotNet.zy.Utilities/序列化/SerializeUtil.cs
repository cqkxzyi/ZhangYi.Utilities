using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace CY.UPM.Utility.Util
{
    /// <summary>
    /// Newtonsoft序列化
    /// </summary>
    public static class SerializeUtil
    {
        /// <summary>
        /// 序列化Json对象
        /// <remarks>采用Newtonsoft序列化类</remarks>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignorenull">是否忽略空值属性</param>
        /// <returns></returns>
        public static string SerializeJson(object obj, bool ignorenull=false)
        {
            return SerializeJson<object>(obj, ignorenull);
        }

        /// <summary>
        /// 序列化Json对象
        /// <remarks>采用Newtonsoft序列化类</remarks>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignorenull">是否忽略空值属性</param>
        /// <param name="converters">自定义转换器</param>
        /// <returns></returns>
        public static string SerializeJson(object obj, bool ignorenull, IList<JsonConverter> converters)
        {
            return SerializeJson<object>(obj, ignorenull, converters);
        }

        /// <summary>
        /// 序列化Json对象
        /// <remarks>采用Newtonsoft序列化类</remarks>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignorenull">是否忽略空值属性</param>
        /// <returns></returns>
        public static string SerializeJson<T>(T obj, bool ignorenull) where T : class
        {
            return SerializeJson<object>(obj, ignorenull, null);
        }

        /// <summary>
        /// 序列化Json对象
        /// <remarks>采用Newtonsoft序列化类</remarks>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignorenull">是否忽略空值属性</param>
        /// <param name="converters">自定义转换器</param>
        /// <returns></returns>
        public static string SerializeJson<T>(T obj, bool ignorenull, IList<JsonConverter> converters) where T : class
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = ignorenull ? NullValueHandling.Ignore : NullValueHandling.Include;
            setting.Converters = converters;
            return JsonConvert.SerializeObject(obj, Formatting.None, setting);
        }

        /// <summary>
        /// 序列化Json对象
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
        /// 反序列化Json对象
        /// <remarks>采用Newtonsoft序列化类</remarks>
        /// <param name="str"></param>
        /// <param name="ignorenull">是否忽略空值属性</param>
        /// </summary>
        public static T DeserializeJson<T>(string str, bool ignorenull) where T : class
        {
            return DeserializeJson<T>(str, ignorenull, null);
        }

        /// <summary>
        /// 反序列化Json对象
        /// <remarks>采用Newtonsoft序列化类</remarks>
        /// <param name="str"></param>
        /// <param name="ignorenull">是否忽略空值属性</param>
        /// <param name="converters">自定义转换器</param>
        /// </summary>
        public static T DeserializeJson<T>(string str, bool ignorenull, IList<JsonConverter> converters) where T : class
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = ignorenull ? NullValueHandling.Ignore : NullValueHandling.Include;
            setting.Converters = converters;
            return JsonConvert.DeserializeObject<T>(str, setting);
        }










        /// <summary>
        /// 序列化object对象
        /// </summary>
        public static string SerializeObject(object obj)
        {
            byte[] bytes = SerializeBinary(obj);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 反序列化object对象
        /// </summary>
        public static object DeserializeObject(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return DesrializeBinary(bytes);
        }






        /// <summary>
        /// 二进制序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeBinary(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(stream, obj);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static object DesrializeBinary(byte[] buffer)
        {
            BinaryFormatter format = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                return format.Deserialize(stream);
            }
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static T DesrializeBinary<T>(byte[] buffer)
        {
            return (T)DesrializeBinary(buffer);
        }
    }
}
