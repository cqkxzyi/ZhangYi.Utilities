using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace DoNet����.Socket���
{
    /// <summary>
    /// ���ڴ���ͻ��������Socket
    /// ���ߣ��ܹ�
    /// ��дʱ�䣺2009-03-18
    /// </summary>
    public class SocketThread:IDisposable
    {
        private Socket socket;
        private Thread thread;
        private bool isListening = false;
        private string text;
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="socket">���ڴ���ͻ���Ӧ���Socket</param>
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
                //<EOF>���Զ����Э�飬��ʾ��ֹ��Ϣ����
                if (text.IndexOf("<EOF>") > -1)
                {
                    isListening=false;
                    socket.Send(new byte[] { 0 });
                }
                else
                {
                    //Console.WriteLine("���յ������ݣ�" + text);
                    //���ݿͻ��˵������ȡ��Ӧ����Ӧ��Ϣ
                    string message = GetMessage(text);
                    //����Ӧ��Ϣ���ֽڵķ�ʽ���͵��ͻ���
                    socket.Send(Encoding.UTF8.GetBytes(message));
                }
            }
        }

        private string GetMessage(string request)
        {
            string message = string.Empty;
            switch (request)
            {
                case "date": message = "���������ڣ�"+DateTime.Now.ToString("yyyy-MM-dd"); break;
                case "time": message ="������ʱ�䣺"+ DateTime.Now.ToString("HH:mm:ss"); break;
                case "datetime": message = "����������ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); break;
                case "year": message = "��������ݣ�" + DateTime.Now.Year.ToString(); break;
                case "month": message = "�������·ݣ�" + DateTime.Now.Month.ToString(); break;
                case "day": message = "���Ǳ��µ�" + DateTime.Now.Day.ToString()+"��"; break;
                default: message = "����ȷ�Ĳ���"; break;
            }
            return message;
        }

        #region IDisposable ��Ա

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
