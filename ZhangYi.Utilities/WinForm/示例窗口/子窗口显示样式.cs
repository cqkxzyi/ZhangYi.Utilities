using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class �Ӵ�����ʾ��ʽ : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public �Ӵ�����ʾ��ʽ()
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
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "�ļ�(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "��(&O)";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "�˳�(&E)";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MdiList = true;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7});
			this.menuItem4.Text = "����(&W)";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 0;
			this.menuItem5.Text = "�������(&C)";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "ˮƽƽ�̴���(&H)";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "��ֱƽ�̴���(&V)";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(680, 353);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "ͼƬ�����";

		}
		#endregion


		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			//�����ļ����͹����� 
			this.openFileDialog1.Filter="Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png" 
				+ "|JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg" 
				+ "|GIF files (*.gif)|*.gif" 
				+ "|BMP files (*.bmp)|*.bmp" 
				+ "|Tiff files (*.tif;*.tiff)|*.tif;*.tiff" 
				+ "|Png files (*.png)|*.png" 
				+ "|All files (*.*)|*.*";
			
			if (this.openFileDialog1.ShowDialog()==DialogResult.OK)
			{
			//��ȡ�ļ�����ϸ·��
			string sFileName=this.openFileDialog1.FileName;
			//����ļ���
			�Ӵ�����ʾ��ʽ2 fm=new �Ӵ�����ʾ��ʽ2();
			//���ļ������ݴ����Ӵ���
			fm.fileName=sFileName;
			//����˴���ĸ����壬�Ӷ��˴����Ϊһ��MDI���� 
			fm.MdiParent=this;
			fm.Show();
	    	}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			
			if (MessageBox.Show("��ȷ��Ҫ�˳���","��ʾ",MessageBoxButtons.OKCancel)
				==DialogResult.OK)
			{
				this.Close();
				Application.Exit();
			}
			else
			{
			//��ʾ�������κδ���
			}

		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			//�����ʾ�����Ӵ���
			this.LayoutMdi(MdiLayout.Cascade  );
            //this.
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			//ˮƽƽ�����д���
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			//��ֱƽ�����д���
			this.LayoutMdi(MdiLayout.TileVertical);
		}
	}
