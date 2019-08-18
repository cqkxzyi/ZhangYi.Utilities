using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using DotNet.zy.Utilities;

    public class ExceptionHelper
    {
        /// <summary>
        /// 将异常写入到日志文件
        /// 
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void WriteExceptionToFile(Exception ex)
        {
            //获取登录对象
            string username = "";

            if (username != null)
            {
                string errorMsg = ErrorHandler.GetErrorDescription(ex, username);
                Log4netHelper.GetErrorLog().Error(errorMsg);
            }
            else
            {
                string errorMsg = ErrorHandler.GetErrorDescription(ex);
                Log4netHelper.GetErrorLog().Error(errorMsg);
            }
        }
    }
