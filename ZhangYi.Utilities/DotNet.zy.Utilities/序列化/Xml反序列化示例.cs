using DotNet.zy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNet.zy.Utilities
{
    class Xml反序列化示例
    {
        public void  GetVersion()
        {
            string baseDirectory = DirFile.GetServerPath("/Xml/AppVersion.xml");
            AppVersionModels models = (AppVersionModels)ConvertHelper.XmlDeserialize(typeof(AppVersionModels), baseDirectory);
            List<AppVersionModel> list = new List<AppVersionModel>(models.Items);
        }


        #region Xml文件解析实体

        [XmlRoot("AppVersionModels", IsNullable = false)]
        public class AppVersionModels
        {
            [XmlArray("Items")]
            public AppVersionModel[] Items { get; set; }

        }

        [XmlRoot("AppVersionModel")]
        public class AppVersionModel
        {
            [XmlAttribute("Id")]
            public string Id { get; set; }

            [XmlAttribute("Type")]
            public string Type { get; set; }

            [XmlAttribute("Name")]
            public string Name { get; set; }

            [XmlAttribute("Version")]
            public string Version { get; set; }

            [XmlAttribute("Content")]
            public string Content { get; set; }
        }
        #endregion



    }








}
