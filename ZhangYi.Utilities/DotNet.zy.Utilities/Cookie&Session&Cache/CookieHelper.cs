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
using System.Web;
using System.Text;

namespace DotNet.zy.Utilities
{
    public class CookieHelper
    {
        #region  创建Cookies(设置Response)
        /// <summary>
        /// 创建Cookies(设置Response)
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <param name="strValue">Cookie 键值</param>
        /// <param name="hh">过期时间</param>
        /// <code>ck.setCookie("主键","键值","天数");</code>
        public static bool SetCookie(string cookName, string strValue,int hh=24)
        {
            try
            {
                TimeSpan ts = new TimeSpan(hh, 0, 0);
                HttpCookie Cookie = new HttpCookie(cookName);
                Cookie.Expires = DateTime.Now.Add(ts);//过期时间
                Cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
                //还有一种方法：Cookie.Values.Add("userid", HttpUtility.UrlEncode(items[0].Userid));显得更加方便
                HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 检测Cookies是否存在或者过时
        /// <summary>
        /// 检测Cookies是否存在或者过时
        /// </summary>
        /// <param name="cookName"></param>
        /// <returns></returns>
        public static bool CheckCookies(string cookName)
        {
            if (GetCookie(cookName) != null)
                return true;
            else
                return false;
        }
        #endregion

        #region  获取Cookies（解码）
        /// <summary>
        /// 获取Cookies（解码）
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.getCookie("主键");</code>
        public static string GetCookie(string strName)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return HttpUtility.UrlDecode(Cookie.Value.ToString());//防止乱码，在这里进行解码
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 清除指定Cookie
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddYears(-3);
                cookie.Values.Clear();
                //HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }
        #endregion

        #region 清除所有Cookie
        /// <summary>
        /// 清除所有Cookie
        /// </summary>
        public static void ClearAllCookies()
        {
            int c = HttpContext.Current.Request.Cookies.Count;

            for (int i = 0; i < c; i++)
            {
                HttpCookie hc = HttpContext.Current.Request.Cookies[i];
                hc.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(hc);
            }
        }
        #endregion
    }
}
