using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC3._0.Controllers
{
    public class LoginTimeOutException : Exception
    {
        public string Message { get { return "未登录！";} }
    }
}