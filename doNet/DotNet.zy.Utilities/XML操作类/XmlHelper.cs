using System.Xml;
using System.Data;
using System;
using System.IO;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    public class XmlHelper
    {

        #region 字段定义
        /// <summary>
        /// XML文件的物理路径
        /// </summary>
        private string _filePath = string.Empty;
        /// <summary>
        /// Xml文档
        /// </summary>
        private XmlDocument _xml;
        /// <summary>
        /// XML的根节点
        /// </summary>
        private XmlElement _element;
        #endregion

        #region 构造方法
        /// <summary>
        /// 实例化XmlHelper对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        public XmlHelper(string xmlFilePath)
        {
            //获取XML文件的绝对路径
            _filePath = Tools.GetPhysicalPath(xmlFilePath);
        }
        #endregion

        #region 创建XML的根节点
        /// <summary>
        /// 创建XML的根节点
        /// </summary>
        private void CreateXMLElement()
        {

            //创建一个XML对象
            _xml = new XmlDocument();

            if (DirFile.IsExistFile(_filePath))
            {
                //加载XML文件
                _xml.Load(this._filePath);
            }

            //为XML的根节点赋值
            _element = _xml.DocumentElement;
        }
        #endregion

        #region 获取指定XPath表达式的节点对象
        /// <summary>
        /// 获取指定XPath表达式的节点对象
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public XmlNode GetNode(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();

            //返回XPath节点
            return _element.SelectSingleNode(xPath);
        }
        #endregion

        #region 获取指定XPath表达式节点的值
        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public string GetValue(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();

            //返回XPath节点的值
            return _element.SelectSingleNode(xPath).InnerText;
        }
        #endregion

        #region 获取指定XPath表达式节点的属性值
        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public string GetAttributeValue(string xPath, string attributeName)
        {
            //创建XML的根节点
            CreateXMLElement();

            //返回XPath节点的属性值
            return _element.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
        #endregion

        #region 新增节点
        /// <summary>
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将任意节点插入到当前Xml文件中。
        /// </summary>        
        /// <param name="xmlNode">要插入的Xml节点</param>
        public void AppendNode(XmlNode xmlNode)
        {
            //创建XML的根节点
            CreateXMLElement();

            //导入节点
            XmlNode node = _xml.ImportNode(xmlNode, true);

            //将节点插入到根节点下
            _element.AppendChild(node);
        }

        /// <summary>
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将DataSet中的第一条记录插入Xml文件中。
        /// </summary>        
        /// <param name="ds">DataSet的实例，该DataSet中应该只有一条记录</param>
        public void AppendNode(DataSet ds)
        {
            //创建XmlDataDocument对象
            XmlDataDocument xmlDataDocument = new XmlDataDocument(ds);

            //导入节点
            XmlNode node = xmlDataDocument.DocumentElement.FirstChild;

            //将节点插入到根节点下
            AppendNode(node);
        }
        #endregion

        #region 删除节点
        /// <summary>
        /// 删除指定XPath表达式的节点
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public void RemoveNode(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();

            //获取要删除的节点
            XmlNode node = _xml.SelectSingleNode(xPath);

            //删除节点
            _element.RemoveChild(node);
        }
        #endregion //删除节点

        #region 保存XML文件
        /// <summary>
        /// 保存XML文件
        /// </summary>        
        public void Save()
        {
            //创建XML的根节点
            CreateXMLElement();

            //保存XML文件
            _xml.Save(this._filePath);
        }
        #endregion //保存XML文件

        #region 静态方法

        #region 创建根节点对象
        /// <summary>
        /// 创建根节点对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>        
        private static XmlElement CreateRootElement(string xmlFilePath)
        {
            //定义变量，表示XML文件的绝对路径
            string filePath = "";

            //获取XML文件的绝对路径
            filePath = Tools.GetPhysicalPath(xmlFilePath);

            //创建XmlDocument对象
            XmlDocument xmlDocument = new XmlDocument();
            //加载XML文件
            xmlDocument.Load(filePath);

            //返回根节点
            return xmlDocument.DocumentElement;
        }
        #endregion

        #region 获取指定XPath表达式节点的值
        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public static string GetValue(string xmlFilePath, string xPath)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);

            //返回XPath节点的值
            return rootElement.SelectSingleNode(xPath).InnerText;
        }
        #endregion

        #region 获取指定XPath表达式节点的属性值
        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public static string GetAttributeValue(string xmlFilePath, string xPath, string attributeName)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);

            //返回XPath节点的属性值
            return rootElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
        #endregion

        #endregion

        #region 写入XML  public static void WriteXML(string path)
        /// <summary>
        /// 写入XML
        /// </summary>
        public static void WriteXML(string path)
        {
            try
            {
                // 创建XmlTextWriter类的实例对象
                XmlTextWriter textWriter = new XmlTextWriter(path, null);
                textWriter.Formatting = Formatting.Indented;

                // 开始写过程，调用WriteStartDocument方法
                textWriter.WriteStartDocument();

                // 写入说明
                textWriter.WriteComment("First Comment XmlTextWriter Sample Example");
                textWriter.WriteComment("w3sky.xml in root dir");

                //创建一个节点
                textWriter.WriteStartElement("Administrator");
                textWriter.WriteElementString("Name", "formble");
                textWriter.WriteElementString("site", "w3sky.com");
                textWriter.WriteEndElement();

                // 写文档结束，调用WriteEndDocument方法
                textWriter.WriteEndDocument();

                // 关闭textWriter
                textWriter.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

        #region XML转换成DataTable 方式一
        /// <summary>
        /// XML转换成DataTable
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="xmlPath"></param>
        /// <returns></returns>

        public static DataTable GetXmlToDataTable(string Field, string xmlPath)
        {
            string[] SplitField = Field.ToString().Split(',');
            //构造DataTable            
            DataTable dt = new DataTable();


            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);

            XmlNode firstNode = xdoc.DocumentElement.SelectSingleNode("/SystemList");

            XmlNodeList xnlist = firstNode.ChildNodes;
            //XmlNodeList excelColumnList = firstNode.SelectNodes("Column");

            //子节点所有数据                   
            for (int i = 0; i < xnlist.Count; i++)//这句是所有xml子节点数据                   
            {
                if (i == 0)
                {
                    //初始化DataTable
                    for (int j = 0; j < xnlist[i].Attributes.Count; j++)
                    {
                        string name = xnlist[i].Attributes[j].Name;
                        DataColumn dc = new DataColumn(name);
                        dt.Columns.Add(dc);
                    }
                }


                DataRow dr = dt.NewRow();
                for (int j = 0; j < xnlist[i].Attributes.Count; j++)
                {
                    string name = xnlist[i].Attributes[j].Name;
                    string value = xnlist[i].Attributes[j].Value;

                    dr[name] = value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region XML转换成DataTable  public DataTable ReadXMLToDataTableng(path)
        /// <summary>
        /// XML转换成DataTable
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataTable ReadXMLToDataTable(string path)
        {
            try
            {
                StreamReader tyj = new StreamReader(path);
                XmlDataDocument datadoc = new XmlDataDocument();
                //创建该对象为了读取XML
                datadoc.DataSet.ReadXml(tyj);
                //读取guest.xml文件内容
                DataTable db = new DataTable();
                db = datadoc.DataSet.Tables[0];
                datadoc = null;
                tyj.Close();//释放资源
                return db;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 建立XML文档
        public static XmlDocument CreateXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            doc.InsertBefore(decl, doc.DocumentElement);
            return doc;
        }

        public static XmlDocument CreateXmlDocument(string rootName)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            doc.InsertBefore(decl, doc.DocumentElement);
            XmlNode newNode = doc.CreateElement(rootName);
            doc.AppendChild(newNode);
            return doc;
        }
        #endregion
    }
}
