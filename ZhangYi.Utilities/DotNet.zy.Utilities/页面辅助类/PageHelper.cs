using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace DotNet.zy.Utilities
{
    public class PageHelper
    {
        #region 锁定页面上的一些组件
        /// <summary>
        /// 锁定页面上的一些组件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">不需锁定的控件</param>
        public static void LockPage(Page page, object[] obj)
        {
            Control htmlForm = null;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is HtmlForm)
                {
                    htmlForm = ctl;
                    break;
                }
            }
            //foreach (Control ctl in page.Controls[1].Controls)
            foreach (Control ctl in htmlForm.Controls)
            {
                if (IsContains(obj, ctl) == false)
                {
                    //锁定
                    LockControl(page, ctl);
                }
                else
                {
                    //解除锁定
                    UnLockControl(page, ctl);
                }
            }
        }
        #endregion

        #region 解除锁定页面上的一些组件
        /// <summary>
        /// 解除锁定页面上的一些组件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">继续保持锁定的控件</param>
        public static void UnLockPage(Page page, object[] obj)
        {
            Control htmlForm = null;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is HtmlForm)
                {
                    htmlForm = ctl;
                    break;
                }
            }
            //foreach (Control ctl in page.Controls[1].Controls)
            foreach (Control ctl in htmlForm.Controls)
            {
                if (IsContains(obj, ctl) == false)
                {
                    //解除锁定
                    UnLockControl(page, ctl);
                }
                else
                {
                    //锁定
                    LockControl(page, ctl);
                }
            }
        }
        #endregion

        #region 禁用控件
        /// <summary>
        /// 禁用控件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        private static void LockControl(Page page, Control ctl)
        {
            //WebControl
            if (ctl is Button || ctl is CheckBox || ctl is HyperLink || ctl is LinkButton
                || ctl is ListControl || ctl is TextBox)
            {
                ((WebControl)ctl).Enabled = false;

                #region 多行文本框不能禁用，应设为只读，不然滚动条不能使用

                if (ctl is TextBox)
                {
                    if (((TextBox)ctl).TextMode == TextBoxMode.MultiLine)
                    {
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = true;
                    }
                }

                #endregion

                #region 时间控件禁用时不显示图片



                #endregion
            }

            //HtmlControl
            if (ctl is HtmlInputFile)
            {
                ((HtmlInputFile)ctl).Disabled = true;
            }
        }
        #endregion

        #region 开放控件
        /// <summary>
        /// 开放控件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        private static void UnLockControl(Page page, Control ctl)
        {
            //WebControl
            if (ctl is Button || ctl is CheckBox || ctl is HyperLink || ctl is LinkButton
                || ctl is ListControl || ctl is TextBox)
            {
                ((WebControl)ctl).Enabled = true;

                //文本框去掉只读属性
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).ReadOnly = false;
                }

                ////时间输入文本框不禁用时显示按钮
                //if (ctl is WebDateTimeEdit)
                //{
                //    ((WebDateTimeEdit)ctl).SpinButtons.Display = ButtonDisplay.OnRight;
                //}

                ////时间选择文本框不禁用时显示按钮
                //if (ctl is WebDateChooser)
                //{
                //    page.ClientScript.RegisterStartupScript(typeof(string), "Display" + ctl.ClientID + "Image", "<script language=javascript>" +
                //        "document.getElementById('" + ctl.ClientID + "_img" + "').style.display='';</script>");
                //}
            }

            //HtmlControl
            if (ctl is HtmlInputFile)
            {
                ((HtmlInputFile)ctl).Disabled = false;
            }
        }
        #endregion

        #region 数组中是否包含当前控件
        /// <summary>
        /// 数组中是否包含当前控件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ctl"></param>
        /// <returns></returns>
        private static bool IsContains(object[] obj, Control ctl)
        {
            foreach (Control c in obj)
            {
                if (c.ID == ctl.ID)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


        #region 得到当前页对象实例
        /// <summary>
        /// 得到当前页对象实例
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }
        #endregion

        #region 从System.Web.HttpRequest的Url中获取所调用的页面名称
        /// <summary>
        /// 从System.Web.HttpRequest的Url中获取所调用的页面名称
        /// </summary>
        /// <returns>页面名称</returns>
        public static string GetPageName()
        {
            int start = 0;
            int end = 0;
            string Url = HttpContext.Current.Request.RawUrl;
            start = Url.LastIndexOf("/") + 1;
            end = Url.IndexOf("?");
            if (end <= 0)
            {
                return Url.Substring(start, Url.Length - start);
            }
            else
            {
                return Url.Substring(start, end - start);
            }
        }
        #endregion

        #region 页面跳转
        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url">URL地址</param>
        public void Redirect(string url)
        {
            Page page = GetCurrentPage();
            page.Response.Redirect(url);
        }
        #endregion

        #region 获取当前请求页面相对于根目录的层级
        /// <summary>
        /// 获取当前请求页面相对于根目录的层级
        /// </summary>
        /// <returns></returns>
        public static string GetRelativeLevel()
        {
            string ApplicationPath = HttpContext.Current.Request.ApplicationPath;
            if (ApplicationPath.Trim() == "/")
            {
                ApplicationPath = "";
            }

            int i = ApplicationPath == "" ? 1 : 2;
            return "";//Nandasoft.Helper.NDHelperString.Repeat("../", Nandasoft.Helper.NDHelperString.RepeatTime(HttpContext.Current.Request.Path, "/") - i);
        }
        #endregion

        #region 返回客户端浏览器版本
        /// <summary>
        /// 返回客户端浏览器版本
        /// 如果是IE类型，返回版本数字
        /// 如果不是IE类型，返回-1
        /// </summary>
        /// <returns>一位数字版本号</returns>
        public static int GetClientBrowserVersion()
        {
            string USER_AGENT = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

            if (USER_AGENT.IndexOf("MSIE") < 0) return -1;

            string version = USER_AGENT.Substring(USER_AGENT.IndexOf("MSIE") + 5, 1);
            if (!RegexCheck.IsInt(version)) return -1;

            return Convert.ToInt32(version);
        }
        #endregion


        #region 导出文件的通用方法

        /// <summary>
        /// 导出文件的通用方法
        /// </summary>
        /// <param name="strFilePath">文件名（包含路径）</param>
        /// <param name="strCntType">输出格式</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="isNeedDelete">下载以后是否要删除</param>
        /// <param name="prEncoding"></param>
        /// <returns></returns>
        public static bool ExportCommonFile(string strFilePath, string strCntType, string FileName = "", bool isNeedDelete = true, params Encoding[] prEncoding)
        {
            if (!(File.Exists(strFilePath)))
            {
                return false;
            }

            if (FileName.Trim().Length > 0)
            {
                InitializeExporting(FileName, strCntType);
            }
            else
            {
                InitializeExporting(Path.GetFileName(strFilePath), strCntType);
            }

            Encoding encoding = Encoding.Default;
            if (prEncoding.Length > 0 && prEncoding[0] != null)
            {
                encoding = prEncoding[0];
            }

            byte[] buffer = File.ReadAllBytes(strFilePath);
            if (isNeedDelete)
            {
                File.Delete(strFilePath);
            }
            HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

            return true;
        }


        #region 文件导出HTTP头初始化操作
        /// <summary>
        /// 文件导出HTTP头初始化操作
        /// </summary>
        /// <param name="strFileName">文件名称</param>
        /// <param name="strCntType">导出格式</param>
        private static void InitializeExporting(string strFileName, string strCntType)
        {
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", strFileName));
            //HttpContext.Current.Response.ContentType = strCntType;
            //HttpContext.Current.Response.AddHeader("DonsonNTX", "public");

            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;

            if (request.UserAgent.ToLower().IndexOf("msie") > -1)
            {
                strFileName = ToHexString(strFileName);
            }

            if (request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + strFileName + "\"");
            }
            else
            {
                response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            }
        }
        #endregion

        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }
            return builder.ToString();
        }

        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        } 

        /// <summary>
        /// Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }       
        #endregion



    }
}
