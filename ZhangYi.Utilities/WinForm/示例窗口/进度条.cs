using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;


	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 进度条 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackBar1;
		
		//代表进程等待时间
		private int waitTime=150;
		//申明一个进程对象
		private Thread barProgress;

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label completedlabel;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        public 进度条()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.completedlabel = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar1.Location = new System.Drawing.Point(0, 250);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(408, 23);
			this.progressBar1.Step = 1;
			this.progressBar1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 160);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "开始";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.trackBar1,
																					this.label2,
																					this.comboBox1,
																					this.label1});
			this.groupBox1.Location = new System.Drawing.Point(16, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 120);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "设置";
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(168, 64);
			this.trackBar1.Maximum = 150;
			this.trackBar1.Minimum = 10;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(136, 42);
			this.trackBar1.TabIndex = 3;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar1.Value = 10;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "进程启动速度";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Items.AddRange(new object[] {
														   "1",
														   "5",
														   "10",
														   "20"});
			this.comboBox1.Location = new System.Drawing.Point(176, 24);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "进度条的步长";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 224);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "进程已完成的百分比";
			// 
			// completedlabel
			// 
			this.completedlabel.Location = new System.Drawing.Point(168, 224);
			this.completedlabel.Name = "completedlabel";
			this.completedlabel.TabIndex = 4;
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(112, 160);
			this.button2.Name = "button2";
			this.button2.TabIndex = 5;
			this.button2.Text = "暂停";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(208, 160);
			this.button3.Name = "button3";
			this.button3.TabIndex = 6;
			this.button3.Text = "继续";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Enabled = false;
			this.button4.Location = new System.Drawing.Point(304, 160);
			this.button4.Name = "button4";
			this.button4.TabIndex = 7;
			this.button4.Text = "中止";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(408, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button4,
																		  this.button3,
																		  this.button2,
																		  this.completedlabel,
																		  this.label3,
																		  this.groupBox1,
																		  this.button1,
																		  this.progressBar1});
			this.Name = "Form1";
			this.Text = "进度条示例";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//将ComboBox1中选择的值转化为进度栏的步长
			progressBar1.Step = Int32.Parse((string)comboBox1.SelectedItem);
		
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			//trackBar拖动时改变等待时间
			TrackBar tb = (TrackBar) sender ;
			int time = 160 - tb.Value ;
			this.waitTime = time ;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//实例化一个线程，并引用此线程开始执行时要调用的方法。
			//这个方法在线程调用Start()方法时候启动
			barProgress = new Thread(new ThreadStart(ProgressProc));
			//指示这个线程为后台线程
			barProgress.IsBackground = true;
			//启动线程
			barProgress.Start();

			//设置其他按钮的Enabled属性
			button2.Enabled=true;
			button1.Enabled=false;
			button4.Enabled=true;
		}

	
		private void ProgressProc() 
		{
		    //实例化一个MethodInvoker 委托 
			MethodInvoker mi = new MethodInvoker(UpdateProgress);

			while (true) 
			{
				Invoke(mi);
				int sleepTime = this.waitTime;
				Thread.Sleep(sleepTime) ;
			}
		
		}
		private void UpdateProgress() 
		{
		    double  completed ;
			double  number;

			//如果值达到最大，进行重置重新开始
			if (progressBar1.Value == progressBar1.Maximum) 
			{
				progressBar1.Value = progressBar1.Minimum ;
			}
			else 
			{
				progressBar1.PerformStep();
			}
			number=this.progressBar1.Value;
			completed   = (number/100) * 100.0 ;
			//改变completedLabel的Text属性，代表进程进度
			completedlabel.Text = Math.Round(completed).ToString() + "%" ;

		}

		

		private void button2_Click(object sender, System.EventArgs e)
		{
			//线程挂起
			barProgress.Suspend  ();
			button3.Enabled=true;
            button4.Enabled=false;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			//继续线程
			barProgress.Resume();
			button3.Enabled=false;
            button4.Enabled=true;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			//结束进程
			barProgress.Abort();
			//将控件的值全部初始化
			progressBar1.Value=0;
            progressBar1.Step=1;
            completedlabel.Text="";
			trackBar1.Value=10;
			comboBox1.SelectedIndex=0;
			
			//将等待事件初始化
			this.waitTime=150;

			//按钮功能初始化
			button1.Enabled=true;
			button2.Enabled=false;
			button3.Enabled=false;
			button4.Enabled=false;
		}
	}