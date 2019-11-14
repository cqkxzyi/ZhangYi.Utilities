using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.DataColumn; 
using System.Windows.Forms;
using System.Data.SqlClient;

public class 数据表操作
{

    /// <summary>
    /// Datatable.ImportRow(DataRow())的方法复制一行数据到表中
    /// </summary>
    public void 数据表()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("name", System.Type.GetType("System.String"));
        //另外一种添加列 dt.Columns.Add("名称",System.Type.GetType("System.String")) ; DataColumn Title = new DataColumn("Title", typeof(string));
        dt.Columns.Add(dc);
        for (int i = 0; i < 10; i++)
        {
            DataRow dr = dt.NewRow();
            dr["name"] = "aaa";
            dt.Rows.Add(dr);
        }
        DataTable dt2 = new DataTable();
        DataColumn dc2 = new DataColumn("name", System.Type.GetType("System.String"));
        dt2.Columns.Add(dc2);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt2.ImportRow(dt.Rows[i]);
        }
        MessageBox.Show(dt.Rows.Count.ToString());
        MessageBox.Show(dt2.Rows.Count.ToString());
    }
    //1.DataTable.Select()，数据筛选
    //2.DataTable.Clone()，复制表结构
    //3.DataTable .Copy()，复制datatable结构和数据
    //4.DataTable.ImportRow()，将特定的DataRow对象从一个表复制到另一个表

    DataColumn column = new DataColumn();
    column.ColumnName="序号";
    column.AutoIncrement=true;
    column.AutoIncrementSeed=1;
    column.AutoIncrementStep=1;
    table.Columns.Add(column);

    // 两张表相互融合成一张表
    DataTable dt2 = new DataTable();
    dt2.Merge(dt) ;

}

//============================================-=======DataTable操作===================================
//对DataTable排序 DefaultView.Sort
//判断是否为空 table.Rows[row][column]== DBNull.Value
//复制datat的框架   DataTable dt = da.Clone();
//datarow集合 DataRow[] dRows    dRows[行].ItemArray[列].ToString()
//DataRow单行 DataRow Row        row[列].tostring();
//获取列头 列头=table.Columns[i].Caption
//获取光标，使之成为沙漏形状
//Windows.Forms.Cursor.Current = Cursors.WaitCursor 