using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSharp.示例窗口
{
    public partial class 右下方弹出窗口哦 : Form
    {
        public 右下方弹出窗口哦()
        {
            InitializeComponent();
        }
        public string P_syear = "2010";
        public string P_smonth = "1";
        public string P_sgcmc = "重庆工厂";
        public string P_sgysdm = "1002";
        public string P_sgysmc = "重庆海特汽车排气系统有限公司";
        public DateTime P_stjtime = Convert.ToDateTime("2010-01-01 14:00");


        /// <summary>窗体动画函数。
        /// 
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>
        /// <param name="dwTime">指定动画持续的时间</param>
        /// <param name="dwFlags">指定动画类型，可以是一个或多个标志的组合。</param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        //下面是可用的常量，根据不同的动画效果声明自己需要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果
        //窗体代码（将窗体的FormBorderStyle属性设置为none）：
        private void Form1_Load_1(object sender, EventArgs e)
        {
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            textBox1.Text = "(" + P_sgysdm + ") " + P_sgysmc + "   " + "在" + P_stjtime.ToString() + " 确认  " + P_syear
                            + "年" + P_smonth + "月 " + P_sgcmc + " 供货数量";
            //My.Application.Info.DirectoryPath
            try
            {
                System.Media.SoundPlayer s = new System.Media.SoundPlayer(@"D:\vs2005生成项目集\BMP\Global.wav");
                s.Play();
                DateTime ss = new DateTime();
                DateTime aa = DateTime.Now;
                string bb = "";
                bb.ToLower();
            }
            catch (Exception)
            {

            }
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
            AnimateWindow(this.Handle, 1000, AW_BLEND);
        }
    }

}