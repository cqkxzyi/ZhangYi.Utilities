using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

	/// <summary>
	/// Form2 ��ժҪ˵����
	/// </summary>
	public class �Ӵ�����ʾ��ʽ2 : Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
        
		//����Ҫ�򿪵��ļ���
		
		public �Ӵ�����ʾ��ʽ2()
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
				if(components != null)
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
			    //���ļ������ͼƬ��Ϣ
				Bitmap bm= new Bitmap(fileName);
                
				//ȥ��·������ļ���
				string sShortName=fileName.Substring(fileName.LastIndexOf("\\")+1);
                //�����ڵ����Ƹ�Ϊ�ļ���
				this.Text=sShortName;
				//����ͼƬ��С�ı䴰�ڴ�С
				this.Width=bm.Width+6;
				this.Height=bm.Height+25;

				//��ʾͼƬ
				g.DrawImage(bm,0,0,bm.Width,bm.Height);
			}
		}

		/// <summary>
		/// ����Ҫ�򿪵��ļ���
		/// </summary>
		public string fileName;
	}
