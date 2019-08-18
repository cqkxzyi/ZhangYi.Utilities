using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
namespace ch2_10
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 图片颜色渐变 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 图片颜色渐变()
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(368, 273);
			this.Name = "Form1";
			this.Text = "图形颜色变换";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g=e.Graphics;
	       
			//载入图片
            Image image = new Bitmap(@"I:\360data\重要数据\桌面\未命名.gif");
			//创建一个ImageAttributes类实例
			ImageAttributes imageAttributes = new ImageAttributes();
			
            float[][] colorMatrixElements = {  
												//将红色因子乘以2
												new float[] {2,  0,  0,  0, 0},       
												//绿色因子不变
												new float[] {0,  1,  0,  0, 0},        
												//蓝色因子乘以1.5
												new float[] {0,  0,  1,  0, 0},        
												new float[] {0,  0,  0,  1, 0},      
												//每个分量都添加0.2
												new float[] {0.2f, 0.2f, 0.2f, 0, 1}
											};    
			ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

			//使用ImageAttributes类的SetColorMatrix方法为colorMatrix对象设置ColorMatrixFlag枚举
			imageAttributes.SetColorMatrix(
				colorMatrix, 
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

			//显示原来的图片
			g.DrawImage(image, 10, 10);
			g.DrawString("原来的图片",this.Font,new SolidBrush(Color.Blue),10,100);

			//显示变换后的图片
			g.DrawImage(
				image, 
				//画图区
				new Rectangle(120, 10, image.Width, image.Height),  
				//源图的左上角
				0, 0,       
				//源图的宽度
				image.Width,
				//源图的高度
				image.Height,    
				GraphicsUnit.Pixel,
				imageAttributes);
			g.DrawString("单色变换后的图片",this.Font,new SolidBrush(Color.Blue),120,100);
		    
			Image image1 = new Bitmap(@"I:\360data\重要数据\桌面\未命名.gif");
			ImageAttributes imageAttributes1 = new ImageAttributes();
			
            float[][] colorMatrixElements1 = { 
												new float[] {1,  0,  0,  0, 0},
												new float[] {0,  1,  0,  0, 0},
												new float[] {0,  0,  1,  0, 0},
												new float[] {0,  0,  0,  1, 0},
												//其它因子不变，仅仅红色分量添加0.80
												new float[] {0.80f, 0, 0, 0, 1}};

			ColorMatrix colorMatrix1 = new ColorMatrix(colorMatrixElements1);

			//使用ImageAttributes类的SetColorMatrix方法为colorMatrix对象设置ColorMatrixFlag枚举
			imageAttributes1.SetColorMatrix(
				colorMatrix1, 
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

            //显示原来的图片
			g.DrawImage(image1, 10, 140, image1.Width, image1.Height);
			g.DrawString("原来的图片",this.Font,new SolidBrush(Color.Blue),10,230);

			//显示变换后的图片
			g.DrawImage(
				image1,
				//画图区
				new Rectangle(150, 140, image1.Width, image1.Height), 
				//源图的左上角
				0, 0,        
				//源图的宽度
				image1.Width,       
				//源图的宽度
				image1.Height,     
				GraphicsUnit.Pixel,
				imageAttributes1);
		   g.DrawString("颜色平移后的图片",this.Font,new SolidBrush(Color.Blue),150,230);
		}
	}
}
