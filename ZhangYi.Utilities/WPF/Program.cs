using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Threading;
using 内嵌文本;

namespace WPF窗口
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application myApp = new Application();
            myApp.Run(new MainWindow());
        }

    }


}