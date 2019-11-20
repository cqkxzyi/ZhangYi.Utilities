using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MvcObject.SignalR
{
    /// <summary>
    /// 持久连接类
    /// </summary>
    public class SignalRConn : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
          {
              Debug.WriteLine("OnConnected");
              return Connection.Send(connectionId, "Welcome!");
          }
  
          protected override Task OnReceived(IRequest request, string connectionId, string data)
         {
             Debug.WriteLine("OnReceived");
             return Connection.Broadcast(data);
         }
 
         protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
         {
             Debug.WriteLine("OnDisconnected");
             return base.OnDisconnected(request, connectionId, stopCalled);
         }
 
         protected override Task OnReconnected(IRequest request, string connectionId)
         {
             Debug.WriteLine("OnReconnected");
             return base.OnReconnected(request, connectionId);
         }

        public void 外部调用() {
            var conn=GlobalHost.ConnectionManager.GetConnectionContext<SignalRConn>();
            conn.Connection.Broadcast("我测试群发广播");
        }

    }
}