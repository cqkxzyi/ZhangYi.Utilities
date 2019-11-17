using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcObject.FilterAttribute
{
    /// <summary>
    /// 身份认证
    /// </summary>
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; private set; }

        /// <summary>
        /// 初始化权限验证类。
        /// </summary>
        /// <param name="permissionName">权限名称</param>
        public AuthorizeExAttribute(string permissionName = "")
        {
            this.PermissionName = permissionName;
        }


        /// <summary>
        /// 验证授权。
        /// </summary>
        /// <param name="httpContext">HTTP 上下文，它封装有关单个 HTTP 请求的所有 HTTP 特定的信息。</param>
        /// <returns>如果用户已经过授权，则为 true；否则为 false。</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                return false;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (IsAllow() && base.AuthorizeCore(httpContext))
                    return true;
            }
            httpContext.Response.StatusCode = 403;
            return false;
        }


        /// <summary>
        /// 重写验证。
        /// </summary>
        /// <param name="filterContext">验证信息上下文。</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    filterContext.Result = new RedirectResult("/AccessError");
                else
                    filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl + "?returnUrl=" + filterContext.HttpContext.Request.UrlReferrer);
            }
        }

        /// <summary>
        /// AuthorizeCore函数结果为false时，就会触发HandleUnauthorizedRequest函数来处理验证失败
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //如果是Ajax请求
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //返回json
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        ResultCode = 500,
                        ResultMess = "请求用户未登录！"
                    }
                };

                //or
                //返回js
                filterContext.Result = new AjaxUnauthorizedResult();
            }
            else
            {
                //处理Url请求
                //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法 
                filterContext.Result = new RedirectResult("/Home/Index");
            }
            base.HandleUnauthorizedRequest(filterContext);
        }


        /// <summary>
        /// 主要的验证权限方法
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool IsAllow()
        {
            //写上验证代码
            return true;
        }
    }
}