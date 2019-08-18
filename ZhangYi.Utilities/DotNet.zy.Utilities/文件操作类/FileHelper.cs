using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Data;
using DotNet.zy.Utilities;
using System.Data.Odbc;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        private StreamReader sr;
        private StreamWriter sw;

        #region 向文本文件写入内容

        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            //向文件写入内容
            File.WriteAllText(filePath, text, encoding);
        }
        #endregion

        #region 向文本文件的尾部追加内容
        /// <summary>
        /// 向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }
        #endregion

        #region 保存文件到指定路径
        /// <summary>
        /// 保存文件到指定路径
        /// </summary>
        /// <param name="Path">文件物理路径</param>
        /// <param name="Text">写入内容</param>
        /// <param name="Coding">文件编码</param>
        public void SaveFile(string Path, string Text, string Coding)
        {
            if (File.Exists(Path))
            {
                DirFile.DeleteFile(Path);
            }
            try
            {
                sw = new StreamWriter(Path, false, Encoding.GetEncoding(Coding));
                sw.WriteLine(Text);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        #endregion

        #region 获取文件字节数组
        /// <summary>
        /// 获取文件字节数组
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="prEncoding">编码格式</param>
        /// <returns>字节数组</returns>
        public static byte[] GetExportFileByte(string strFilePath, params Encoding[] prEncoding)
        {
            Encoding encoding = Encoding.Default;
            if (prEncoding.Length > 0 && prEncoding[0] != null)
            {
                encoding = prEncoding[0];
            }
            byte[] buffer = File.ReadAllBytes(strFilePath);
            return buffer;
        }
        #endregion

        #region 将字节流信息写入指定路径文件
        /// <summary>
        /// 将字节流信息写入指定路径文件
        /// </summary>
        /// <param name="fs">流文件</param>
        /// <param name="path">文件夹路径</param>
        /// <param name="fileName">文件名</param>
        public void WriteStream(byte[] fs, string path, string fileName)
        {
            if (!(File.Exists(Path.GetDirectoryName(path))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            FileStream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

            MemoryStream memoryStream = new MemoryStream(fs);
            memoryStream.WriteTo(fileStream);
            memoryStream.Close();
            fileStream.Flush();
            fileStream.Close();
        }
        #endregion

        #region 将数据流信息写入指定路径文件
        /// <summary>
        /// 将数据流信息写入指定路径文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileStream">数据流信息</param>
        /// <param name="encoding">文件编码格式</param>
        /// <returns>操作状态 True：成功 False：失败</returns>
        public static bool WriteFile(string filePath, Stream fileStream, Encoding encoding)
        {
            bool bResult = true;

            if (!(File.Exists(Path.GetDirectoryName(filePath))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            if (null == encoding)
            {
                encoding = Encoding.Default;
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                try
                {
                    BinaryWriter writer = new BinaryWriter(fs, encoding);
                    BinaryReader reader = new BinaryReader(fileStream);

                    byte[] buffer;
                    do
                    {
                        buffer = reader.ReadBytes(1024);
                        writer.Write(buffer);
                    }
                    while (buffer.Length > 0);
                }
                finally
                {
                    fileStream.Close();
                    fs.Flush();
                    fs.Close();
                }
            }
            return bResult;
        }
        #endregion

        #region 将字符串写入指定路径文件

        /// <summary>
        /// 将字符串写入指定路径文件
        /// </summary>
        /// <param name="strDirectory">文件保存路径</param>
        /// <param name="strFileName">文件保存名称</param>
        /// <param name="strContext">指定字符串信息</param>
        /// <param name="encoding">文件编码格式</param>
        /// <returns>操作状态 True：成功 False：失败</returns>
        public static bool WriteFile(string strDirectory, string strFileName, string strContext, Encoding encoding)
        {
            bool bResult = true;
            StringBuilder sbContext = new StringBuilder();
            sbContext.Append(strContext);
            bResult = WriteFile(strDirectory, strFileName, sbContext, encoding);
            return bResult;
        }

        /// <summary>
        /// 将字符串写入指定路径文件
        /// </summary>
        /// <param name="strDirectory">文件保存路径</param>
        /// <param name="strFileName">文件保存名称</param>
        /// <param name="sbContext">指定字符串信息</param>
        /// <param name="encoding">文件编码格式</param>
        /// <returns>操作状态 True：成功 False：失败</returns>
        public static bool WriteFile(string strDirectory, string strFileName, StringBuilder sbContext, Encoding encoding)
        {
            bool bResult = true;

            string strPath = Path.Combine(strDirectory, strFileName);
            if (!Path.HasExtension(strPath))
            {
                strPath = Path.ChangeExtension(strPath, ".txt");
            }
            if (!Directory.Exists(strDirectory))
            {
                Directory.CreateDirectory(strDirectory);
            }
            if (null == encoding)
            {
                encoding = Encoding.Default;
            }

            FileStream stream = null;
            StreamWriter objWriter = null;
            try
            {
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }
                stream = File.Open(strPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                objWriter = new StreamWriter(stream, encoding);
                objWriter.Write(sbContext.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (null != objWriter)
                {
                    objWriter.Flush();
                    objWriter.Close();
                }
                if (null != stream)
                {
                    stream.Flush();
                    stream.Close();
                }
            }
            return bResult;
        }
        #endregion


        #region 将字符串内容导出成TXT文件到Web
        /// <summary>
        /// 将字符串内容导出成TXT文件
        /// </summary>
        /// <param name="strFileName">文件名称</param>
        /// <param name="strContent">数据内容</param>
        public static void ExportStringToFile(string strFileName, string strContent)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                InitializeExporting(strFileName, "text/plain");
                byte[] buffer = Encoding.Default.GetBytes(strContent);
                HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #region 导出文件至Web通用方法
        /// <summary>
        /// 导出文件至Web通用方法
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="strCntType">输出格式</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="isNeedDelete">下载以后是否要删除</param>
        /// <returns></returns>
        public static bool ExportCommonFile(string strFilePath, string strCntType, string FileName = "", bool isNeedDelete = true)
        {
            if (!(File.Exists(strFilePath)))
            {
                return false;
            }

            byte[] buffer = File.ReadAllBytes(strFilePath);
            if (isNeedDelete)
            {
                File.Delete(strFilePath);
            }

            if (FileName.Trim().Length > 0)
            {
                InitializeExporting(FileName, strCntType);
            }
            else
            {
                InitializeExporting(Path.GetFileName(strFilePath), strCntType);
            }
            HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

            return true;
        }
        #endregion



        #region 读取文件 ***************

        /// <summary>
        /// 读文件内容到StringBuilder
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public StringBuilder ReadFile(string filepath)
        {
            StringBuilder Sb = new StringBuilder();
            StreamReader sr = new StreamReader(filepath, Encoding.UTF8);
            string str = "";
            while ((str = sr.ReadLine()) != null)
            {
                Sb.Append(str + "\n");
            }
            sr.Close();
            return Sb;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="Coding">文件编码</param>
        /// <returns></returns>
        public string ReadFile(string filepath, string Coding = "gb2312")
        {
            string str = "";
            if (File.Exists(filepath))
            {
                try
                {
                    sr = new StreamReader(filepath, Encoding.GetEncoding(Coding));
                    str = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
            return str;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<string> ReadFile2(string filepath, string sEncoding = "GB2312")
        {
            List<string> msgList = null;
            StreamReader sr = null;
            try
            {
                if (!Directory.Exists(filepath))
                {
                    return null;
                }

                if (!File.Exists(filepath))
                {
                    return null;
                }

                msgList = new List<string>();
                sr = new StreamReader(filepath, Encoding.GetEncoding(sEncoding));
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    msgList.Add(line);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return msgList;
        }

        #endregion ~~~~~~~~~~~~~~~~~~~~~~~



        #region 文件导出时HTTP头初始化操作
        /// <summary>
        /// 文件导出时HTTP头初始化操作
        /// </summary>
        /// <param name="strFileName">文件名称</param>
        /// <param name="strCntType">导出格式</param>
        public static void InitializeExporting(string strFileName, string strCntType)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();

            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie") > -1)
            {
                strFileName = FileNameEncode(strFileName);
            }

            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + strFileName + "\"");
            }
            else
            {
                response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            }

            HttpContext.Current.Response.ContentType = strCntType;
        }
        #endregion

        #region 修复文件名编码 
        /// <summary>
        /// 修复文件名编码
        /// 编码非US-ASCII字符的字符串.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FileNameEncode(string strFileName)
        {
            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie") > -1)
            {
                char[] chars = strFileName.ToCharArray();
                StringBuilder builder = new StringBuilder();
                for (int index = 0; index < chars.Length; index++)
                {
                    bool needToEncode = NeedToEncode(chars[index]);
                    if (needToEncode)
                    {
                        string encodedString = ToHexString(chars[index]);
                        builder.Append(encodedString);
                    }
                    else
                    {
                        builder.Append(chars[index]);
                    }
                }
                return builder.ToString();
            }
            return strFileName;
        }

        /// <summary>
        /// 确定该字符需要被编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 编码一个非US-ASCII字符.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }

            return builder.ToString();
        }

        #endregion


        #region 图片保存到数据库
        //方法1
        /// <summary>
        /// 图片保存到数据库
        /// </summary>
        /// <param name="sql"></param>
        public void SavePicToDB(string sql)
        {
            FileStream fs = File.OpenRead("路径");
            byte[] imageb = new byte[fs.Length];
            fs.Read(imageb, 0, imageb.Length);
            fs.Close();
            SqlCommand com3 = new SqlCommand();
            com3.Parameters.Add("@images", SqlDbType.Image).Value = imageb;
            try
            {
                com3.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            { com3.Connection.Close(); }
        }

        //方法2
        /// <summary>
        /// 图片保存到数据库
        /// </summary>
        /// <param name="UP_FILE1"></param>
        public void SavePicToDB(System.Web.UI.WebControls.FileUpload UP_FILE1)
        {
            HttpPostedFile UpFile = UP_FILE1.PostedFile;  //HttpPostedFile对象，用于读取图象 文件属性 
            Int64 FileLength = UpFile.InputStream.Length; //记录文件长度变量 
            try
            {
                if (FileLength == 0)
                {   //文件长度为零时 
                    JsHelper.MsgBox("请选择图片");
                }
                else
                {
                    Byte[] FileByteArray = new Byte[FileLength];   //图象文件临时储存Byte数组 
                    Stream StreamObject = UpFile.InputStream;      //建立数据流对像 
                    //读取图象文件数据，FileByteArray为数据储存体，0为数据指针位置、FileLnegth为数据长度 
                    StreamObject.Read(FileByteArray, 0, UpFile.ContentLength); //建立SQL Server链接 

                    SqlHelper.SqlCheckIsExists("SQL语句");
                    JsHelper.MsgBox("你已经成功上传你的图片");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将图片转换成流文件输出
        /// <summary>
        /// 将图片转换成流文件输出
        /// </summary>
        /// <param name="path">物理路径</param>
        /// <param name="path">允许的最大输出大小（单位：M）</param>
        /// <returns></returns>
        public byte[] ImageToStream(string path)
        {
            try
            {
                FileStream fs = File.OpenRead(path);

                byte[] imageb = new byte[fs.Length];
                fs.Read(imageb, 0, imageb.Length);
                fs.Close();
                return imageb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 数据库中读出图片
        /// <summary>
        /// 数据库中读出图片并显示在picturebox中
        /// </summary>
        /// <param name="sql"></param>
        private void ShowImage(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            byte[] b = (byte[])cmd.ExecuteScalar();
            if (b.Length > 0)
            {
                MemoryStream stream = new MemoryStream(b, true);
                stream.Write(b, 0, b.Length);
                //pictureBox1.Image = new Bitmap(stream);
                stream.Close();
            }
        }
        #endregion


        #region 从文本文件中读取数据到DataTable中(大家统一用这个进行)
        /// <summary>
        /// 从文本文件中读取数据到DataTable中(大家统一用这个进行)
        /// </summary>
        /// <param name="iFullPath">文件路径</param>
        /// <param name="iFileName">文件名称</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static IDataReader GetDataFromTxtFile(string iFullPath, string iFileName, string strSql)
        {
            try
            {
                NewLife.Log.XTrace.WriteLine(string.Format("iFullPath:{0}iFileName:{1} ", iFullPath, iFileName));
                OdbcConnection mOleConn = OpenOdbcConn(iFullPath);

                if (null != mOleConn)
                {
                    OdbcCommand mOleCmd = new OdbcCommand(strSql, mOleConn);
                    OdbcDataReader mOleReader = mOleCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return mOleReader;
                }
                else
                {
                    NewLife.Log.XTrace.WriteLine("OdbcConnection连接失败");
                }
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteLine("GetDataFromTxtFile方法发生异常");
                NewLife.Log.XTrace.WriteLine(ex.ToString());
                throw ex;
            }

            return null;
        }
        #endregion

        #region ODBC驱动链接

        private static string connString = string.Empty;
        public static OdbcConnection OpenOdbcConn(string iFullPath)
        {

            OdbcConnection mOleConn = null;
            mOleConn = OpenIssOdbcConn(iFullPath);

            if (mOleConn.State == ConnectionState.Open)
            {
                return mOleConn;
            }
            else
            {
                mOleConn = null;
                mOleConn = OpenLocalOdbcConn(iFullPath);

                if (mOleConn.State == ConnectionState.Open)
                {
                    return mOleConn;
                }
            }

            return null;
        }

        private static OdbcConnection OpenIssOdbcConn(string iFullPath)
        {
            OdbcConnection odbcConn = null;
            try
            {
                //IIS connect string
                connString = @"Driver={Microsoft Access Text Driver (*.txt, *.csv)};Extensions=asc,csv,tab,txt;Persist Security Info=False;Dbq=" + iFullPath.Trim();

                odbcConn = new OdbcConnection(connString.Trim());
                odbcConn.Open();
            }
            catch
            {
            }

            return odbcConn;
        }

        private static OdbcConnection OpenLocalOdbcConn(string iFullPath)
        {
            OdbcConnection odbcConn = null;
            try
            {
                //local host connect string
                connString = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Extensions=asc,csv,tab,txt;Persist Security Info=False;Dbq=" + iFullPath.Trim();

                odbcConn = new OdbcConnection(connString.Trim());
                odbcConn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return odbcConn;
        }

        #endregion

        #region 将DataReader数据导入数据库
        /// <summary>
        /// 将DataReader数据导入数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="dr"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static bool SqlBulkCopyDataToDb(string connectionString, string tableName, IDataReader dr,Dictionary<string,string > dictionary )
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlTransaction mTran = conn.BeginTransaction())
                {
                    SqlBulkCopy mSqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, mTran);

                    mSqlBulkCopy.DestinationTableName = tableName;

                    mSqlBulkCopy.BulkCopyTimeout = 1000;

                    foreach (KeyValuePair<string, string> kvp in dictionary)
                    {
                        mSqlBulkCopy.ColumnMappings.Add(kvp.Key, kvp.Value);
                    }

                    try
                    {
                        // 写入数据
                        mSqlBulkCopy.WriteToServer(dr);

                        // 提交数据
                        mTran.Commit();
                    }
                    catch (Exception ex)
                    {
                        // 回滚数据
                        mTran.Rollback();
                        return false;
                    }
                    finally
                    {
                        dr.Close();
                        mSqlBulkCopy.Close();
                    }
                }
            }

            return true;
        }
        #endregion

        #region 将DataTable批量导入到数据库
        /// <summary>
        /// 将DataTable批量导入到数据库
        /// </summary>
        /// <param name="db">待导入的数据</param>
        public bool ImportData(string connectionString, string tableName, DataTable dt)
        {
            try
            {
                if (dt == null)
                {
                    return true;
                }

                //修复为为空的数据不能正常导入
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (string.IsNullOrWhiteSpace(dt.Rows[i][j].ToString()))
                        {
                            dt.Rows[i][j] = DBNull.Value;
                        }
                    }
                }

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                using (SqlTransaction tran = sqlConn.BeginTransaction())
                {
                    // 批量保存数据，只能用于Sql
                    SqlBulkCopy sqlbulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.Default, tran);
                    // 设置源表名称
                    sqlbulkCopy.DestinationTableName = tableName;
                    // 设置超时限制
                    sqlbulkCopy.BulkCopyTimeout = 2000;
                    sqlbulkCopy.BatchSize = 10;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlbulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }

                    try
                    {
                        // 写入
                        sqlbulkCopy.WriteToServer(dt);
                        // 提交事务
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        sqlbulkCopy.Close();
                        return false;
                    }
                    finally
                    {
                        sqlbulkCopy.Close();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.Write("批量导入到数据库时发生错误");
                LogWriter.Write(ex.Message);
                throw ex;
            }
        }
        #endregion

    }
}
