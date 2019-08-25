using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DotNet.zy.Utilities;

namespace WebFrom
{
   /// <summary>
   /// Handler1 的摘要说明
   /// </summary>
   public class Handler1 : IHttpHandler
   {

      public void ProcessRequest(HttpContext context)
      {
         context.Response.ContentType = "text/plain";
         string url = context.Request.Url.ToString();
         url = HttpUtility.UrlDecode(url);

         string postType = context.Request["postType"] as string;

         switch (postType)
         {
            case "reg": Reg(context); break;
            case "login": Login(context); break;
            case "userList": userList(context); break;
            case "AddUser": AddUser(context); break;
            case "EditUser": EditUser(context); break;
            case "DeleteUser": DeleteUser(context); break;
            case "getTree": getTree(context); break;
            default: Error(context); break; ;
         }
      }

      #region 注册
      public void Reg(HttpContext context)
      {
         try
         {
            string userName = context.Request.QueryString["userName"];
            string retnJson = "{\"name\":\"" + userName + "\",\"success\":true}";
            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      #region 登录
      public void Login(HttpContext context)
      {
         try
         {
            string userName = context.Request.QueryString["userName"];
            string pws = context.Request.QueryString["pwd"];
            string retnJson = "{\"name\":\"" + userName + "\",\"success\":true}";
            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      #region 获取用户列表
      
      public void userList(HttpContext context)
      {
         try
         {
            int page = ConvertHelper.CastInt(context.Request["page"]);
            int rows =ConvertHelper.CastInt( context.Request["rows"]);
            string sortName = context.Request["sort"];
            string sortOrder = context.Request["order"];

            string strSort = "";
            if (!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder))
            {
               strSort = sortName + " " + sortOrder;
            }

            string retnJson = JsonHelper1.GetJsonForEasyuiDatagrid(page, rows, "id", "", strSort, "Bus_User");

            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      #region 添加
      public void AddUser(HttpContext context)
      {
         try
         {
            string userName = context.Request.QueryString["userName"];
            string pws = context.Request.QueryString["pwd"];
            userName = "添加成功";
            string retnJson = "{\"msg\":\"" + userName + "\",\"success\":true}";
            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      #region 编辑
      public void EditUser(HttpContext context)
      {
         try
         {
            string userName = context.Request.QueryString["userName"];
            string pws = context.Request.QueryString["pwd"];
            userName = "编辑成功";
            string retnJson = "{\"msg\":\"" + userName + "\",\"success\":true}";
            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      #region 删除
      public void DeleteUser(HttpContext context)
      {
         try
         {
            string userName = context.Request.QueryString["userName"];
            string pws = context.Request.QueryString["pwd"];
            userName = "删除成功";
            string retnJson = "{\"msg\":\"" + userName + "\",\"success\":true}";
            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      #region 获取tree列表
      public void getTree(HttpContext context)
      {
         try
         {
            string id = context.Request["id"];
            string retnJson="";
            if (string.IsNullOrEmpty(id))
            {
               retnJson = "[{\"text\":\"城市列表\",\"state\":\"open\",\"children\":[";
               DataTable db = SqlHelper.GetSqlDataTable("select * from Base_Area where parentID=0");
               for (int i = 0; i < db.Rows.Count; i++)
               {
                  retnJson += "{\"id\":" + db.Rows[i]["id"].ToString() + ",\"text\":\"" + db.Rows[i]["Name"].ToString() + "\",\"state\":\"closed\"},";
               }
               retnJson = retnJson.Substring(0, retnJson.Length-1);
               retnJson += "]}]";
            }
            else
            {
               DataTable db = SqlHelper.GetSqlDataTable("select * from Base_Area where parentID="+id);
               retnJson = "[";
               for (int i = 0; i < db.Rows.Count; i++)
               {
                  retnJson += "{\"id\":" + db.Rows[i]["id"].ToString() + ",\"text\":\"" + db.Rows[i]["Name"].ToString() + "\",\"state\":\"closed\"},";
               }
               retnJson = retnJson.Substring(0, retnJson.Length - 1);
               retnJson+="]";
            }
            
            context.Response.Write(retnJson);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion


      #region 参数有问题

      public void Error(HttpContext context)
      {
         try
         {
            context.Response.Write("postType 错误，请从新编码！");
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      #endregion


      public bool IsReusable
      {
         get
         {
            return false;
         }
      }

   }
}