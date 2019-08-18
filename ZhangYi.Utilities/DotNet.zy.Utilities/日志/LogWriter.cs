using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace DotNet.zy.Utilities
{
    public class LogWriter
    {
        /// <summary>
        /// 日志记录地址
        /// </summary>
        private static string G_LogPath = ConfigurationManager.AppSettings["LogPath"] ?? "/LogError";

        public static object lockobj = new object();

        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="path">保存路径</param>
        public static void WriteError(Exception ex, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = G_LogPath;

            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

            StringBuilder errorStr = new StringBuilder();
            errorStr.Append("\r\n异常信息：" + ex.Message);
            errorStr.Append("\r\n" + "异常对象：" + ex.Source);
            errorStr.Append("\r\n" + "调用堆栈：" + ex.StackTrace.Trim());
            errorStr.Append("\r\n" + "触发方法：" + ex.TargetSite);
            errorStr.Append("\r\n" + trace.GetFrame(trace.FrameCount - 1).GetMethod().Name);
            errorStr.Append("\r\n" + trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber().ToString());
            errorStr.Append("\r\n" + trace.GetFrame(trace.FrameCount - 1).GetFileColumnNumber().ToString());

            Write(errorStr.ToString(), path);
        }


        /// <summary>
        /// 写文本日志
        /// </summary>
        /// <param name="info">文本信息</param>
        /// <param name="path">报错路劲</param>
        /// <returns></returns>
        public static void Write(string info, string path = "")
        {
            try
            {
                lock (lockobj)
                {
                    if (string.IsNullOrEmpty(path))
                        path = G_LogPath;

                    path = AppDomain.CurrentDomain.BaseDirectory + path;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filePath = Path.Combine(path, string.Format("{0:yyyyMMdd}.log", DateTime.Now));

                    info = string.Format("[{0}]\r\n {1}", DateTime.Now.ToString(), info);

                    FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                    StreamWriter streamWriter = new StreamWriter(fs);
                    streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                    streamWriter.WriteLine(info);
                    streamWriter.Flush();
                    streamWriter.Close();
                    fs.Close();
                }
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
            lock (lockobj)
            {
                string result = string.Empty;
                try
                {
                    if (!Directory.Exists(G_LogPath))
                    {
                        Directory.CreateDirectory(G_LogPath);
                    }

                    string filePath = Path.Combine(G_LogPath, string.Format("{0:yyyyMMdd}.log", DateTime.Now));

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
}
