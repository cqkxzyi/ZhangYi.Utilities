using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 多线程登录
{
    /// <summary>
    /// Interaction logic for winSplash.xaml
    /// </summary>
    public partial class winSplash : Window
    {
        public winSplash()
        {
            InitializeComponent();
        }


        public void ShowProgress(int Value)
        {
            pgbProcess.Value = Value;
            tbInfo.Text ="已完成"+ Value.ToString() + "%";
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //通知主线程自己已经启动完毕
            Program.mre.Set();
        }
    }
}
