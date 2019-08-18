using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_PictureBox
{
    public partial class 进度条2 : Form
    {
        public 进度条2()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //this.pictureBox1.ImageLocation = "big.bmp";
            this.progressBar1.Value = 0;
            this.pictureBox1.LoadAsync("big.bmp");
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error!=null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("用户取消");
            }
            else
            {
                MessageBox.Show("加载完成");
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.pictureBox1.CancelAsync();
        }

        private void pictureBox1_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            long i;
            UInt64 sum = 0;
            this.progressBar1.Minimum = 0;

            for (i = 0; i < 1000000; i++)
            {
                this.progressBar1.Value = 1000000;
                sum++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 1000000;
        }
    }
}