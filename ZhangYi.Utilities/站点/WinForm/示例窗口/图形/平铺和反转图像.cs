using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace ch2_7
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ƽ�̺ͷ�תͼ�� : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;

		private WrapMode wr=WrapMode.Tile;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ƽ�̺ͷ�תͼ��()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "ƽ��ͼ��";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "ˮƽ��ת";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "��ֱ��ת";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "ˮƽ��ֱͬʱ��ת";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(376, 237);
			this.ContextMenu = this.contextMenu1;
			this.Name = "Form1";
			this.Text = "ƽ�̺ͷ�תͼ��";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g=e.Graphics;

            Image image = new Bitmap("I:\\360data\\��Ҫ����\\����\\δ����.gif");
			TextureBrush tBrush = new TextureBrush(image);
           
	    	//��������ͼƬ�ķ�ʽ
			tBrush.WrapMode = wr;
			
			g.FillRectangle(tBrush, this.ClientRectangle);
     	}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.Tile;
		this.Refresh();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.TileFlipX;
		this.Refresh();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.TileFlipY;
		this.Refresh();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
		this.wr=WrapMode.TileFlipXY;
		this.Refresh();
		}
	}
}
