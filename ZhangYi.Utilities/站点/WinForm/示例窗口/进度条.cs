using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;


	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ������ : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackBar1;
		
		//������̵ȴ�ʱ��
		private int waitTime=150;
		//����һ�����̶���
		private Thread barProgress;

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label completedlabel;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ������()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			this.button1.Text = "��ʼ";
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
			this.groupBox1.Text = "����";
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
			this.label2.Text = "���������ٶ�";
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
			this.label1.Text = "�������Ĳ���";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 224);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "��������ɵİٷֱ�";
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
			this.button2.Text = "��ͣ";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(208, 160);
			this.button3.Name = "button3";
			this.button3.TabIndex = 6;
			this.button3.Text = "����";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Enabled = false;
			this.button4.Location = new System.Drawing.Point(304, 160);
			this.button4.Name = "button4";
			this.button4.TabIndex = 7;
			this.button4.Text = "��ֹ";
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
			this.Text = "������ʾ��";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//��ComboBox1��ѡ���ֵת��Ϊ�������Ĳ���
			progressBar1.Step = Int32.Parse((string)comboBox1.SelectedItem);
		
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			//trackBar�϶�ʱ�ı�ȴ�ʱ��
			TrackBar tb = (TrackBar) sender ;
			int time = 160 - tb.Value ;
			this.waitTime = time ;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//ʵ����һ���̣߳������ô��߳̿�ʼִ��ʱҪ���õķ�����
			//����������̵߳���Start()����ʱ������
			barProgress = new Thread(new ThreadStart(ProgressProc));
			//ָʾ����߳�Ϊ��̨�߳�
			barProgress.IsBackground = true;
			//�����߳�
			barProgress.Start();

			//����������ť��Enabled����
			button2.Enabled=true;
			button1.Enabled=false;
			button4.Enabled=true;
		}

	
		private void ProgressProc() 
		{
		    //ʵ����һ��MethodInvoker ί�� 
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

			//���ֵ�ﵽ��󣬽����������¿�ʼ
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
			//�ı�completedLabel��Text���ԣ�������̽���
			completedlabel.Text = Math.Round(completed).ToString() + "%" ;

		}

		

		private void button2_Click(object sender, System.EventArgs e)
		{
			//�̹߳���
			barProgress.Suspend  ();
			button3.Enabled=true;
            button4.Enabled=false;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			//�����߳�
			barProgress.Resume();
			button3.Enabled=false;
            button4.Enabled=true;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			//��������
			barProgress.Abort();
			//���ؼ���ֵȫ����ʼ��
			progressBar1.Value=0;
            progressBar1.Step=1;
            completedlabel.Text="";
			trackBar1.Value=10;
			comboBox1.SelectedIndex=0;
			
			//���ȴ��¼���ʼ��
			this.waitTime=150;

			//��ť���ܳ�ʼ��
			button1.Enabled=true;
			button2.Enabled=false;
			button3.Enabled=false;
			button4.Enabled=false;
		}
	}