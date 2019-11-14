using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
namespace ch2_6
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 图像显示与保存 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button1;

		private Image bm;
		public 图像显示与保存()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			string s="football.jpg";
            bm= new Bitmap(s);
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(400, 344);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 48);
			this.button1.TabIndex = 0;
			this.button1.Text = "保存图像";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(544, 513);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1});
			this.Name = "Form1";
			this.Text = "图像显示与保存";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
	    protected override void OnPaint(PaintEventArgs e) 
		{

		Graphics g=e.Graphics;

		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

		g.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

			
		//绘制图片
	    g.DrawImage(bm,0,0,bm.Width,bm.Height);

	    //绘制一个只有原图1/3大小的图片
		g.DrawImage(bm,0,340,(int)(bm.Width/3),(int)(bm.Height/3));


		//现在旋转该图像
		g.RotateTransform(-30);  
		//设置平移距离
		g.TranslateTransform(210,420,MatrixOrder.Append);
		//显示图像
		g.DrawImage(bm,0, 0,(int)(bm.Width/3),(int)(bm.Height/3));
	    g.ResetTransform();

		
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
		
			//构造一个和要保存图片尺寸相同的图像
			Bitmap image = new Bitmap(this.bm.Width, this.bm.Height);
			
			//获取Graphics对象
			Graphics g = Graphics.FromImage(image);
			g.Clear(this.BackColor);

			//调用Form1_Paint方法在图像上绘制图形		
		    Rectangle rect=new Rectangle(0,0,this.bm.Width,this.bm.Height);
			OnPaint(new PaintEventArgs(g,rect));
			
			//保存图像
			image.Save("SavedFootball.jpg", ImageFormat.Jpeg);
			MessageBox.Show("图片已经保存到当前项目的bin\\Debug目录下,名为SavedFootball.jpg");
	
		}
	}
}
