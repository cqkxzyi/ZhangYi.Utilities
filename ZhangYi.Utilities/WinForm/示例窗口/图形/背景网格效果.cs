using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
namespace ch2_3
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��������Ч�� : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ��������Ч��()
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(472, 263);
			this.Name = "Form1";
			this.Text = "HatchBrush��ʾ";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e) 
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillRectangle(new SolidBrush(Color.FromArgb(180,255,255,255)), this.ClientRectangle);

			//���Ʋ�ͬ��Ӱ��ʽ��Բ��
			HatchBrush hb = new HatchBrush(HatchStyle.ForwardDiagonal, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb, 0, 0, 100, 100);

			//���������Ӱ��ʽ���ַ���
       	    g.DrawString("ForwardDiagonal",this.Font,new SolidBrush(Color.Blue),0,100);

			HatchBrush hb1 = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb1, 110, 0, 100, 100);
			g.DrawString("BackwardDiagonal",this.Font,new SolidBrush(Color.Blue),110,100);
	
			HatchBrush hb2 = new HatchBrush(HatchStyle.Cross, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb2, 220, 0, 100, 100);
			g.DrawString("cross",this.Font,new SolidBrush(Color.Blue),250,100);
		
			HatchBrush hb3 = new HatchBrush(HatchStyle.DiagonalCross, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb3, 330, 0, 100, 100);
            g.DrawString("DiagonalCross",this.Font,new SolidBrush(Color.Blue),330,100);

			HatchBrush hb4 = new HatchBrush(HatchStyle.HorizontalBrick, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb4, 0, 120, 100, 100);
			g.DrawString("HorizontalBrick",this.Font,new SolidBrush(Color.Blue),0,220);

			HatchBrush hb5 = new HatchBrush(HatchStyle.Vertical, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb5, 110, 120, 100, 100);
			g.DrawString("Verical",this.Font,new SolidBrush(Color.Blue),140,220);

			HatchBrush hb6 = new HatchBrush(HatchStyle.DottedDiamond, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb6, 220, 120, 100, 100);
			g.DrawString("DottedDiamond",this.Font,new SolidBrush(Color.Blue),220,220);

			HatchBrush hb7 = new HatchBrush(HatchStyle.Plaid, Color.Green, Color.FromArgb(100, Color.Aqua));
			g.FillEllipse(hb7, 330, 120, 100, 100);
			g.DrawString("Plaid",this.Font,new SolidBrush(Color.Blue),360,220);
		}
	}
}
