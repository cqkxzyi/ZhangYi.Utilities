using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 内嵌文本
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty CounterProperty = DependencyProperty.Register("Counter", typeof(int), typeof(Window));

        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            Counter++;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock1.DataContext = this;
        }
    }
}
