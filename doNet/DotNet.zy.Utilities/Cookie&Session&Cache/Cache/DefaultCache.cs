using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace DotNet.zy.Utilities
{
    public class  DefaultCache : ICache
    {
        private static Cache cache;
        private static readonly int cacheMinute = 120;
        private static TimeSpan timeSpan = new TimeSpan(0, cacheMinute,0);

        static DefaultCache()
        {
            cache = HttpRuntime.Cache;
        }

        public object Get(string cache_key)
        {
            return cache.Get(cache_key);
        }

        public List<string> GetCacheKeys()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator ca = cache.GetEnumerator();
            while (ca.MoveNext())
            {
                keys.Add(ca.Key.ToString());
            }
            return keys;
        }

        /// <summary>
        /// 设置绝对过期缓存  缓存时间为配置文件中设置的时间间隔
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        public void Set(string cache_key, object cache_object)
        {
            Set(cache_key, cache_object, DateTime.Now.AddMinutes(cacheMinute));
        }

        /// <summary>
        /// 设置绝对过期缓存  缓存时间为配置文件中设置的时间间隔
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        /// <param name="expiration">缓存过期时间</param>
        public void Set(string cache_key, object cache_object, DateTime expiration)
        {
            Set(cache_key, cache_object, expiration, CacheItemPriority.Normal);
        }

        public void Set(string cache_key, object cache_object, DateTime expiration, CacheItemPriority priority)
        {
            cache.Insert(cache_key, cache_object, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration, priority, null);
        }

        /// <summary>
        /// 设置滑动过期缓存  
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        /// <param name="expiration">缓存时间</param>
        public void Set(string cache_key, object cache_object, TimeSpan expiration)
        {
            Set(cache_key, cache_object, expiration, CacheItemPriority.Normal);
        }
        public void Set(string cache_key, object cache_object, TimeSpan expiration, CacheItemPriority priority)
        {
            cache.Insert(cache_key, cache_object, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expiration, priority, null);
        }


        public void Delete(string cache_key)
        {
            if (Exists(cache_key))
                cache.Remove(cache_key);
        }

        public bool Exists(string cache_key)
        {
            if (cache[cache_key] != null)
                return true;
            else
                return false;
        }

        public void Flush()
        {
            foreach (string s in GetCacheKeys())
            {
                Delete(s);
            }
        }
    }
}
