using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 画图下拉框 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxStyle;
		private System.Windows.Forms.ComboBox comboBoxDraw;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		
		private System.ComponentModel.Container components = null;
        public 画图下拉框()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			//初始化3个comboBox
			InitControl();
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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxDraw = new System.Windows.Forms.ComboBox();
			this.comboBoxStyle = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.AllowDrop = true;
			this.comboBox1.Enabled = false;
			this.comboBox1.Location = new System.Drawing.Point(16, 72);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			this.comboBox1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox1_MeasureItem);
			this.comboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(16, 176);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(120, 24);
			this.panel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.comboBoxDraw,
																					this.comboBoxStyle,
																					this.label2,
																					this.label1,
																					this.checkBox2,
																					this.checkBox1});
			this.groupBox1.Location = new System.Drawing.Point(160, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 200);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "组合框设置";
			// 
			// comboBoxDraw
			// 
			this.comboBoxDraw.Location = new System.Drawing.Point(120, 128);
			this.comboBoxDraw.Name = "comboBoxDraw";
			this.comboBoxDraw.Size = new System.Drawing.Size(121, 20);
			this.comboBoxDraw.TabIndex = 5;
			this.comboBoxDraw.SelectedIndexChanged += new System.EventHandler(this.comboBoxDraw_SelectedIndexChanged);
			// 
			// comboBoxStyle
			// 
			this.comboBoxStyle.Location = new System.Drawing.Point(120, 96);
			this.comboBoxStyle.Name = "comboBoxStyle";
			this.comboBoxStyle.Size = new System.Drawing.Size(121, 20);
			this.comboBoxStyle.TabIndex = 4;
			this.comboBoxStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxStyle_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "绘制模式";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "样式";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(32, 56);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "排序";
			this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(32, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "启用";
			this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "从组合框中选取颜色";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 144);
			this.label4.Name = "label4";
			this.label4.TabIndex = 4;
			this.label4.Text = "显示颜色";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 261);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label4,
																		  this.label3,
																		  this.groupBox1,
																		  this.panel1,
																		  this.comboBox1});
			this.Name = "Form1";
			this.Text = "组合框";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		///    为程序设置控制两个组合框的属性的默认状态。
		/// </summary>
		private void InitControl() 
		{
		//初始化样式组合框
		comboBoxStyle.Items.Add(ComboBoxStyle.Simple.ToString());
        comboBoxStyle.Items.Add(ComboBoxStyle.DropDown.ToString());
        comboBoxStyle.Items.Add(ComboBoxStyle.DropDownList.ToString());
		//初始化绘图模式组合框
		comboBoxDraw.Items.Add(DrawMode.Normal.ToString());
	    comboBoxDraw.Items.Add(DrawMode.OwnerDrawFixed.ToString());
		comboBoxDraw.Items.Add(DrawMode.OwnerDrawVariable.ToString());
		//初始化ComboBox1
		//获取或设置一个字符串，该字符串指定要显示其内容的数据源的属性
        comboBox1.DisplayMember="Color";
		comboBox1.Items.Add(Brushes.Cyan);
		comboBox1.Items.Add(Brushes.DarkSalmon);
		comboBox1.Items.Add(Brushes.Gray);
		comboBox1.Items.Add(Brushes.Green);
		comboBox1.Items.Add(Brushes.AliceBlue);
		comboBox1.Items.Add(Brushes.Black);
		comboBox1.Items.Add(Brushes.Blue);
	    comboBox1.Items.Add(Brushes.Chocolate);
		comboBox1.Items.Add(Brushes.Pink);
		comboBox1.Items.Add(Brushes.Red);
		comboBox1.Items.Add(Brushes.LightBlue);
		comboBox1.Items.Add(Brushes.Brown);
		comboBox1.Items.Add(Brushes.DodgerBlue);
		comboBox1.Items.Add(Brushes.MediumPurple);
		comboBox1.Items.Add(Brushes.White);
		comboBox1.Items.Add(Brushes.Yellow);
	 
		}



		private void comboBox1_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
		//设置绘制图形的高度比comboBox1项目的高度略小
		e.ItemHeight = comboBox1.ItemHeight - 2;
		}
        

		private void comboBox1_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
		    ComboBox cmb = (ComboBox) sender;
			if (e.Index == -1)
				return;
			if (sender == null)
				return;
			SolidBrush selectedBrush = (SolidBrush)cmb.Items[e.Index];
			Graphics g = e.Graphics;

			// 如果选择了项，则绘制正确的背景颜色和聚焦框
			e.DrawBackground();
			e.DrawFocusRectangle();

			// 绘制颜色的预览框
			Rectangle rectPreview = e.Bounds;
            rectPreview.Offset(3,3);
			rectPreview.Width = 20;
			rectPreview.Height -= 4;
			g.DrawRectangle(new Pen(e.ForeColor), rectPreview);

			// 获取选定颜色的相应 Brush 对象，并填充预览框
			rectPreview.Offset(1,1);
			rectPreview.Width -= 2;
			rectPreview.Height -= 2;
			g.FillRectangle(selectedBrush, rectPreview);

			// 绘制选定颜色的名称
			g.DrawString(selectedBrush.Color.ToString(), Font, new SolidBrush(e.ForeColor), e.Bounds.X+30,e.Bounds.Y+1);
    
		}

		private void comboBoxDraw_SelectedIndexChanged(object sender, System.EventArgs e)
		{  
		    //设定comboBox1的DrawMode属性
			String i=comboBoxDraw.SelectedItem.ToString();
			switch (i)
			{   
				case "Normal":
					comboBox1.DrawMode=DrawMode.Normal;
					break;
				case "OwnerDrawFixed":
					comboBox1.DrawMode=DrawMode.OwnerDrawFixed;
					break;
				case "OwnerDrawVariable":
					comboBox1.DrawMode=DrawMode.OwnerDrawVariable;
					break;
			}
		}

		private void comboBoxStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		   //设定comboBox1的DropDownStyle属性
			String  i=comboBoxStyle.SelectedItem.ToString();
			switch (i)
			{   
				case "Simple":
					comboBox1.DropDownStyle=ComboBoxStyle.Simple;
					Size size=new Size(comboBox1.Size.Width,66);
					comboBox1.Size=size;
					break;
				case "DropDown":
					comboBox1.DropDownStyle=ComboBoxStyle.DropDown;
					break;
				case "DropDownList":
					comboBox1.DropDownStyle=ComboBoxStyle.DropDownList;
					break;
			}
		}

	

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//绘制panel1的背景颜色
			if (comboBox1.SelectedIndex >= 0)
			{
			SolidBrush brush = (SolidBrush)(comboBox1.SelectedItem);
	        panel1.BackColor=brush.Color;
			}
		}

		private void checkBox1_CheckStateChanged(object sender, System.EventArgs e)
		{
			//根据checkBox的Checked属性确定是否启用comboBox1
			comboBox1.Enabled=checkBox1.Checked;
		}

		private void checkBox2_CheckStateChanged(object sender, System.EventArgs e)
		{
		    //根据checkBox的Checked属性确定comboBox1中的选项是否排序
			comboBox1.Sorted=checkBox2.Checked;
		}

	
	}
