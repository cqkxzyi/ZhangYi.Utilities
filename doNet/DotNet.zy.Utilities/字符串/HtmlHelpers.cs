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
    /// Html��ʽ����
    /// </summary>
    public partial class HtmlHelpers
    {
        #region HTMLת�г�TEXT
        /// <summary>
        /// HTMLת�г�TEXT
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

        #region �ַ���ת����Html��ʽ����
        /// <summary>
        /// �ַ���ת����Html��ʽ����
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputStr)
        {
            return HttpUtility.HtmlEncode(inputStr);
        }
        #endregion

        #region HTML�����ʽ��
        /// <summary>
        /// HTML�����ʽ��
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

        #region HTML�����ʽ��
        /// <summary>
        /// HTML�����ʽ��
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        public static string GetEncodeHtml(string sDetail)
        {
            Regex r;
            Match m;

            #region ����ո�
            sDetail = sDetail.Replace(" ", "&nbsp;");
            #endregion
            #region ��������
            sDetail = sDetail.Replace("'", "��");
            #endregion
            #region ����˫����
            sDetail = sDetail.Replace("\"", "&quot;");
            #endregion
            #region html��Ƿ�
            sDetail = sDetail.Replace("<", "&lt;");
            sDetail = sDetail.Replace(">", "&gt;");

            #endregion
            #region ������
            //�����У���ÿ�����е�ǰ���������ȫ�ǿո�
            r = new Regex(@"(\r\n((&nbsp;)|��)+)(?<����>\S+)", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<BR>����" + m.Groups["����"].ToString());
            }
            //�����У���ÿ�����е�ǰ���������ȫ�ǿո�
            sDetail = sDetail.Replace("\r\n", "<BR>");
            #endregion

            return sDetail;
        }
        #endregion

        #region ����html����ͨ�ı�
        /// <summary>
        ///����html����ͨ�ı�
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

        #region  ���HTML���и�ʽ(������ʽ)
        /// <summary>
        /// ���HTML���и�ʽ(������ʽ)
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

        #region ���HTMLָ����ʽ
        /// <summary>
        /// ���HTMLָ����ʽ
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

        #region ����༭���еķǷ��ַ���
        /// <summary>
        /// ����༭���еķǷ��ַ���
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

        #region ����JS�ű�
        /// <summary>
        /// ����JS�ű�
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
            html = regex1.Replace(html, ""); //����<script></script>��� 
            html = regex2.Replace(html, ""); //����href=javascript: (<A>) ���� 
            html = regex3.Replace(html, " _disibledevent="); //���������ؼ���on...�¼� 
            html = regex4.Replace(html, ""); //����iframe 
            html = regex5.Replace(html, ""); //����frameset 
            return html;
        }
        #endregion
    }
}
