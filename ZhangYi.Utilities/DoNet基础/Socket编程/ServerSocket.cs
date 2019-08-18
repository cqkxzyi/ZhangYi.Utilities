using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DoNet����.Socket���
{
    /// <summary>
    /// Socket���������������ڼ����ͻ�����������
    /// ���ߣ��ܹ�
    /// ��дʱ�䣺2009-03-18    
    /// </summary>
    public class ServerSocket:IDisposable
    {
        Socket listener = null;
        /// <summary>
        /// ��ʼ����ָ���˿�
        /// </summary>
        public void StartListening(int port)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            //�����з������˳������ڵĻ���Ϊ�����������ͻ�������
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[2];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            //����һ��TCP/IP Socket���ڼ����ͻ�������
            Socket listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //�Ȱ�Ҫ�����������Ͷ˿�
                listener.Bind(localEndPoint);
                //�ٿ�ʼ����������ָ���������е���󳤶�
                listener.Listen(10);

                //��ʼ��������
                while (true)
                {
                    Console.WriteLine("�ȴ��ͻ�������...");
                    //�߳̽�һֱ����ֱ�����µĿͻ�������
                    Socket handler = listener.Accept();
                    //����һ���µ��߳����ڴ���ͻ�������
                    //�������̻߳����Լ������ܿͻ�������
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

        #region IDisposable ��Ա

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