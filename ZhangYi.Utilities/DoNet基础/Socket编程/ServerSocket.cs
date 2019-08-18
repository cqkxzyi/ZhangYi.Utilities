using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DoNet基础.Socket编程
{
    /// <summary>
    /// Socket监听服务器，用于监听客户端连接请求
    /// 作者：周公
    /// 编写时间：2009-03-18    
    /// </summary>
    public class ServerSocket:IDisposable
    {
        Socket listener = null;
        /// <summary>
        /// 开始监听指定端口
        /// </summary>
        public void StartListening(int port)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            //以运行服务器端程序所在的机器为服务器监听客户端连接
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[2];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            //创建一个TCP/IP Socket用于监听客户端连接
            Socket listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //先绑定要监听的主机和端口
                listener.Bind(localEndPoint);
                //再开始监听，并且指定监听队列的最大长度
                listener.Listen(10);

                //开始监听连接
                while (true)
                {
                    Console.WriteLine("等待客户端连接...");
                    //线程将一直阻塞直到有新的客户端连接
                    Socket handler = listener.Accept();
                    //启用一个新的线程用于处理客户端连接
                    //这样主线程还可以继续接受客户端连接
                    SocketThread socketThread = new SocketThread(handler);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public static int SocketMain(String[] args)
        {
            ServerSocket server = new ServerSocket();
            server.StartListening(11000);
            return 0;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (listener != null)
            {
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        #endregion
    }
}