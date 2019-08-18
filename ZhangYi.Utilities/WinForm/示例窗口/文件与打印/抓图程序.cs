using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ch3_7
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ץͼ���� : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ץͼ����()
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "ͼ��";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "��׽ͼ��";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "����ͼ��";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.pictureBox1});
			this.panel1.Location = new System.Drawing.Point(16, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(488, 384);
			this.panel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(484, 380);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileName = "doc1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(520, 437);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "ץͼ����";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion



	
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			try
			{
				IDataObject d = Clipboard.GetDataObject ( ) ;
				//�жϼ��а��������ǲ���λͼ
				if ( d.GetDataPresent ( DataFormats.Bitmap ) ) 
				{
					//���λͼ����
					Bitmap b = ( Bitmap ) d.GetData ( DataFormats.Bitmap ) ;
					//����PictureBox�Ĵ�С
					this.pictureBox1.Width=b.Width;
					this.pictureBox1.Height=b.Height;
					//����Panel1�Ĵ�С
					this.panel1.Width=b.Width;
					this.panel1.Height=b.Height;
					//��ʾͼƬ 
					this.pictureBox1.Image=b;
				}
				else
				{
					//�����������û��ͼ���ļ����򷢳�����
					MessageBox.Show("û�п���ʾ��λͼ�ļ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

				}
			}
			catch(Exception error)
			{
	           //��ȡ�����������		
		       MessageBox.Show("������Ϣ�ǣ� "+error.Message,"����",MessageBoxButtons.OK,MessageBoxIcon.Error);

			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			try
			{
				IDataObject d = Clipboard.GetDataObject ( ) ;
				//�жϼ��а��������ǲ���λͼ
				if ( d.GetDataPresent ( DataFormats.Bitmap ) ) 
				{
					Bitmap b = ( Bitmap ) d.GetData ( DataFormats.Bitmap ) ;	
					//���ù�����
					this.saveFileDialog1.Filter="Bitmap file(*.bmp)|*.bmp|All files(*.*)|*.*";
					if (this.saveFileDialog1.ShowDialog()==DialogResult.OK)
					{
				
						//��ȡ�����ļ���
						string fileName=this.saveFileDialog1.FileName;
						//�����ļ�
						b.Save(fileName);
					}
				}
				else
				{
					//�����������û��ͼ���ļ����򷢳�����
					MessageBox.Show("û�пɱ����λͼ�ļ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				}
			}
			catch(Exception error)
			{
				//��ȡ�����������		
				MessageBox.Show("������Ϣ�ǣ� "+error.Message,"����",MessageBoxButtons.OK,MessageBoxIcon.Error);

			}
		}

		
	}
}
