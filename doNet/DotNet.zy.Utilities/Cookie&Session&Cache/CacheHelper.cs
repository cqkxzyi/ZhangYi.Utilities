using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// ������صĲ�����
    /// ����
    /// ����
    /// </summary>
    public class CacheHelper
    {
        private static Cache objCache = HttpRuntime.Cache;

        /// <summary>  
        /// ��ȡ�����е�����  
        /// </summary>  
        public static int Count
        {
            get
            {
                return objCache.Count;
            }
        }


        /// <summary>
        /// ��ȡ���ݻ���
        /// </summary>
        /// <param name="CacheKey">��</param>
        public static object GetCache(string CacheKey)
        {
            return objCache[CacheKey];
        }

        /// <summary>
        /// �������ݻ���
        /// </summary>
        public static void SetCache(string CacheKey, object objObject)
        {
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// �������ݻ���
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        {
            objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, CacheItemPriority.NotRemovable, null);

        }

        /// <summary>
        /// �������ݻ���
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);

        }

        /// <summary>
        /// �Ƴ�ָ�����ݻ���
        /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            if (objCache[CacheKey] != null)
            {
                objCache.Remove(CacheKey);
            }

        }

        /// <summary>
        /// �Ƴ�ȫ������
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
        /// ����ͻ��˻���
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