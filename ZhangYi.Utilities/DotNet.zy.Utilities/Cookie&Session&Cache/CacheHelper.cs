using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 缓存相关的操作类
    /// 张毅
    /// 深圳
    /// </summary>
    public class CacheHelper
    {
        private static Cache objCache = HttpRuntime.Cache;

        /// <summary>  
        /// 获取缓存中的项数  
        /// </summary>  
        public static int Count
        {
            get
            {
                return objCache.Count;
            }
        }


        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static object GetCache(string CacheKey)
        {
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject)
        {
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        {
            objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, CacheItemPriority.NotRemovable, null);

        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);

        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            if (objCache[CacheKey] != null)
            {
                objCache.Remove(CacheKey);
            }

        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = objCache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                objCache.Remove(CacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// 清除客户端缓存
        /// </summary>
        public static void ClearClientPageCache()
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("cache-control", "private");
            HttpContext.Current.Response.CacheControl = "no-cache";
        }
    }
}