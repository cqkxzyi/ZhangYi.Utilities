using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 子窗口显示样式 : System.Windows.Forms.Form
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
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 子窗口显示样式()
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
			this.menuItem1.Text = "文件(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "打开(&O)";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "退出(&E)";
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
			this.menuItem4.Text = "窗口(&W)";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 0;
			this.menuItem5.Text = "层叠窗口(&C)";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "水平平铺窗口(&H)";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "垂直平铺窗口(&V)";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(680, 353);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "图片浏览器";

		}
		#endregion


		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			//设置文件类型过滤器 
			this.openFileDialog1.Filter="Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png" 
				+ "|JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg" 
				+ "|GIF files (*.gif)|*.gif" 
				+ "|BMP files (*.bmp)|*.bmp" 
				+ "|Tiff files (*.tif;*.tiff)|*.tif;*.tiff" 
				+ "|Png files (*.png)|*.png" 
				+ "|All files (*.*)|*.*";
			
			if (this.openFileDialog1.ShowDialog()==DialogResult.OK)
			{
			//获取文件的详细路径
			string sFileName=this.openFileDialog1.FileName;
			//获得文件名
			子窗口显示样式2 fm=new 子窗口显示样式2();
			//将文件名数据传给子窗体
			fm.fileName=sFileName;
			//定义此窗体的父窗体，从而此窗体成为一个MDI窗体 
			fm.MdiParent=this;
			fm.Show();
	    	}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			
			if (MessageBox.Show("您确定要退出吗？","提示",MessageBoxButtons.OKCancel)
				==DialogResult.OK)
			{
				this.Close();
				Application.Exit();
			}
			else
			{
			//表示不进行任何处理
			}

		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			//层叠显示所有子窗口
			this.LayoutMdi(MdiLayout.Cascade  );
            //this.
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			//水平平铺所有窗口
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			//垂直平铺所有窗口
			this.LayoutMdi(MdiLayout.TileVertical);
		}
	}
