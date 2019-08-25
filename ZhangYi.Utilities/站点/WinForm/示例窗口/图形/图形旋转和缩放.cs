using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
namespace ch2_4
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ͼ����ת������ : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;

		private System.Drawing.Brush backgroundBrush;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		
		//������ת�ĽǶ�
		private  float  rotate=0;
		private System.Windows.Forms.MenuItem menuItem9;
		//����Ŵ�ı���
		private  float  size=1;
		
		public ͼ����ת������()
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
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem6,
																						 this.menuItem7,
																						 this.menuItem8,
																						 this.menuItem9,
																						 this.menuItem5});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "��ת45��";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "��ת90��";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "��ת120��";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "��ת180��";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 8;
			this.menuItem5.Text = "��ԭ";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "-";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "�Ŵ�120%";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "��С80%";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 7;
			this.menuItem9.Text = "-";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(480, 357);
			this.ContextMenu = this.contextMenu1;
			this.Name = "Form1";
			this.Text = "ͼ�ε���ת������";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e) 
		{
			Graphics g = e.Graphics;

			g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.White)), ClientRectangle);
			
			//��ȫ�ֱ任��������Ϊ��λ���󣬼�����ת
			g.ResetTransform();
			//������ת�Ƕ�
			g.RotateTransform(this.rotate);
			//����һ�����ű仯���任˳��Ϊ����
			g.ScaleTransform(this.size,this.size,MatrixOrder.Append);
			//����һ��ƽ�Ʊ任���任˳��Ϊ���
			g.TranslateTransform(200,130,MatrixOrder.Append);
			//����ͼ��
			g.FillRectangle(new SolidBrush(Color.SlateBlue), 0, 0, 100, 125);


		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.rotate=45;
			this.Refresh();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.rotate=90;
			this.Refresh();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.rotate=120;
			this.Refresh();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.rotate=180;
			this.Refresh();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			this.rotate=0;
			this.size=1;
			this.Refresh();
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
		   this.size=1.2f;
		   this.Refresh();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
		   this.size=0.8f;
			this.Refresh();
		}

		
	}
}
