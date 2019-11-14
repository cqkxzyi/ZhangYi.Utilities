using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;

namespace ch3_6
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class �ۺϴ�ӡʵ�� : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;

		private StreamReader sReader;
		private System.Drawing.Printing.PrintDocument pDoc;
		private System.Windows.Forms.MenuItem menuItem5;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public �ۺϴ�ӡʵ��()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

            sReader = new StreamReader(@"I:\360data\��Ҫ����\����\������վ������.txt", System.Text.Encoding.Default);
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
			this.pDoc = new System.Drawing.Printing.PrintDocument();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// pDoc
			// 
			this.pDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDoc_PrintPage);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(292, 273);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
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
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5});
			this.menuItem1.Text = "�ļ�(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "��ӡ(&P)...";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "��ӡԤ��(&V)...";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "ҳ������(&U)...";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "��ӡ����(&S)...";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.richTextBox1});
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "�ۺϴ�ӡʵ��";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			
			
			//�����ӡ�Ի���
			PrintDialog printDialog = new PrintDialog();
			//����Document����
			printDialog.Document = this.pDoc;
			if (printDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{   
					pDoc.Print();
				}
				catch(Exception excep)
				{
					//��ʾ��ӡ������Ϣ
					MessageBox.Show(excep.Message, "��ӡ����", MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
	      
			//�����ӡԤ���Ի��� 
		    PrintPreviewDialog printPreviewDialog1=new PrintPreviewDialog();
            //����Document����
            printPreviewDialog1.Document=this.pDoc;

			//��ʾ��ӡԤ������
			try
			{
				printPreviewDialog1.ShowDialog();
			}
			catch(Exception excep)
			{
				//��ʾ��ӡ������Ϣ
				MessageBox.Show(excep.Message, "��ӡ����", MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			//����ҳ�����öԻ���
			PageSetupDialog  	pageSetupDialog1=new PageSetupDialog();
			//����Document����
			pageSetupDialog1.Document=this.pDoc;
            //��ʾ�Ի���
			pageSetupDialog1.ShowDialog();
		}

		private void pDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			//�����ӡ�Ի���
			PrintDialog printDialog = new PrintDialog();
			//����Document����
			printDialog.Document = this.pDoc;
			//��ʾ�Ի���
			printDialog.ShowDialog();
		}
	}
}
