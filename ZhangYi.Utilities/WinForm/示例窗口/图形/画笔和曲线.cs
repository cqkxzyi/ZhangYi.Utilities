using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D; 

namespace ch2_5
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 画笔和曲线 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 画笔和曲线()
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
			this.ClientSize = new System.Drawing.Size(520, 357);
			this.Name = "Form1";
			this.Text = "演示画笔和曲线";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e) 
		{
			Graphics g = e.Graphics;
            //图像效果为消除锯齿
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			//设置背景颜色
            g.FillRectangle(new SolidBrush(Color.Aqua), ClientRectangle);
            
			Pen pen= new Pen(Color.Blue,2f);
			//做成点划线笔
			pen.DashStyle=DashStyle.DashDot;
			g.DrawLine(pen,20,15,480,15);

			//创建一支粗画笔 
			Pen pen1 = new Pen(Color.Blue, 20);
			//将其做成点线笔
			pen1.DashStyle = DashStyle.Dot;
			g.DrawLine(pen1,20,45,480,45);
            
		
			Pen pen2 = new Pen(Color.Blue, 20);
			//将其做成虚线笔
			pen2.DashStyle = DashStyle.Dash;
            //使开始端为圆角
			pen2.StartCap = LineCap.Round;
            //使末端成为箭头
			pen2.EndCap = LineCap.ArrowAnchor;
			g.DrawLine(pen2,20,90,480,90);
			
			
			Pen pen3= new Pen(Color.Blue, 2);
            Point[] points= new Point[] {
													 new Point(20, 135),
													 new Point(40, 145),
													 new Point(30, 180),
													 new Point(160, 140),
											         new Point(300,120),
													 new Point(470, 140),
			};
			//绘制一条曲线，定义弯曲强度
			g.DrawCurve(pen3,points,1.2f);
			//标注定义点
			foreach (Point p in points)
			{
				g.DrawEllipse(pen3,p.X-5,p.Y-5,10,10);
			}
			
			Pen pen4= new Pen(Color.Blue, 2);
			Point[] points1=new Point[] {
											  new Point(40, 220),
											  new Point(70, 200),
											  new Point(120, 190),
											  new Point(160, 250),
											  new Point(300,220),
											  new Point(470, 190),
			};
			//绘制封闭曲线
			g.DrawClosedCurve(pen4, points1);
			//标注定义点
			foreach (Point p in points1)
			{
				g.DrawEllipse(pen4,p.X-5,p.Y-5,10,10);
			}
            
       
			Pen pen5= new Pen(Color.Blue, 1);
			g.DrawString("贝赛尔曲线",this.Font,new SolidBrush(Color.Blue),20,260);
			Point[] points2=new Point[]{
								   new Point(100,260),
								   new Point(150,340),
				                   new Point(320,270),
				                   new Point(480,260)
								   };
			//绘制贝赛尔曲线
            g.DrawBezier(pen5,points2[0],points2[1],points2[2],points2[3]);

			//显示贝赛尔曲线的控制点
			foreach (Point p in points2)
			{
			g.DrawEllipse(pen5,p.X-5,p.Y-5,10,10);
			}

		}
	}
}
