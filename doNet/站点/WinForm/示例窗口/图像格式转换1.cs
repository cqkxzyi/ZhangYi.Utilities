using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//Ҫ�õ�ImageFormat
using System.Drawing.Imaging;

namespace ch2_9
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ͼ���ʽת��1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.ComponentModel.IContainer components;

		public ͼ���ʽת��1()
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ͼ���ʽת��1));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(1016, 40);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Text = "��";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Enabled = false;
            this.toolBarButton2.ImageIndex = 1;
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Text = "ת��";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolBar1);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "ͼƬ��ʽת����";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		
		public  void UpdateToolBar()
		{
			//˵�������һ���Ӵ�����
			if (this.MdiChildren.Length==1)
			{
			this.toolBarButton2.Enabled=false;
			}
		}
		
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//������Ǵ򿪰�ť�Ĵ���
			if (e.Button==this.toolBarButton1)
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
					//ȥ��·������ļ���
					string sShortName=sFileName.Substring(sFileName.LastIndexOf("\\")+1);

					Bitmap bm=new Bitmap(sFileName);

					ͼ���ʽת��2 fm=new ͼ���ʽת��2();
					//���ļ������ݴ����Ӵ����pictureBox
					fm.pictureBox1.Image=bm;
					fm.Width=bm.Width;
					fm.Height=bm.Height+20;
					fm.Text=sShortName;
					//����˴���ĸ����壬�Ӷ��˴����Ϊһ��MDI���� 
					fm.MdiParent=this;
					fm.Show();

					//��ʾͼƬ֮��Ϳ���ʹ��ת����ť��
					this.toolBarButton2.Enabled=true;

				}
			}

			//�������ת����ť�Ĵ������
			if (e.Button==this.toolBarButton2)
			{
				
				ͼ���ʽת��3 fm3=new ͼ���ʽת��3();
				if (fm3.ShowDialog()==DialogResult.OK)
				{
					//����ͼƬת������ȡ��fm3��ȡ�õ���Ϣ���趨�ļ�ת����ʽ
					string  fileFormat=fm3.listView1.SelectedItems[0].Text;
					ImageFormat iFormat=null;
	
					switch  (fileFormat[0].ToString())
					{
						case "B":
							iFormat=ImageFormat.Bmp;
							break;
						case "J":
							iFormat=ImageFormat.Jpeg;
							break;
						case "T":
							iFormat=ImageFormat.Tiff;
							break;
						case "P":
							iFormat=ImageFormat.Png;
							break;
						case  "G":
							iFormat=ImageFormat.Gif;
							break;
					}
					string  filePath=fm3.textBox1.Text;

					//��ȡ��ǰ�Ӵ����ͼ�����
					ͼ���ʽת��2 fm2=(ͼ���ʽת��2)this.ActiveMdiChild;
					Bitmap bm=(Bitmap)fm2.pictureBox1.Image;
					try
					{
						//���ļ���ʽת��
						bm.Save(filePath,iFormat);
					}
					catch(Exception error)
					{
						MessageBox.Show(" ������Ϣ�ǣ�"+error.Message,"����",
							MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				}		   
				
		
	            	
			 }
         
		 }//end toolBar1_ButtonClick
		

	}
}
