using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ch2_9
{
	/// <summary>
	/// Form2 ��ժҪ˵����
	/// </summary>
	public class ͼ���ʽת��2 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ͼ���ʽת��2()
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(292, 273);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.Name = "Form2";
			this.Text = "Form2";
			this.Closed += new System.EventHandler(this.Form2_Closed);
			this.ResumeLayout(false);

		}
		#endregion


		private void Form2_Closed(object sender, System.EventArgs e)
		{
			ͼ���ʽת��1 fm=(ͼ���ʽת��1)this.MdiParent;
			fm.UpdateToolBar();
		}

	}
}
