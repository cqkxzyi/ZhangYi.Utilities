using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D; 

namespace ch2_5
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ���ʺ����� : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ���ʺ�����()
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
			this.ClientSize = new System.Drawing.Size(520, 357);
			this.Name = "Form1";
			this.Text = "��ʾ���ʺ�����";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e) 
		{
			Graphics g = e.Graphics;
            //ͼ��Ч��Ϊ�������
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			//���ñ�����ɫ
            g.FillRectangle(new SolidBrush(Color.Aqua), ClientRectangle);
            
			Pen pen= new Pen(Color.Blue,2f);
			//���ɵ㻮�߱�
			pen.DashStyle=DashStyle.DashDot;
			g.DrawLine(pen,20,15,480,15);

			//����һ֧�ֻ��� 
			Pen pen1 = new Pen(Color.Blue, 20);
			//�������ɵ��߱�
			pen1.DashStyle = DashStyle.Dot;
			g.DrawLine(pen1,20,45,480,45);
            
		
			Pen pen2 = new Pen(Color.Blue, 20);
			//�����������߱�
			pen2.DashStyle = DashStyle.Dash;
            //ʹ��ʼ��ΪԲ��
			pen2.StartCap = LineCap.Round;
            //ʹĩ�˳�Ϊ��ͷ
			pen2.EndCap = LineCap.ArrowAnchor;
			g.DrawLine(pen2,20,90,480,90);
			
			
			Pen pen3= new Pen(Color.Blue, 2);
            Point[] points= new Point[] {
													 new Point(20, 135),
													 new Point(40, 145),
													 new Point(30, 180),
													 new Point(160, 140),
											         new Point(300,120),
													 new Point(470, 140),
			};
			//����һ�����ߣ���������ǿ��
			g.DrawCurve(pen3,points,1.2f);
			//��ע�����
			foreach (Point p in points)
			{
				g.DrawEllipse(pen3,p.X-5,p.Y-5,10,10);
			}
			
			Pen pen4= new Pen(Color.Blue, 2);
			Point[] points1=new Point[] {
											  new Point(40, 220),
											  new Point(70, 200),
											  new Point(120, 190),
											  new Point(160, 250),
											  new Point(300,220),
											  new Point(470, 190),
			};
			//���Ʒ������
			g.DrawClosedCurve(pen4, points1);
			//��ע�����
			foreach (Point p in points1)
			{
				g.DrawEllipse(pen4,p.X-5,p.Y-5,10,10);
			}
            
       
			Pen pen5= new Pen(Color.Blue, 1);
			g.DrawString("����������",this.Font,new SolidBrush(Color.Blue),20,260);
			Point[] points2=new Point[]{
								   new Point(100,260),
								   new Point(150,340),
				                   new Point(320,270),
				                   new Point(480,260)
								   };
			//���Ʊ���������
            g.DrawBezier(pen5,points2[0],points2[1],points2[2],points2[3]);

			//��ʾ���������ߵĿ��Ƶ�
			foreach (Point p in points2)
			{
			g.DrawEllipse(pen5,p.X-5,p.Y-5,10,10);
			}

		}
	}
}
