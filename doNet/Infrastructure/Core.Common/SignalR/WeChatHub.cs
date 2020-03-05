using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Common
{
    /// <summary>
    /// SignalR 集线器
    /// </summary>
    public class WeChatHub : Hub
    {
        public Dictionary<string, List<string>> UserList { get; set; } = new Dictionary<string, List<string>>();

        public void Send(MessageBody body)
        {
            Clients.All.SendAsync("Recv", body);
        }


        [Authorize(Roles = "User")]
        [HttpPost("SendToUser")]
        public async void SendToUser([FromBody] MessageBody model)
        {
            MessageBody message = new MessageBody()
            {
                Type = 1,
                Content = model.Content,
                UserName = model.UserName
            };

            if (UserList.ContainsKey(model.UserName))
            {
                var connections =UserList[model.UserName].First();
                await Clients.Client(connections).SendAsync("Recv", new object[] { message });
            }

            //return Json(new { Code = 0 });
        }

        /// <summary>
        /// 加入分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task AddToGroupAsync(string groupName)
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 离开分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task RemoveFromGroupAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(this.Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 发送消息到指定分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToGroupAsync(string groupName, MessageBody message)
        {
            await Clients.Group(groupName).SendAsync(groupName, new object[] { message });
        }

        public override Task OnConnectedAsync()
        {
            var userName = this.Context.User.Identity.Name;
            var connectionId = this.Context.ConnectionId;
            if (!UserList.ContainsKey(userName))
            {
                UserList[userName] = new List<string>();
                UserList[userName].Add(connectionId);
            }
            else if (!UserList[userName].Contains(connectionId))
            {
                UserList[userName].Add(connectionId);
            }
            Console.WriteLine("哇，有人进来了：{0},{1},{2}", this.Context.UserIdentifier, this.Context.User.Identity.Name, this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = this.Context.User.Identity.Name;
            var connectionId = this.Context.ConnectionId;
            if (UserList.ContainsKey(userName))
            {
                if (UserList[userName].Contains(connectionId))
                {
                    UserList[userName].Remove(connectionId);
                }
            }

            Console.WriteLine("靠，有人跑路了：{0}", this.Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }

    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageBody
    {
        public int Type { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
