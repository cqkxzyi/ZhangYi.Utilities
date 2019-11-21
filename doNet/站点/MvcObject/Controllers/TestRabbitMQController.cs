using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    /// <summary>
    /// RabbitMQ测试
    /// 
    /// ExchangeType有direct、Fanout和Topic三种，不同类型的Exchange路由的行为是不一样的
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public class TestRabbitMQController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}