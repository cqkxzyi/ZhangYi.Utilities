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

namespace CSharp.ʾ������
{
    public partial class �����ʼ� : Form
    {
        public �����ʼ�()
        {
            InitializeComponent();
        }
        SmtpClient SmtpClient = null;   //����SMTPЭ��       
        MailAddress MailAddress_from = null; //���÷����˵�ַ  ��Ȼ����Ҫ����      
        MailAddress MailAddress_to = null;  //���������˵�ַ  ����Ҫ����      
        MailMessage MailMessage_Mai = null;
        FileStream FileStream_my = null; //�����ļ���


        #region ����smtp��������Ϣ
        /// <summary>
        /// ���ã�mtp��������Ϣ 
        /// </summary>     
        /// <param name="ServerName">SMTP������</param>       
        /// <param name="Port">�˿ں�</param>       
        private void SetSmtpClient(string ServerHost, int Port)
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Host = ServerHost;//ָ��SMTP������  ����QQ����Ϊ smtp.qq.com ����cn����Ϊ smtp.sina.cn��          
            SmtpClient.Port = Port; //ָ���˿ں�           
            SmtpClient.Timeout = 5;  //��ʱʱ��        
        }
        #endregion

        #region ��֤��������Ϣ
        /// <summary>        
        /// ��֤��������Ϣ        
        /// </summary>       
        /// <param name="MailAddress">���������ַ</param>       
        ///  <param name="MailPwd">��������</param>       
        private void SetAddressform(string MailAddress, string MailPwd)
        {
            //������������֤           
            NetworkCredential NetworkCredential_my = new NetworkCredential(MailAddress, MailPwd);
            //ʵ���������˵�ַ           
            MailAddress_from = new System.Net.Mail.MailAddress(MailAddress, textBoxX4.Text);
            //ָ����������Ϣ  ���������ַ����������          
            SmtpClient.Credentials = new System.Net.NetworkCredential(MailAddress_from.Address, MailPwd);
        }
        #endregion


        #region ��⸽����С
       /// <summary>
        /// ��⸽����С
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
                //�����ļ���С������10��               
                if (size > 10)
                {
                    MessageBox.Show("�ļ����Ȳ��ܴ���10M����ѡ����ļ���СΪ" + size.ToString() + "M", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region ��ť�������
        /// <summary>
       /// ��ť�������
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //��⸽����С ��������С��10M ���򷵻�  ����ִ�����´���        
            if (txt_Path.Text != "")
            {
                if (!Attachment_MaiInit(txt_Path.Text.Trim()))
                {
                    return;
                }
            }
            if (txt_SmtpServer.Text == "")
            {
                MessageBox.Show("������SMTP����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("�����뷢���������ַ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtformPwd.Text == "")
            {
                MessageBox.Show("�����뷢�����������룡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridViewX1.Rows.Count == 0)
            {
                MessageBox.Show("������ռ��ˣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("��ȷ��Ҫ���͵�ǰ�ʼ���", "ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    //��ʼ����mtp��������Ϣ                  
                    SetSmtpClient("smtp." + txt_SmtpServer.Text.Trim() + comboBoxEx3.Text, Convert.ToInt32(numericUpDown1.Value));
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("�ʼ�����ʧ��,��ȷ��SMTP�������Ƿ���ȷ��" + "\n" + "������Ϣ:\n" + Ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    //��֤���������ַ������                  
                    SetAddressform(textBoxX2.Text.Trim() + comboBoxEx2.Text, txtformPwd.Text.Trim());
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("�ʼ�����ʧ��,��ȷ�����������ַ���������ȷ�ԣ�" + "\n" + "������Ϣ:\n" + Ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //�����ʷ������Ϣ �Է�����ʱ�ռ����յ��Ĵ�����Ϣ(�ռ����б�᲻���ظ�)                
                MailMessage_Mai.To.Clear();
                //����ռ��������ַ               
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    MailAddress_to = new MailAddress(row.Cells["Column1"].Value.ToString());
                    MailMessage_Mai.To.Add(MailAddress_to);
                }
                MessageBox.Show("�ռ��ˣ�" + dataGridViewX1.Rows.Count.ToString() + "  ��");
                //����������
                MailMessage_Mai.From = MailAddress_from;
                //�ʼ�����
                MailMessage_Mai.Subject = txttitle.Text;
                MailMessage_Mai.SubjectEncoding = System.Text.Encoding.UTF8;
                //�ʼ�����
                MailMessage_Mai.Body = Rtb_Message.Text;
                MailMessage_Mai.BodyEncoding = System.Text.Encoding.UTF8;
                //�����ʷ����  �Է������ظ�����
                MailMessage_Mai.Attachments.Clear();
                //��Ӹ���
                MailMessage_Mai.Attachments.Add(new Attachment(txt_Path.Text.Trim(), MediaTypeNames.Application.Octet));
                //ע���ʼ�������Ϻ�Ĵ����¼�
                SmtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //��ʼ�����ʼ�
                SmtpClient.SendAsync(MailMessage_Mai, "000000000");
            }
        }
        #endregion


        #region �����ʼ���������ĺ���
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("������ȡ����");
                }
                if (e.Error != null)
                {
                    MessageBox.Show("�ʼ�����ʧ�ܣ�" + "\n" + "������Ϣ:\n" + e.ToString(), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("�ʼ��ɹ�����!", "��ϲ!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("�ʼ�����ʧ�ܣ�" + "\n" + "������Ϣ:\n" + Ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}