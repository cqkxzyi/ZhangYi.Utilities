/***********************************
 * 功能：拖放操作的演示
 * 创建者：杨仁怀
 * 时间：2008-9-16
***********************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_3_47
{
    public partial class 拖放 : Form
    {
        public 拖放()
        {
            InitializeComponent();
        }

        private void txtSource_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtSource.SelectedText  =="" || e.Button.ToString().ToLower()=="left")
            {
                return ;
            }
            txtSource.DoDragDrop(txtSource.SelectedText, DragDropEffects.Move);
        }

        private void lstDestination_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lstDestination_DragDrop(object sender, DragEventArgs e)
        {
            this.lstDestination.Items.Add(e.Data.GetData(DataFormats.Text).ToString());
            e.Effect = DragDropEffects.Move;
        }
    }
}