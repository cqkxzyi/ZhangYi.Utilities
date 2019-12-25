using MvcObject.FilterAttribute;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcObject.Controllers
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


        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //return;


            if (filterContext.IsChildAction)
            {
                return;
            }

            bool isAllowAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Length > 0;
            if (isAllowAnonymous)
            {
                return;
            }

            //1角色信息
            var roles = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthUserRoleAttribute), false);
            string[] roleArray = null;
            string actionName = null;
            string controllerName = null;
            if (roles.Length > 0)
            {
                var attributeInfo = ((AuthUserRoleAttribute)(roles)[0]);
                roleArray = attributeInfo.Roles.Split(',');
                actionName = attributeInfo.ActionName;
                controllerName = attributeInfo.ControllerName;
            }

            //2用户信息
            object[] authUsers = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeAttribute), false);


            //未登录直接跳转到登陆页
            //登录成功配置FormsAuthentication.SetAuthCookie("登录唯一标识", true);
            if (User.Identity.IsAuthenticated == false)
            {
                filterContext.Result = new RedirectResult(string.Format("{0}?redirectUrl={1}", FormsAuthentication.LoginUrl, HttpUtility.UrlEncode(filterContext.RequestContext.HttpContext.Request.Path)));
            }
            else
            {
                //登录标识
                var userPhone = User.Identity.Name;

                if (string.IsNullOrEmpty(userPhone))
                {
                    filterContext.Result = new RedirectResult(string.Format("{0}?redirectUrl={1}", FormsAuthentication.LoginUrl, HttpUtility.UrlEncode(filterContext.RequestContext.HttpContext.Request.Path)));
                }
                else
                {
                    //根据登录者userPhone查询所属角色
                    string role = string.Empty;

                    //1判断角色
                    if (roleArray != null && roleArray.Length > 0)
                    {
                        if (!roleArray.Contains(role))
                        {
                            filterContext.Result = RedirectToAction(actionName, controllerName);
                        }
                    }

                    //2判断用户
                    if (authUsers.Length > 0 && ((AuthorizeAttribute)authUsers[0]).Users.Contains(userPhone))
                    {
                        //允许访问
                    }
                    else {
                        filterContext.Result = RedirectToAction(actionName, controllerName);
                    }

                }
                return;
            }
        }



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
