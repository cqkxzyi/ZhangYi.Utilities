using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    public class HomeController : BaseController
    {
        //允许匿名访问
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //允许以下用户访问
        [Authorize(Users = "13500379061,13983784926,18323851534,13594663608,13594663608,13883945069")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}