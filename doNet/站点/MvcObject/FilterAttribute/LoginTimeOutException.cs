using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcObject.FilterAttribute
{
    public class LoginTimeOutException : Exception
    {
        public string Message { get { return "未登录！";} }
    }
}