/*mysql ���ݿ��������
 * 
 * ��Ȩ���У�2010����
 * �������ţ�IT 
 * ������zhangyi
 * �绰��13594663608
 * ������ϵ�������С�������
 * Email��kxyi-lover@163.com
 * MSN��10011
 * QQ:284124391
 * 
 * ����ʱ�䣺2012��2��28��
 * ���������������Լ�ʹ�ã����ý�����ҵ������Υ�߱ؾ���
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

/// <summary>
/// javascript�Ի���ȫ�ְ�����
/// </summary>
public class JsHelper
{
   public static bool isError = IsEnrollAA.IsEnroll();

   #region �����Ի�����Ϣ
   /// <summary>
   /// �����Ի�����Ϣ
   /// </summary>
   /// <param name="MsgBoxStr">�����Ի�������</param>
   public static void MsgBox(string MsgBoxStr)
   {
      string StrScript;
      StrScript = ("<script type='text/javascript' defer='defer'>");//���defer="defer"������Ϊ�������ִ�иýű�����������ٶȡ�
      StrScript += ("alert(\"" + RemoveDanYinHao(MsgBoxStr) + "\");");
      StrScript += ("</script>");
      System.Web.HttpContext.Current.Response.Write(StrScript);
   }
   /// <summary>
   /// �����Ի�����Ϣ(���ҳ����ҵ�����)
   /// </summary>
   /// <param name="js">������Ϣ</param>
   public static void PageMsgBox(string MsgBoxStr, Page page)
   { 
      string StrScript;
      StrScript = ("<script type='text/javascript' defer>");
      StrScript += ("alert(\"" + RemoveDanYinHao(MsgBoxStr) + "\");");
      StrScript += ("</script>");
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "alert"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "alert", StrScript);
      }
   }

   /// <summary>
   /// �ؼ���� ��Ϣȷ����ʾ��
   /// </summary>
   /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
   /// <param name="msg">��ʾ��Ϣ</param>
   public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
   {
       //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
       Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
   }

   #endregion

   #region �����Ի���ֹͣ��ǰҳ��ļ���ִ�С�
   /// <summary>
   /// �����Ի���ֹͣ��ǰҳ��ļ���ִ�С�
   /// </summary>
   /// <param name="MsgBoxStr">�����Ի�������</param>
   public static void MsgBox_ReturnUp(string MsgBoxStr)
   {
      string StrScript;
      StrScript = "<script type='text/javascript' defer>";
      StrScript += "alert(\"" + RemoveDanYinHao(MsgBoxStr) + "\");";
      StrScript += "</script>";
      System.Web.HttpContext.Current.Response.Write(StrScript);
      System.Web.HttpContext.Current.Response.End();//�����滻Ϊ��HttpContext.Current.ApplicationInstance.CompleteRequest();
   }
   #endregion

   #region ������Ϣ����ת���µ�URL
   /// <summary>
   /// ������Ϣ����ת���µ�URL
   /// </summary>
   /// <param name="message">��Ϣ����</param>
   /// <param name="GoToURL">���ӵ�ַ</param>
   public static void MsgBox_ToUml(string message, string GoToURL, bool isTop = false)
   {
      string js = "<script type='text/javascript'>alert('{0}');";

      if (isTop)
      {
          js += ("top.location.href='{1}'");
      }
      else
      {
          js += ("location.href='{1}'");
      }
      js += "</script>";
      System.Web.HttpContext.Current.Response.Write(string.Format(js, message, GoToURL));
   }

   /// <summary>
   /// �����Ի���,����ת����Ӧ��ҳ��(���ҳ����ҵ�����)
   /// </summary>
   /// <param name="message">�����Ի�������</param>
   /// <param name="GoToURL">��ת�ĵ�ַ</param>
   /// <param name="page">��ǰҳ��</param>
   public static void MsgBox_ToUml2(string message, string GoToURL, Page page,bool isTop=false)
   {
      string StrScript;
      StrScript = ("<script type='text/javascript'>");
      StrScript += ("alert(\"" + RemoveDanYinHao(message) + "\");");
      if (isTop)
      {
          StrScript += ("top.location.href = '" + GoToURL + "';");
      }
      else
      {
          StrScript += ("location.href = '" + GoToURL + "';");
      }
      StrScript += ("</script>");
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "MsgBox_ToUml"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox_ToUml", StrScript);
      }
   }

   #endregion

   #region ��ת���µ�Urlҳ��
   /// <summary>
   /// ��ת���µ�Urlҳ��
   /// </summary>
   /// <param name="url">��ַ</param>
   public static void ToUml(string url, Page page)
   {
      string js = @"<Script language='JavaScript'> window.location.replace('{0}');</Script>";
      js = string.Format(js, url);
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "JavaScriptLocationHref"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "JavaScriptLocationHref", js);
      }
   }
   #endregion

   #region �����Ի���,���ص���ʷҳ��
   /// <summary>
   /// �����Ի���,���ص���ʷҳ��
   /// </summary>
   /// <param name="MsgBoxStr">�����Ի�������</param>
   public static void MsgBox_GoHistory(string MsgBoxStr, int value)
   {
      string StrScript;
      StrScript = ("<script type='text/javascript' defer>");
      StrScript += ("alert(\"" + RemoveDanYinHao(MsgBoxStr) + "\");");
      StrScript += ("history.back(" + value + ");");
      StrScript += ("</script>");
      System.Web.HttpContext.Current.Response.Write(StrScript);
      //System.Web.HttpContext.Current.Response.End();
   }
   #endregion

   #region �ص���ʷҳ��
   /// <summary>
   /// �ص���ʷҳ��
   /// </summary>
   /// <param name="value">-1/1</param>
   public static void GoHistory(int value, Page page)
   {
      string js = @"<Script language='JavaScript'> history.go(" + value + ");</Script>";
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "GoHistory"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "GoHistory", js);
      }
   }
   #endregion

   #region ����������  public string Form_Update(string str1, int reload, int close)
   /// <summary>
   /// ����������
   /// </summary>
   /// <param name="str1"></param>
   /// <param name="reload"></param>
   /// <param name="close"></param>
   /// <returns></returns>
   public string Form_Update(string str1, int reload, int close)
   {
      string temp = "";
      temp = "<script language=javascript>";
      if (str1 != null && str1 != "")
         temp += "alert('" + str1 + "');";
      if (reload == 1)
         temp += "window.opener.location.reload(true);";
      if (close == 1)
         temp += "window.setTimeout('self.close()',3000);";
      temp += "window.close();" +
          "</script>";
      return temp;
   }
   #endregion

   #region ����ѯ����Ϣ��ʾ,ѡ������ҳ�� MessageBox.ShowConfirmURL()
   /// <summary>
   /// ����ѯ����Ϣ��ʾ,���Ҵ���ҳ��
   /// </summary>
   /// <param name="page0">����:Page</param>
   /// <param name="this0">����:this</param>
   /// <param name="str">����:��Ϣ��ʾ�ı�����</param>
   /// <param name="urlYes">����:��ѡ"ȷ��"�󵼺���ҳ���ַ</param>
   /// <param name="urlNo">����:��ѡ"ȡ��"�󵼺���ҳ���ַ</param>
   public static void ShowConfirmURL(Page page0, Page this0, string str, string urlYes, string urlNo)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("if (window.confirm('" + str + "'))");
      sb.Append("{");
      if (urlYes != "")
      {
         sb.Append("  document.location.href='" + urlYes + "';");
      }
      else
      {
         sb.Append("  true;");
      }
      sb.Append("}");
      sb.Append("else");
      sb.Append("{");
      if (urlNo != "")
      {
         sb.Append("  document.location.href='" + urlNo + "';");
      }
      else
      {
         sb.Append("  false;");
      }
      sb.Append("}");
      ClientScriptManager cs = page0.ClientScript;
      if (!cs.IsStartupScriptRegistered(this0.GetType(), "ShowUrl"))
      {
         cs.RegisterStartupScript(this0.GetType(), "ShowUrl", sb.ToString(), true);
      }
   }

   /// <summary>
   /// AJAX
   /// </summary>
   /// <param name="control"></param>
   /// <param name="type"></param>
   /// <param name="str"></param>
   /// <param name="urlYes"></param>
   /// <param name="urlNo"></param>
   public static void ShowConfirmURL(Control control, Type type, string str, string urlYes, string urlNo)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("if (window.confirm('" + str + "'))");
      sb.Append("{");
      if (urlYes != "")
      {
         sb.Append("  document.location.href='" + urlYes + "';");
      }
      else
      {
         sb.Append("  true;");
      }
      sb.Append("}");
      sb.Append("else");
      sb.Append("{");
      if (urlNo != "")
      {
         sb.Append("  document.location.href='" + urlNo + "';");
      }
      else
      {
         sb.Append("  false;");
      }
      sb.Append("}");

      ScriptManager.RegisterStartupScript(control, type, "ShowConfirmURL", sb.ToString(), true);

   }
   #endregion

   #region �Ƴ������ַ�
   /// <summary>
   /// �Ƴ������ַ�
   /// </summary>
   /// <returns></returns>
   private static string RemoveDanYinHao(string msg)
   {
      string returnStr = msg;
      returnStr = returnStr.Replace("\n", "");
      returnStr = returnStr.Replace("\r", "");
      returnStr = returnStr.Replace("\"", "'");
      return returnStr;
   }
   #endregion

   #region ˢ�¸�����
   /// <summary>
   /// ˢ�¸�����
   /// </summary>
   public static void RefreshParent(string url, Page page)
   {
      string js = @"<Script language='JavaScript'>window.opener.location.href='" + url + "';window.close();</Script>";
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "RefreshParent"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "RefreshParent", js);
      }
   }
   #endregion

   #region ˢ�´򿪴���
   /// <summary>
   /// ˢ�´򿪴���
   /// </summary>
   public static void RefreshOpener(Page page)
   {
      string js = @"<Script language='JavaScript'>opener.location.reload();</Script>";
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "RefreshOpener"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "RefreshOpener", js);
      }
   }
   #endregion

   #region �򿪵��´���
   /// <summary>
   /// �򿪵��´���
   /// </summary>
   /// <param name="url">��ַ</param>
   public static void OpenWebForm(string url, Page page)
   {
      string js = @"<Script language='JavaScript'>window.open('" + url + @"');</Script>";        //HttpContext.Current.Response.Write(js);
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenWebForm"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebForm", js);
      }
   }
   #endregion

   #region ��ָ����С���´���
   /// <summary>
   /// ��ָ����С���´���
   /// </summary>
   /// <param name="url">��ַ</param>
   /// <param name="width">��</param>
   /// <param name="heigth">��</param>
   /// <param name="top">ͷλ��</param>
   /// <param name="left">��λ��</param>
   public static void OpenWebFormSize(string url, int width, int heigth, int top, int left, Page page)
   {
      string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left
               + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenWebFormSize"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebFormSize", js);
      }
   }
   #endregion

   #region ��ָ����Сλ�õ�ģʽ�Ի���
   /// <summary>
   /// ��ָ����Сλ�õ�ģʽ�Ի���
   /// </summary>
   /// <param name="webFormUrl">���ӵ�ַ</param>
   /// <param name="width">��</param>
   /// <param name="height">��</param>
   /// <param name="top">������λ��</param>
   /// <param name="left">������λ��</param>
   public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left, Page page)
   {
      string features = "dialogWidth:" + width.ToString() + "px"
          + ";dialogHeight:" + height.ToString() + "px"
          + ";dialogLeft:" + left.ToString() + "px"
          + ";dialogTop:" + top.ToString() + "px"
          + ";center:yes;help=no;resizable:no;status:no;scroll=yes";

      string js = @"<script language=javascript> showModalDialog('" + webFormUrl + "','','" + features + "');</script>"; ;
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ShowModalDialogWindow"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "ShowModalDialogWindow", js);
      }
   }
   #endregion

   #region ��ģ̬��ʾ showModelessDialog(Page page0, Page this0, string url, int width, int height)
   /// <summary>
   /// ��ģ̬��ʾ
   /// </summary>
   /// <param name="page0">����:Page</param>
   /// <param name="this0">����:this</param>
   /// <param name="url">��ʾҳ��ĵ�ַ</param>
   /// <param name="width">��ʾС���ڵĿ��</param>
   /// <param name="height">��ʾС���ڵĸ߶�</param>
   public static void showModelessDialog(Page page0, Page this0, string url, int width, int height)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("ReturnValue = window.showModelessDialog('" + url + "', window ,'dialogWidth=" + width.ToString() + "px;dialogHeight=" + height.ToString() + "px');");
      sb.Append("");
      ClientScriptManager cs = page0.ClientScript;
      if (!cs.IsStartupScriptRegistered(this0.GetType(), "showModelessDialog"))
      {
         cs.RegisterStartupScript(this0.GetType(), "showModelessDialog", sb.ToString(), true);
      }
   }
   #endregion

   #region ģ̬��ʾ showModalDialog(Page page0, Page this0, string url, int width, int height, string HideName)
   /// <summary>
   /// ģ̬��ʾ
   /// </summary>
   /// <param name="page0">����:Page</param>
   /// <param name="this0">����:this</param>
   /// <param name="url">��ʾҳ��ĵ�ַ</param>
   /// <param name="width">��ʾС���ڵĿ��</param>
   /// <param name="height">��ʾС���ڵĸ߶�</param>
   /// <param name="HideName">���ܵ���ҳ��Ļش�ֵ,һ����Hiddden���ı���������</param>
   public static void showModalDialog(Page page0, Page this0, string url, int width, int height, string HideName)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("ReturnValue = window.showModalDialog('" + url + "', window ,'dialogWidth=" + width.ToString() + "px;dialogHeight=" + height.ToString() + "px');");
      sb.Append("window.document.getElementById('" + HideName + "').value = ReturnValue;");
      ClientScriptManager cs = page0.ClientScript;
      if (!cs.IsStartupScriptRegistered(this0.GetType(), "showModalDialog"))
      {
         cs.RegisterStartupScript(this0.GetType(), "showModalDialog", sb.ToString(), true);
      }
   }

   /// <summary>
   /// AJAX ģ̬��ʾ
   /// </summary>
   /// <param name="control"></param>
   /// <param name="type"></param>
   /// <param name="url"></param>
   /// <param name="width"></param>
   /// <param name="height"></param>
   /// <param name="HideName"></param>
   public static void showModalDialog(Control control, Type type, string url, int width, int height, string HideName)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("ReturnValue = window.showModalDialog('" + url + "', window ,'dialogWidth=" + width.ToString() + "px;dialogHeight=" + height.ToString() + "px');");
      sb.Append("window.document.getElementById('" + HideName + "').value = ReturnValue;");
      ScriptManager.RegisterStartupScript(control, type, "showModalDialog", sb.ToString(), true);
   }
   #endregion

   #region �ر�ҳ�洰�� MessageBox.WinClose()

   /// <summary>
   /// �رյ�ǰ����
   /// </summary>
   public static void CloseWindow()
   {
      // window.opener = null; //��ֹ�رմ��ڵ���ʾ
      //parent.close(); 
      // window.close(); //�Զ��رձ�����

      string js = @"<Script language='JavaScript'>parent.opener=null;window.open('','');window.close();</Script>";
      HttpContext.Current.Response.Write(js);
      HttpContext.Current.Response.End();
   }

   /// <summary>
   /// �ر�ҳ�洰��
   ///һ��Ӧ���ڷ������ؼ��¼�������,֮ǰ��ִ����������;
   /// </summary>
   /// <param name="page0">����:Page</param>
   /// <param name="this0">����:this</param>
   public static void WinClose(Page page0, Page this0)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append(" window.dialogArguments.location.href =window.dialogArguments.location.href;");//ˢ�¸�ҳ��
      sb.Append(" window.close();");
      ClientScriptManager cs = page0.ClientScript;
      if (!cs.IsStartupScriptRegistered(this0.GetType(), "WinClose"))
      {
         cs.RegisterStartupScript(this0.GetType(), "WinClose", sb.ToString(), true);
      }
   }

   public static void WinClose(Control control, Type type)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append(" window.dialogArguments.location.href =window.dialogArguments.location.href;");//ˢ�¸�ҳ��
      sb.Append(" window.close();");
      ScriptManager.RegisterStartupScript(control, type, "WinCloseajax", sb.ToString(), true);
   }

   /// <summary>
   /// �ر�ҳ�洰��
   ///һ��Ӧ���ڷ������ؼ��¼�������,֮ǰ��ִ����������;
   /// </summary>
   /// <param name="page0">����:Page</param>
   /// <param name="this0">����:this</param>
   /// <param name="str">����:����ֵ</param>
   public static void WinClose(Page page0, Page this0, string str)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("window.returnValue = '" + str + "';");
      sb.Append("window.close();");
      ClientScriptManager cs = page0.ClientScript;
      if (!cs.IsStartupScriptRegistered(this0.GetType(), "WinClose1"))
      {
         cs.RegisterStartupScript(this0.GetType(), "WinClose1", sb.ToString(), true);
      }
   }

   /// <summary>
   /// �ر�ҳ�洰��AJAX
   /// </summary>
   /// <param name="control"></param>
   /// <param name="type"></param>
   /// <param name="str">����:����ֵ</param>
   public static void WinClose(Control control, Type type, string str)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("window.returnValue = " + str + ";");
      sb.Append("window.close();");
      ScriptManager.RegisterStartupScript(control, type, "WinClose1ajax", sb.ToString(), true);
   }
   #endregion

   #region ����Զ���ű���Ϣ
   /// <summary>
   /// ����Զ���ű���Ϣ
   /// </summary>
   /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
   /// <param name="script">����ű�</param>
   public static void ResponseScript(System.Web.UI.Page page, string script)
   {
       page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
   }

   #endregion

   public void Msg()
   {
      //�۷��˽⵽�ĵ����Ի�����룬ֻ������ҳ���ϡ�
      //this.ClientScript.RegisterStartupScript(this.GetType(), "��ʾ", "<script type='text/javascript' defer>alert('�����ɹ���');location.href='FrmLinksManage.aspx'</script>");
      //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "��ʾ", "alert('ɾ���ɹ���'); ", true);
   }

}
