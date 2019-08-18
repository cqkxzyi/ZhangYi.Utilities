using System;
using System.Web;
using System.Configuration;
using System.IO;
using System.Web.Configuration;
using System.Globalization;

namespace DotNet.zy.Utilities
{
    public class UploadFileHelper
    {
        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="upImg">文件流</param>
        /// <param name="maxSize">限制图片最大值kb</param>
        /// <param name="maxWidth">像素最大宽度</param>
        /// <param name="maxHeight">像素最大高度</param>
        /// <param name="isThumbnail">是否生成缩略图(1：生成等比缩放的；2：不生成；3:生成非等比缩放的)</param>
        /// <param name="targetWidth">缩略后的宽度</param>
        /// <param name="targetHeight">缩略后的高度</param>
        /// <returns></returns>
        public static string UploadImg(ref string errorStr, HttpPostedFileBase upImg, int maxSize = 1000000, int maxWidth = 100000, int maxHeight = 100000, int isThumbnail = 2, double targetWidth = 50, double targetHeight = 50)
        {
            if (upImg == null || upImg.ContentLength == 0)
            {
                return "";
            }

            //验证文件格式
            string allowedImage = "gif,img,imge";
            if (!CheckFileType(upImg, allowedImage))
            {
                errorStr = "图片格式不正确！";
                return "";
            }

            //验证文件大小
            int ImageMaxSize = 2048;
            if (!CheckFileSize(upImg, ImageMaxSize))
            {
                errorStr = "上传图片大小不能超过" + ImageMaxSize + "KB！";
                return "";
            }

            //验证图片宽高
            if (!CheckImgWidthAndHeight(upImg, maxWidth, maxHeight))
            {
                errorStr = "图片格式有误，请上传[" + maxWidth + "*" + maxHeight + "]以内的图片！";
                return "";
            }

            //后缀
            string extName = Path.GetExtension(upImg.FileName);
            //文件名称
            string fileName = GetNewFileName() + extName;

            //文件保存Web相对路劲
            string dirWebPath = "/upload/";
            //文件保存物理绝对路劲
            string dirAbsolutePath = DirFile.GetServerPath(dirWebPath);

            try
            {
                //正式上传
                Upload(upImg, dirAbsolutePath, fileName);

                //缩略图保存路径
                string ThumbnailAbsolutePath = dirAbsolutePath + "Thumbnail/";
                if (isThumbnail == 1)
                {
                    //生成缩略图片
                    PTImage.ImageZoomAuto(dirAbsolutePath + fileName, ThumbnailAbsolutePath, fileName, targetWidth, targetHeight);
                }
                else if (isThumbnail == 3)
                {
                    PTImage.Compress(dirAbsolutePath + fileName, ThumbnailAbsolutePath, (int)targetWidth, (int)targetHeight);
                }

                return dirWebPath + fileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="errorStr">错误信息</param>
        /// <param name="file">流文件</param>
        /// <param name="FileType">允许的文件类型</param>
        /// <param name="FileMaxSize">允许的最大值</param>
        /// <param name="strSaveFileWebPath">文件保存Web相对路劲</param>
        /// <param name="strSaveFileName">文件保存名称</param>
        /// <returns></returns>
        public static string UploadFile(ref string errorStr, HttpPostedFileBase file, string FileType, long FileMaxSize, string strSaveFileWebPath, string strSaveFileName = "")
        {
            if (file == null || file.ContentLength == 0)
            {
                return "";
            }

            //验证文件格式
            if (!CheckFileType(file, FileType))
            {
                errorStr = "文件格式不正确！";
                return "";
            }

            //验证文件大小

            if (!CheckFileSize(file, FileMaxSize))
            {
                errorStr = "上传文件大小不能超过" + FileMaxSize + "KB！";
                return "";
            }

            //后缀
            string extName = Path.GetExtension(file.FileName);
            //文件名称
            if (strSaveFileName == "")
            {
                strSaveFileName = GetNewFileName() + "$$" + file.FileName;
            }

            //文件保存物理绝对路劲
            string dirAbsolutePath = DirFile.GetServerPath(strSaveFileWebPath);

            try
            {
                //正式上传
                Upload(file, dirAbsolutePath, strSaveFileName);

                return strSaveFileWebPath + strSaveFileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 验证文件格式
        /// <summary>
        /// 验证文件格式
        /// </summary>
        /// <returns></returns>
        private static bool CheckFileType(HttpPostedFileBase file, string fileType)
        {
            bool bIsImage = true;
            string filePath = file.FileName.ToLower();
            string extendName = filePath.Substring(filePath.LastIndexOf('.') + 1, filePath.Length - filePath.LastIndexOf('.') - 1);
            if (!fileType.Contains(extendName))
            {
                bIsImage = false;
            }
            return bIsImage;
        }
        #endregion

        #region 验证文件大小
        /// <summary>
        /// 验证文件大小
        /// </summary>
        /// <param name="file"></param>
        /// <param name="maxSize">文件最大值(单位：Kb)</param>
        /// <returns></returns>
        private static bool CheckFileSize(HttpPostedFileBase file, long maxSize)
        {
            if (file.ContentLength > maxSize * 1024)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 验证图片宽高
        /// <summary>
        /// 验证图片宽高
        /// </summary>
        private static bool CheckImgWidthAndHeight(HttpPostedFileBase file, int maxWidth, int maxHeight)
        {
            //验证图片宽高
            System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);//实例化image类型

            int width = image.Width;
            int height = image.Height;

            if (width > maxWidth || height > maxHeight)
            {
                return false;
            }
            return true;
        }
        #endregion




        #region 获得一个新的文件名称
        /// <summary>
        /// 获得一个新的文件名称
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + new Random().Next(1000, 9999);
        }
        #endregion

        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filebyte">文件流</param>
        /// <param name="TypePath">文件目录</param>
        /// <param name="FileName">文件名称</param>
        /// <returns></returns>
        public static string Upload(HttpPostedFileBase filebyte, string FilePath, string FileName)
        {
            if (filebyte.ContentLength == 0)
            {
                return "";
            }
            //判断是否是正常文件
            string FileExt = Path.GetExtension(filebyte.FileName);
            if (FileExt.IndexOf(".") < 0)
            {
                return "";
            }

            DirFile.CreateDir(FilePath);

            //保存图片到服务器
            filebyte.SaveAs(FilePath + FileName);//这种方式没有问题

            //HttpPostedFileWrapper hw = (HttpPostedFileWrapper)filebyte;
            //hw.InputStream.Read()

            //获取字节流
            //int fileLength = (int)filebyte.ContentLength;
            //byte[] Img_j = new byte[fileLength];
            //filebyte.InputStream.Read(Img_j, 0, fileLength);
            //filebyte.InputStream.Close();

            //Stream stream = filebyte.InputStream;
            //stream.Read(Img_j, 0, fileLength);
            //stream.Position = 0;

            //StreamReader reader = new StreamReader(filebyte.InputStream);
            //BinaryReader r = new BinaryReader(filebyte.InputStream); 
            //r.Read(Img_j, 0, fileLength);
            //r.Close();

            //保存图片到服务器
            //FileManage.WriteStream(Img_j, FileName, FilePath);
            return FilePath + FileName;
        }
        #endregion
    }
}
