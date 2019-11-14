using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;

namespace ch3_5
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��ӡ : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Drawing.Printing.PrintDocument pDoc;
		
        //��ʾ����ӡ��
		private StreamReader sReader;

		public ��ӡ()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

					        
			//��ʼ����ӡ��
            sReader = new StreamReader(@"I:\360data\��Ҫ����\����\������վ������.txt", System.Text.Encoding.Default);
			//��ȡ�ı���ȫ�����ݸ�RichTextBox
			this.richTextBox1.Text=sReader.ReadToEnd();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(��ӡ));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pDoc = new System.Drawing.Printing.PrintDocument();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(292, 41);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Text = "��ӡ";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 41);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(292, 232);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // pDoc
            // 
            this.pDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDoc_PrintPage);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolBar1);
            this.Name = "Form1";
            this.Text = "�ļ���ӡ";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button==this.toolBarButton1)
			{
				try 
				{
					pDoc.Print();
				} 	
				catch(Exception error) 
				{
					//��ӡ������
					MessageBox.Show("��ӡ�ļ���������   " + error.Message);
				}
				finally 
				{
					//�ر��ַ���
					sReader.Close() ;
				}		
			}
		}

		//ÿ��Ҫ��ӡ��ҳ���������¼�
		private void pDoc_PrintPage(object sender, PrintPageEventArgs e) 
		{
			//��û��ƶ���
			Graphics g=e.Graphics;
            //һҳ�е�����
			float linePage = 0 ;
			//�����ı�����������
			float yPosition =  0 ;
			//�м���
			int count = 0 ;
			//��߽�
			float leftMargin = e.MarginBounds.Left;
			//���߽�
			float topMargin = e.MarginBounds.Top;
			//�ַ�����
			String line=null;

		
			//����ҳ��ĸ߶Ⱥ�����ĸ߶ȼ���
			//һҳ�п��Դ�ӡ������
			linePage = e.MarginBounds.Height  / this.Font.GetHeight(g) ;
            
			//ÿ�δ��ַ������ж�ȡһ�в���ӡ
			while (count < linePage && ((line=sReader.ReadLine()) != null)) 
			{
				//������һ�е���ʾλ��
				yPosition = topMargin + (count * this.Font.GetHeight(g));
                //�����ı�
				g.DrawString (line, this.Font, Brushes.Black, leftMargin, yPosition, new StringFormat());
                //��������
				count++;
			}

			//����ж��У��������ӡһҳ
			if (line != null)
				e.HasMorePages = true ;
			else
				e.HasMorePages = false ;
		}

		
		
	}
}
