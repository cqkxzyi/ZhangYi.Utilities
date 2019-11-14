/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：全局异常过滤    
*└──────────────────────────────────────────────────────────────┘
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core2._2_Web.Filter
{
    public class GlobalExceptionFilter: IExceptionFilter
    {
       public static Logger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {
            
            //logger.Error(filterContext.Exception, filterContext.Exception.Message);
            //var result = new BaseResult()
            //{
            //    ResultCode = 106,//系统异常代码
            //    ResultMsg = "系统异常",//系统异常信息
            //};
            //filterContext.Result = new ObjectResult(result);
            //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //filterContext.ExceptionHandled = true;
        }
    }

    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int ResultCode { get; set; } = 0;
        /// <summary>
        /// 结果消息 如果不成功，返回的错误信息
        /// </summary>
        public string ResultMsg { get; set; } = "";
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BaseResult()
        {

        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="resultCode">结果代码</param>
        /// <param name="resultMsg">结果信息</param>
        public BaseResult(int resultCode, string resultMsg)
        {
            ResultCode = resultCode;
            ResultMsg = resultMsg;
        }
    }
}
