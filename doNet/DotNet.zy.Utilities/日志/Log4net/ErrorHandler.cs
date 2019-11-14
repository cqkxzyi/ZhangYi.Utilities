using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DotNet.zy.Utilities
{
    public sealed class ErrorHandler
    {
        // Fields
        private static XmlDocument m_docErrorMessage = new XmlDocument();

        // Methods
        static ErrorHandler()
        {
            //m_docErrorMessage.Load(SystemPath.GetErrorMessageFilePath());
        }

        private ErrorHandler()
        {
        }

        public static string GetErrorDescription(Exception ex)
        {
            string errMessage = "";
            errMessage = errMessage + "\r\n发生错误，具体如下\r\n";
            InnerGetErrorDescription(ex, ref errMessage);
            return errMessage;
        }

        public static string GetErrorDescription(Exception ex, string userName)
        {
            string errMessage = "";
            errMessage = errMessage + "\r\n发生错误，具体如下\r\n";
            InnerGetErrorDescription(ex, ref errMessage, userName);
            return errMessage;
        }


        private static void InnerGetErrorDescription(Exception ex, ref string errMessage)
        {
            if (ex != null)
            {
                string str = "";
                string str3 = errMessage;
                errMessage = str3 + str + "发生时间：" + DateTime.Now.ToString() + "\r\n";
                string str4 = errMessage;
                errMessage = str4 + str + "所在机器名：" + Environment.MachineName + "\r\n";
                string str5 = errMessage;
                errMessage = str5 + str + "错误消息：" + ex.Message + "\r\n";
                string str6 = errMessage;
                errMessage = str6 + str + "错误来源：" + ex.Source + "\r\n";
                object obj3 = errMessage;
                errMessage = string.Concat(new object[] { obj3, str, "所在方法：", ex.TargetSite, "\r\n" });
                string str7 = errMessage;
                errMessage = str7 + str + "堆栈：" + ex.StackTrace + "\r\n";
            }
        }

        private static void InnerGetErrorDescription(Exception ex, ref string errMessage, string userName)
        {
            if (ex != null)
            {
                string str = "";
                errMessage = errMessage + "\r\n" + "操作人:" + userName +  "\r\n";
                string str3 = errMessage;
                errMessage = str3 + str + "发生时间：" + DateTime.Now.ToString() + "\r\n";
                string str4 = errMessage;
                errMessage = str4 + str + "所在机器名：" + Environment.MachineName + "\r\n";
                string str5 = errMessage;
                errMessage = str5 + str + "错误消息：" + ex.Message + "\r\n";
                string str6 = errMessage;
                errMessage = str6 + str + "错误来源：" + ex.Source + "\r\n";
                object obj3 = errMessage;
                errMessage = string.Concat(new object[] { obj3, str, "所在方法：", ex.TargetSite, "\r\n" });
                string str7 = errMessage;
                errMessage = str7 + str + "堆栈：" + ex.StackTrace + "\r\n";
            }
        }

    }
}
