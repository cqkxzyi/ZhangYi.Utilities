using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MVC3._0.Controllers
{
    /// <summary>
    /// 过滤器
    ///  MVC4.0后，微软加了一个AllowAnoumous的过滤器验证，即允许匿名用户访问，方法上的过滤器可以覆盖掉控制器上的标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LoginFilterAttribute : ActionFilterAttribute
    {
        ///// <summary>
        ///// 会判断是json请求 还是action请求
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (true)//没有权限
        //    {
        //        if (((((System.Web.Mvc.ReflectedActionDescriptor)((filterContext.ActionDescriptor))).MethodInfo).ReturnType).FullName == "System.Web.Mvc.JsonResult")
        //        {
        //            filterContext.RequestContext.HttpContext.AddError(new LoginTimeOutException());
        //        }

        //        filterContext.Result = new RedirectResult("/home/Login");
        //    }
        //}


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            
            //获取当前访问Url
            string sWebUrl = filterContext.HttpContext.Request.Url.Host;
            if (filterContext.HttpContext.Request.Url.Port != 80)
            {
                sWebUrl += ":"+filterContext.HttpContext.Request.Url.Port;
            }

            //判断是否已登录 方式1
            if (true)
            {
                //进入登录页面
                filterContext.HttpContext.Response.Redirect(new UrlHelper(filterContext.RequestContext).Action("login", "home"));
                filterContext.Result = new EmptyResult();
                return;
            }

            //判断是否已登录 方式2
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //完整的URL
                string okurl = filterContext.HttpContext.Request.RawUrl;
                string redirectUrl = string.Format("?ReturnUrl={0}", okurl);
                string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;

                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }
}