/*Request参数相关操作类
 * 
 * 开发部门：IT 
 * 程序负责：zhangyi
 * Email：kxyi-lover@163.com
 * QQ:284124391
 * */
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Specialized;// HashTable 的Using是 System.Collections   NameValueCollection允许重复 Dictionary<int,int> dic=new Dictionary<int,int>()  KeyValuePair<string, object>  SortedList集合 
using System.Net;
using MSXML2;
using System.DirectoryServices;
using DotNet.zy.Utilities;

/// <summary>
/// Request参数相关操作类
/// </summary>
public class RequestHelper
{
   public static bool isError = IsEnrollAA.IsEnroll();

   static System.Text.Encoding encoding = System.Text.Encoding.UTF8;

   #region  获取客户端IP
   /// <summary>
   /// 获取客户端IP
   /// </summary>
   /// <returns></returns>
   public static string GetIP()
   {
      // 优先取得代理IP 
      string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (string.IsNullOrEmpty(userHostAddress))
      {
         //没有代理IP则直接取客户端IP 
         userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
      }
      if ((userHostAddress != null) && !(userHostAddress == string.Empty))
      {
         return userHostAddress;
      }
      return "0.0.0.0";
   }
   /// <summary>
   /// IPv6的格式转换成IPv4
   /// </summary>
   /// <returns></returns>
   public static string GetClientIPv4()
   {
      string ipv4 = String.Empty;
      foreach (IPAddress ip in Dns.GetHostAddresses(GetClientIP()))
      {
         if (ip.AddressFamily.ToString() == "InterNetwork")
         {
            ipv4 = ip.ToString();
            break;
         }
      }

      if (ipv4 != String.Empty)
      {
         return ipv4;
      }

      foreach (IPAddress ip in Dns.GetHostEntry(GetClientIP()).AddressList)
      {
         if (ip.AddressFamily.ToString() == "InterNetwork")
         {
            ipv4 = ip.ToString();
            break;
         }
      }
      return ipv4;
   }

   /// <summary>
   /// 获取客户端IP(可能返回的是IPv6的格式。)
   /// </summary>
   /// <returns></returns>
   private static string GetClientIP()
   {
      string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (null == result || result == String.Empty)
      {
         result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
      }

      if (null == result || result == String.Empty)
      {
         result = HttpContext.Current.Request.UserHostAddress;
      }
      return result;
   }

   #endregion

   #region 获取请求URL public static string GetUrl()
   /// <summary>
   /// 获取请求URL
   /// </summary>
   /// <returns></returns>
   public static string GetUrl()
   {
      return HttpContext.Current.Request.Url.ToString();
   }
   #endregion

   #region 取得Get提交的URL参数  public static string GetQueryValue(string strName)
   /// <summary>
   /// 取得Get提交的URL参数
   /// </summary>
   /// <param name="strName">参数名</param>
   /// <returns></returns>
   public static string GetQueryValue(string strName)
   {
      if (HttpContext.Current.Request.QueryString[strName] == null)
      {
         return "";
      }
      return HttpContext.Current.Request.QueryString[strName].Trim();
   }
   #endregion

   #region 取得POST提交的FORM参数  public static string GetFormValue(string strName)
   /// <summary>
   /// 取得POST提交的FORM参数
   /// </summary>
   /// <param name="strName">控件ID</param>
   /// <returns></returns>
   public static string GetFormValue(string strName)
   {
      //string values = HttpContext.Current.Request.Params[strName];
      if (HttpContext.Current.Request.Form[strName] == null)
      {
         return "";
      }
      return HttpContext.Current.Request.Form[strName].Trim();
   }
   #endregion

   #region 根据指定的编码格式返回请求的参数集合(Get、Post)
   /// <summary>
   /// 根据指定的编码格式返回请求的参数集合(Get、Post)
   /// </summary>
   /// <param name="request">当前请求的request对象</param>
   /// <param name="encode">编码格式字符串</param>
   /// <returns>键为参数名,值为参数值的NameValue集合</returns>
   public static NameValueCollection GetRequestParameters(HttpRequest request, string encode)
   {
      NameValueCollection result = null;
      Encoding destEncode = null;
      //获取指定编码格式的Encoding对象
      if (!String.IsNullOrEmpty(encode))
      {
         try
         {
            //获取指定的编码格式
            destEncode = Encoding.GetEncoding(encode);
         }
         catch
         {
            //如果获取指定编码格式失败,则设置为null
            destEncode = null;
         }
      }
      //根据不同的HttpMethod方式,获取请求的参数.如果没有Encoding对象则使用服务器端默认的编码.
      if (request.HttpMethod == "POST")
      {
         if (null != destEncode)
         {
            System.IO.Stream resStream = request.InputStream;
            byte[] filecontent = new byte[resStream.Length];
            resStream.Read(filecontent, 0, filecontent.Length);
            string postquery = destEncode.GetString(filecontent);
            result = HttpUtility.ParseQueryString(postquery, destEncode);
         }
         else
         {
            result = request.Form;
         }
      }
      else
      {
         if (null != destEncode)
         {
            result = HttpUtility.ParseQueryString(request.QueryString.ToString(), destEncode);
         }
         else
         {
            result = request.QueryString;
         }
      }

      //返回结果
      return result;
   }
   #endregion

   #region 获取URL参数值(复杂情况使用)
   /// <summary>
   /// 测试.
   /// </summary>
   public void Test()
   {
       string pageURL = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=%E5%8D%9A%E6%B1%87%E6%95%B0%E7%A0%81&aq=f&aqi=g2&aql=&oq=&gs_rfai=";
       Uri uri = new Uri(pageURL);
       string queryString = uri.Query;
       NameValueCollection col = GetQueryString(queryString, null, true);
       string searchKey = col["q"];
       //结果 searchKey = "博汇数码"
   }

   /// <summary>
   /// 将URL解析为名值集合
   /// </summary>
   /// <param name="queryString">要解析的URL</param>
   /// <param name="encoding">编码格式，不确定则传入NUll</param>
   /// <param name="isEncoded">是否编码</param>
   /// <returns></returns>
   public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
   {
       queryString = queryString.Replace("?", "");
       NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
       if (!string.IsNullOrEmpty(queryString))
       {
           int count = queryString.Length;
           for (int i = 0; i < count; i++)
           {
               int startIndex = i;
               int index = -1;
               while (i < count)
               {
                   char item = queryString[i];
                   if (item == '=')
                   {
                       if (index < 0)
                       {
                           index = i;
                       }
                   }
                   else if (item == '&')
                   {
                       break;
                   }
                   i++;
               }
               string key = null;
               string value = null;
               if (index >= 0)
               {
                   key = queryString.Substring(startIndex, index - startIndex);
                   value = queryString.Substring(index + 1, (i - index) - 1);
               }
               else
               {
                   key = queryString.Substring(startIndex, i - startIndex);
               }
               if (isEncoded)
               {
                   result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
               }
               else
               {
                   result[key] = value;
               }
               if ((i == (count - 1)) && (queryString[i] == '&'))
               {
                   result[key] = string.Empty;
               }
           }
       }
       return result;
   }

   /// <summary>
   /// 解码URL.
   /// </summary>
   /// <param name="encoding">null为自动选择编码</param>
   /// <param name="str"></param>
   /// <returns></returns>
   public static string MyUrlDeCode(string str, Encoding encoding)
   {
       if (encoding == null)
       {
           Encoding utf8 = Encoding.UTF8;
           //首先用utf-8进行解码                     
           string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
           //将已经解码的字符再次进行编码.
           string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
           if (str == encode)
               encoding = Encoding.UTF8;
           else
               encoding = Encoding.GetEncoding("gb2312");
       }
       return HttpUtility.UrlDecode(str, encoding);
   }
   #endregion

   #region 获取URL解析为名值集合
   /// <summary>
   /// 获取URL解析为名值集合
   /// </summary>
   /// <param name="url">输入的 URL</param>
   /// <param name="baseUrl">输出 URL 的基础部分</param>
   /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>
   public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
   {
       if (url == null || url == "")
       { throw new ArgumentNullException("url"); }

       baseUrl = "";
       nvc = new NameValueCollection();

       int questionMarkIndex = url.IndexOf('?');

       if (questionMarkIndex == -1)
       {
           baseUrl = url;
           return;
       }
       baseUrl = url.Substring(0, questionMarkIndex);
       if (questionMarkIndex == url.Length - 1)
       {
           return;
       }
       string ps = url.Substring(questionMarkIndex + 1);

       // 开始分析参数对    
       Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
       MatchCollection mc = re.Matches(ps);

       foreach (Match m in mc)
       {
           nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
       }
   }
   #endregion

   #region 添加URL参数
   /// <summary>
   /// 添加URL参数
   /// </summary>
   /// <param name="url">传入的URL</param>
   /// <param name="paramName">新增参数名称</param>
   /// <param name="value">参数值</param>
   /// <returns></returns>
   public static string AddParam(string url, string paramName, string value)
   {
      Uri uri = new Uri(url);
      if (string.IsNullOrEmpty(uri.Query))
      {
         string eval = HttpContext.Current.Server.UrlEncode(value);
         return String.Concat(url, "?" + paramName + "=" + eval);
      }
      else
      {
         string eval = HttpContext.Current.Server.UrlEncode(value);
         return String.Concat(url, "&" + paramName + "=" + eval);
      }
   }
   #endregion

   #region 更新URL参数
   /// <summary>
   /// 更新URL参数
   /// </summary>
   public static string UpdateParam(string url, string paramName, string value)
   {
      string keyWord = paramName + "=";
      int index = url.IndexOf(keyWord) + keyWord.Length;
      int index1 = url.IndexOf("&", index);
      if (index1 == -1)
      {
         url = url.Remove(index, url.Length - index);
         url = string.Concat(url, value);
         return url;
      }
      url = url.Remove(index, index1 - index);
      url = url.Insert(index, value);
      return url;
   }
   #endregion

   #region 移除URL参数
   /// <summary>
   /// 方法一
   /// 设法用RegularExpression比对抽换
   /// </summary>
   /// <param name="url"></param>
   /// <param name="key"></param>
   /// <returns></returns>
   public static string RemoveQueryParam1(string url, string key)
   {
      //若为&boo=...则直接去除，?boo=...&...则要传回?
      //...&boo=...&...时要传回&
      return
      Regex.Replace(url, "[?&]" + key + "=[^&]*[&]{0,1}",
      o => (o.Value.StartsWith("?") && o.Value.EndsWith("&") ? "?" : "") +
      (o.Value.StartsWith("&") && o.Value.EndsWith("&") ? "&" : "")
      );
   }

   /// <summary>
   /// 方法二
   /// 使用.NET内建机制
   /// </summary>
   /// <param name="url"></param>
   /// <param name="key"></param>
   /// <returns></returns>
   public static string RemoveQueryParam2(string url, string key)
   {
      //拆出QueryString部分
      Uri uri = new Uri(url);
      if (string.IsNullOrEmpty(uri.Query))
      {
         return url;
      }
      //用HttpUtility.ParseQueryString()将其解析为成对Name/Value集合
      //在ASP.NET中直接用Request.QueryString即可
      NameValueCollection args = HttpUtility.ParseQueryString(uri.Query);
      args.Remove(key);

      string[] aaaa = (string[])(args.Cast<string>().Select(k => string.Format("{0}={1}", k, HttpUtility.UrlEncode(args[k]))));
      return url.Substring(0, url.IndexOf("?") + (args.Count > 0 ? 1 : 0)) + string.Join("&", aaaa);
      //若有参数就多加1
      //将NameValueCollection Cast<string>取得可LINQ的Keys集合
   }
   #endregion

   #region URL的64位编码
   /// <summary>
   /// URL的64位编码
   /// </summary>
   /// <param name="sourthUrl"></param>
   /// <returns></returns>
   public static string URLBase64Encrypt(string sourthUrl)
   {
       string eurl = HttpUtility.UrlEncode(sourthUrl);
       eurl = Convert.ToBase64String(encoding.GetBytes(eurl));
       return eurl;
   }
   #endregion

   #region URL的64位解码
   /// <summary>
   /// URL的64位解码
   /// </summary>
   /// <param name="eStr"></param>
   /// <returns></returns>
   public static string URLBase64Decrypt(string eStr)
   {
       if (!IsBase64(eStr))
       {
           return eStr;
       }
       byte[] buffer = Convert.FromBase64String(eStr);
       string sourthUrl = encoding.GetString(buffer);
       sourthUrl = HttpUtility.UrlDecode(sourthUrl);
       return sourthUrl;
   }

   /// <summary>
   /// 是否是Base64字符串
   /// </summary>
   /// <param name="eStr"></param>
   /// <returns></returns>
   public static bool IsBase64(string eStr)
   {
       if ((eStr.Length % 4) != 0)
       {
           return false;
       }
       if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
       {
           return false;
       }
       return true;
   }
   #endregion

   #region 分析URL所属的域
   /// <summary>
   /// 分析URL所属的域
   /// </summary>
   /// <param name="fromUrl"></param>
   /// <param name="domain"></param>
   /// <param name="subDomain"></param>
   public static void GetDomain(string fromUrl, out string domain, out string subDomain)
   {
       domain = "";
       subDomain = "";
       try
       {
           if (fromUrl.IndexOf("的名片") > -1)
           {
               subDomain = fromUrl;
               domain = "名片";
               return;
           }

           UriBuilder builder = new UriBuilder(fromUrl);
           fromUrl = builder.ToString();

           Uri u = new Uri(fromUrl);

           if (u.IsWellFormedOriginalString())
           {
               if (u.IsFile)
               {
                   subDomain = domain = "客户端本地文件路径";

               }
               else
               {
                   string Authority = u.Authority;
                   string[] ss = u.Authority.Split('.');
                   if (ss.Length == 2)
                   {
                       Authority = "www." + Authority;
                   }
                   int index = Authority.IndexOf('.', 0);
                   domain = Authority.Substring(index + 1, Authority.Length - index - 1).Replace("comhttp", "com");
                   subDomain = Authority.Replace("comhttp", "com");
                   if (ss.Length < 2)
                   {
                       domain = "不明路径";
                       subDomain = "不明路径";
                   }
               }
           }
           else
           {
               if (u.IsFile)
               {
                   subDomain = domain = "客户端本地文件路径";
               }
               else
               {
                   subDomain = domain = "不明路径";
               }
           }
       }
       catch
       {
           subDomain = domain = "不明路径";
       }
   }

   #endregion

   #region 检测参数中是否有非法SQL字符
   /// <summary>
   /// 检测上传参数中是否有非法SQL字符（get、post）
   /// </summary>
   /// <returns></returns>
   public static bool ValidatorURL()
   {
      NameValueCollection nameOfVal = GetRequestParameters(HttpContext.Current.Request, "");
      foreach (string item in nameOfVal.AllKeys)
      {
          if (!StringValidate.CheckSQLWord(nameOfVal[item]))
         {
            return false;
         }
      }
      return true;
   }
   #endregion

   #region JQuery查询地址并返回内容 QueryJson(string jsonurl, string querystr, out string responsejson, out string errmsg)
   /// <summary>
   /// 查询接口（返回字符串）
   /// </summary>
   /// <param name="jsonurl">查询地址</param>
   /// <param name="querystr">查询字符串</param>
   /// <param name="responsejson">返回数据</param>
   /// <param name="errmsg">错误信息</param>
   /// <returns></returns>
   public static bool QueryJson(string jsonurl, string querystr, out string responsejson, out string errmsg)
   {
      #region
      ServerXMLHTTP xmlhttp = new ServerXMLHTTP();
      errmsg = "";
      responsejson = "";
      try
      {
         xmlhttp.open("POST", jsonurl, false, null, null);

         //xmlhttp.setRequestHeader("Content-Type", "text/xml;charset=utf-8");
         xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
         xmlhttp.send(querystr);

         if (xmlhttp.readyState == 4)
         {
            if (xmlhttp.responseText.Trim() != "")
            {
               responsejson = xmlhttp.responseText;
            }
            else
            {
               responsejson = "";
               errmsg = "ErrEmpty";
            }
         }
      }
      catch
      {
         errmsg = "ErrConnect";

      }
      finally { xmlhttp.abort(); }

      if (errmsg != "")
      {
          Tools.SendeMail("", "andy.yang@hubs1.net", "hbe json error", errmsg, true, Encoding.UTF8);
         return false;
      }
      else
         return true;
      #endregion
   }

   /// <summary>
   /// 查询接口（返回字节数组）
   /// </summary>
   /// <param name="jsonurl">地址</param>
   /// <param name="querystr">查询内容</param>
   /// <param name="methods">所用方法(GET/POST)</param>
   /// <param name="resp">查询内容</param>
   /// <param name="errmsg">返回错误</param>
   /// <returns></returns>
   public static bool queryjson(string jsonurl, string querystr, string methods, out byte[] resp, out string errmsg)
   {
      #region
      ServerXMLHTTP xmlhttp = new ServerXMLHTTP();
      errmsg = "";
      resp = null;
      try
      {
         xmlhttp.open(methods, jsonurl, false, null, null);

         xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
         xmlhttp.send(querystr);

         if (xmlhttp.readyState == 4)
         {
            if (xmlhttp.responseText.Trim() != "")
            {
                resp =xmlhttp.responseBody;
            }
            else
            {
               errmsg = "ErrEmpty";
            }
         }
      }
      catch
      {
         errmsg = "ErrConnect";

      }
      finally { xmlhttp.abort(); }

      if (errmsg != "")
      {
          Tools.SendeMail("", "andy.yang@hubs1.net", "hbe json error", errmsg, true, Encoding.UTF8);
         return false;
      }
      else
         return true;
      #endregion
   }
   #endregion

   #region 获取服务器IIS版本
   /**/
   /// <summary>
   /// 服务器IIS版本
   /// </summary>
   [Serializable]
   public enum WebServerTypes
   {
      /**/
      /// <summary>
      /// 未知版本
      /// </summary>
      Unknown,
      /**/
      /// <summary>
      /// IIS 4.0
      /// </summary>
      IIS4,
      /**/
      /// <summary>
      /// IIS 5.0,5.1
      /// </summary>
      IIS5,
      /**/
      /// <summary>
      /// IIS 6.0
      /// </summary>
      IIS6,
      /**/
      /// <summary>
      /// IIS 7.0
      /// </summary>
      IIS7
   }
   /// <summary>
   /// 获取服务器IIS版本
   /// </summary>
   /// <returns></returns>
   public WebServerTypes GetIISServerType()
   {
      string DomainName = "localhost";
      string path = "IIS://" + DomainName + "/W3SVC/INFO";
      System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path);
      int num = -1;
      //num = (int)entry.Properties["MajorIISVersionNumber"].Value;//此行代码在 Windows XP SP2 下有问题。

      switch (num)
      {
         case 4:
            return WebServerTypes.IIS4;
         case 5:
            return WebServerTypes.IIS5;
         case 6:
            return WebServerTypes.IIS6;
         case 7:
            return WebServerTypes.IIS7;
         default:
            return WebServerTypes.Unknown;
      }
   }
   #endregion


   #region 判断是哪个按钮提交的事件
   /// <summary>
   /// 判断是哪个按钮提交的事件
   /// </summary>
   /// <returns></returns>
   //internal String GetFormPostbackTriggerId()
   //{
   //   if (HttpContext.Current == null) return String.Empty;
   //   if (!HttpContext.Current.Request.Url.AbsolutePath.EndsWith(".aspx")) return String.Empty;

   //   string _triggerControlId = HttpContext.Current.Request.Form["__EVENTTARGET"];
   //   if (String.IsNullOrEmpty(_triggerControlId))
   //   {
   //      foreach (string str in HttpContext.Current.Request.Form)
   //      {
   //         Control c = this.Page.FindControl(str);
   //         if (c is Button)// 暂时忽略 imagebuttion
   //         {
   //            _triggerControlId = c.ID;
   //            break;
   //         }
   //      }
   //   }
   //   else
   //   {
   //      string[] controlCollections = _triggerControlId.Split('$');
   //      Control ctl = null;
   //      ctl = this.Page;
   //      foreach (string s in controlCollections)
   //      {
   //         ctl = ctl.FindControl(s);
   //         if (null == ctl)
   //            break;
   //      }
   //      _triggerControlId = (null == ctl) ? null : ctl.ID;
   //   }
   //   return _triggerControlId;
   //}
   #endregion

   #region 是否在本地运行
   /// <summary>
   /// 是否在本地运行
   /// </summary>
   public static bool IsRunAtLocal()
   {
       string http_host = string.Empty;
       http_host = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
       if (http_host.Contains("localhost") || http_host.Contains("127.0.0.1") || http_host.Contains("192.168."))
       {
           return true;
       }
       else
       {
           return false;
       }
   }
   #endregion

   #region  检测是不是从外部提交的数据  public bool CheckIsRequest()
   /// <summary>
   /// 检测是不是从外部提交的数据
   /// </summary>
   /// <returns></returns>
   public bool CheckIsRequest()
   {
       try
       {
           string Referer = HttpContext.Current.Request.UrlReferrer.ToString();
           if (Referer == null || Referer == "")
           {
               return false;
           }
           Referer = Referer.ToLower().Replace("http://", ""); //去掉http://
           string[] Hosts = (Referer.Split('/'))[0].Split(':'); //把地址以‘/’以割把地址每段存在数组
           if (Hosts[0].CompareTo(HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString().ToLower()) == 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       catch
       {
           return false;
       }
   }
   #endregion
}