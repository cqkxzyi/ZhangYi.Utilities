using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.序列化
{
    /// <summary>
    /// 自定义序列化
    /// </summary>
    public class JsonConverterHelper
    {
        /// <summary>
        /// 自定义序列化 长日期控制
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings GetJosnSetting()
        {
            var lstJsonConverter = new List<JsonConverter>
            {
                new CustomDateTimeConverter()
            };

            JsonSerializerSettings jsonSetting = new JsonSerializerSettings
            {
                Converters = lstJsonConverter
            };
            return jsonSetting;
        }

        /// <summary>
        /// 自定义序列化 异常处理
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings GetJosnSettingByNullError()
        {
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings
            {
                Error = (obj, args2) =>
                {
                    string error = args2.ErrorContext.Error.Message;
                    if (args2.ErrorContext.Error.InnerException.Message == "空对象不能转换为值类型。")
                        args2.ErrorContext.Handled = true;
                }
            };
            return jsonSetting;
        }
    }



    /// <summary>
    /// 长日期控制
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }


    /// <summary>
    /// 数值控制
    /// </summary>
    public class StrictIntConverter : JsonConverter
    {
        readonly JsonSerializer defaultSerializer = new JsonSerializer();

        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            var result = objectType.IsNumeric2();
            return result;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float: // Accepts numbers like 4.00
                case JsonToken.Null:
                    return defaultSerializer.Deserialize(reader, objectType);
                default:
                    throw new JsonSerializationException(string.Format("Token \"{0}\" of type {1} 不是一个数值", reader.Value, reader.TokenType));
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            new NotImplementedException();
        }
    }

    /// <summary>
    /// 数值控制
    /// </summary>
    public class StrictIntConverter2 : JsonConverter
    {
        //是否开启自定义反序列化，值为true时，反序列化时会走ReadJson方法，值为false时，不走ReadJson方法，而是默认的反序列化
        public override bool CanRead => true;
        //是否开启自定义序列化，值为true时，序列化时会走WriteJson方法，值为false时，不走WriteJson方法，而是默认的序列化
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //var model = new Model();
            ////获取JObject对象，该对象对应着我们要反序列化的json
            //var jobj = serializer.Deserialize<JObject>(reader);
            ////从JObject对象中获取键位ID的值
            //var id = jobj.Value<bool>("ID");
            ////根据id值判断，进行赋值操作
            //if (id)
            //{
            //    model.ID = 1;
            //}
            //else
            //{
            //    model.ID = 0;
            //}
            ////最终返回的model对象就是json反序列化所得到的Model对象
            ////主要，这里的model对象不一定非得是Model类型，ReadJson方法与WriteJson方法是一样的，可以自由操作反序列生成的对象或者序列化生成的json
            //return model;
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ////new一个JObject对象,JObject可以像操作对象来操作json
            //var jobj = new JObject();
            ////value参数实际上是你要序列化的Model对象，所以此处直接强转
            //var model = value as Model;
            //if (model.ID != 1)
            //{
            //    //如果ID值为1，添加一个键位"ID"，值为false
            //    jobj.Add("ID", false);
            //}
            //else
            //{
            //    jobj.Add("ID", true);
            //}
            ////通过ToString()方法把JObject对象转换成json
            //var jsonstr = jobj.ToString();
            ////调用该方法，把json放进去，最终序列化Model对象的json就是jsonstr，由此，我们就能自定义的序列化对象了
            //writer.WriteValue(jsonstr);
            writer.WriteValue("");
        }
    }
}
