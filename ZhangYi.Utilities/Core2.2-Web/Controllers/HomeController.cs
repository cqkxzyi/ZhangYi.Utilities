using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core2._2_Web.Models;
using NLog;

namespace Core2._2_Web.Controllers
{
    public class HomeController : Controller
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();


        public IActionResult Index()
        {
            try
            {
                TestNLog();
            }
            catch (Exception ex)
            {
                logger.Error(ex,ex.Message);
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        /// <summary>
        /// 测试NLog
        /// </summary>
        public void TestNLog() {

            logger.Trace("Trace Message");
            logger.Debug("Debug调试消息 Message");
            logger.Info("Info我的信息");
            logger.Fatal("Fatal  啊啊啊啊啊啊啊啊啊啊啊啊啊");

            LogEventInfo lei = new LogEventInfo();
            lei.Properties.Add("id", Guid.NewGuid().ToString());
            lei.Properties.Add("content", "sdfsd3222第三代2222");
            lei.Message = "自定义餐宿的Info";
            lei.Level = LogLevel.Info;
            logger.Log(lei);

            logger.Log(LogLevel.Info, "Log And Error");


            int a = 0;
            int b = 100 / a;
        }
    }
}
