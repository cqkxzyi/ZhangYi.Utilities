/*共用工具类
 * 
 * 版权所有：2010张毅
 * 开发部门：IT 
 * */
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using QQWry.NET;
using System.IO;
using System.Web;
using System.Diagnostics;
using System.Threading;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 共用工具类
    /// </summary>
    public class Tools
    {
        #region 根据ip获取所在省市、市区
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        public static string GetIpCity(string dbPath, string strIp)
        {
            QQWryLocator qqWry = new QQWryLocator(dbPath);//初始化数据库文件，并获得IP记录数，通过Count可以获得
            IPLocation ip = qqWry.Query(strIp);  //查询一个IP地址
            return ip.Country.Trim();

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //stopwatch.Stop();
            //Console.WriteLine("一共花了{0} ms的时间", stopwatch.ElapsedMilliseconds);
        }
        #endregion

        #region 随机数生成系列**********

        /// <summary>
        /// 随机数生成
        /// </summary>
        /// <param name="count">随机数个数</param>
        /// <returns></returns>
        public static string GenerateCheckCode(int count)
        {
            int number;
            char code;
            string checkCode = String.Empty;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                number = random.Next();
                if (number % 2 == 0)
                { code = (char)('0' + (char)(number % 10)); }
                else
                { code = (char)('A' + (char)(number % 26)); }
                checkCode += code.ToString();
            }
            return checkCode;
        }

        /// <summary>
        /// 生成随机id
        /// </summary>
        /// <returns>随机id</returns>
        public static string RandomWord()
        {
            int iii;
            string RandWord;
            Random ro = new Random();
            iii = ro.Next(1000, 9999);
            string Smonth = System.DateTime.Now.Month.ToString("D2");
            string Sday = System.DateTime.Now.Day.ToString("D2");
            string Shour = System.DateTime.Now.Hour.ToString("D2");
            string Sminite = System.DateTime.Now.Minute.ToString("D2");
            string Ssecond = System.DateTime.Now.Second.ToString("D2");
            RandWord = System.DateTime.Now.Year.ToString() + Smonth + Sday + Shour + Sminite + Ssecond + iii;
            return RandWord;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="sm">获取个数</param>
        /// <returns>结果级别，分开的字符串</returns>
        public static string RandomWord(int sm)
        {
            int iii;
            string RandWord = "";
            Random ro = new Random();

            string Smonth = System.DateTime.Now.Month.ToString("D2");
            string Sday = System.DateTime.Now.Day.ToString("D2");
            string Shour = System.DateTime.Now.Hour.ToString("D2");
            string Sminite = System.DateTime.Now.Minute.ToString("D2");
            string Ssecond = System.DateTime.Now.Second.ToString("D2");

            for (int j = 0; j < sm; j++)
            {
                iii = ro.Next(1000, 9999);
                if (RandWord == "")
                {
                    RandWord = RandWord + System.DateTime.Now.Year.ToString() + Smonth + Sday + Shour + Sminite + Ssecond + iii;
                }
                else
                {
                    RandWord = RandWord + "," + System.DateTime.Now.Year.ToString() + Smonth + Sday + Shour + Sminite + Ssecond + iii;
                }
            }
            return RandWord;
        }

        /// <summary>
        /// 5位随机数（4位到种子日期的天数+1位随机数）
        /// </summary>
        /// <param name="startDate">种子日期 null 或者 大于当前日期时，取默认2010-8-1</param>
        /// <returns></returns>
        public string RandomWord(DateTime startDate)
        {
            DateTime RandStartDate = new DateTime(2013, 8, 1);
            string conf = "";
            if (startDate == null || startDate > DateTime.Now)
            {
                startDate = RandStartDate;
            }

            TimeSpan ts1 = new TimeSpan(startDate.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);

            TimeSpan ts = ts2.Subtract(ts1);
            conf = ts.Days.ToString("D4");
            string ranId = new Random(DateTime.Now.Millisecond).Next(9).ToString();
            conf += ranId;

            return conf;
        }
        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="CodeCount">返回个数</param>
        /// <returns></returns>
        private string GetRandomCode(int CodeCount)
        {
            string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }

        #endregion 随机数生成系列**********

        #region 发送邮件相关方法*************

        SmtpClient SmtpClient = null;   //设置SMTP协议  
        MailMessage MailMessage_Mai = new MailMessage();
        MailAddress MailAddress_from = null; //设置发信人地址  当然还需要密码      
        MailAddress MailAddress_to = null;   //设置收信人地址   
        FileStream FileStream_my = null;     //附件文件流

        #region 设置smtp服务器信息
        /// <summary>
        /// 设置Ｓmtp服务器信息 
        /// </summary>     
        /// <param name="ServerName">SMTP服务名</param>       
        /// <param name="Port">端口号</param>       
        private void SetSmtpClient(string ServerHost, int Port = 0)
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Host = ServerHost;//指定SMTP服务名  例如QQ邮箱为 smtp.qq.com 新浪cn邮箱为 smtp.sina.cn等 
            SmtpClient.Timeout = 5;  //超时时间           
            //SmtpClient.Port = Port; //指定端口号                 
        }
        #endregion

        #region 验证发件人信息
        /// <summary>        
        /// 验证发件人信息        
        /// </summary>       
        /// <param name="MailAddress">发件邮箱地址</param>       
        /// <param name="MailPwd">邮箱密码</param>          
        /// <param name="showName">显示名</param>     
        private void SetAddressform(string MailAddress, string MailPwd, string showName = "呼你网总部")
        {
            //创建服务器认证           
            NetworkCredential NetworkCredential_my = new NetworkCredential(MailAddress, MailPwd);
            //实例化发件人地址           
            MailAddress_from = new System.Net.Mail.MailAddress(MailAddress, showName);
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
            FileStream_my = new FileStream(path, FileMode.Open);
            string name = FileStream_my.Name;
            int size = (int)(FileStream_my.Length / 1024 / 1024);
            FileStream_my.Close();
            //控制文件大小不大于10Ｍ               
            if (size > 10)
            {
                JsHelper.MsgBox("文件长度不能大于10M！你选择的文件大小为" + size.ToString() + "M");
                return false;
            }
            return true;
        }
        #endregion

        #region  发送邮件1
        /// <summary>
        /// 发送邮件1
        /// </summary>
        /// <param name="sendto">发送给对方邮箱名称</param>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        /// <param name="attachPath">附件地址</param>
        /// <returns></returns>
        public bool SendeMail(string sendto, string title, string text, string attachPath = "")
        {
            string mailserver = ConfigHelper.GetAppStr("mailserver");
            string mailserverUser = ConfigHelper.GetAppStr("mailusername");
            string mailserverPass = ConfigHelper.GetAppStr("mailpassword");

            //检测附件大小 发件必需小于10M 否则返回  不会执行以下代码        
            if (attachPath != "")
            {
                if (!Attachment_MaiInit(attachPath))
                {
                    return false;
                }
            }

            try
            {
                SetSmtpClient(mailserver);//初始化Ｓmtp服务器信息 
            }
            catch (Exception Ex)
            {
                JsHelper.MsgBox("邮件发送失败,请确定SMTP服务名是否正确！");
                return false;
            }

            try
            {
                SetAddressform(mailserverUser, mailserverPass);//验证发件邮箱地址和密码 
            }
            catch (Exception Ex)
            {
                JsHelper.MsgBox("邮件发送失败,请确定发件邮箱地址和密码的正确性！");
                return false;
            }

            try
            {
                MailMessage_Mai.To.Clear(); //清空历史发送信息 以防发送时收件人收到的错误信息(收件人列表会不断重复)  
                MailAddress_to = new MailAddress(sendto);
                MailMessage_Mai.To.Add(MailAddress_to);//添加收件人邮箱地址     
                //发件人邮箱              
                MailMessage_Mai.From = MailAddress_from;
                //邮件主题            
                MailMessage_Mai.Subject = title;
                MailMessage_Mai.SubjectEncoding = System.Text.Encoding.UTF8;
                //邮件正文      
                MailMessage_Mai.Body = text;
                MailMessage_Mai.BodyEncoding = System.Text.Encoding.UTF8;
                //清空历史附件  以防附件重复发送                
                MailMessage_Mai.Attachments.Clear();
                //添加附件  
                if (attachPath != "")
                {
                    MailMessage_Mai.Attachments.Add(new Attachment(attachPath, MediaTypeNames.Application.Octet));
                }
                //注册邮件发送完毕后的处理事件               
                //SmtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //开始发送邮件   
                //SmtpClient.SendAsync(MailMessage_Mai, "000000");  
                SmtpClient.Send(MailMessage_Mai);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        #endregion

        #region 发送邮件2
        /// <summary>
        /// 发送邮件2
        /// </summary>
        /// <param name="from">发件人</param>
        /// <param name="sendto">发送到</param>
        /// <param name="title">标题</param>
        /// <param name="text">详情</param>
        /// <param name="isHtml">是否是html格式</param>
        /// <param name="encod">编码</param>
        /// <returns></returns>
        public static bool SendeMail(string from, string sendto, string title, string text, bool isHtml, Encoding encod)
        {
            string mailserver = ConfigHelper.GetAppStr("mailserver");
            string mailserverUser = ConfigHelper.GetAppStr("mailusername");
            string mailserverPass = ConfigHelper.GetAppStr("mailpassword");
            if (from == "")
                from = mailserverUser;
            System.Net.Mail.SmtpClient client = new SmtpClient(mailserver);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mailserverUser, mailserverPass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailAddress MailFrom = new MailAddress(from);
            System.Net.Mail.MailMessage message = new MailMessage();
            System.Net.Mail.MailMessage messageUTF = new MailMessage();

            message.From = MailFrom;
            message.To.Add(sendto);
            message.Subject = title;
            message.Body = text;
            message.Priority = MailPriority.High;
            message.BodyEncoding = encod;
            message.IsBodyHtml = isHtml;

            client.Send(message);
            return true;
        }
        #endregion

        #endregion 发送邮件相关方法*************




        #region 获取文件相对路径映射的物理路径
        /// <summary>
        /// 获取文件相对路径映射的物理路径
        /// </summary>
        /// <param name="virtualPath">文件的相对路径</param>        
        public static string GetPhysicalPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }
        #endregion

        #region 根据给出的相对地址获取网站绝对地址
        /// <summary>
        /// 根据给出的相对地址获取网站绝对地址
        /// </summary>
        /// <param name="localPath">相对地址</param>
        /// <returns>绝对地址</returns>
        public static string GetWebPath(string localPath)
        {
            string path = HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            string thisLocalPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            if (localPath.StartsWith("~/"))
            {
                thisLocalPath = localPath.Substring(2);
            }
            else
            {
                return localPath;
            }
            return thisPath + thisLocalPath;
        }

        #endregion

        #region 获取网站绝对地址
        /// <summary>
        ///  获取网站绝对地址
        /// </summary>
        /// <returns></returns>
        public static string GetWebPath()
        {
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            return thisPath;
        }
        #endregion

        #region 根据相对路径或绝对路径获取绝对路径
        /// <summary>
        /// 根据相对路径或绝对路径获取绝对路径
        /// </summary>
        /// <param name="localPath">相对路径或绝对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetFilePath(string localPath)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(localPath, @"([A-Za-z]):\\([\S]*)"))
            {
                return localPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(localPath);
            }
        }
        #endregion


        #region 获取指定调用层级的方法名
        /// <summary>
        /// 获取指定调用层级的方法名
        /// </summary>
        /// <param name="level">调用的层数</param>        
        public static string GetMethodName(int level)
        {
            //创建一个堆栈跟踪
            StackTrace trace = new StackTrace();

            //获取指定调用层级的方法名
            return trace.GetFrame(level).GetMethod().Name;
        }
        #endregion

        #region 获取GUID值
        /// <summary>
        /// 获取GUID值
        /// </summary>
        public static string NewGUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        #endregion

        #region 获取换行字符
        /// <summary>
        /// 获取换行字符
        /// </summary>
        public static string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }
        #endregion

        #region 获取当前应用程序域
        /// <summary>
        /// 获取当前应用程序域
        /// </summary>
        public static AppDomain CurrentAppDomain
        {
            get
            {
                return Thread.GetDomain();
            }
        }
        #endregion
    }
}
