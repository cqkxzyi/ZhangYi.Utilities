using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MVC5._0.SignalR
{
    /// <summary>
    /// 集线器
    /// </summary>
    public class ServerHub : Hub
    {
        /// <summary>
        /// 客户端连接的时候调用
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Trace.WriteLine("客户端连接成功");
            return base.OnConnected();
        }

        [HubMethodName("Hello")]
        public void Hello()
        {
            Clients.All.hello();
        }

        //public void Send(string name, string message, string datetime, string receiver)
        //{
        //    Clients.All.broadcastMessage(name, message, datetime, receiver);
        //}

        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="message"></param>
        [HubMethodName("Send")]
        public void Send(string message)
        {
            var name = "zhangyi";

            // 调用所有客户端的sendMessage方法
            Clients.All.sendMessage(name, message);
        }
    }
}