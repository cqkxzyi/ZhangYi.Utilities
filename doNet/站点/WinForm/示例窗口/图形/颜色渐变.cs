using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace ch2_1
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��ɫ���� : System.Windows.Forms.Form
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
        
	  
        private LinearGradientMode lgMode=LinearGradientMode.Horizontal;

		public ��ɫ����()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(��ɫ����));
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
			this.menuItem1.Text = "��ɫ�仯����";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "���ϵ���";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "������";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "�����ϵ�����";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "�����ϵ�����";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(492, 273);
			this.ContextMenu = this.contextMenu1;
			this.Name = "Form1";
			this.Text = "ɫ�ʽ����ͼ��";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e) 
		{
		   //��ȡ�¼���Graphics����
			Graphics g = e.Graphics;
	       //���ڴ�����Ҫ���Ƶĳ���������
			Rectangle r = new Rectangle(75, 50, 350, 170);
		   //����һ������ˢ
			LinearGradientBrush lb = new LinearGradientBrush(r, Color.White,Color.Blue, lgMode);
		   //�ý���ˢ��䳤��������
			g.FillRectangle(lb, r);
        }


		private void menuItem2_Click(object sender, System.EventArgs e)
		{
	    //ָ�����ϵ��µĽ���
		this.lgMode=LinearGradientMode.Vertical;
		this.Refresh();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
		//ָ�������ҵĽ��䡣 
		this.lgMode=LinearGradientMode.Horizontal;
		this.Refresh();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
		//ָ�������ϵ����µĽ��䡣 
		this.lgMode=LinearGradientMode.BackwardDiagonal;
		this.Refresh();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
		//�����ϵ����µĽ��䡣 
		this.lgMode=LinearGradientMode.ForwardDiagonal;
		this.Refresh();
		}

	}
}
