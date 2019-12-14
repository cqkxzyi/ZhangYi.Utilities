using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcObject.通用.Redis
{
    /// <summary>
    /// Redis 操作类
    /// 启动命令：redis-server.exe redis.windows.conf
    /// </summary>
    public class StackExchange_Redis_Helper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string ConnectionString = "127.0.0.1:6379";
        /// <summary>
        /// 锁
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// 连接对象
        /// </summary>
        private volatile IConnectionMultiplexer _connection;
        /// <summary>
        /// 数据库
        /// </summary>
        private IDatabase _db;
        public StackExchange_Redis_Helper()
        {
            _connection = ConnectionMultiplexer.Connect(ConnectionString);
            _db = GetDatabase();
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        protected IConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected)
            {
                return _connection;
            }
            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected)
                {
                    return _connection;
                }

                if (_connection != null)
                {
                    _connection.Dispose();
                }
                _connection = ConnectionMultiplexer.Connect(ConnectionString);
            }

            return _connection;
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase GetDatabase(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="data">值</param>
        /// <param name="cacheTime">时间</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }
            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            _db.StringSet(key, entryBytes, expiresIn);

        }

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Get<T>(string key)
        {

            var rValue = _db.StringGet(key);
            if (!rValue.HasValue)
            {
                return default(T);
            }

            var result = Deserialize<T>(rValue);

            return result;
        }

        /// <summary>
        /// 判断是否已经设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }


        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual long StringDecrement(string key,long i)
        {
            long val= _db.StringDecrement(key, i);//增量,每次加i
            return val;
        }


        /// <summary>
        /// 事务测试
        /// </summary>
        public void Transaction()
        {
            //方式1
            //var tran = _db.CreateTransaction();
            //tran.AddCondition(Condition.ListIndexNotEqual("zlh:1", 0, "zhanglonghao"));
            //tran.ListRightPushAsync("zlh:1", "zhanglonghao");
            //bool committed = tran.Execute();


            //方式2
            string name = _db.StringGet("name");
            string age = _db.StringGet("age");
            var tran = _db.CreateTransaction();//创建事物
            tran.AddCondition(Condition.StringEqual("name", name));//乐观锁
            tran.StringSetAsync("name", "海");
            tran.StringSetAsync("age", 25);
            _db.StringSet("name", "Cang");//此时更改 name 值，提交事物的时候会失败。
            bool committed = tran.Execute();//提交事物，true成功，false回滚。
            
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns>byte[]</returns>
        private byte[] Serialize(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
            {
                return default(T);
            }
            var json = Encoding.UTF8.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}