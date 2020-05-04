using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Core31.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// NLog对象
        /// </summary>
        public static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 原生log
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                TestNLog();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id:int:min(3)}")]
        public ActionResult<string> Get(int id)
        {
            return new JsonResult(new { id });
        }

        [HttpPost]
        public ActionResult<string> POST()
        {
            return new JsonResult(new { code = 200 });
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DELETE(int id)
        {
            return new JsonResult(new { msg = id + "删除成功" });
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
    }
}
