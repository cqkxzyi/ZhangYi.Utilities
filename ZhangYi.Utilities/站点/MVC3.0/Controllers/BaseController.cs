using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3._0.Controllers
{
    public class BaseController : Controller
    {

        #region MVC方式文件下载
        /// <summary>
        /// MVC方式文件下载
        /// </summary>
        /// <param name="path">文件所在路径</param>
        /// <param name="fileName">输出文件名称</param>
        /// <returns></returns>
        public FileResult DownloadTemplate(string path, string fileName)
        {
            string contentType = "image/jpeg";
            return File(path + fileName, contentType, fileName);
        }

        /// <summary>
        /// MVC方式文件下载
        /// 还有这几种方式：FilePathResult、FileStreamResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileContentResult GetFile(int id)
        {
            SqlDataReader rdr; byte[] fileContent = null;
            string mimeType = ""; string fileName = "";
            const string connect = @"Server=.\SQLExpress;Database=FileTest;Trusted_Connection=True;";
            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT FileContent, MimeType, FileName FROM FileStore WHERE ID = @ID";
                var cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fileContent = (byte[])rdr["FileContent"];
                    mimeType = rdr["MimeType"].ToString();
                    fileName = rdr["FileName"].ToString();
                }
            }
            return File(fileContent, mimeType, fileName);
        }  

        #endregion

    }

    public static class SelectListItemHelp
    {

        #region 返回绑定dropdownlist的列表
        /// <summary>
        /// 返回绑定dropdownlist的列表 
        /// </summary>
        /// <param name="selectListItem"></param>
        /// <param name="ListType">所需要的类型</param>
        /// <param name="selectID"></param>
        /// <returns></returns>
        public static List<SelectListItem> SetSelectListData(this SelectListItem selectListItem, int ListType, int selectID, string strWhere = "")
        {
            List<SelectListItem> CategorySelect = new List<SelectListItem>();
            Type week;
            Array Arrays;
            switch (ListType)
            {
                case 3://新闻管理栏目类型
                    week = typeof(MyEnum.Category);
                    Arrays = Enum.GetValues(week);
                    for (int i = 0; i < Arrays.LongLength; i++)
                    {
                        CategorySelect.Add(new SelectListItem
                        {
                            //Selected = selectID == Convert.ToInt32(Arrays.GetValue(i)) ? true : false,
                            Text = ((MyEnum.Category)Arrays.GetValue(i)).ToString(),
                            Value = "" + Convert.ToInt32((MyEnum.Category)Arrays.GetValue(i))
                        });
                    }
                    break;
            }
            return CategorySelect;
        }

        public static List<SelectListItem> SetSelectListData(int ListType, int selectID)
        {
            return (new SelectListItem()).SetSelectListData(ListType, selectID);
        }
        #endregion
    }

}
