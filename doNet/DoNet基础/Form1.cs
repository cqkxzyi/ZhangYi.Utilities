using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DoNet基础;
using DoNet基础.委托;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace DoNet基础
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         Exec();
      }

      public void Exec()
      {
          委托学习 _delegateTest = new 委托学习();
          //int a = _delegateTest.ExecDelegate();
      }

      #region 跨线程委托之：MethodInvoker

      //    //方式1
      private void button1_Click(object sender, EventArgs e)
      {
          Thread thread = new Thread(new ThreadStart(Change));
          thread.IsBackground = true;
          thread.Start();
      }

      private void Change()
      {
          //创建一个委托，Hello是该委托所托管的代码，必须是声明为void的。
          MethodInvoker mi = new MethodInvoker(Hello);
          this.Invoke(mi);       //同步执行委托。               
          //BeginInvoke(mi);  //异步执行委托。
          
      }

      private void Hello()
      {
          this.label1.Text = "我是跨线程的！";
      }


      //方式2
      private Thread myThread;
      //private void button1_Click(object sender, EventArgs e)
      //{
      //    myThread = new Thread(new ThreadStart(RunsOnWorkerThread));
      //    myThread.IsBackground = true;
      //    myThread.Start();
      //}

      private void RunsOnWorkerThread()
      {
          string txt = "myThread线程调用UI控件";
          this.label1.BeginInvoke(new System.EventHandler(UpdateUI), txt);
      }

      //直接用System.EventHandler,没有必要自定义委托
      private void UpdateUI(object o, System.EventArgs e)
      {
          //UI线程设置label1属性
          this.label1.Text = o.ToString() + "成功!";
      }      
      #endregion


      #region 异步委托

      public delegate DataTable GetLogDelegate();
      public delegate void SetDelegate(DataTable db);

      private void button2_Click(object sender, EventArgs e)
      {
          GetLogDelegate getLogDel = new GetLogDelegate(GetLogs);

          getLogDel.BeginInvoke(new AsyncCallback(LogTableCallBack), null);

      }

      /// <summary>
      /// 从数据库中获取操作日志，该操作耗费时间较长，
      /// 且返回数据量较大，日志记录可能超过万条。
      /// </summary>
      /// <returns></returns>
      private DataTable GetLogs()
      {
          DataTable db = new DataTable();
          return db;
      }

      /// <summary>
      /// 回调函数
      /// </summary>
      /// <param name="tag"></param>
      private void LogTableCallBack(IAsyncResult tag)
      {
          AsyncResult result = (AsyncResult)tag;
          GetLogDelegate del = (GetLogDelegate)result.AsyncDelegate;

          DataTable logTable = del.EndInvoke(tag);


          if (this.label2.InvokeRequired)
          {
              this.label2.Invoke(new MethodInvoker(delegate() { BindLog(logTable); }));

              //this.label2.Invoke((MethodInvoker)(delegate() { BindLog(logTable); }));

              //this.label2.Invoke((EventHandler)(delegate { BindLog(logTable); }));

              //this.label2.Invoke(new SetDelegate(BindLog), logTable);

              //SetDelegate mydel = BindLog;
              //this.label2.Invoke(mydel, logTable);
          }
          else
          {
              BindLog(logTable);
          }
      }

      private void BindLog(DataTable logTable)
      {
          this.label2.Text = "异步加载成功" ;
      }


      #endregion
   }
}
