using MvcObject.Controllers;
using MvcObject.FilterAttribute;
using MvcObject.ͨ��;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcObject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            RegisterGlobalFilters(GlobalFilters.Filters);

;
        }

        /// <summary>
        /// ȫ�ֹ���������
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeExAttribute());
            //filters.Add(new LoginFilterAttribute());

        }


        /// <summary>
        /// ϵͳ�쳣����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            if (exception == null || exception.Message.Contains("δ�ҵ�·��") || exception.Message.Contains("�ļ�������"))
            {
                return;
            }
            Response.Clear();

            //���쳣��¼���ļ�
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
