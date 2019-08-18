using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;

namespace ch3_6
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class 综合打印实例 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;

		private StreamReader sReader;
		private System.Drawing.Printing.PrintDocument pDoc;
		private System.Windows.Forms.MenuItem menuItem5;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public 综合打印实例()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

            sReader = new StreamReader(@"I:\360data\重要数据\桌面\导航网站排名表.txt", System.Text.Encoding.Default);
			this.richTextBox1.Text=sReader.ReadToEnd();
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
			this.pDoc = new System.Drawing.Printing.PrintDocument();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// pDoc
			// 
			this.pDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDoc_PrintPage);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(292, 273);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
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
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5});
			this.menuItem1.Text = "文件(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "打印(&P)...";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "打印预览(&V)...";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "页面设置(&U)...";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "打印设置(&S)...";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.richTextBox1});
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "综合打印实例";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			
			
			//构造打印对话框
			PrintDialog printDialog = new PrintDialog();
			//设置Document属性
			printDialog.Document = this.pDoc;
			if (printDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{   
					pDoc.Print();
				}
				catch(Exception excep)
				{
					//显示打印出错消息
					MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
	      
			//构造打印预览对话框 
		    PrintPreviewDialog printPreviewDialog1=new PrintPreviewDialog();
            //设置Document属性
            printPreviewDialog1.Document=this.pDoc;

			//显示打印预览窗口
			try
			{
				printPreviewDialog1.ShowDialog();
			}
			catch(Exception excep)
			{
				//显示打印出错消息
				MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			//构造页面设置对话框
			PageSetupDialog  	pageSetupDialog1=new PageSetupDialog();
			//设置Document属性
			pageSetupDialog1.Document=this.pDoc;
            //显示对话框
			pageSetupDialog1.ShowDialog();
		}

		private void pDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//获得绘制对象
			Graphics g=e.Graphics;
			//一页中的行数
			float linePage = 0 ;
			//待绘文本的纵向坐标
			float yPosition =  0 ;
			//行计数
			int count = 0 ;
			//左边界
			float leftMargin = e.MarginBounds.Left;
			//顶边界
			float topMargin = e.MarginBounds.Top;
			//字符串流
			String line=null;

		
			//根据页面的高度和字体的高度计算
			//一页中可以打印的行数
			linePage = e.MarginBounds.Height  / this.Font.GetHeight(g) ;
            
			//每次从字符串流中读取一行并打印
			while (count < linePage && ((line=sReader.ReadLine()) != null)) 
			{
				//计算这一行的显示位置
				yPosition = topMargin + (count * this.Font.GetHeight(g));
				//绘制文本
				g.DrawString (line, this.Font, Brushes.Black, leftMargin, yPosition, new StringFormat());
				//行数增加
				count++;
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			//构造打印对话框
			PrintDialog printDialog = new PrintDialog();
			//设置Document属性
			printDialog.Document = this.pDoc;
			//显示对话框
			printDialog.ShowDialog();
		}
	}
}
