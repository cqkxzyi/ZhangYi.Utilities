using MvcObject.通用.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    /// <summary>
    /// Redis 测试
    /// 启动命令：redis-server.exe redis.windows.conf
    /// 客户端连接：redis-cli.exe -h 127.0.0.1 -p 6379
    /// 客户端关闭服务器：shutdown  save|nosave
    /// 获取参数：config get *
    /// 
    /// </summary>
    public class TestRedisController : Controller
    {
        public ActionResult Index()
        {
            // Set();

            new StackExchange_Redis_Helper().Transaction();

            return View();
        }


        public void Set()
        {

            var data = Request["val"];
            new StackExchange_Redis_Helper().Set("abc", data, 10);
            var value = new StackExchange_Redis_Helper().Get<string>("abc");
        }


    }
}