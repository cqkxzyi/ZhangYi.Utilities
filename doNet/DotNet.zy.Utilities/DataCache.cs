using System;
using System.Web;

/// <summary>
/// 缓存相关的操作类
/// 张毅
/// 2011.9
/// 深圳
/// </summary>
public class DataCache
{
    /// <summary>
    /// 获取当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <returns></returns>
    public static object GetCache(string CacheKey)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        return objCache[CacheKey];
    }

    /// <summary>
    /// 设置当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <param name="objObject"></param>
    public static void SetCache(string CacheKey, object objObject)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject);
    }

    /// <summary>
    /// 设置当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <param name="objObject"></param>
    public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
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
