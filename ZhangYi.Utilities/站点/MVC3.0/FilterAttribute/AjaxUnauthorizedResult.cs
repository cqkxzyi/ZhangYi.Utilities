﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC3._0.FilterAttribute
{
    /// <summary>
    /// 返回js
    /// </summary>
    public class AjaxUnauthorizedResult : JavaScriptResult
    {
        public AjaxUnauthorizedResult()
        {
            this.Script = "location.href='" + FormsAuthentication.LoginUrl + "'";
        }
    }
}