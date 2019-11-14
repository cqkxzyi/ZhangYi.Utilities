using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3._0.FilterAttribute
{
    /// <summary>
    /// 自定义角色权限控制
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AuthUserRoleAttribute : AuthorizeAttribute
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }
    }
}