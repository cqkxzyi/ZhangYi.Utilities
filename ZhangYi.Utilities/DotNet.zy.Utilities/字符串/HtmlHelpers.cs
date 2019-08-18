using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// Html格式控制
    /// </summary>
    public partial class HtmlHelpers
    {
        #region HTML转行成TEXT
        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);", 
            @"&(nbsp|#160);", 
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
        #endregion

        #region 字符串转换成Html格式编码
        /// <summary>
        /// 字符串转换成Html格式编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputStr)
        {
            return HttpUtility.HtmlEncode(inputStr);
        }
        #endregion

        #region HTML编码格式化
        /// <summary>
        /// HTML编码格式化
        /// </summary>
        /// <param name="inputStr">string</param>
        /// <returns>string</returns>
        public static string ToEncode(string inputStr)
        {
            inputStr = inputStr.Replace("&", "&amp;");
            inputStr = inputStr.Replace("'", "''"); //"&#39;"
            inputStr = inputStr.Replace("\"", "&quot;");
            inputStr = inputStr.Replace(" ", "&nbsp;");
            inputStr = inputStr.Replace("<", "&lt;");
            inputStr = inputStr.Replace(">", "&gt;");
            inputStr = inputStr.Replace("\n", "<br>");
            return inputStr;
        }
        #endregion

        #region HTML编码格式化
        /// <summary>
        /// HTML编码格式化
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        public static string GetEncodeHtml(string sDetail)
        {
            Regex r;
            Match m;

            #region 处理空格
            sDetail = sDetail.Replace(" ", "&nbsp;");
            #endregion
            #region 处理单引号
            sDetail = sDetail.Replace("'", "’");
            #endregion
            #region 处理双引号
            sDetail = sDetail.Replace("\"", "&quot;");
            #endregion
            #region html标记符
            sDetail = sDetail.Replace("<", "&lt;");
            sDetail = sDetail.Replace(">", "&gt;");

            #endregion
            #region 处理换行
            //处理换行，在每个新行的前面添加两个全角空格
            r = new Regex(@"(\r\n((&nbsp;)|　)+)(?<正文>\S+)", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<BR>　　" + m.Groups["正文"].ToString());
            }
            //处理换行，在每个新行的前面添加两个全角空格
            sDetail = sDetail.Replace("\r\n", "<BR>");
            #endregion

            return sDetail;
        }
        #endregion

        #region 解析html成普通文本
        /// <summary>
        ///解析html成普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string ToDecode(string str)
        {
            str = str.Replace("&amp;", "&");
            str = str.Replace("''", "'");
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        #endregion

        #region  清除HTML所有格式(正则表达式)
        /// <summary>
        /// 清除HTML所有格式(正则表达式)
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ClearAllHtml(string strHtml)
        {
            if (strHtml != "")
            {
                Regex regex = null;
                Match match = null;
                regex = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
                for (match = regex.Match(strHtml); match.Success; match = match.NextMatch())
                {
                    strHtml = strHtml.Replace(match.Groups[0].ToString(), "");
                }
            }
            return strHtml;
        }
        #endregion

        #region 清除HTML指定格式
        /// <summary>
        /// 清除HTML指定格式
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string RemoveHTML(string strHTML)
        {
            string input = strHTML;
            Regex regex = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            Regex regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
            Regex regex7 = new Regex("</p>", RegexOptions.IgnoreCase);
            Regex regex8 = new Regex("<p>", RegexOptions.IgnoreCase);
            Regex regex9 = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
            Regex regex10 = new Regex(@"<table[\s\S]+</table *>", RegexOptions.IgnoreCase);
            Regex regex11 = new Regex(@"<div[\s\S]+</div *>", RegexOptions.IgnoreCase);
            input = regex.Replace(input, "");
            input = regex2.Replace(input, "");
            input = regex3.Replace(input, " _disibledevent=");
            input = regex4.Replace(input, "");
            input = regex5.Replace(input, "");
            input = regex6.Replace(input, "");
            input = regex7.Replace(input, "");
            input = regex8.Replace(input, "");
            input = regex9.Replace(input, "");
            input = regex10.Replace(input, "");
            return regex11.Replace(input, "").Replace(" ", "").Replace("</strong>", "").Replace("<strong>", "");
        }
        #endregion

        #region 清除编辑器中的非法字符串
        /// <summary>
        /// 清除编辑器中的非法字符串
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string RemoveHTMLForEditor(string strHTML)
        {
            string input = strHTML;
            Regex regex = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<div[\s\S]+</div *>", RegexOptions.IgnoreCase);
            input = regex.Replace(input, "");
            input = regex2.Replace(input, " _disibledevent=");
            input = regex3.Replace(input, "");
            input = regex4.Replace(input, "");
            return regex5.Replace(input, "");
        }
        #endregion

        #region 过滤JS脚本
        /// <summary>
        /// 过滤JS脚本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string WipeScript(string html)
        {
            Regex regex1 = new Regex(@"<script[/s/S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[/s/S]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" on[/s/S]*=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[/s/S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[/s/S]+</frameset *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            return html;
        }
        #endregion
    }
}
