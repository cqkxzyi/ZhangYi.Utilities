using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace zhangyi_Test
{
    class NetStream
    {
        /*
           网络流事例
        */
        public void DataStream()
        {
            SocketInformation mySocketinfo = new SocketInformation();
            Socket mySocket = new Socket(mySocketinfo);

            NetworkStream myNetStrem = new NetworkStream(mySocket);
            FileStream myFileStream = new FileStream("文件路径", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None,255,true);//true 表示异步流
            StreamReader myStreamRead = new StreamReader("C:\\Documents and Settings\\All Users\\桌面\\金山毒霸账号.txt");
            StreamWriter myStreamWriter = new StreamWriter("C:\\Documents and Settings\\All Users\\桌面\\金山毒霸账号.txt", true, System.Text.Encoding.UTF8);//如果不存在就新建一个文本 true

            byte[] myByt = new byte[1024];
            string text = "字符串";
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(text);//字符转换成字节
            text = System.Text.Encoding.ASCII.GetString(myByte);     //字节转换成字符
            int number = 0;


            //读取网络文本流
            myNetStrem.Read(myByt, 0, myByt.Length);
            myNetStrem.Flush();

            //读取文本流
            text = myStreamRead.ReadToEnd();
            myStreamRead.Close();

            //写入文本流
            myStreamWriter.Write(text);
            myStreamWriter.Close();
            myStreamWriter.Dispose();

            //将网络流写入到文件
            while ((number = myNetStrem.Read(myByt, 0, myByt.Length)) > 0)
            {
                myFileStream.Write(myByt, 0, number);
                myFileStream.Flush();
            }
            myFileStream.Close();

            //读取文件并写入网络流中
            myFileStream = new FileStream("文件路径", FileMode.OpenOrCreate, FileAccess.Read);
            while ((number = myFileStream.Read(myByt, 0, myByt.Length)) > 0)
            {
                myNetStrem.Write(myByt, 0, myByt.Length);
                myNetStrem.Flush();
                myByt = new byte[1024];
            }
            myFileStream.Close();

            //文本转换成RichTextBox
            text = "AA\rBB\rCC\n";
            RichTextBox rich = new RichTextBox();
            rich.Text = text;
            rich.AppendText("追加文本，不会换行");
            text = rich.Lines[0];

        }


        /*
            可变参数事例
        */
        public void Params_Text(params string[] str)
        {
            foreach (string str2 in str)
            {
                MessageBox.Show(str2);
            }
        }
        /*
            RichTextBox文本调试
        */
        public void 调试()
        {
            string text = "AA\rBB\rCC\n";
            RichTextBox rich = new RichTextBox();
            rich.Text = text;
            rich.AppendText("ddd");
            rich.AppendText("\r");
            rich.AppendText("\\r");
            rich.AppendText("eee");
            text = rich.Lines[0];
            MessageBox.Show(rich.Text);

        }

    }

}
