using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
namespace ch2_6
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ͼ����ʾ�뱣�� : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button1;

		private Image bm;
		public ͼ����ʾ�뱣��()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			string s="football.jpg";
            bm= new Bitmap(s);
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(400, 344);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 48);
			this.button1.TabIndex = 0;
			this.button1.Text = "����ͼ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(544, 513);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1});
			this.Name = "Form1";
			this.Text = "ͼ����ʾ�뱣��";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
	    protected override void OnPaint(PaintEventArgs e) 
		{

		Graphics g=e.Graphics;

		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

		g.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

			
		//����ͼƬ
	    g.DrawImage(bm,0,0,bm.Width,bm.Height);

	    //����һ��ֻ��ԭͼ1/3��С��ͼƬ
		g.DrawImage(bm,0,340,(int)(bm.Width/3),(int)(bm.Height/3));


		//������ת��ͼ��
		g.RotateTransform(-30);  
		//����ƽ�ƾ���
		g.TranslateTransform(210,420,MatrixOrder.Append);
		//��ʾͼ��
		g.DrawImage(bm,0, 0,(int)(bm.Width/3),(int)(bm.Height/3));
	    g.ResetTransform();

		
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
		
			//����һ����Ҫ����ͼƬ�ߴ���ͬ��ͼ��
			Bitmap image = new Bitmap(this.bm.Width, this.bm.Height);
			
			//��ȡGraphics����
			Graphics g = Graphics.FromImage(image);
			g.Clear(this.BackColor);

			//����Form1_Paint������ͼ���ϻ���ͼ��		
		    Rectangle rect=new Rectangle(0,0,this.bm.Width,this.bm.Height);
			OnPaint(new PaintEventArgs(g,rect));
			
			//����ͼ��
			image.Save("SavedFootball.jpg", ImageFormat.Jpeg);
			MessageBox.Show("ͼƬ�Ѿ����浽��ǰ��Ŀ��bin\\DebugĿ¼��,��ΪSavedFootball.jpg");
	
		}
	}
}
