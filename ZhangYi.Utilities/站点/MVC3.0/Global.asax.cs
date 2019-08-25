using MVC3._0.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC3._0
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

        }


        /// <summary>
        /// 系统异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            if (exception == null || exception.Message.Contains("未找到路径") || exception.Message.Contains("文件不存在"))
            {
                return;
            }
            Response.Clear();

            //将异常记录到文件
            ExceptionHelper.WriteExceptionToFile(exception);

            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Error");
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500: // Server error.
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }

            routeData.Values.Add("error", exception.Message);

            Server.ClearError();

            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

        }
    }
}