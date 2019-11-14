using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data.SqlClient;


namespace DotNet.zy.Utilities
{
    /// <summary>
    /// errorlog 三种，Txt=文本日志、Mail=邮件、System=系统消息（暂未实现）
    /// </summary>
    public class ErrorLog
    {
        private static string logtype = "";//日志类型
        private static string erroremail = "";
        private static string logdirectory = "";

        static ErrorLog()
        {
            logtype = ConfigHelper.GetAppStr("LogType");
            erroremail = ConfigHelper.GetAppStr("errmail");
            logdirectory = ConfigHelper.GetAppStr("errdirectory");
            if (erroremail == "")
            {
                erroremail = "abcandy.yang@hubs1.net";
            }
        }

        /// <summary>
        /// 发送log
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="msg"></param>
        public static void sendLog(System.Exception e1, string msg)
        {
            msg = RequestHelper.GetClientIPv4() + ":" + msg;
            switch (logtype)
            {
                case "Txt":
                    if (logdirectory != "")
                        saveTxt(e1, msg);
                    break;
                case "System":
                    saveSystem(e1, msg);
                    break;
                default:
                    sendMail(e1, msg);
                    break;
            }
        }

        /// <summary>
        /// 发送log
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="msg"></param>
        /// <param name="cmdParms"></param>
        public static void sendLog(System.Exception e1, string msg, params SqlParameter[] cmdParms)
        {
            string errStr = "";
            foreach (SqlParameter pam in cmdParms)
            {
                errStr += "Name:" + pam.ParameterName + ";Value:" + pam.Value.ToString() + "\r\n";
            }
            sendLog(e1, msg + "\r\n" + errStr);

        }

        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="msg"></param>
        private static void saveTxt(System.Exception e1, string msg)
        {
            string errmsg = DateTime.Now.ToString() + "：" + e1.Message + "\r\n" + msg + "\r\n--------------------------------\r\n\r\n";
            string filename = logdirectory + "\\" + DateTime.Now.ToShortDateString() + ".txt";
            FileHelper fileHelper = new FileHelper();
            fileHelper.SaveFile(errmsg, filename, "UTF8");
        }

        /// <summary>
        /// 邮件日志
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="msg"></param>
        private static void sendMail(System.Exception e1, string msg)
        {
            string errmsg = e1.Message + "\r\n" + msg.Replace("\r\n", "<br />");
            string fromurl = System.Web.HttpContext.Current.Request.Url.ToString();
            Tools.SendeMail("", erroremail, "数据库访问错误From:" + fromurl, errmsg, true, Encoding.UTF8);
        }

        /// <summary>
        /// 系统事件日志
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="msg"></param>
        private static void saveSystem(System.Exception e1, string msg)
        {
            EventLog.WriteEntry("tademo error", "message" + e1.ToString(), EventLogEntryType.Warning, 80);
        }
    }
}
