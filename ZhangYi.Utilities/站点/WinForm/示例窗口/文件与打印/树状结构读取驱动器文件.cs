using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace ch3_4
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class ��״�ṹ��ȡ�������ļ� : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.ComponentModel.IContainer components;

		public ��״�ṹ��ȡ�������ļ�()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//��ʼ��TreeView�ĸ��ڵ�
			InitialTreeView();

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
           
		private void InitialTreeView()
		{
		    //���������ַ���������Ϊ��
			string[] drivers=null;
			//�����˼�������߼�������������
			drivers=Directory.GetLogicalDrives();
			int i=0;

			//��ʼ��ÿһ���߼�������
			while ( i<drivers.GetLength(0))
			{
				this.treeView1.Nodes.Add(new TreeNode(drivers[i],1,1));
				string path=drivers[i];
				string[] dirs=null;
				try
				{
					//���ָ���������е�һ��Ŀ¼������
					dirs=Directory.GetDirectories(path);
				}
				catch(Exception  error)
				{
					//������Ϊ�գ�������
				}
				if (dirs!=null)
				{
					//Ϊÿһ�������������ĸ��ڵ�����ӽڵ�
					for (int j=0;j<dirs.Length;j++)
					{
						//��ýڵ��ȥ��·�����Ŀ¼��
						TreeNode node = new TreeNode(dirs[j].ToString().Substring(dirs[j].ToString().LastIndexOf("\\")+1));
						//���ò�ѡ��״̬�µ�ͼ��
						node.ImageIndex =2;
						//���ô�״̬�µ�ͼ��
						node.SelectedImageIndex =0;
						//��ӽڵ�
						this.treeView1.Nodes[i].Nodes.Add(node);
					}
				}
				//������һ��ѭ��
				i++; 
			}
		
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(��״�ṹ��ȡ�������ļ�));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(292, 341);
			this.treeView1.TabIndex = 0;
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBox7,
																					this.checkBox6,
																					this.checkBox5,
																					this.checkBox4,
																					this.checkBox3,
																					this.checkBox2,
																					this.checkBox1});
			this.groupBox1.Location = new System.Drawing.Point(320, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 304);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "��ʽ";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(40, 32);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "����";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(40, 69);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(128, 24);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "��ʾ�ڵ������";
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.Checked = true;
			this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox3.Location = new System.Drawing.Point(40, 106);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(136, 24);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "��ʾ���ڵ������";
			this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(40, 143);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Text = "��ʾ��ѡ��";
			this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
			// 
			// checkBox5
			// 
			this.checkBox5.Checked = true;
			this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox5.Location = new System.Drawing.Point(40, 180);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.TabIndex = 4;
			this.checkBox5.Text = "��ʾ+ - ��ť";
			this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
			// 
			// checkBox6
			// 
			this.checkBox6.Checked = true;
			this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox6.Location = new System.Drawing.Point(40, 217);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.TabIndex = 5;
			this.checkBox6.Text = "����ѡ��";
			this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
			// 
			// checkBox7
			// 
			this.checkBox7.Location = new System.Drawing.Point(40, 254);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.TabIndex = 6;
			this.checkBox7.Text = "����ɫ";
			this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(552, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.treeView1});
			this.Name = "Form1";
			this.Text = "�鿴������Ŀ¼";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			
            //��ȡ������Ϣ�Ľڵ�
			TreeNode node=e.Node;
			//������ΪtreeView�ĵ�ǰ�ڵ�
			this.treeView1.SelectedNode=node;
			string path=node.FullPath;
			string[] dirs=null;
			try
			{
				//��õ�ǰ�ڵ����е��ӽڵ�
				dirs=Directory.GetDirectories(path);
			}
			catch(Exception  error)
			{
				//������Ϊ�գ�������
			}
            
			if (dirs!=null)
			{
				//Ϊÿһ����ǰ�ڵ���ֽڵ�����ӽڵ�
				for (int j=0;j<dirs.Length;j++)
				{	
					//�����ǰ�ڵ���ӽڵ���Ϊ0����Ϊ������ӽڵ�
					//����˵���Ѿ���ӹ�����û����Ŀ¼
					if (node.Nodes[j].Nodes.Count==0)
					{
						//���ú���Ϊ�ӽڵ�����ӽڵ�
						addDirectroy(node.Nodes[j],dirs[j]);
					}
					
				}
			}
		}
		private void addDirectroy(TreeNode node,string path)
		{
			
				string[] dirs=null;
				try
				{
					dirs=Directory.GetDirectories(path);
				}
				catch(Exception  e)
				{
					//������Ϊ�գ�������
				}
                if (dirs==null)
                {
                    return;
                }
				for (int j=0;j<dirs.Length;j++)
				{
					//����µ��ӽڵ�
					TreeNode nodeNew= new TreeNode(dirs[j].ToString().Substring(dirs[j].ToString().LastIndexOf("\\")+1));
					//���ò�ѡ��״̬�µ�ͼ��
					nodeNew.ImageIndex =2;
					//���ô�״̬�µ�ͼ��
					nodeNew.SelectedImageIndex =0;
					node.Nodes.Add(nodeNew);
				}
		
		}

		
		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.Sorted = this.checkBox1.Checked;
		}

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.ShowLines=this.checkBox2.Checked;
		}

		private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.ShowRootLines=this.checkBox3.Checked;
		}

		private void checkBox4_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.CheckBoxes=this.checkBox4.Checked;
		}
        private void checkBox5_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.ShowPlusMinus=this.checkBox5.Checked;
		}

		private void checkBox6_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.HideSelection=this.checkBox6.Checked;
		}

		private void checkBox7_CheckedChanged(object sender, System.EventArgs e)
		{
			this.treeView1.HotTracking=this.checkBox7.Checked;
		}
	}

}
