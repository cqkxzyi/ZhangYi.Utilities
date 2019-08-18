using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;

class DataBind
{

    #region 绑定服务器数据控件 简单绑定DataList
    /// <summary>
    /// 简单绑定DataList
    /// </summary>
    /// <param name="ctrl">控件ID</param>
    /// <param name="mydv">数据视图</param>
    public static void BindDataList(Control ctrl, DataView mydv)
    {
        ((DataList)ctrl).DataSourceID = null;
        ((DataList)ctrl).DataSource = mydv;
        ((DataList)ctrl).DataBind();
    }
    #endregion

    #region 绑定服务器数据控件 SqlDataReader简单绑定DataList
    /// <summary>
    /// SqlDataReader简单绑定DataList
    /// </summary>
    /// <param name="ctrl">控件ID</param>
    /// <param name="mydv">数据视图</param>
    public static void BindDataReaderList(Control ctrl, SqlDataReader mydv)
    {
        ((DataList)ctrl).DataSourceID = null;
        ((DataList)ctrl).DataSource = mydv;
        ((DataList)ctrl).DataBind();
    }
    #endregion

    #region 绑定服务器数据控件 简单绑定GridView
    /// <summary>
    /// 简单绑定GridView
    /// </summary>
    /// <param name="ctrl">控件ID</param>
    /// <param name="mydv">数据视图</param>
    public static void BindGridView(Control ctrl, DataView mydv)
    {
        ((GridView)ctrl).DataSourceID = null;
        ((GridView)ctrl).DataSource = mydv;
        ((GridView)ctrl).DataBind();
    }
    #endregion

    #region 绑定服务器控件 简单绑定Repeater
    /// <summary>
    /// 绑定服务器控件 简单绑定Repeater
    /// </summary>
    /// <param name="ctrl">控件ID</param>
    /// <param name="mydv">数据视图</param>
    public static void BindRepeater(Control ctrl, DataView mydv)
    {
        ((Repeater)ctrl).DataSourceID = null;
        ((Repeater)ctrl).DataSource = mydv;
        ((Repeater)ctrl).DataBind();
    }
    #endregion

    #region 为GridView绑定数据
    /// <summary>
    /// 为GridView绑定数据,数据来源为DataSet；
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="GV"></param>
    public static void BindDataToGV(DataSet ds, GridView GV)
    {
        GV.DataSource = ds.Tables[0];
        GV.DataBind();
    }

    /// <summary>
    /// 为GridView绑定数据,数据来源为DataTable；
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="GV"></param>
    public static void BindDataToGV(DataTable dt, GridView GV)
    {
        GV.DataSource = dt;
        GV.DataBind();
    }
    #endregion

    #region 动态改变GridView中的当前行颜色
    /// <summary>
    /// 让光标移动到行上的时候改变当前行颜色
    /// </summary>
    /// <param name="MyE"></param>
    /// <param name="MyId"></param>
    /// <param name="MyColor"></param>
    public static void AlternatingColor(System.Web.UI.WebControls.GridViewRowEventArgs e, GridView GV)
    {
        //使鼠标到达的行和列显示不同的颜色
        if ((e.Row.RowState == DataControlRowState.Normal && e.Row.RowType == DataControlRowType.DataRow) ||
            (e.Row.RowState == DataControlRowState.Alternate && e.Row.RowType == DataControlRowType.DataRow))
        {
            e.Row.Attributes.Add("onmouseover", "this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#C8F7FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldcolor");
            for (int i = 0; i < GV.Columns.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("onmouseover", "this.oldcolor=this.style.backgroundColor;this.style.backgroundColor='#99ccff'");
                e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldcolor");
            }
        }
    }
    #endregion

    #region GridView中删除列的通用代码
    /// <summary>
    /// GridView中删除列的通用代码
    /// </summary>
    /// <param name="e"></param>
    /// <param name="GV">GridView的名称</param>
    /// <param name="ds">数据集</param>
    /// <param name="FieldName">主键字段名</param>
    public static void GVRowDelete(GridViewDeleteEventArgs e, GridView GV, DataSet ds, String FieldName)
    {
        int index = e.RowIndex;
        string sid = GV.DataKeys[e.RowIndex].Value.ToString();
        ds.Tables[0].PrimaryKey = new DataColumn[]
        {
            ds.Tables[0].Columns[FieldName]        
        };
        DataRow myrow = ds.Tables[0].Rows.Find(sid);
        ds.Tables[0].Rows.Remove(myrow);
    }
    #endregion

    #region  对GridView各字段进行排序
    public static void GVSorting(DataView DV, GridView GV, GridViewSortEventArgs e, string flag)
    {
        //
        if (flag == "ASC")
        {
            DV.Sort = e.SortExpression.ToString() + "  DESC";
        }
        else
        {
            DV.Sort = e.SortExpression.ToString() + "  ASC";
        }
        GV.DataSource = DV;
        GV.DataBind();
    }
    #endregion

    #region GridView分页通用方法
    /// <summary>
    /// GridView分页通用方法
    /// </summary>
    /// <param name="GV"></param>
    /// <param name="e"></param>
    /// <param name="ds"></param>
    public static void GVPageIndex(GridView GV, GridViewPageEventArgs e, DataSet ds)
    {
        GV.PageIndex = e.NewPageIndex;
        BindDataToGV(ds, GV);
    }
    #endregion

}