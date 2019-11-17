using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/      

        public ActionResult Error(string error)
        {
            ViewData["Title"] = "WebSite 网站内部错误";
            ViewData["Description"] = error;
            return View();
        }

        public ActionResult HttpError404(string error)
        {
            ViewData["Title"] = "HTTP 404- 无法找到文件";
            ViewData["Description"] = error;
            return View("HttpError");
        }

        public ActionResult HttpError500(string error)
        {
            ViewData["Title"] = "HTTP 500 - 内部服务器错误";
            ViewData["Description"] = error;
            return View("Error");
        }

        public ActionResult General(string error)
        {
            ViewData["Title"] = "HTTP 发生错误";
            ViewData["Description"] = error;
            return View("Error");
        }

        public ActionResult CustomError(string error = "系统错误", string title = "系统错误")
        {
            ViewData["Title"] = title;
            ViewData["Description"] = error;
            ViewData["ErrorMessage"] = error;
            return View("Error");
        }
    }
}
