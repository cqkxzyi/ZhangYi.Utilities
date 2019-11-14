using System;
using System.Web;

/// <summary>
/// ������صĲ�����
/// ����
/// 2011.9
/// ����
/// </summary>
public class DataCache
{
    /// <summary>
    /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <returns></returns>
    public static object GetCache(string CacheKey)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        return objCache[CacheKey];
    }

    /// <summary>
    /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <param name="objObject"></param>
    public static void SetCache(string CacheKey, object objObject)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject);
    }

    /// <summary>
    /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <param name="objObject"></param>
    public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
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
