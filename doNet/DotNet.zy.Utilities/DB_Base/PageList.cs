using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

    /// <summary>
    /// PageList 的摘要说明
    /// </summary>
    public class PageList
    {
        /// <summary>
        ///  分页函数,执行SP_Pagination3分页操作
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="strGetFields">需要返回的列</param>
        /// <param name="fldName">排序的字段名</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strwhere">查询条件(注意: 不要加where)</param>
        /// <returns></returns>
        public static DataTable GetDataTableOfRow_Number(string tblName, string strGetFields, string fldName, int PageSize, int PageIndex, string strwhere)
        {

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@tblName", tblName), //表名
                new SqlParameter("@strGetFields", strGetFields), //需要返回的列
                new SqlParameter("@fldName", fldName),    //排序的字段名
                new SqlParameter("@PageSize", PageSize),  //页尺寸
                new SqlParameter("@PageIndex", PageIndex),//页码 
                new SqlParameter("@strwhere", strwhere), //查询条件(注意: 不要加where)
            };

            //SqlParameter parm1 = new SqlParameter("@WebName", SqlDbType.Image);
            //parm1.Value = imageb;


            return SqlHelper2.ExecuteTable(CommandType.StoredProcedure, "SP_Pagination3", param);
        }

        /// <summary>
        /// 分页数据库操作函数
        /// </summary>
        /// <param name="strwhere">查询条件(注意: 不要加where)</param>
        /// <param name="tblName">表名</param>
        /// <param name="OrderType">设置排序类别,非 0 值则降序</param>
        /// <param name="fldName">排序的字段名</param>
        /// <param name="strGetFields">需要返回的列</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="doCount">,[0:返回查询的表数据,非0:值则返回记录总数]</param>
        /// <returns>返回一个表</returns>
        public static DataTable GetDataTable(string strwhere, string tblName, int OrderType, string fldName, string strGetFields, int PageSize, int PageIndex, int doCount)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@strwhere", strwhere), //查询条件(注意: 不要加where)
                new SqlParameter("@tblName", tblName), //表名
                new SqlParameter("@OrderType", OrderType), //设置排序类别,非 0 值则降序
                new SqlParameter("@fldName", fldName),   //排序的字段名
                new SqlParameter("@strGetFields", strGetFields), //需要返回的列
                new SqlParameter("@PageSize", PageSize),     //页尺寸
                new SqlParameter("@PageIndex", PageIndex),     //页码 
                new SqlParameter("@doCount", doCount)     //返回记录总数,非 0 值则返回 
            };
            return SqlHelper2.ExecuteTable(CommandType.StoredProcedure, "SP_Pagination", param);
        }


        /// <summary>
        /// 分页导航获取总页数
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <param name="tblName">表、视图</param>
        /// <returns>Total：字段名（总的记录数）</returns>
        public static DataTable GetDataTable(string strwhere, string tblName)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@strwhere", strwhere), //查询条件(注意: 不要加where)
                new SqlParameter("@tblName", tblName), //表名
                new SqlParameter("@doCount", 1)     //返回记录总数,非 0 值则返回 
            };
            return SqlHelper2.ExecuteTable(CommandType.StoredProcedure, "SP_Pagination", param);
        }


        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="page">要连接的页</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="Url">链接路径</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="tblName">要查询的表、视图</param>
        /// <returns>返回分页导航栏</returns>
        public static string GoToPager(int page, int pageSize, string Url, string strWhere, string tblName)
        {
            if (tblName == null) goto Err;
            DataTable tb = GetDataTable(strWhere, tblName);
            int Count = Convert.ToInt32(tb.Rows[0]["Total"]);  //取得总的记录数
            StringBuilder strHtml = new StringBuilder();
            int prevPage = page - 1;
            int nextPage = page + 1;
            int startPage;
            int pageCount = (int)Math.Ceiling((double)Count / pageSize);
            //strHtml.Append(@"总记录：");
            //strHtml.Append(Count);
            //strHtml.Append(@"&nbsp;&nbsp页码：");
            //strHtml.Append(page);
            //strHtml.Append(@"/");      
            //strHtml.Append(pageCount);
            //strHtml.Append(@"&nbsp;&nbsp;  ");
            //strHtml.Append(@"");
            if (prevPage < 1)
            {
                strHtml.Append("首页&nbsp;");
                strHtml.Append("上一页&nbsp;");
            }
            else
            {
                strHtml.Append(@"<a href='" + Url + "=1'>首页</a>&nbsp;");
                strHtml.Append(@"<a href='" + Url + "=" + prevPage + "'>上一页</a>&nbsp;");
            }
            if (page % 10 == 0)
            {
                startPage = page - 9;
            }
            else
            {
                startPage = page - page % 10 + 1;
            }
            if (startPage > 10)
            {
                strHtml.Append(@"<a href='");
                strHtml.Append(Url);
                strHtml.Append(@"=");
                strHtml.Append(startPage - 1);
                strHtml.Append(@"'>【←前10页</a>");
            }
            for (int i = startPage; i < startPage + 10; i++)
            {
                if (i > pageCount) break;
                if (i == page)
                {
                    strHtml.Append(@" <span class=""page_b"">" + i + "</span>");
                }
                else
                {
                    strHtml.Append(@" <a href='" + Url + "=" + i + "'>" + i + "</a> ");
                }
            }
            if (pageCount >= startPage + 10) strHtml.Append(@"<a href='" + Url + "=" + (startPage + 10) + "'>后10页→】</a>");
            if (nextPage > pageCount)
            {
                strHtml.Append(@"&nbsp;下一页&nbsp;");
                strHtml.Append(@"末页&nbsp;");
            }
            else
            {
                strHtml.Append(@"&nbsp;<a href='" + Url + "=" + nextPage + "'>下一页</a>&nbsp;");
                strHtml.Append(@"<a href='" + Url + "=" + pageCount + "'>末页</a>&nbsp;");
            }
            return strHtml.ToString();
        Err:
            return "缺少数据表或视图";
        }


        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="page">要连接的页</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="tblName">要查询的表、视图</param>
        /// <returns>返回分页导航栏</returns>
        public static string GoToPager(int page, int pageSize, string strWhere, string tblName)
        {
            if (tblName == null) goto Err;
            DataTable tb = GetDataTable(strWhere, tblName);
            int Count = Convert.ToInt32(tb.Rows[0]["Total"]);  //取得总的记录数
            StringBuilder strHtml = new StringBuilder();
            int prevPage = page - 1;
            int nextPage = page + 1;
            int startPage;
            int pageCount = (int)Math.Ceiling((double)Count / pageSize);
            strHtml.Append(@"总记录：");
            strHtml.Append(Count);
            strHtml.Append(@"&nbsp;&nbsp页码：");
            strHtml.Append(page);
            strHtml.Append(@"/");
            strHtml.Append(pageCount);
            strHtml.Append(@"&nbsp;&nbsp;  ");
            strHtml.Append(@"");
            if (prevPage < 1)
            {
                strHtml.Append("首页&nbsp;");
                strHtml.Append("上一页&nbsp;");
            }
            else
            {
                strHtml.Append(@"<span class='ddee' onclick=""redirection('1','tt')"">首页</span>&nbsp;");
                strHtml.Append(@"<span class='ddee' onclick=""redirection('"+ prevPage +"','tt')\">上一页</span>&nbsp;");
            }
            if (page % 10 == 0)
            {
                startPage = page - 9;
            }
            else
            {
                startPage = page - page % 10 + 1;
            }
            if (startPage > 10)
            {
                strHtml.Append(@"<span onclick=""redirection('" + (startPage - 1) + "','tt')\">【←前10页</span>&nbsp;");
                //strHtml.Append(@"<a href='");
                //strHtml.Append(Url);
                //strHtml.Append(@"=");
                //strHtml.Append(startPage - 1);
                //strHtml.Append(@"'>【←前10页</a>");
            }
            for (int i = startPage; i < startPage + 10; i++)
            {
                if (i > pageCount) break;
                if (i == page)
                {
                    strHtml.Append(@"<font color='#ff0000'>[" + i + "]</font>&nbsp;");
                }
                else
                {
                    strHtml.Append(@"<span class='ddee' onclick=""redirection('" + i + " ','tt')\">[" + i + "]</span>&nbsp;");
                    //strHtml.Append(@" <a href='" + Url + "=" + i + "'>[" + i + "]</a> ");
                }
            }
            if (pageCount >= startPage + 10)
                strHtml.Append(@"<span onclick=""redirection('" + (startPage + 10) + " ','tt')\">后10页→】</span>&nbsp;");
                //strHtml.Append(@"<a href='" + Url + "=" + (startPage + 10) + "'>后10页→】</a>");
            if (nextPage > pageCount)
            {
                strHtml.Append(@"&nbsp;下一页&nbsp;");
                strHtml.Append(@"末页&nbsp;");
            }
            else
            {
                strHtml.Append(@"<span class='ddee' onclick=""redirection('" + nextPage + " ','tt')\">下一页</span>&nbsp;");
                //strHtml.Append(@"&nbsp;<a href='" + Url + "=" + nextPage + "'>下一页</a>&nbsp;");
                strHtml.Append(@"<span class='ddee' onclick=""redirection('" + pageCount + " ','tt')\">末页</span>&nbsp;");
                //strHtml.Append(@"<a href='" + Url + "=" + pageCount + "'>末页</a>&nbsp;");
            }
            return strHtml.ToString();
        Err:
            return "缺少数据表或视图";
        }
    }