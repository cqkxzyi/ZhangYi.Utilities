using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
namespace ch2_10
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ͼƬ��ɫ���� : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ͼƬ��ɫ����()
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
			this.ClientSize = new System.Drawing.Size(368, 273);
			this.Name = "Form1";
			this.Text = "ͼ����ɫ�任";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g=e.Graphics;
	       
			//����ͼƬ
            Image image = new Bitmap(@"I:\360data\��Ҫ����\����\δ����.gif");
			//����һ��ImageAttributes��ʵ��
			ImageAttributes imageAttributes = new ImageAttributes();
			
            float[][] colorMatrixElements = {  
												//����ɫ���ӳ���2
												new float[] {2,  0,  0,  0, 0},       
												//��ɫ���Ӳ���
												new float[] {0,  1,  0,  0, 0},        
												//��ɫ���ӳ���1.5
												new float[] {0,  0,  1,  0, 0},        
												new float[] {0,  0,  0,  1, 0},      
												//ÿ�����������0.2
												new float[] {0.2f, 0.2f, 0.2f, 0, 1}
											};    
			ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

			//ʹ��ImageAttributes���SetColorMatrix����ΪcolorMatrix��������ColorMatrixFlagö��
			imageAttributes.SetColorMatrix(
				colorMatrix, 
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

			//��ʾԭ����ͼƬ
			g.DrawImage(image, 10, 10);
			g.DrawString("ԭ����ͼƬ",this.Font,new SolidBrush(Color.Blue),10,100);

			//��ʾ�任���ͼƬ
			g.DrawImage(
				image, 
				//��ͼ��
				new Rectangle(120, 10, image.Width, image.Height),  
				//Դͼ�����Ͻ�
				0, 0,       
				//Դͼ�Ŀ��
				image.Width,
				//Դͼ�ĸ߶�
				image.Height,    
				GraphicsUnit.Pixel,
				imageAttributes);
			g.DrawString("��ɫ�任���ͼƬ",this.Font,new SolidBrush(Color.Blue),120,100);
		    
			Image image1 = new Bitmap(@"I:\360data\��Ҫ����\����\δ����.gif");
			ImageAttributes imageAttributes1 = new ImageAttributes();
			
            float[][] colorMatrixElements1 = { 
												new float[] {1,  0,  0,  0, 0},
												new float[] {0,  1,  0,  0, 0},
												new float[] {0,  0,  1,  0, 0},
												new float[] {0,  0,  0,  1, 0},
												//�������Ӳ��䣬������ɫ�������0.80
												new float[] {0.80f, 0, 0, 0, 1}};

			ColorMatrix colorMatrix1 = new ColorMatrix(colorMatrixElements1);

			//ʹ��ImageAttributes���SetColorMatrix����ΪcolorMatrix��������ColorMatrixFlagö��
			imageAttributes1.SetColorMatrix(
				colorMatrix1, 
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

            //��ʾԭ����ͼƬ
			g.DrawImage(image1, 10, 140, image1.Width, image1.Height);
			g.DrawString("ԭ����ͼƬ",this.Font,new SolidBrush(Color.Blue),10,230);

			//��ʾ�任���ͼƬ
			g.DrawImage(
				image1,
				//��ͼ��
				new Rectangle(150, 140, image1.Width, image1.Height), 
				//Դͼ�����Ͻ�
				0, 0,        
				//Դͼ�Ŀ��
				image1.Width,       
				//Դͼ�Ŀ��
				image1.Height,     
				GraphicsUnit.Pixel,
				imageAttributes1);
		   g.DrawString("��ɫƽ�ƺ��ͼƬ",this.Font,new SolidBrush(Color.Blue),150,230);
		}
	}
}
