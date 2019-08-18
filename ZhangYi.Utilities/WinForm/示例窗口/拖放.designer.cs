namespace GUI_3_47
{
    partial class 拖放
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Parent Node");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点3");
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.treeViewSource = new System.Windows.Forms.TreeView();
            this.lstDestination = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Controls";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination Control";
            // 
            // txtSource
            // 
            this.txtSource.AllowDrop = true;
            this.txtSource.Location = new System.Drawing.Point(97, 95);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(100, 21);
            this.txtSource.TabIndex = 2;
            this.txtSource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSource_MouseDown);
            // 
            // treeViewSource
            // 
            this.treeViewSource.Location = new System.Drawing.Point(97, 141);
            this.treeViewSource.Name = "treeViewSource";
            treeNode5.Name = "节点0";
            treeNode5.Text = "Parent Node";
            treeNode6.Name = "节点1";
            treeNode6.Text = "节点1";
            treeNode7.Name = "节点2";
            treeNode7.Text = "节点2";
            treeNode8.Name = "节点3";
            treeNode8.Text = "节点3";
            this.treeViewSource.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.treeViewSource.Size = new System.Drawing.Size(100, 162);
            this.treeViewSource.TabIndex = 3;
            // 
            // lstDestination
            // 
            this.lstDestination.AllowDrop = true;
            this.lstDestination.FormattingEnabled = true;
            this.lstDestination.ItemHeight = 12;
            this.lstDestination.Location = new System.Drawing.Point(335, 95);
            this.lstDestination.Name = "lstDestination";
            this.lstDestination.Size = new System.Drawing.Size(143, 208);
            this.lstDestination.TabIndex = 4;
            this.lstDestination.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstDestination_DragDrop);
            this.lstDestination.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstDestination_DragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 364);
            this.Controls.Add(this.lstDestination);
            this.Controls.Add(this.treeViewSource);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TreeView treeViewSource;
        private System.Windows.Forms.ListBox lstDestination;


    }
}

