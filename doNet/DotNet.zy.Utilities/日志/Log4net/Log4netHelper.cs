using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DotNet.zy.Utilities
{
    public class Log4netHelper
    {
        public static System.IO.FileInfo fin;
        private static log4net.ILog ErrorLog;

        static Log4netHelper()
        {
            loadfinbyFileInfo(null);
        }
        public static void loadfinbyFileInfo(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                string defaultLogPath = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
                fileInfo = new FileInfo(defaultLogPath);
            }
            log4net.Config.XmlConfigurator.Configure(fileInfo);
        }

        private static readonly object syncObject = new object();

        

        public static log4net.ILog GetErrorLog()
        {
            if (ErrorLog == null)
            {
                lock (syncObject)
                {
                    ErrorLog = log4net.LogManager.GetLogger("ErrorLog");
                }
            }
            return ErrorLog;
        }
    }
}
