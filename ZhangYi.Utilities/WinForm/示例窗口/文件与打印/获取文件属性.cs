using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
namespace ch3_3
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��ȡ�ļ����� : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ��ȡ�ļ�����()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
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
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 224);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "ѡ���ļ�";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "�ļ���";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 24);
			this.label3.TabIndex = 3;
			this.label3.Text = "�ļ�·��";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 23);
			this.label5.TabIndex = 5;
			this.label5.Text = "����ʱ��";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(112, 24);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(120, 21);
			this.textBox1.TabIndex = 7;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(112, 64);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(120, 21);
			this.textBox2.TabIndex = 8;
			this.textBox2.Text = "";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(112, 104);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(120, 21);
			this.textBox3.TabIndex = 9;
			this.textBox3.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 10;
			this.label2.Text = "�޸�ʱ��";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(112, 144);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(120, 21);
			this.textBox4.TabIndex = 11;
			this.textBox4.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 184);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 12;
			this.label4.Text = "�ϴη���";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(112, 184);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(120, 21);
			this.textBox5.TabIndex = 13;
			this.textBox5.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBox6,
																					this.checkBox5,
																					this.checkBox4,
																					this.checkBox3,
																					this.checkBox2,
																					this.checkBox1});
			this.groupBox1.Location = new System.Drawing.Point(264, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 232);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�ļ�����";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(40, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "�Ƿ�ֻ���ļ�";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(40, 56);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "�Ƿ�ϵͳ�ļ�";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(40, 88);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "�Ƿ������ļ�";
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(40, 120);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Text = "�Ƿ�Ŀ¼";
			// 
			// checkBox5
			// 
			this.checkBox5.Location = new System.Drawing.Point(40, 152);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(128, 24);
			this.checkBox5.TabIndex = 4;
			this.checkBox5.Text = "�ϴα��ݺ��Ѹ���";
			// 
			// checkBox6
			// 
			this.checkBox6.Location = new System.Drawing.Point(40, 184);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.TabIndex = 5;
			this.checkBox6.Text = "�Ƿ���ʱ�ļ�";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(520, 269);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.textBox5,
																		  this.label4,
																		  this.textBox4,
																		  this.label2,
																		  this.textBox3,
																		  this.textBox2,
																		  this.textBox1,
																		  this.label5,
																		  this.label3,
																		  this.label1,
																		  this.button1});
			this.Name = "Form1";
			this.Text = "�ļ���Ϣ��ȡ";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void button1_Click(object sender, System.EventArgs e)
		{
			//checkBox��ʼ��
			this.checkBox1.Checked=false;
			this.checkBox2.Checked=false;
			this.checkBox3.Checked=false;
			this.checkBox4.Checked=false;
			this.checkBox5.Checked=false;
			this.checkBox6.Checked=false;
			
			if (this.openFileDialog1.ShowDialog()==DialogResult.OK)
			{
			//��ȡ�ļ���ϸ·�����ļ���
			string fileName=this.openFileDialog1.FileName;
			//��ȡ�ļ���
			string shortName=fileName.Substring(fileName.LastIndexOf("\\")+1);
			//��ȡ�ļ�·��
			string filePath=fileName.Substring(0,fileName.Length-shortName.Length);
            
			this.textBox1.Text=shortName;
			this.textBox2.Text=filePath;
			
			//��ȡ�ļ�����ʱ��
			string creatTime=File.GetCreationTime(fileName).ToLongDateString();
			this.textBox3.Text=creatTime;
			//��ȡ�ļ��޸�ʱ��
			string amendTime=File.GetLastWriteTime(fileName).ToLongDateString();
			this.textBox4.Text=amendTime;
			//��ȡ�ļ��ϴη���ʱ��
			string lastTime=File.GetLastAccessTime(fileName).ToLongDateString();
			this.textBox5.Text=lastTime;
	
		    //��ȡ�ļ�������Ϣ
			FileAttributes fAttris=File.GetAttributes(fileName);
			 
			string fileType=fAttris.ToString();
			//��Ϊһ���ļ�����ͬʱ�ж������ԡ�����ÿһ�������Ҫ����
			//�ж��Ƿ���ֻ���ļ�
			if ( fileType.LastIndexOf("ReadOnly")!=-1)
			{
				this.checkBox1.Checked=true;
			}
			//�ж��Ƿ���ϵͳ�ļ�
			if (fileType.LastIndexOf("System")!=-1)
			{
				this.checkBox2.Checked=true;
			}
			//�ж��Ƿ��������ļ�
			if (fileType.LastIndexOf("Hidden")!=-1)
			{
					
				this.checkBox3.Checked=true;
			}	
			//�ж��Ƿ���Ŀ¼
			if (fileType.LastIndexOf("Directory")!=-1)
			{
				this.checkBox4.Checked=true;
			}				
			//�ж��Ƿ��ϴα��ݺ��Ѹ���
			if (fileType.LastIndexOf("Archive")!=-1)
			{
				this.checkBox5.Checked=true;
			}
			//�ж��Ƿ�����ʱ�ļ�
			if (fileType.LastIndexOf("Temporary")!=-1)
			{
				this.checkBox5.Checked=true;
			}
				}
		}
	}
}
