using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using MvcObject.SignalR;
using Owin;

[assembly: OwinStartup(typeof(MvcObject.Startup))]

namespace MvcObject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888

           app.MapSignalR();
            //app.MapSignalR<SignalRConn>("/myconnection");//采用持久化连接类（PersistentConnection）
        }
    }
}
