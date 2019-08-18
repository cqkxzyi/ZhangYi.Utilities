using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace ch2_7
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 平铺和反转图像 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;

		private WrapMode wr=WrapMode.Tile;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 平铺和反转图像()
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
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "平铺图像";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "水平翻转";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "垂直翻转";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "水平垂直同时翻转";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(376, 237);
			this.ContextMenu = this.contextMenu1;
			this.Name = "Form1";
			this.Text = "平铺和翻转图像";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g=e.Graphics;

            Image image = new Bitmap("I:\\360data\\重要数据\\桌面\\未命名.gif");
			TextureBrush tBrush = new TextureBrush(image);
           
	    	//设置铺设图片的方式
			tBrush.WrapMode = wr;
			
			g.FillRectangle(tBrush, this.ClientRectangle);
     	}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.Tile;
		this.Refresh();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.TileFlipX;
		this.Refresh();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.TileFlipY;
		this.Refresh();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.TileFlipXY;
		this.Refresh();
		}
	}
}
