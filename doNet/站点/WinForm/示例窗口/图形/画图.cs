using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��ͼ : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;

        private Rectangle rectClient;
        private IContainer components;
		private bool drawEllipse;

        public ��ͼ()
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "ͼ��(&G)";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.OwnerDraw = true;
            this.menuItem2.Text = "��Բ��";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            this.menuItem2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.menuItem2_DrawItem);
            this.menuItem2.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.menuItem2_MeasureItem);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 209);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Բ�β˵�";
            this.ResumeLayout(false);

		}
		#endregion


		private void menuItem2_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			//���ò˵���Ŀ��
			e.ItemWidth=150;
			//���ò˵���ĸ߶�
			e.ItemHeight=60;
		}

		private void menuItem2_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			Graphics g=e.Graphics;
			//�жϲ˵����״̬
			if ((e.State & DrawItemState.Selected)==DrawItemState.Selected)
			{
				//���Ʊ���������
				g.FillRectangle(new SolidBrush(Color.Red),e.Bounds.Left,e.Bounds.Top,e.Bounds.Width,
					e.Bounds.Height);
				//����ǰ����Բ
				g.DrawEllipse(new Pen(Color.Yellow,2.0f),e.Bounds.Left+5,e.Bounds.Top+3,
					e.Bounds.Width-10,e.Bounds.Height-6);
			}
			else 
			{
			   //�任��ɫ����ǰ���ͱ���
				g.FillRectangle(new SolidBrush(Color.Yellow),e.Bounds.Left,e.Bounds.Top,e.Bounds.Width,
					e.Bounds.Height);
				g.DrawEllipse(new Pen( Color.Red,2.0f),e.Bounds.Left+5,e.Bounds.Top+3,
					e.Bounds.Width-10,e.Bounds.Height-6);

			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
		//֪ͨOnPaint������ͼ��
		this.drawEllipse=true;
	    //������Ŀͻ��������Ӳ˵���ת�Ƶ�������
	    this.Enabled=false;
        this.Enabled=true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{ 
		     //�ж��Ƿ���Ҫ����ͼ��
			 if(this.drawEllipse==true)
				{
					Graphics g=e.Graphics;
				    //�ҵ����嵱ǰ�Ĺ�����
				    Rectangle rect=this.ClientRectangle;
				    //���Ʊ�������
				    g.FillRectangle(new SolidBrush(Color.Red),rect);
				    //����ǰ����Բ
					g.DrawEllipse(new Pen(Color.Yellow,2.0f),rect.X+5,rect.Y+3,
						rect.Width-10,rect.Height-6);
				}
		}

		
	}

