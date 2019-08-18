using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Threading;

namespace 内嵌文本
{
    class Program2
    {
        [STAThread]
        static void Main1()
        {
            Application myApp = new Application();
            myApp.Run(new MainWindow());
        }

    }
}

