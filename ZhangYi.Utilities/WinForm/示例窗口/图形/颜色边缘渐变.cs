using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
  
namespace ch2_2
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 颜色边缘渐变 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 颜色边缘渐变()
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
			this.ClientSize = new System.Drawing.Size(492, 323);
			this.Name = "Form1";
			this.Text = "图形色彩从中心到边缘的变化";

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		protected override void OnPaint(PaintEventArgs e) 
		{
			//获得Graphics对象
			Graphics g = e.Graphics;

            //图像呈现质量为消除锯齿
			g.SmoothingMode = SmoothingMode.AntiAlias;
            //使用SolidBrush将窗体涂为白色
		    g.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

			//创建一个包含单个椭圆路径的path对象
			GraphicsPath path = new GraphicsPath();
		    path.AddEllipse(250,100,200,100);
			//path.AddEllipse(100, 100, 280, 140);

			// 使用这个Path对象创建一个渐变路径画笔.
			PathGradientBrush pBrush = new PathGradientBrush(path);

			// 将path对象的中心颜色设为蓝色
			pBrush.CenterColor = Color.Blue;

			// 将环绕在path对象边界上的颜色设为Aqua
			Color[] colors = {Color.Aqua};
			pBrush.SurroundColors = colors;

			//绘制椭圆
            g.FillEllipse(pBrush, 250,100,200,100);
 
		    GraphicsPath path1 = new GraphicsPath();
		   //设置五角星的每点的坐标数组
			Point[] points = {
								 new Point(125, 75),
								 new Point(150, 125),
								 new Point(200, 125),
								 new Point(162, 150),
								 new Point(200, 225),
								 new Point(125, 175),
								 new Point(50, 225),
								 new Point(87, 150),
								 new Point(50, 125),
								 new Point(100, 125)};

			path1.AddLines(points);
			PathGradientBrush pBrush1 = new PathGradientBrush(path1);

			//构造ColorBlend对象，用于多色渐变
			ColorBlend colorBlend = new ColorBlend();

			//定义三种颜色的多色渐变
			colorBlend.Colors = new Color[]
			{
				Color.Blue, Color.Aqua, Color.Green
			};

			//定义每一种颜色的位置
			colorBlend.Positions = new float[]
			{
				0.0f, 0.4f, 1.0f
			};
	
			//把colorBlend对象赋给刷子的InterpolationColor属性
			pBrush1.InterpolationColors = colorBlend;
			
			//绘制五角星
            g.FillRectangle(pBrush1, new Rectangle(0, 0, 200, 225));

  
		}
	}
}
