using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// 针对asp.net上传图片
/// </summary>
public class ImageUpLoadAsp
{
    #region 生成唯一文件名称
   /// <summary>
    /// 生成唯一文件名称
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string SetFileName(string path)
    {
        //生成唯一的文件名称
        Random Rnd = new Random();
        int strRnd = Rnd.Next(1, 10);
        string datatime = System.DateTime.Now.ToString("yyyyMMddHHmmssffff")+strRnd;
        datatime += System.IO.Path.GetExtension(path);
        return datatime;
    }

    public static string SetFileNamelast(System.Web.UI.WebControls.FileUpload myFile)
    {
        string temp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
        temp += DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()+"1";
        temp += System.IO.Path.GetExtension(myFile.PostedFile.FileName);
        return temp;
    }
   #endregion

    #region 上传图片(传递控件)
    /// <summary>
    /// 上传图片(传递控件)
    /// </summary>
    /// <param name="myFile">控件名称</param>
    /// <param name="myFileName">文件名称</param>
    /// <param name="myFileSize">文件大小(M)</param>
    /// <param name="myFileType">允许上传的文件类别</param>
    /// <param name="mySaveDirectory">文件保存路径</param>
    /// <param name="file_fg">如果文件存在是否覆盖，TRUE覆盖，FALSE不覆盖</param>
    public static  bool myUpFile(System.Web.UI.WebControls.FileUpload myFile, string myFileName, int myFileSize, string myFileType, string mySaveDirectory, string file_fg)
    {
            try
            {
                //判断文件是否存在或有数据
                if (myFile.PostedFile.ContentLength <= 0)
                {
                    JsHelper.MsgBox("请选择您要上传的图片！");
                    return false;
                }

                //判断文件是否超过限制
                if (myFile.PostedFile.ContentLength / 1048576d > myFileSize)
                {
                    JsHelper.MsgBox("上传的文件不能超过 " + myFileSize.ToString() + "M！");
                    return false;
                }

                //判断文件类别是否允许上传
                string myType = System.IO.Path.GetExtension(myFile.PostedFile.FileName).Substring(1).ToLower();
                if (myFileType.IndexOf(myType) < 0)
                {
                    JsHelper.MsgBox("对不起，只允许 " + myFileType + " 类别的文件上传！");
                    return false;
                }

                //判断文件夹是否存在
                if (!Directory.Exists(mySaveDirectory))
                {
                    Directory.CreateDirectory(mySaveDirectory);
                }

                //获取文件物理路径
                string mySaveFile = mySaveDirectory + myFileName;
                //判断文件是否存在
                if (System.IO.File.Exists(mySaveFile))
                {
                    if (file_fg == "FALSE")
                    {
                        JsHelper.MsgBox("文件已经存在，请选择其它要上传的文件！");
                        return false;
                    }
                }
                myFile.PostedFile.SaveAs(mySaveFile);
                return true;
            }
            catch (Exception ex)
            {
                JsHelper.MsgBox(ex.Message);
                return false;
            }
    }
    #endregion

    #region 上传图片(传递页面)
    /// <summary>
    /// 上传图片(传递页面)
    /// </summary>
    /// <param name="Size">文件实际大小</param>
    /// <param name="myFileName">文件名称</param>
    /// <param name="myFileSize">允许文件大小</param>
    /// <param name="myFileType">允许上传的文件类别</param>
    /// <param name="mySaveDirectory">文件保存路径</param>
    /// <param name="file_fg">如果文件存在是否覆盖，TRUE覆盖，FALSE不覆盖</param>
    /// <param name="page">当前页面</param>
    public static bool myUpFile_ToPage2(long Size, string myFileName, int myFileSize, string myFileType, string mySaveDirectory, string file_fg, Page page)
    {
        try
        {
            //判断文件是否存在或有数据
            if (Size <= 0)
            {
                JsHelper.PageMsgBox("请选择您要上传的图片！", page);
                return false;
            }

            //判断文件是否超过限制
            if (Size / 1048576d > myFileSize)
            {
                JsHelper.PageMsgBox("上传的文件不能超过 " + myFileSize.ToString() + "M ！", page);
                return false;
            }

            //判断文件类别是否允许上传
            string myType = System.IO.Path.GetExtension(myFileName).Substring(1).ToLower();
            if (myFileType.IndexOf(myType) < 0)
            {
                JsHelper.PageMsgBox("对不起，只允许 " + myFileType + " 类别的文件上传！", page);
                return false;
            }

            //判断文件夹是否存在
            if (!Directory.Exists(mySaveDirectory))
            {
                Directory.CreateDirectory(mySaveDirectory);
            }

            //获取文件物理路径
            string mySaveFile = mySaveDirectory + myFileName;
            //判断文件是否存在
            if (System.IO.File.Exists(mySaveFile))
            {
                if (file_fg == "FALSE")
                {
                    JsHelper.PageMsgBox("文件已经存在，请选择其它要上传的文件！", page);
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            JsHelper.MsgBox(ex.Message);
            return false;
        }
    }
    #endregion

    #region 上传图片(传递控件 页面)
    /// <summary>
    /// 上传图片(传递控件 页面)
    /// </summary>
    /// <param name="myFile">控件名称</param>
    /// <param name="myFileName">文件名称</param>
    /// <param name="myFileSize">允许文件大小(M)</param>
    /// <param name="myFileType">允许上传的文件类别</param>
    /// <param name="mySaveDirectory">文件保存路径</param>
    /// <param name="file_fg">如果文件存在是否覆盖，TRUE覆盖，FALSE不覆盖</param>
    /// <param name="page">当前页面</param>
    public static bool myUpFile_ToPage(System.Web.UI.WebControls.FileUpload myFile, string myFileName, int myFileSize, string myFileType, string mySaveDirectory, string file_fg, Page page)
    {
        try
        {
            //判断文件是否存在或有数据
            if (myFile.PostedFile.ContentLength <= 0)
            {
                JsHelper.PageMsgBox("请选择您要上传的图片！", page);
                return false;
            }

            //判断文件是否超过限制
            if (myFile.PostedFile.ContentLength / 1048576d > myFileSize)
            {
                JsHelper.PageMsgBox("上传的文件不能超过 " + myFileSize.ToString() + "M ！", page);
                return false;
            }

            //判断文件类别是否允许上传
            string myType = System.IO.Path.GetExtension(myFile.PostedFile.FileName).Substring(1).ToLower();
            if (myFileType.IndexOf(myType) < 0)
            {
                JsHelper.PageMsgBox("对不起，只允许 " + myFileType + " 类别的文件上传！", page);
                return false;
            }

            //判断文件夹是否存在
            if (!Directory.Exists(mySaveDirectory))
            {
                Directory.CreateDirectory(mySaveDirectory);
            }

            //获取文件物理路径
            string mySaveFile = mySaveDirectory + myFileName;
            //判断文件是否存在
            if (System.IO.File.Exists(mySaveFile))
            {
                if (file_fg == "FALSE")
                {
                    JsHelper.PageMsgBox("文件已经存在，请选择其它要上传的文件！", page);
                    return false;
                }
            }
            myFile.PostedFile.SaveAs(mySaveFile);
            return true;
        }
        catch (Exception ex)
        {
            JsHelper.MsgBox(ex.Message);
            return false;
        }
    }
    #endregion

    #region 将临时文件夹的文件移动到新的文件夹
    /// <summary>
    /// 将临时文件夹的文件移动到新的文件夹
    /// </summary>
    /// <param name="FirstPath">临时文件夹路径</param>
    /// <param name="LastPath">目的文件夹路径</param>
    /// <param name="MoveFile">移动的文件名</param>
    public static void myMoveFile(string FirstPath, string LastPath, string MoveFile)
    {
        string First_Path = System.Web.HttpContext.Current.Server.MapPath(FirstPath + MoveFile);
        string Last_Path = System.Web.HttpContext.Current.Server.MapPath(LastPath + MoveFile);
        if (System.IO.File.Exists(First_Path))
        {
            System.IO.File.Move(First_Path, Last_Path);
            System.IO.File.Delete(First_Path);
        }
    }
   #endregion
}
