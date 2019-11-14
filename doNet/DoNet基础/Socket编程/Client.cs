using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DoNet����.Socket���
{
    /// <summary>
    /// �ͻ��������Socket��װ��
    /// ���ߣ��ܹ�
    /// ��дʱ�䣺2009-03-18
    /// </summary>
    public class ClientSocket:IDisposable
    {
        private Socket sender = null;
        private bool isConnect = false;
        //�������ڽ��շ�������Ӧ�Ĵ洢��.
        private byte[] bytes = new byte[1024];
        //������ֹSoket����Ϣ
        private string shutDownMessage = "<EOF>";

        /// <summary>
        /// ���캯��
        /// </summary>
        public ClientSocket()
        {
            try
            {
                //����Ҫ���ӵ�������Ϣ��ʹ��11000��Ϊ�����˿�.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[2];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // ����һ��TCP/IPЭ���socket����
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);
                isConnect = true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }


        public static int SocketMain(String[] args)
        {
            ClientSocket client = new ClientSocket();
            client.StartClient();
            return 0;
        }


        public void StartClient()
        {
            // ���ӵ�Զ��������������������Ϣ
            try
            {

                Console.WriteLine("���ӵ�����{0}",sender.RemoteEndPoint.ToString());
                OutParameters();

                string consoleMessage = Console.ReadLine();
                while (isConnect)
                {
                    consoleMessage = consoleMessage.ToLower();
                    if (consoleMessage == "bye")
                    {
                        SendShutDownMessage();
                    }
                    else
                    {
                        string resultMessage = SendMessage(consoleMessage);
                        Console.WriteLine(resultMessage);
                        //OutParameters();
                        consoleMessage = Console.ReadLine();
                        Console.WriteLine("consoleMessage=" + consoleMessage);
                    }
                }

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("�����쳣 : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("����Socket�쳣: {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("�������쳣 : {0}", e.ToString());
            }

        }

        /// <summary>
        /// ����������͹ر�Socket��Ϣ������ֹ�������������
        /// </summary>
        public void SendShutDownMessage()
        {
            SendMessage(shutDownMessage);
            Console.WriteLine("�Ѿ��ر��������������");
            isConnect = false;
            Environment.Exit(0);
        }

        /// <summary>
        /// ��Զ������������Ϣ
        /// </summary>
        /// <param name="message">Ҫ���͵���Ϣ</param>
        public string SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            sender.Send(buffer);
            int count = sender.Receive(bytes);
            return Encoding.UTF8.GetString(bytes, 0, count);
        }

        private void OutParameters()
        {
            Console.WriteLine("����˵����");
            Console.WriteLine("��ȡ���������ڣ�date");
            Console.WriteLine("��ȡ������ʱ�䣺time");
            Console.WriteLine("��ȡ����������ʱ�䣺datetime");
            Console.WriteLine("��ȡ��������ݣ�year");
            Console.WriteLine("��ȡ�������·ݣ�month");
            Console.WriteLine("��ȡ������������day");
            Console.WriteLine("�ر����ӣ�bye");
            Console.WriteLine("��������Ҫ���еĲ�����");
        }

        #region IDisposable ��Ա

        public void Dispose()
        {
            isConnect = false;
            if (sender != null)
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }

        #endregion
    }
}