using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    public class TestSignalRController : Controller
    {
        /// <summary>
        /// 无代理模式
        /// </summary>
        /// <returns></returns>
        public ActionResult NoAgent()
        {
            return View();
        }

        /// <summary>
        /// 有代理模式
        /// </summary>
        /// <returns></returns>
        public ActionResult Agent()
        {
            return View();
        }

        /// <summary>
        /// 简易聊天功能
        /// </summary>
        /// <returns></returns>
        public ActionResult ChatHub2()
        {
            return View();
        }

        /// <summary>
        /// 长连接测试
        /// </summary>
        /// <returns></returns>
        public ActionResult SignalRConn()
        {
            return View();
        }
    }
}