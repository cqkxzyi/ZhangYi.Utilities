using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using 多线程登录;
using System.IO;

namespace CSharp通用方法
{

    public delegate void ShowProgressDelegate(string ProgressInfo);
    public delegate void CloseFormDelegate();
    public delegate void SystemInitializeDelegate();

    public class 程序自带
    {
        //创建启动屏幕主界面
         frmSplash splash = new frmSplash();
         //线程同步对象
         public static ManualResetEvent mre = new ManualResetEvent(false);

        public 程序自带()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //在辅助线程中显示启动窗体
            Thread thBegin = new Thread(ShowSplash);
            thBegin.Start();
            //在主线程中进行系统初始化工作
            SystemInitialize();
        }


        //系统初始化过程
        public void SystemInitialize()
        {
            mre.WaitOne();
            for (int i = 0; i < 100; i++)
            {
                splash.Invoke(splash.ShowProgress, new object[] { Convert.ToString(i) });
                Thread.Sleep(200);
            }
            //初始化完成，关闭启动屏幕
            splash.BeginInvoke(splash.CloseSplash);
        }

        //显示启动屏幕
        public void ShowSplash()
        {
            splash.ShowDialog();
            //系统初始化完成，显示主窗体
            Application.Run(new frmMain());
        }
    }





    public class 自己写的
    {
        public 自己写的()
        {
            Thread.CurrentThread.Name = "我叫线程";//给当前线程起名为"System Thread"
            Console.WriteLine(Thread.CurrentThread.Name + "'Status:" + Thread.CurrentThread.ThreadState);
            Console.ReadLine();
            FileStream outputfs = new FileStream("路径", FileMode.Create, FileAccess.Write, FileShare.None, 256, true);

            Alpha oAlpha = new Alpha();

            ThreadStart threadStart = new ThreadStart(oAlpha.Beta);
            Thread oThread = new Thread(threadStart);   //创建一个线程

            oThread.Start();　//　程序运行的是Alpha.Beta()方法 
            while (!oThread.IsAlive)
                Thread.Sleep(1);  //让主线程停1ms　　      
            oThread.Abort();  //终止线程oThread　　     
            oThread.Join();　 //使主线程等待，直到oThread线程结束。可以指定一个int型的参数作为等待的最长时间  
            Console.WriteLine();
            Console.WriteLine("Alpha.Beta has finished");
            try
            {
                Console.WriteLine("重新启动线程");
                oThread.Start();
            }
            catch (ThreadStateException)
            {
                Console.Write("线程发生错误 ");
                Console.ReadLine();
            }
        }
    }



    public class Alpha
    {
        public void Beta()
        {
            while (true)
            {
                Console.WriteLine("Alpha.Beta is running in its own thread.");
            }
        }
    }

}