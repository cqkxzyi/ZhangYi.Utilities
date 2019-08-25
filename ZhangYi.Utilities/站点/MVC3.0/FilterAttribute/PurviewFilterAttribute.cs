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
    /// 验证权限的过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PurviewFilterAttribute : ActionFilterAttribute
    {
        //应传入的功能权限值
        public string CheckRole { get; set; } 

        public PurviewFilterAttribute(string checkRole)
        {
            CheckRole = checkRole;
        }    

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断是否已登录


            //判断角色
            string[] roles = new string[] { "1", "2" };
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(HttpContext.Current.User.Identity, roles);

            bool isAuthorize = filterContext.HttpContext.User.IsInRole(CheckRole);
            if (!isAuthorize)  //判断用户是否拥有checkRole权限，没有的话跳转到权限错误页。
            {
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { Controller = "Account", Action = "AuthorizeError" }));
            }
        }
    }
}