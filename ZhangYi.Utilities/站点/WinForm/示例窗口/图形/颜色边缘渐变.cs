using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
  
namespace ch2_2
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��ɫ��Ե���� : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ��ɫ��Ե����()
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
			this.ClientSize = new System.Drawing.Size(492, 323);
			this.Name = "Form1";
			this.Text = "ͼ��ɫ�ʴ����ĵ���Ե�ı仯";

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		protected override void OnPaint(PaintEventArgs e) 
		{
			//���Graphics����
			Graphics g = e.Graphics;

            //ͼ���������Ϊ�������
			g.SmoothingMode = SmoothingMode.AntiAlias;
            //ʹ��SolidBrush������ͿΪ��ɫ
		    g.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

			//����һ������������Բ·����path����
			GraphicsPath path = new GraphicsPath();
		    path.AddEllipse(250,100,200,100);
			//path.AddEllipse(100, 100, 280, 140);

			// ʹ�����Path���󴴽�һ������·������.
			PathGradientBrush pBrush = new PathGradientBrush(path);

			// ��path�����������ɫ��Ϊ��ɫ
			pBrush.CenterColor = Color.Blue;

			// ��������path����߽��ϵ���ɫ��ΪAqua
			Color[] colors = {Color.Aqua};
			pBrush.SurroundColors = colors;

			//������Բ
            g.FillEllipse(pBrush, 250,100,200,100);
 
		    GraphicsPath path1 = new GraphicsPath();
		   //��������ǵ�ÿ�����������
			Point[] points = {
								 new Point(125, 75),
								 new Point(150, 125),
								 new Point(200, 125),
								 new Point(162, 150),
								 new Point(200, 225),
								 new Point(125, 175),
								 new Point(50, 225),
								 new Point(87, 150),
								 new Point(50, 125),
								 new Point(100, 125)};

			path1.AddLines(points);
			PathGradientBrush pBrush1 = new PathGradientBrush(path1);

			//����ColorBlend�������ڶ�ɫ����
			ColorBlend colorBlend = new ColorBlend();

			//����������ɫ�Ķ�ɫ����
			colorBlend.Colors = new Color[]
			{
				Color.Blue, Color.Aqua, Color.Green
			};

			//����ÿһ����ɫ��λ��
			colorBlend.Positions = new float[]
			{
				0.0f, 0.4f, 1.0f
			};
	
			//��colorBlend���󸳸�ˢ�ӵ�InterpolationColor����
			pBrush1.InterpolationColors = colorBlend;
			
			//���������
            g.FillRectangle(pBrush1, new Rectangle(0, 0, 200, 225));

  
		}
	}
}
