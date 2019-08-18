using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSharp.示例窗口
{
    public partial class 实用技巧集合 : Form
    {
        public 实用技巧集合()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 定义系统时间的一些属性
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SystemTime
        {
            public ushort year;
            public ushort month;
            public ushort dayofweek;
            public ushort day;
            public ushort hour;
            public ushort minute;
            public ushort second;
            public ushort milliseconds;
        }
        /// <summary>
        /// 设置api函数
        /// </summary>
        public class LibWrapDateTime
        {
            [DllImport("Kernel32.dll")]
            public static extern void GetSystemTime([In, Out] SystemTime st);
            [DllImport("Kernel32.dll")]
            public static extern bool SetSystemTime([In] SystemTime st);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime year = DateTime.Now;
            DateTime time = DateTime.Now;
            SystemTime st = new SystemTime();
            LibWrapDateTime.GetSystemTime(st);
            //设置系统日期
            st.year = (ushort)year.Year;
            st.month = (ushort)year.Month;
            st.dayofweek = (ushort)year.DayOfWeek;
            st.day = (ushort)year.Day;
            //设置系统时间
            st.hour = (ushort)time.Hour;
            st.minute = (ushort)time.Minute;
            st.second = (ushort)time.Second;
            st.milliseconds = (ushort)time.Millisecond;
            LibWrapDateTime.SetSystemTime(st);
        }
    }
}
