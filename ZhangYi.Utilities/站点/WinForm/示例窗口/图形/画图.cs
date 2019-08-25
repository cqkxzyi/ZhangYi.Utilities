using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 画图 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;

        private Rectangle rectClient;
        private IContainer components;
		private bool drawEllipse;

        public 画图()
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
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
            this.menuItem2});
            this.menuItem1.Text = "图形(&G)";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.OwnerDraw = true;
            this.menuItem2.Text = "椭圆形";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            this.menuItem2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.menuItem2_DrawItem);
            this.menuItem2.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.menuItem2_MeasureItem);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 209);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "圆形菜单";
            this.ResumeLayout(false);

		}
		#endregion


		private void menuItem2_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			//设置菜单项的宽度
			e.ItemWidth=150;
			//设置菜单项的高度
			e.ItemHeight=60;
		}

		private void menuItem2_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			Graphics g=e.Graphics;
			//判断菜单项的状态
			if ((e.State & DrawItemState.Selected)==DrawItemState.Selected)
			{
				//绘制背景填充矩形
				g.FillRectangle(new SolidBrush(Color.Red),e.Bounds.Left,e.Bounds.Top,e.Bounds.Width,
					e.Bounds.Height);
				//绘制前景椭圆
				g.DrawEllipse(new Pen(Color.Yellow,2.0f),e.Bounds.Left+5,e.Bounds.Top+3,
					e.Bounds.Width-10,e.Bounds.Height-6);
			}
			else 
			{
			   //变换颜色绘制前景和背景
				g.FillRectangle(new SolidBrush(Color.Yellow),e.Bounds.Left,e.Bounds.Top,e.Bounds.Width,
					e.Bounds.Height);
				g.DrawEllipse(new Pen( Color.Red,2.0f),e.Bounds.Left+5,e.Bounds.Top+3,
					e.Bounds.Width-10,e.Bounds.Height-6);

			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
		//通知OnPaint可以做图了
		this.drawEllipse=true;
	    //将窗体的客户工作区从菜单上转移到窗体上
	    this.Enabled=false;
        this.Enabled=true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{ 
		     //判断是否需要绘制图形
			 if(this.drawEllipse==true)
				{
					Graphics g=e.Graphics;
				    //找到窗体当前的工作区
				    Rectangle rect=this.ClientRectangle;
				    //绘制背景矩形
				    g.FillRectangle(new SolidBrush(Color.Red),rect);
				    //绘制前景椭圆
					g.DrawEllipse(new Pen(Color.Yellow,2.0f),rect.X+5,rect.Y+3,
						rect.Width-10,rect.Height-6);
				}
		}

		
	}

