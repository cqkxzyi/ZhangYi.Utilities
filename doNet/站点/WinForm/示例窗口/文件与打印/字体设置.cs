using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//添加的引用
using System.Drawing.Text;

namespace ch3_1
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 字体设置 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox3;

		//代表本机装的字体集合
		private FontFamily[] fontFamilies;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.GroupBox groupBox3;
//        代表当前使用的Font
//		private Font font;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 字体设置()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			//初始化3个comboBox
            InitialComboBox();
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		private void InitialComboBox()
		{
			InstalledFontCollection fontCollection = new InstalledFontCollection();

			// 得到FontFamily对象矩阵
            fontFamilies = fontCollection.Families;
 
			int count = fontFamilies.Length;
			//将所有字体都加到comboBox中
			for(int j = 0; j < count; ++j)
			{
				string familyName = fontFamilies[j].Name;  
				this.comboBox1.Items.Add(familyName);
			}
            comboBox1.Text=this.label1.Font.FontFamily.Name;

			float[] size=new float[]{5,5.5f,6.5f,7.5f,8,9,10,10.5f,11,12,14,16,18,20,22,24,26,28,36};
			//将表示字体的浮点数加到comboBox3中去
			foreach (float f in size)
			{
				comboBox3.Items.Add(f);
			}
			comboBox3.Text=this.label1.Font.Size.ToString();
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.comboBox3,
																					this.comboBox2,
																					this.comboBox1,
																					this.label4,
																					this.label3,
																					this.label2,
																					this.groupBox3});
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 216);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "字体设置";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label1});
			this.groupBox2.Location = new System.Drawing.Point(16, 248);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 104);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "预览";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new System.Drawing.Point(32, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(296, 56);
			this.label1.TabIndex = 0;
			this.label1.Text = "Visual C#实例入门与提高";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "字体";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(152, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "字形";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(272, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "字号";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.comboBox1.Location = new System.Drawing.Point(16, 56);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(112, 80);
			this.comboBox1.TabIndex = 3;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.comboBox2.Items.AddRange(new object[] {
														   "常规",
														   "倾斜",
														   "加粗"});
			this.comboBox2.Location = new System.Drawing.Point(152, 56);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(104, 80);
			this.comboBox2.TabIndex = 4;
			this.comboBox2.Text = "常规";
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.comboBox3.Location = new System.Drawing.Point(272, 56);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(64, 80);
			this.comboBox3.TabIndex = 5;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(200, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "字体颜色选择";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(16, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 24);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "下划线";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(104, 24);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(64, 24);
			this.checkBox2.TabIndex = 8;
			this.checkBox2.Text = "删除线";
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBox1,
																					this.checkBox2,
																					this.button1});
			this.groupBox3.Location = new System.Drawing.Point(16, 136);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(320, 64);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "其他";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(384, 365);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.groupBox1});
			this.Name = "Form1";
			this.Text = "字体设置";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//获得当前选择的字体索引
			int i=this.comboBox1.SelectedIndex;
			//获取要选择的字体号
			FontFamily ff=this.fontFamilies[i];
			//新建一个字体对象，不改变其它的字体设置
		    Font font=new Font(ff,this.label1.Font.Size,this.label1.Font.Style);
			this.label1.Font=font;
		

		}

		private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//获得当前的索引
			int i =this.comboBox2.SelectedIndex;
	        Font fontNew;
		   //新建一个字体对象，但不改变其它的字体设置
			switch (i)
			{
				case 0:
		            fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Regular);
				    this.label1.Font=fontNew;
			    	break;
				case 1:
					fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Italic);
					this.label1.Font=fontNew;
					break;
				case 2:
					fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Bold);
					this.label1.Font=fontNew;
					break;
			
			}
		}

		private void comboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		    //获取选择的size大小
		    float size=(float)this.comboBox3.SelectedItem;
			//新建一个字体对象，但不改变其它的字体设置
		    Font fontnew=new Font(this.label1.Font.FontFamily,size,this.label1.Font.Style);
			this.label1.Font=fontnew;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (this.colorDialog1.ShowDialog()==DialogResult.OK)
			{
				//将字体的颜色设置为colorDialog的选择颜色
				this.label1.ForeColor=this.colorDialog1.Color;
			}
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.checkBox1.Checked==true)
			{
				//设置下划线
				Font fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Underline);
				this.label1.Font=fontNew;		  
			}
			else 
			{
				Font fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Regular);
				this.label1.Font=fontNew;	
			}
		}

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.checkBox2.Checked==true)
			{
			//设置删除线
			Font fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Strikeout);
			this.label1.Font=fontNew;		
			}
			else 
			{
				Font fontNew=new Font(this.label1.Font.FontFamily,this.label1.Font.Size,FontStyle.Regular);
				this.label1.Font=fontNew;	
			}
		}

	}
}
