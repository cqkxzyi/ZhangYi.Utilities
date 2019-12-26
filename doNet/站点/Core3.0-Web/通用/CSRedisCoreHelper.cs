using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRedis;
using Newtonsoft.Json;

namespace Core3._0_Web.通用
{
    public class CSRedisCoreHelper
    {
        CSRedisClient rds = new CSRedisClient("127.0.0.1:6379,password=123,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240,prefix=key前辍");

        public void  普通() {
            rds.Set("test1", "123123", 60);
            rds.Get("test1");
            //函数名与 redis-cli 的命令相同，rds 一定是单例单例单例
        }

        public void 分区()
        {
            var rds = new CSRedisClient(null,
      "127.0.0.1:6371,password=123,defaultDatabase=11,poolsize=10,ssl=false,writeBuffer=10240,prefix=key前辍",
      "127.0.0.1:6372,password=123,defaultDatabase=12,poolsize=11,ssl=false,writeBuffer=10240,prefix=key前辍",
      "127.0.0.1:6373,password=123,defaultDatabase=13,poolsize=12,ssl=false,writeBuffer=10240,prefix=key前辍",
      "127.0.0.1:6374,password=123,defaultDatabase=14,poolsize=13,ssl=false,writeBuffer=10240,prefix=key前辍");
            //实现思路：根据key.GetHashCode() % 节点总数量，确定连向的节点
            //也可以自定义规则(第一个参数设置)

            rds.MSet("key1", 1, "key2", 2, "key3", 3, "key4", 4);
            rds.MGet("key1", "key2", "key3", "key4");
        }

        public void 发布订阅()
        {
            //普通订阅
            rds.Subscribe(
              ("chan1", msg => Console.WriteLine(msg.Body)),
              ("chan2", msg => Console.WriteLine(msg.Body)));

            //模式订阅（通配符）
            rds.PSubscribe(new[] { "test*", "*test001", "test*002" }, msg => {
                Console.WriteLine($"PSUB   {msg.MessageId}:{msg.Body}    {msg.Pattern}: chan:{msg.Channel}");
            });

            //模式订阅已经解决的难题：
            //1、分区的节点匹配规则，导致通配符最大可能匹配全部节点，所以全部节点都要订阅
            //2、本组 "test*", "*test001", "test*002" 订阅全部节点时，需要解决同一条消息不可执行多次

            //发布
            rds.Publish("chan1", "123123123");
            //无论是分区或普通模式，rds.Publish 都可以正常通信
        }

        public void 缓存壳()
        {
            //不加缓存的时候，要从数据库查询
            string str1 = "val";

            //1一般的缓存代码，如不封装还挺繁琐的
            var cacheValue = rds.Get("test1");
            if (!string.IsNullOrEmpty(cacheValue))
            {
                try
                {
                    var obj= JsonConvert.DeserializeObject(cacheValue);
                }
                catch
                {
                    //出错时删除key
                    rds.Del("test1");
                    throw;
                }
            }
            rds.Set("test1", JsonConvert.SerializeObject(str1), 10); //缓存10秒

            //2使用缓存壳效果同上，以下示例使用 string 和 hash 缓存数据
            var t1 = rds.CacheShell("test1", 10, () => str1);
            var t2 = rds.CacheShell("test", "1", 10, () => str1);
            var t3 = rds.CacheShell("test", new[] { "1", "2" }, 10, notCacheFields => new[] { ("1", str1), ("2", str1) });
        }

        public void 管道()
        {
           
           var ret1 = rds.StartPipe().Set("a", "1").Get("a").EndPipe();
            var ret2 = rds.StartPipe(p => p.Set("a", "1").Get("a"));

            var ret3 = rds.StartPipe().Get("b").Get("a").Get("a").EndPipe();
            //与 rds.MGet("b", "a", "a") 性能相比，经测试差之毫厘

        }

        public void 多数据库()
        {
            var connectionString = "127.0.0.1:6379,password=123,poolsize=10,ssl=false,writeBuffer=10240,prefix=key前辍";
            var redis = new CSRedisClient[14]; //定义成单例
            for (var a = 0; a < redis.Length; a++)
            {
                redis[a] = new CSRedisClient(connectionString + "; defualtDatabase=" + a);
            } 

            //访问数据库1的数据
            redis[1].Get("test1");
            
        }


        public void Test()
        {
            var rds = new CSRedisClient("127.0.0.1:6379,password=,poolsize=50,ssl=false,writeBuffer=10240");

            //sub1, sub2 争抢订阅（只可一端收到消息）
            var sub1 = rds.SubscribeList("list1", msg => Console.WriteLine($"sub1 -> list1 : {msg}"));
            var sub2 = rds.SubscribeList("list1", msg => Console.WriteLine($"sub2 -> list1 : {msg}"));

            //sub3, sub4, sub5 非争抢订阅（多端都可收到消息）
            var sub3 = rds.SubscribeListBroadcast("list2", "sub3", msg => Console.WriteLine($"sub3 -> list2 : {msg}"));
            var sub4 = rds.SubscribeListBroadcast("list2", "sub4", msg => Console.WriteLine($"sub4 -> list2 : {msg}"));
            var sub5 = rds.SubscribeListBroadcast("list2", "sub5", msg => Console.WriteLine($"sub5 -> list2 : {msg}"));

            //sub6 是redis自带的普通订阅
            var sub6 = rds.Subscribe(("chan1", msg => Console.WriteLine(msg.Body)));

            sub1.Dispose();
            sub2.Dispose();
            sub3.Dispose();
            sub4.Dispose();
            sub5.Dispose();
            sub6.Dispose();

            rds.Dispose();
            return;
        }
    }
}
