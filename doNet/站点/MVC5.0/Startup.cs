using Microsoft.Owin;
using MVC5._0.SignalR;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5._0.Startup))]
namespace MVC5._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            //app.MapSignalR<SignalRConn>("/SignalRConn");//采用持久化连接类（PersistentConnection）
        }
    }
}
