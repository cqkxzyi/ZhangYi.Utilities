using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��ͼ������ : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxStyle;
		private System.Windows.Forms.ComboBox comboBoxDraw;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;

		/// <summary>
		/// ����������������
		/// </summary>
		
		private System.ComponentModel.Container components = null;
        public ��ͼ������()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			//��ʼ��3��comboBox
			InitControl();
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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxDraw = new System.Windows.Forms.ComboBox();
			this.comboBoxStyle = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.AllowDrop = true;
			this.comboBox1.Enabled = false;
			this.comboBox1.Location = new System.Drawing.Point(16, 72);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			this.comboBox1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox1_MeasureItem);
			this.comboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(16, 176);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(120, 24);
			this.panel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.comboBoxDraw,
																					this.comboBoxStyle,
																					this.label2,
																					this.label1,
																					this.checkBox2,
																					this.checkBox1});
			this.groupBox1.Location = new System.Drawing.Point(160, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 200);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "��Ͽ�����";
			// 
			// comboBoxDraw
			// 
			this.comboBoxDraw.Location = new System.Drawing.Point(120, 128);
			this.comboBoxDraw.Name = "comboBoxDraw";
			this.comboBoxDraw.Size = new System.Drawing.Size(121, 20);
			this.comboBoxDraw.TabIndex = 5;
			this.comboBoxDraw.SelectedIndexChanged += new System.EventHandler(this.comboBoxDraw_SelectedIndexChanged);
			// 
			// comboBoxStyle
			// 
			this.comboBoxStyle.Location = new System.Drawing.Point(120, 96);
			this.comboBoxStyle.Name = "comboBoxStyle";
			this.comboBoxStyle.Size = new System.Drawing.Size(121, 20);
			this.comboBoxStyle.TabIndex = 4;
			this.comboBoxStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxStyle_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "����ģʽ";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "��ʽ";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(32, 56);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "����";
			this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(32, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "����";
			this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "����Ͽ���ѡȡ��ɫ";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 144);
			this.label4.Name = "label4";
			this.label4.TabIndex = 4;
			this.label4.Text = "��ʾ��ɫ";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 261);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label4,
																		  this.label3,
																		  this.groupBox1,
																		  this.panel1,
																		  this.comboBox1});
			this.Name = "Form1";
			this.Text = "��Ͽ�";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		///    Ϊ�������ÿ���������Ͽ�����Ե�Ĭ��״̬��
		/// </summary>
		private void InitControl() 
		{
		//��ʼ����ʽ��Ͽ�
		comboBoxStyle.Items.Add(ComboBoxStyle.Simple.ToString());
        comboBoxStyle.Items.Add(ComboBoxStyle.DropDown.ToString());
        comboBoxStyle.Items.Add(ComboBoxStyle.DropDownList.ToString());
		//��ʼ����ͼģʽ��Ͽ�
		comboBoxDraw.Items.Add(DrawMode.Normal.ToString());
	    comboBoxDraw.Items.Add(DrawMode.OwnerDrawFixed.ToString());
		comboBoxDraw.Items.Add(DrawMode.OwnerDrawVariable.ToString());
		//��ʼ��ComboBox1
		//��ȡ������һ���ַ��������ַ���ָ��Ҫ��ʾ�����ݵ�����Դ������
        comboBox1.DisplayMember="Color";
		comboBox1.Items.Add(Brushes.Cyan);
		comboBox1.Items.Add(Brushes.DarkSalmon);
		comboBox1.Items.Add(Brushes.Gray);
		comboBox1.Items.Add(Brushes.Green);
		comboBox1.Items.Add(Brushes.AliceBlue);
		comboBox1.Items.Add(Brushes.Black);
		comboBox1.Items.Add(Brushes.Blue);
	    comboBox1.Items.Add(Brushes.Chocolate);
		comboBox1.Items.Add(Brushes.Pink);
		comboBox1.Items.Add(Brushes.Red);
		comboBox1.Items.Add(Brushes.LightBlue);
		comboBox1.Items.Add(Brushes.Brown);
		comboBox1.Items.Add(Brushes.DodgerBlue);
		comboBox1.Items.Add(Brushes.MediumPurple);
		comboBox1.Items.Add(Brushes.White);
		comboBox1.Items.Add(Brushes.Yellow);
	 
		}



		private void comboBox1_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
		//���û���ͼ�εĸ߶ȱ�comboBox1��Ŀ�ĸ߶���С
		e.ItemHeight = comboBox1.ItemHeight - 2;
		}
        

		private void comboBox1_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
		    ComboBox cmb = (ComboBox) sender;
			if (e.Index == -1)
				return;
			if (sender == null)
				return;
			SolidBrush selectedBrush = (SolidBrush)cmb.Items[e.Index];
			Graphics g = e.Graphics;

			// ���ѡ������������ȷ�ı�����ɫ�;۽���
			e.DrawBackground();
			e.DrawFocusRectangle();

			// ������ɫ��Ԥ����
			Rectangle rectPreview = e.Bounds;
            rectPreview.Offset(3,3);
			rectPreview.Width = 20;
			rectPreview.Height -= 4;
			g.DrawRectangle(new Pen(e.ForeColor), rectPreview);

			// ��ȡѡ����ɫ����Ӧ Brush ���󣬲����Ԥ����
			rectPreview.Offset(1,1);
			rectPreview.Width -= 2;
			rectPreview.Height -= 2;
			g.FillRectangle(selectedBrush, rectPreview);

			// ����ѡ����ɫ������
			g.DrawString(selectedBrush.Color.ToString(), Font, new SolidBrush(e.ForeColor), e.Bounds.X+30,e.Bounds.Y+1);
    
		}

		private void comboBoxDraw_SelectedIndexChanged(object sender, System.EventArgs e)
		{  
		    //�趨comboBox1��DrawMode����
			String i=comboBoxDraw.SelectedItem.ToString();
			switch (i)
			{   
				case "Normal":
					comboBox1.DrawMode=DrawMode.Normal;
					break;
				case "OwnerDrawFixed":
					comboBox1.DrawMode=DrawMode.OwnerDrawFixed;
					break;
				case "OwnerDrawVariable":
					comboBox1.DrawMode=DrawMode.OwnerDrawVariable;
					break;
			}
		}

		private void comboBoxStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		   //�趨comboBox1��DropDownStyle����
			String  i=comboBoxStyle.SelectedItem.ToString();
			switch (i)
			{   
				case "Simple":
					comboBox1.DropDownStyle=ComboBoxStyle.Simple;
					Size size=new Size(comboBox1.Size.Width,66);
					comboBox1.Size=size;
					break;
				case "DropDown":
					comboBox1.DropDownStyle=ComboBoxStyle.DropDown;
					break;
				case "DropDownList":
					comboBox1.DropDownStyle=ComboBoxStyle.DropDownList;
					break;
			}
		}

	

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//����panel1�ı�����ɫ
			if (comboBox1.SelectedIndex >= 0)
			{
			SolidBrush brush = (SolidBrush)(comboBox1.SelectedItem);
	        panel1.BackColor=brush.Color;
			}
		}

		private void checkBox1_CheckStateChanged(object sender, System.EventArgs e)
		{
			//����checkBox��Checked����ȷ���Ƿ�����comboBox1
			comboBox1.Enabled=checkBox1.Checked;
		}

		private void checkBox2_CheckStateChanged(object sender, System.EventArgs e)
		{
		    //����checkBox��Checked����ȷ��comboBox1�е�ѡ���Ƿ�����
			comboBox1.Sorted=checkBox2.Checked;
		}

	
	}
