using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 错误日志类
    /// </summary>
    public class LogManager
    {
        public static void Error(string error)
        {
            Log4netHelper.GetErrorLog().Error(error);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            string errorMsg = ErrorHandler.GetErrorDescription(ex);
            Log4netHelper.GetErrorLog().Error(errorMsg);
        }

    }
}
