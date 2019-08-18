using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ch3_7
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 抓图程序 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 抓图程序()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
			this.menuItem1.Text = "图像";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "捕捉图像";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "保存图像";
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
			this.Text = "抓图程序";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion



	
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			try
			{
				IDataObject d = Clipboard.GetDataObject ( ) ;
				//判断剪切板中数据是不是位图
				if ( d.GetDataPresent ( DataFormats.Bitmap ) ) 
				{
					//获得位图对象
					Bitmap b = ( Bitmap ) d.GetData ( DataFormats.Bitmap ) ;
					//设置PictureBox的大小
					this.pictureBox1.Width=b.Width;
					this.pictureBox1.Height=b.Height;
					//设置Panel1的大小
					this.panel1.Width=b.Width;
					this.panel1.Height=b.Height;
					//显示图片 
					this.pictureBox1.Image=b;
				}
				else
				{
					//如果剪贴板上没有图像文件，则发出提醒
					MessageBox.Show("没有可显示的位图文件","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

				}
			}
			catch(Exception error)
			{
	           //读取剪贴板出错处理		
		       MessageBox.Show("错误信息是： "+error.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);

			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			try
			{
				IDataObject d = Clipboard.GetDataObject ( ) ;
				//判断剪切板中数据是不是位图
				if ( d.GetDataPresent ( DataFormats.Bitmap ) ) 
				{
					Bitmap b = ( Bitmap ) d.GetData ( DataFormats.Bitmap ) ;	
					//设置过滤器
					this.saveFileDialog1.Filter="Bitmap file(*.bmp)|*.bmp|All files(*.*)|*.*";
					if (this.saveFileDialog1.ShowDialog()==DialogResult.OK)
					{
				
						//获取保存文件名
						string fileName=this.saveFileDialog1.FileName;
						//保存文件
						b.Save(fileName);
					}
				}
				else
				{
					//如果剪贴板上没有图像文件，则发出提醒
					MessageBox.Show("没有可保存的位图文件","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				}
			}
			catch(Exception error)
			{
				//读取剪贴板出错处理		
				MessageBox.Show("错误信息是： "+error.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);

			}
		}

		
	}
}
