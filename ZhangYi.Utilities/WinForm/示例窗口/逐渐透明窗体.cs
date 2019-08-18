using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ch1_5
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.ComponentModel.IContainer components;

		public Form1()
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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label4.Location = new System.Drawing.Point(144, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 23);
			this.label4.TabIndex = 11;
			this.label4.Text = "100%";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label3.Location = new System.Drawing.Point(18, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "窗体的透明度为：";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label2.Location = new System.Drawing.Point(144, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 9;
			this.label2.Text = "10毫秒";
			// 
			// trackBar1
			// 
			this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar1.Location = new System.Drawing.Point(8, 72);
			this.trackBar1.Maximum = 1000;
			this.trackBar1.Minimum = 10;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(214, 42);
			this.trackBar1.TabIndex = 8;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar1.Value = 10;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "渐变时间间隔为：";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.button1.Location = new System.Drawing.Point(16, 192);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "开始";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.button2.Location = new System.Drawing.Point(128, 192);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "停止";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label4,
																					this.label3,
																					this.label2,
																					this.trackBar1,
																					this.label1,
																					this.button1,
																					this.button2});
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 248);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.ClientSize = new System.Drawing.Size(272, 285);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1});
			this.Name = "Form1";
			this.Text = "逐渐透明的窗体";
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void timer1_Tick(object sender, System.EventArgs e)
		{
		   //如果透明度大于0，则每次减少0.01，否则，透明度重设为1
			if (this.Opacity>0)
			{
				this.Opacity=(this.Opacity*100-1)/100;
				double p=this.Opacity*100;
				label4.Text=p.ToString()+"%";
			}
			else
			{
			this.Opacity=1;
			label4.Text="100%";
            }

		}

		
	
		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			//设置timer控件的Interval属性
			timer1.Interval=trackBar1.Value;
			label2.Text=timer1.Interval.ToString()+"毫秒";
		}

		private void button1_Click(object sender, System.EventArgs e)
		{  
			//计时器开始计时
			timer1.Start();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
		   //计时器停止
		   timer1.Stop();
		   //窗体透明度属性还原为1
		   this.Opacity=1;
		   label4.Text="100%";
		}

		
	}
}
