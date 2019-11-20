using MvcObject.通用;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    public class TestMencacheController : Controller
    {
        //Mencache缓存测试
        public ActionResult Index()
        {
            var obj=MemcacheHelper.Get("zhangyi");
            if (obj == null)
            {
                MemcacheHelper.Set("zhangyi", "我的名字叫张毅");
            }
      


            return View();
        }
    }
}