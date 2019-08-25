using DotNet.zy.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public static class WinFormExpand
{
    #region 添加行号
    /// <summary>
    /// 添加行号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
        DataGridView dgv = (DataGridView)sender;

        Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);

        TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, rectangle,
            dgv.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
    }
    #endregion

    #region 添加全选

    /// <summary>  
    /// DataGridView添加全选  
    /// </summary>  
    /// <param name="dgv">DataGridView控件ID</param>   
    public static void AddFullSelect(this DataGridView dgv, int leftPoint = 65)
    {
        bool ishave = dgv.Columns.Contains("dgv_check");

        if (!ishave)
        {
            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.ReadOnly = false;
            newColumn.Name = "dgv_check";
            newColumn.HeaderText = "";
            newColumn.Width = 60;
            newColumn.Resizable = DataGridViewTriState.False;
            dgv.Columns.Insert(0, newColumn);

            Rectangle rect = dgv.GetCellDisplayRectangle(-1, -1, true);
            Point point = new Point(rect.X + leftPoint, rect.Y + 3);

            CheckBox ckBox = new CheckBox();
            //ckBox.Size = new Size(dgv.Columns[columnIndex+1].Width - 12, 12); //大小
            ckBox.Name = "dgv_allcheck";
            ckBox.Size = new Size(13, 13);
            ckBox.Location = point;                                           //位置
            ckBox.CheckedChanged += delegate(object sender, EventArgs e)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells[0].Value = ((CheckBox)sender).Checked;
                }
                dgv.EndEdit();
            };

            dgv.Controls.Add(ckBox);
        }
    }
    #endregion

    #region  组合查询条件
    /// <summary>
    /// 根据控件是否为空组合查询条件.
    /// </summary>
    /// <param name="GBox">GroupBox控件的数据集</param>
    /// <param name="TName">获取信息控件的部份名称</param>
    /// <param name="TName">查询关系</param>
    public static void Find_Grids(Control.ControlCollection GBox, string TName, string ANDSign)
    {
        string FindValue = "";
        string sID = "";    //定义局部变量
        if (FindValue.Length > 0)
            FindValue = FindValue + ANDSign;
        foreach (Control C in GBox)
        { //遍历控件集上的所有控件
            if (C.GetType().Name == "TextBox" | C.GetType().Name == "ComboBox")
            { //判断是否要遍历的控件
                if (C.GetType().Name == "ComboBox" && C.Text != "")
                {   //当指定控件不为空时
                    sID = C.Name;
                    if (sID.IndexOf(TName) > -1)
                    {    //当TName参数是当前控件名中的部分信息时
                        string[] Astr = sID.Split(Convert.ToChar('_')); //用“_”符号分隔当前控件的名称,获取相应的字段名
                        FindValue = FindValue + "(" + Astr[1] + " = '" + C.Text + "')" + ANDSign;   //生成查询条件
                    }
                }
                if (C.GetType().Name == "TextBox" && C.Text != "")  //如果当前为TextBox控件，并且控件不为空
                {
                    sID = C.Name;   //获取当前控件的名称
                    if (sID.IndexOf(TName) > -1)    //判断TName参数值是否为当前控件名的子字符串
                    {
                        string[] Astr = sID.Split(Convert.ToChar('_')); //以“_”为分隔符，将控件名存入到一维数组中
                        string m_Sgin = ""; //用于记录逻辑运算符
                        string mID = "";    //用于记录字段名
                        if (Astr.Length > 2)    //当数组的元素个数大于2时
                            mID = Astr[1] + "_" + Astr[2];  //将最后两个元素组成字段名
                        else
                            mID = Astr[1];  //获取当前条件所对应的字段名称
                        foreach (Control C1 in GBox)    //遍历控件集
                        {
                            if (C1.GetType().Name == "ComboBox")    //判断是否为ComboBox组件
                                if ((C1.Name).IndexOf(mID) > -1)    //判断当前组件名是否包含条件组件的部分文件名
                                {
                                    if (C1.Text == "")  //当查询条件为空时
                                        break;  //退出本次循环
                                    else
                                    {
                                        m_Sgin = C1.Text;   //将条件值存储到m_Sgin变量中
                                        break;
                                    }
                                }
                        }
                        if (m_Sgin != "")   //当该务件不为空时
                            FindValue = FindValue + "(" + mID + m_Sgin + C.Text + ")" + ANDSign;    //组合SQL语句的查询条件
                    }
                }
            }
        }
        if (FindValue.Length > 0)   //当存储查询条的变量不为空时，删除逻辑运算符AND和OR
        {
            if (FindValue.IndexOf("AND") > -1)  //判断是否用AND连接条件
                FindValue = FindValue.Substring(0, FindValue.Length - 4);
            if (FindValue.IndexOf("OR") > -1)  //判断是否用OR连接条件
                FindValue = FindValue.Substring(0, FindValue.Length - 3);
        }
        else
            FindValue = "";

    }
    #endregion

    #region 遍历控件
    /// <summary>
    /// 遍历控件
    /// </summary>
    /// <param name="fControl"></param>
    public static void FindControl(Control fControl)
    {
        foreach (Control child in fControl.Controls)
        {
            if (child.GetType() == typeof(Button))
            {

            }
            if (child.Controls.Count > 0)
            {
                FindControl(child);
            }
        }
    }
    #endregion

    #region 把DataGridView的数据复制到DataTable
    /// <summary>
    /// 把DataGridView的数据复制到DataTable
    /// </summary>
    /// <param name="dgv">dgv控件作为参数</param>
    /// <returns>返回临时内存表</returns>
    public static DataTable GetDgvToTable(DataGridView dgv)
    {
        DataTable dt = new DataTable();
        for (int count = 0; count < dgv.Columns.Count; count++)
        {
            DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
            dt.Columns.Add(dc);
        }

        for (int count = 0; count < dgv.Rows.Count; count++)
        {
            if (dgv.Rows[count].IsNewRow == false)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dr[i] = ConvertHelper.CastString(dgv.Rows[count].Cells[i].Value);
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    #endregion

}
