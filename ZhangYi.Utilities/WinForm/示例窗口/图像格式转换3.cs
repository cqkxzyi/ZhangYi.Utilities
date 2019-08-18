using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ch2_9
{
	/// <summary>
	/// Form3 的摘要说明。
	/// </summary>
	public class 图像格式转换3 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		
		//这行代码修改过
		public  System.Windows.Forms.ListView listView1;
		//这行代码修改过
		public  System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 图像格式转换3()
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
				if(components != null)
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "BMP   Windows Bitmap", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))))}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "JPG   JPEG ", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))))}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "TIFF  Tag Image File Format", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))))}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "PNG   Portable Network Graphics", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))))}, -1);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "GIF   CompuServer GIF", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))))}, -1);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.listView1});
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 136);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "目标格式";
			// 
			// listView1
			// 
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.HoverSelection = true;
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2,
																					  listViewItem3,
																					  listViewItem4,
																					  listViewItem5});
			this.listView1.Location = new System.Drawing.Point(48, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(248, 97);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.List;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button3,
																					this.textBox1,
																					this.label1});
			this.groupBox2.Location = new System.Drawing.Point(16, 168);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(320, 112);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "文件输出";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(216, 80);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "浏览";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(120, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(160, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "输出文件路径";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(200, 304);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "确定";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(280, 304);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "取消";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.CreatePrompt = true;
			this.saveFileDialog1.FileName = "doc1";
			// 
			// Form3
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(360, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button2,
																		  this.button1,
																		  this.groupBox2,
																		  this.groupBox1});
			this.Name = "Form3";
			this.Text = "文件格式转换";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			//检查文件名是否错误
			if (this.textBox1.Text=="")
			{
				MessageBox.Show("文件名填写不正确","警告",
					MessageBoxButtons.OK,MessageBoxIcon.Error);
			
			}
			//检查是否已经选定了文件格式
			else if(this.listView1.SelectedItems.Count==0)
			{
				MessageBox.Show("请选择保存类型","警告",
					MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			else
			{
				//返回DialogResult枚举，关闭窗体
				this.DialogResult=DialogResult.OK;
				this.Close();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//返回DialogResult枚举，关闭窗体
			this.DialogResult=DialogResult.Cancel;
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			//设置过滤器
			this.saveFileDialog1.Filter="Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png" 
					+ "|JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg" 
					+ "|GIF files (*.gif)|*.gif" 
					+ "|BMP files (*.bmp)|*.bmp" 
					+ "|Tiff files (*.tif;*.tiff)|*.tif;*.tiff" 
					+ "|Png files (*.png)|*.png" 
					+ "|All files (*.*)|*.*";

			if (this.saveFileDialog1.ShowDialog()==DialogResult.OK)
			{
			//将选定的路径写到textBox中去
			string fileName=this.saveFileDialog1.FileName;
			this.textBox1.Text=fileName;
			}
		}
	}
}
