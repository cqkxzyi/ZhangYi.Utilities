using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace ch2_1
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 颜色渐变 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
        
	  
        private LinearGradientMode lgMode=LinearGradientMode.Horizontal;

		public 颜色渐变()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(颜色渐变));
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5});
			this.menuItem1.Text = "颜色变化方向";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "从上到下";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "从左到右";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "有右上到左下";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "从左上到右下";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(492, 273);
			this.ContextMenu = this.contextMenu1;
			this.Name = "Form1";
			this.Text = "色彩渐变的图形";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e) 
		{
		   //获取事件的Graphics对象
			Graphics g = e.Graphics;
	       //现在创建需要绘制的长方形区域
			Rectangle r = new Rectangle(75, 50, 350, 170);
		   //创建一个渐变刷
			LinearGradientBrush lb = new LinearGradientBrush(r, Color.White,Color.Blue, lgMode);
		   //用渐变刷填充长方形区域
			g.FillRectangle(lb, r);
        }


		private void menuItem2_Click(object sender, System.EventArgs e)
		{
	    //指定从上到下的渐变
		this.lgMode=LinearGradientMode.Vertical;
		this.Refresh();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
		//指定从左到右的渐变。 
		this.lgMode=LinearGradientMode.Horizontal;
		this.Refresh();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
		//指定从右上到左下的渐变。 
		this.lgMode=LinearGradientMode.BackwardDiagonal;
		this.Refresh();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
		//从左上到右下的渐变。 
		this.lgMode=LinearGradientMode.ForwardDiagonal;
		this.Refresh();
		}

	}
}
