using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

	/// <summary>
	/// Form2 的摘要说明。
	/// </summary>
	public class 子窗口显示样式2 : Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
        
		//代表将要打开的文件名
		
		public 子窗口显示样式2()
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
			this.components = new System.ComponentModel.Container();
			this.Size = new System.Drawing.Size(300,300);
			this.Text = "Form2";
		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
		    Graphics g=e.Graphics;			
			if (fileName!=null)
			{
			    //从文件名获得图片信息
				Bitmap bm= new Bitmap(fileName);
                
				//去掉路径获得文件名
				string sShortName=fileName.Substring(fileName.LastIndexOf("\\")+1);
                //将窗口的名称改为文件名
				this.Text=sShortName;
				//根据图片大小改变窗口大小
				this.Width=bm.Width+6;
				this.Height=bm.Height+25;

				//显示图片
				g.DrawImage(bm,0,0,bm.Width,bm.Height);
			}
		}

		/// <summary>
		/// 代表将要打开的文件名
		/// </summary>
		public string fileName;
	}
