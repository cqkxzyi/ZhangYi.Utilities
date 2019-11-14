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

using System.Web;

namespace DotNet.zy.Utilities
{
    public static class SessionHelper
    {
        #region 添加Session
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">调动有效期（默认30分钟）</param>
        public static void Add(string strSessionName, string strValue, int iExpires = 30)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 添加Session数组
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValues">Session值数组</param>
        /// <param name="iExpires">调动有效期（默认30分钟）</param>
        public static void Adds(string strSessionName, string[] strValues, int iExpires = 30)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = iExpires;
        }
        #endregion

        #region 读取Session
        /// <summary>
        /// 读取Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static string GetSession(string SessionName)
        {
            if (HttpContext.Current.Session[SessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[SessionName].ToString();
            }
        }

        /// <summary>
        /// 读取某个Session对象值数组
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值数组</returns>
        public static string[] GetSessions(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[strSessionName];
            }
        }
        #endregion

        #region 删除某个Session对象
        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
        #endregion
    }
}