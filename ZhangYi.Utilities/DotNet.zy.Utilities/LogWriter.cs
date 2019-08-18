using System;
using System.Configuration;
using System.IO;
using System.Text;
using DotNet.zy.Utilities;

namespace DotNet.zy.Utilities
{
    public class LogWriter
    {
        /// <summary>
        /// 日志记录地址
        /// </summary>
        private static string logPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ErrorLogPath"];

        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="info"></param>
        public static void Write(Exception ex)
        {
            Write(ex.Message);
            Write(ex.StackTrace);
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

            Write(trace.GetFrame(trace.FrameCount - 1).GetMethod().Name);
            Write(ConvertHelper.CastString(trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()));
            Write(ConvertHelper.CastString(trace.GetFrame(trace.FrameCount - 1).GetFileColumnNumber()));
        }


        /// <summary>
        /// 写文本
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static void Write(string info)
        {
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string filePath = Path.Combine(logPath, string.Format("{0:yyyyMMdd}.log", DateTime.Now));

                info = string.Format("[{0}]\r\n {1}", DateTime.Now.ToString(), info);

                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fs);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.WriteLine(info);
                streamWriter.Flush();
                streamWriter.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取文本
        /// </summary>
        /// <returns></returns>
        public static string Read()
        {
            string result = string.Empty;

            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string filePath = Path.Combine(logPath, string.Format("{0:yyyyMMdd}.log", DateTime.Now));

                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamReader streamReader = new StreamReader(fs);
                result = streamReader.ReadLine();
                fs.Close();

            }
            catch { }

            return result;
        }

    }
}
