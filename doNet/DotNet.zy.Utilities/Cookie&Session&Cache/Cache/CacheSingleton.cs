using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 缓存入口
    /// </summary>
    public class CacheSingleton
    {
        private static ICache _Instance = null;
        private static readonly object _SynObject = new object();

        public static ICache GetCache()
        {
            if (_Instance == null)
            {
                lock (_SynObject)
                {
                    if (_Instance == null)
                    {
                        _Instance = new DefaultCache();
                    }
                }
            }
            return _Instance;
        }
    }

    /// <summary>
    /// 测试
    /// </summary>
    public class Test {
         public Test() {
          var cache=  CacheSingleton.GetCache();
            cache.Get("1");
        }
    }

}
