using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Net.Mime;

namespace CSharp.示例窗口
{
    public partial class 发送邮件 : Form
    {
        public 发送邮件()
        {
            InitializeComponent();
        }
        SmtpClient SmtpClient = null;   //设置SMTP协议       
        MailAddress MailAddress_from = null; //设置发信人地址  当然还需要密码      
        MailAddress MailAddress_to = null;  //设置收信人地址  不需要密码      
        MailMessage MailMessage_Mai = null;
        FileStream FileStream_my = null; //附件文件流


        #region 设置smtp服务器信息
        /// <summary>
        /// 设置Ｓmtp服务器信息 
        /// </summary>     
        /// <param name="ServerName">SMTP服务名</param>       
        /// <param name="Port">端口号</param>       
        private void SetSmtpClient(string ServerHost, int Port)
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Host = ServerHost;//指定SMTP服务名  例如QQ邮箱为 smtp.qq.com 新浪cn邮箱为 smtp.sina.cn等          
            SmtpClient.Port = Port; //指定端口号           
            SmtpClient.Timeout = 5;  //超时时间        
        }
        #endregion

        #region 验证发件人信息
        /// <summary>        
        /// 验证发件人信息        
        /// </summary>       
        /// <param name="MailAddress">发件邮箱地址</param>       
        ///  <param name="MailPwd">邮箱密码</param>       
        private void SetAddressform(string MailAddress, string MailPwd)
        {
            //创建服务器认证           
            NetworkCredential NetworkCredential_my = new NetworkCredential(MailAddress, MailPwd);
            //实例化发件人地址           
            MailAddress_from = new System.Net.Mail.MailAddress(MailAddress, textBoxX4.Text);
            //指定发件人信息  包括邮箱地址和邮箱密码          
            SmtpClient.Credentials = new System.Net.NetworkCredential(MailAddress_from.Address, MailPwd);
        }
        #endregion


        #region 检测附件大小
       /// <summary>
        /// 检测附件大小
       /// </summary>
       /// <param name="path"></param>
       /// <returns></returns>
        private bool Attachment_MaiInit(string path)
        {
            try
            {
                FileStream_my = new FileStream(path, FileMode.Open);
                string name = FileStream_my.Name;
                int size = (int)(FileStream_my.Length / 1024 / 1024);
                FileStream_my.Close();
                //控制文件大小不大于10Ｍ               
                if (size > 10)
                {
                    MessageBox.Show("文件长度不能大于10M！你选择的文件大小为" + size.ToString() + "M", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (IOException E)
            {
                MessageBox.Show(E.Message);
                return false;
            }
        }
        #endregion

        #region 按钮点击发送
        /// <summary>
       /// 按钮点击发送
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //检测附件大小 发件必需小于10M 否则返回  不会执行以下代码        
            if (txt_Path.Text != "")
            {
                if (!Attachment_MaiInit(txt_Path.Text.Trim()))
                {
                    return;
                }
            }
            if (txt_SmtpServer.Text == "")
            {
                MessageBox.Show("请输入SMTP服务器名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("请输入发件人邮箱地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtformPwd.Text == "")
            {
                MessageBox.Show("请输入发件人邮箱密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridViewX1.Rows.Count == 0)
            {
                MessageBox.Show("请添加收件人！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("您确定要发送当前邮件吗？", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    //初始化Ｓmtp服务器信息                  
                    SetSmtpClient("smtp." + txt_SmtpServer.Text.Trim() + comboBoxEx3.Text, Convert.ToInt32(numericUpDown1.Value));
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("邮件发送失败,请确定SMTP服务名是否正确！" + "\n" + "技术信息:\n" + Ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    //验证发件邮箱地址和密码                  
                    SetAddressform(textBoxX2.Text.Trim() + comboBoxEx2.Text, txtformPwd.Text.Trim());
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("邮件发送失败,请确定发件邮箱地址和密码的正确性！" + "\n" + "技术信息:\n" + Ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //清空历史发送信息 以防发送时收件人收到的错误信息(收件人列表会不断重复)                
                MailMessage_Mai.To.Clear();
                //添加收件人邮箱地址               
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    MailAddress_to = new MailAddress(row.Cells["Column1"].Value.ToString());
                    MailMessage_Mai.To.Add(MailAddress_to);
                }
                MessageBox.Show("收件人：" + dataGridViewX1.Rows.Count.ToString() + "  个");
                //发件人邮箱
                MailMessage_Mai.From = MailAddress_from;
                //邮件主题
                MailMessage_Mai.Subject = txttitle.Text;
                MailMessage_Mai.SubjectEncoding = System.Text.Encoding.UTF8;
                //邮件正文
                MailMessage_Mai.Body = Rtb_Message.Text;
                MailMessage_Mai.BodyEncoding = System.Text.Encoding.UTF8;
                //清空历史附件  以防附件重复发送
                MailMessage_Mai.Attachments.Clear();
                //添加附件
                MailMessage_Mai.Attachments.Add(new Attachment(txt_Path.Text.Trim(), MediaTypeNames.Application.Octet));
                //注册邮件发送完毕后的处理事件
                SmtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //开始发送邮件
                SmtpClient.SendAsync(MailMessage_Mai, "000000000");
            }
        }
        #endregion


        #region 发送邮件后所处理的函数
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("发送已取消！");
                }
                if (e.Error != null)
                {
                    MessageBox.Show("邮件发送失败！" + "\n" + "技术信息:\n" + e.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("邮件成功发出!", "恭喜!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("邮件发送失败！" + "\n" + "技术信息:\n" + Ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}