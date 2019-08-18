using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 序列、反序列化Xml文件
    /// </summary>
    public class XmlSerializeHelper
    {
        #region XmlSerializer序列化
        /// <summary>
        /// 对象序列化成Xml，返回xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder output = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                serializer.Serialize(writer, item);
                return output.ToString();
            }
        }

        /// <summary>
        /// 对象序列化成Xml，并保存xml文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
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

        /// <summary>
        /// Xml反序列化成对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object XmlDeserializeObject(Type type, string filename)
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

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public static T XmlDeserializeObject<T>(string str)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        #endregion

        #region SoapFormatter序列化
        /// <summary>
        /// SoapFormatter序列化
        /// 已经被微软放弃
        /// </summary>
        /// <param name="item">对象</param>
        public string ToSoap<T>(T item)
        {
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// SoapFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromSoap<T>(string str)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                xmlDoc.Save(ms);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

        #region BinaryFormatter序列化
        /// <summary>
        /// BinaryFormatter序列化
        /// [Serializable] 和[NonSerialized]控制是否允许字段进行序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToBinary<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                byte[] bytes = ms.ToArray();
                StringBuilder sb = new StringBuilder();
                foreach (byte bt in bytes)
                {
                    sb.Append(string.Format("{0:X2}", bt));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromBinary<T>(string str)
        {
            int intLen = str.Length / 2;
            byte[] bytes = new byte[intLen];
            for (int i = 0; i < intLen; i++)
            {
                int ibyte = Convert.ToInt32(str.Substring(i * 2, 2), 16);
                bytes[i] = (byte)ibyte;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

    }
}

