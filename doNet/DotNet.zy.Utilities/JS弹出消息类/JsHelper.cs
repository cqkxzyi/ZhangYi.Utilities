/*mysql 数据库操作方法
 * 
 * 版权所有：2010张毅
 * 开发部门：IT 
 * 程序负责：zhangyi
 * 电话：13594663608
 * 其他联系：重庆市、深圳市
 * Email：kxyi-lover@163.com
 * MSN：10011
 * QQ:284124391
 * 
 * 开发时间：2012年2月28日
 * 声明：仅限于您自己使用，不得进行商业传播，违者必究！
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

/// <summary>
/// javascript对话框全局帮助类
/// </summary>
public class JsHelper
{
   public static bool isError = IsEnrollAA.IsEnroll();

   #region 弹出对话框信息
   /// <summary>
   /// 弹出对话框信息
   /// </summary>
   /// <param name="MsgBoxStr">弹出对话框内容</param>
   public static void MsgBox(string MsgBoxStr)
   {
      string StrScript;
      StrScript = ("<script type='text/javascript' defer='defer'>");//添加defer="defer"属性是为了在最后执行该脚本，提高运行速度。
      StrScript += ("alert(\"" + RemoveDanYinHao(MsgBoxStr) + "\");");
      StrScript += ("</script>");
      System.Web.HttpContext.Current.Response.Write(StrScript);
   }
   /// <summary>
   /// 弹出对话框信息(解决页面错乱的问题)
   /// </summary>
   /// <param name="js">窗口信息</param>
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
   /// 控件点击 消息确认提示框
   /// </summary>
   /// <param name="page">当前页面指针，一般为this</param>
   /// <param name="msg">提示信息</param>
   public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
   {
       //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
       Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
   }

   #endregion

   #region 弹出对话框，停止当前页面的继续执行。
   /// <summary>
   /// 弹出对话框，停止当前页面的继续执行。
   /// </summary>
   /// <param name="MsgBoxStr">弹出对话框内容</param>
   public static void MsgBox_ReturnUp(string MsgBoxStr)
   {
      string StrScript;
      StrScript = "<script type='text/javascript' defer>";
      StrScript += "alert(\"" + RemoveDanYinHao(MsgBoxStr) + "\");";
      StrScript += "</script>";
      System.Web.HttpContext.Current.Response.Write(StrScript);
      System.Web.HttpContext.Current.Response.End();//可以替换为：HttpContext.Current.ApplicationInstance.CompleteRequest();
   }
   #endregion

   #region 弹出消息框并且转向到新的URL
   /// <summary>
   /// 弹出消息框并且转向到新的URL
   /// </summary>
   /// <param name="message">消息内容</param>
   /// <param name="GoToURL">连接地址</param>
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
   /// 弹出对话框,并跳转到相应的页面(解决页面错乱的问题)
   /// </summary>
   /// <param name="message">弹出对话框内容</param>
   /// <param name="GoToURL">跳转的地址</param>
   /// <param name="page">当前页面</param>
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

   #region 跳转到新的Url页面
   /// <summary>
   /// 跳转到新的Url页面
   /// </summary>
   /// <param name="url">地址</param>
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

   #region 弹出对话框,返回到历史页面
   /// <summary>
   /// 弹出对话框,返回到历史页面
   /// </summary>
   /// <param name="MsgBoxStr">弹出对话框内容</param>
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

   #region 回到历史页面
   /// <summary>
   /// 回到历史页面
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

   #region 更新主窗体  public string Form_Update(string str1, int reload, int close)
   /// <summary>
   /// 更新主窗体
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

   #region 弹出询问消息提示,选择后打开新页面 MessageBox.ShowConfirmURL()
   /// <summary>
   /// 弹出询问消息提示,并且打开新页面
   /// </summary>
   /// <param name="page0">参数:Page</param>
   /// <param name="this0">参数:this</param>
   /// <param name="str">参数:消息提示文本内容</param>
   /// <param name="urlYes">参数:点选"确定"后导航的页面地址</param>
   /// <param name="urlNo">参数:点选"取消"后导航的页面地址</param>
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

   #region 移除特殊字符
   /// <summary>
   /// 移除特殊字符
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

   #region 刷新父窗口
   /// <summary>
   /// 刷新父窗口
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

   #region 刷新打开窗口
   /// <summary>
   /// 刷新打开窗口
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

   #region 打开的新窗体
   /// <summary>
   /// 打开的新窗体
   /// </summary>
   /// <param name="url">地址</param>
   public static void OpenWebForm(string url, Page page)
   {
      string js = @"<Script language='JavaScript'>window.open('" + url + @"');</Script>";        //HttpContext.Current.Response.Write(js);
      if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenWebForm"))
      {
         page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebForm", js);
      }
   }
   #endregion

   #region 打开指定大小的新窗体
   /// <summary>
   /// 打开指定大小的新窗体
   /// </summary>
   /// <param name="url">地址</param>
   /// <param name="width">宽</param>
   /// <param name="heigth">高</param>
   /// <param name="top">头位置</param>
   /// <param name="left">左位置</param>
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

   #region 打开指定大小位置的模式对话框
   /// <summary>
   /// 打开指定大小位置的模式对话框
   /// </summary>
   /// <param name="webFormUrl">连接地址</param>
   /// <param name="width">宽</param>
   /// <param name="height">高</param>
   /// <param name="top">距离上位置</param>
   /// <param name="left">距离左位置</param>
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

   #region 非模态显示 showModelessDialog(Page page0, Page this0, string url, int width, int height)
   /// <summary>
   /// 非模态显示
   /// </summary>
   /// <param name="page0">参数:Page</param>
   /// <param name="this0">参数:this</param>
   /// <param name="url">显示页面的地址</param>
   /// <param name="width">显示小窗口的宽度</param>
   /// <param name="height">显示小窗口的高度</param>
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

   #region 模态显示 showModalDialog(Page page0, Page this0, string url, int width, int height, string HideName)
   /// <summary>
   /// 模态显示
   /// </summary>
   /// <param name="page0">参数:Page</param>
   /// <param name="this0">参数:this</param>
   /// <param name="url">显示页面的地址</param>
   /// <param name="width">显示小窗口的宽度</param>
   /// <param name="height">显示小窗口的高度</param>
   /// <param name="HideName">接受弹出页面的回传值,一般用Hiddden或文本框来接受</param>
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
   /// AJAX 模态显示
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

   #region 关闭页面窗口 MessageBox.WinClose()

   /// <summary>
   /// 关闭当前窗口
   /// </summary>
   public static void CloseWindow()
   {
      // window.opener = null; //禁止关闭窗口的提示
      //parent.close(); 
      // window.close(); //自动关闭本窗口

      string js = @"<Script language='JavaScript'>parent.opener=null;window.open('','');window.close();</Script>";
      HttpContext.Current.Response.Write(js);
      HttpContext.Current.Response.End();
   }

   /// <summary>
   /// 关闭页面窗口
   ///一般应用于服务器控件事件代码中,之前可执行其他代码;
   /// </summary>
   /// <param name="page0">参数:Page</param>
   /// <param name="this0">参数:this</param>
   public static void WinClose(Page page0, Page this0)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append(" window.dialogArguments.location.href =window.dialogArguments.location.href;");//刷新父页面
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
      sb.Append(" window.dialogArguments.location.href =window.dialogArguments.location.href;");//刷新父页面
      sb.Append(" window.close();");
      ScriptManager.RegisterStartupScript(control, type, "WinCloseajax", sb.ToString(), true);
   }

   /// <summary>
   /// 关闭页面窗口
   ///一般应用于服务器控件事件代码中,之前可执行其他代码;
   /// </summary>
   /// <param name="page0">参数:Page</param>
   /// <param name="this0">参数:this</param>
   /// <param name="str">参数:返回值</param>
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
   /// 关闭页面窗口AJAX
   /// </summary>
   /// <param name="control"></param>
   /// <param name="type"></param>
   /// <param name="str">参数:返回值</param>
   public static void WinClose(Control control, Type type, string str)
   {
      StringBuilder sb = new StringBuilder("");
      sb.Append("window.returnValue = " + str + ";");
      sb.Append("window.close();");
      ScriptManager.RegisterStartupScript(control, type, "WinClose1ajax", sb.ToString(), true);
   }
   #endregion

   #region 输出自定义脚本信息
   /// <summary>
   /// 输出自定义脚本信息
   /// </summary>
   /// <param name="page">当前页面指针，一般为this</param>
   /// <param name="script">输出脚本</param>
   public static void ResponseScript(System.Web.UI.Page page, string script)
   {
       page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
   }

   #endregion

   public void Msg()
   {
      //巅峰了解到的弹出对话框代码，只用用在页面上。
      //this.ClientScript.RegisterStartupScript(this.GetType(), "提示", "<script type='text/javascript' defer>alert('操作成功！');location.href='FrmLinksManage.aspx'</script>");
      //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "提示", "alert('删除成功！'); ", true);
   }

}
