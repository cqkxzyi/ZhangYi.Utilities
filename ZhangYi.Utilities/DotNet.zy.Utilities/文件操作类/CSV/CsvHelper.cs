using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// CSV文件转换类
    /// </summary>
    public class CsvHelper
    {
        #region DataTable转换成Csv保存至文件
        /// <summary>
        /// DataTable转换成Csv保存至文件
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">物理路径</param>
        /// <param name="tableheader">表头</param>
        /// <param name="columname">字段标题,逗号分隔</param>
        public static bool dt2csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                string strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, Encoding.UTF8);
                strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columname);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            strBufferLine += ",";
                        }
                        strBufferLine += dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DataTable导出CSV格式至Web
        /// <summary>
        /// DataTable导出CSV格式至Web
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="filename">文件名称</param>
        /// <param name="header">标题文本</param>
        /// <param name="footer">结尾文本</param>
        /// <returns></returns>
        public static void ExportCSV(DataTable dt, string filename, string header = "", string footer = "")
        {
            StringBuilder str = new StringBuilder();

            if (!string.IsNullOrEmpty(header))
            {
                str.Append(header);
                str.Append(Environment.NewLine);
                str.Append(Environment.NewLine);
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str.AppendFormat("{0},", dt.Columns[i].ColumnName);
            }
            if (str.Length > 1)
            {
                str = str.Remove(str.Length - 1, 1);
            }

            str.Append(Environment.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    str.AppendFormat("{0},", string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                }
                if (str.Length > 1)
                {
                    str = str.Remove(str.Length - 1, 1);
                }
                str.Append(Environment.NewLine);
            }

            if (!string.IsNullOrEmpty(footer))
            {
                str.Append(Environment.NewLine);
                str.Append(footer);
                str.Append(Environment.NewLine);
            }

            //方式1
            //byte[] strByt = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            //byte[] outBuffer = new byte[strByt.Length + 3];
            ////有BOM,解决乱码
            //outBuffer[0] = (byte)0xEF;
            //outBuffer[1] = (byte)0xBB;
            //outBuffer[2] = (byte)0xBF;
            //Array.Copy(strByt, 0, outBuffer, 3, strByt.Length);
            //return File(outBuffer, "application/ms-excel");

            //方式2
            FileHelper.InitializeExporting(filename, "text/csv");
            byte[] buffer = System.Text.Encoding.Default.GetBytes(str.ToString());
            System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        #endregion

        #region 将字符串导出为CVS格式到Web
        /// <summary>
        /// 将字符串导出为CVS格式到Web
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        public static void ExportCSV(string data, string filename)
        {
            StringBuilder str = new StringBuilder();
            data = data.Replace("\r\n", "");
            data = data.Replace("{+}", ",");
            data = data.Replace("{-}", Environment.NewLine);

            //方式1
            //byte[] strByt = System.Text.Encoding.UTF8.GetBytes(data);
            //byte[] outBuffer = new byte[strByt.Length + 3];
            ////有BOM,解决乱码
            //outBuffer[0] = (byte)0xEF;
            //outBuffer[1] = (byte)0xBB;
            //outBuffer[2] = (byte)0xBF;
            //Array.Copy(strByt, 0, outBuffer, 3, strByt.Length);
            //return File(outBuffer, "application/ms-excel");

            //方式2
            FileHelper.InitializeExporting(filename, "text/csv");
            byte[] buffer = System.Text.Encoding.Default.GetBytes(str.ToString());
            System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        #endregion

        #region Csv转换成DataTable
        /// <summary>
        /// Csv转换成DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable csv2dt(string filePath, int n, DataTable dt)
        {
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8, false);
            int i = 0, m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    System.Data.DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion

        #region 读取CSV文件
        /// <summary>
        /// 读取CSV文件
        /// </summary>
        public static void OpenExcel()
        {
            FileStream fs = new FileStream("d:\\Customer.csv", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding(936));
            string str = "";
            string s = "1";

            while (str != null)
            {
                str = sr.ReadLine();
                string[] xu = new string[2];
                xu = str.Split(',');
                string ser = xu[0];
                string dse = xu[1]; if (ser == s)
                {
                    //Console.WriteLine(dse); 
                    break;
                }
            }
            sr.Close();
        }
        #endregion
    }
}
