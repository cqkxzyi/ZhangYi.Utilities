using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace DoNet基础.Socket编程
{
    /// <summary>
    /// 用于处理客户端请求的Socket
    /// 作者：周公
    /// 编写时间：2009-03-18
    /// </summary>
    public class SocketThread:IDisposable
    {
        private Socket socket;
        private Thread thread;
        private bool isListening = false;
        private string text;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="socket">用于处理客户端应答的Socket</param>
        public SocketThread(Socket socket)
        {
            this.socket = socket;
            isListening = true;
            thread = new Thread(new ThreadStart(Work));
            thread.Start();
        }


        public void Work()
        {
            byte[] buffer=new byte[1024];
            while (isListening)
            {
                int receivedLength = socket.Receive(buffer);
                text=System.Text.Encoding.UTF8.GetString(buffer,0,receivedLength);
                Console.WriteLine(text);
                //<EOF>是自定义的协议，表示中止消息交流
                if (text.IndexOf("<EOF>") > -1)
                {
                    isListening=false;
                    socket.Send(new byte[] { 0 });
                }
                else
                {
                    //Console.WriteLine("接收到的数据：" + text);
                    //根据客户端的请求获取相应的响应信息
                    string message = GetMessage(text);
                    //将响应信息以字节的方式发送到客户端
                    socket.Send(Encoding.UTF8.GetBytes(message));
                }
            }
        }

        private string GetMessage(string request)
        {
            string message = string.Empty;
            switch (request)
            {
                case "date": message = "服务器日期："+DateTime.Now.ToString("yyyy-MM-dd"); break;
                case "time": message ="服务器时间："+ DateTime.Now.ToString("HH:mm:ss"); break;
                case "datetime": message = "服务器日期时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); break;
                case "year": message = "服务器年份：" + DateTime.Now.Year.ToString(); break;
                case "month": message = "服务器月份：" + DateTime.Now.Month.ToString(); break;
                case "day": message = "这是本月第" + DateTime.Now.Day.ToString()+"天"; break;
                default: message = "不正确的参数"; break;
            }
            return message;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            isListening = false;
            if (thread != null)
            {
                if (thread.ThreadState != ThreadState.Aborted)
                {
                    thread.Abort();
                }
                thread = null;
            }
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        #endregion

        
    }
}
