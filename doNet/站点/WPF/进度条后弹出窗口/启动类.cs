using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Windows;

namespace 进度条后弹出窗口
{
    class Program
    {
        static winSplash win = null;
        [STAThread]
        static void 进度条后弹出窗口()
        {
            //在一个独立UI线程中显示启动屏幕
            Thread th = new Thread(ShowSplashScreenThenMainWindow);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            //启动初始化过程
            SystemInit();
        }

        //线程同步信号，用于通知主线程启动屏幕已显示完毕
        public static ManualResetEvent mre = new ManualResetEvent(false);

        /// <summary>
        /// 进行系统初始化……
        /// </summary>
        static void SystemInit()
        {
            //等待启动窗体启动完毕
            mre.WaitOne();
            for (int i = 0; i <= 100; i++)
            {
                //跨线程更新启动窗体上的显示信息
                win.Dispatcher.BeginInvoke(
                    new Action<int>(win.ShowProgress),
                    DispatcherPriority.Input,
                    i);
                //休眠0.1秒，以模拟显示系统初始化的过程
                Thread.Sleep(100);
            }
            //关闭启动窗体
            win.Dispatcher.BeginInvoke(new Action(CloseSplashScreen), DispatcherPriority.Input, null);

        }

        /// <summary>
        /// 创建启动屏幕对象，显示系统初始化信息，最后显示主窗体
        /// </summary>
        static void ShowSplashScreenThenMainWindow()
        {
            win = new winSplash();
            win.ShowDialog();
            //显示主窗体
            Application myApp = new Application();
            myApp.Run(new winMain());
        }

        static void CloseSplashScreen()
        {
            win.Close();
        }
    }
}