using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core31.Web.Models;
using NLog;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;

namespace Core31.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// NLog对象
        /// </summary>
        public static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ILogger<HomeController> _logger;
        private readonly IServerAddressesFeature _serverAddressesFeature;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IServerAddressesFeature serverAddressesFeature, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _serverAddressesFeature = serverAddressesFeature;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            try
            {
                TestNLog();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// 异常页
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region 测试NLog
        /// <summary>
        /// 测试NLog
        /// </summary>
        private void TestNLog()
        {

            logger.Trace("Trace Message ");
            logger.Debug("Debug调试消息 Message");
            logger.Info("Info我的信息");
            logger.Fatal("Fatal  啊啊啊啊啊啊啊啊啊啊啊啊啊");

            LogEventInfo lei = new LogEventInfo();
            lei.Properties.Add("id", Guid.NewGuid().ToString());
            lei.Properties.Add("content", "sdfsd3222第三代2222");
            lei.Message = "自定义餐宿的Info";
            lei.Level = NLog.LogLevel.Info;
            logger.Log(lei);

            logger.Log(NLog.LogLevel.Info, "Log And Error");


            int a = 0;
            int b = 100 / a;
        }
        #endregion

        //[Route("api/[controller]")]
        public IActionResult GetServiceAddres()
        {
            var ip = _serverAddressesFeature.Addresses.FirstOrDefault();
            return Json("本机IP：" + ip);
        }

        [HttpGet]
        public IActionResult GetRemoteIpAddress()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return Json("客户端IP：" + ip);
        }
    }
}
