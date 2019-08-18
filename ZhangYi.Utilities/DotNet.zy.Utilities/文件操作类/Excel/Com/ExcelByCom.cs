using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 操作Excel(COM组件Microsoft Excel 11.0的方式)
    /// </summary>
    public class ExcelByCom
    {
        #region 打开Excel文件
        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="strFileName"></param>
        private void OpenExcel(string strFileName)
        {
            object missing = System.Reflection.Missing.Value;
            Application excel = new Application();
            if (excel != null)
            {
                excel.Visible = false; excel.UserControl = true;
                // 以只读的形式打开EXCEL文件
                Workbook wb = excel.Application.Workbooks.Open(strFileName, missing, true, missing, missing, missing,
                                              missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄
                Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
                excel.Quit();
                excel = null;

                Process[] procs = Process.GetProcessesByName("excel");
                foreach (Process pro in procs)
                {
                    pro.Kill();//没有更好的方法,只有杀掉进程
                }
                GC.Collect();
            }
        }
        #endregion


        public void 各种属性设置()
        {
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Add(Missing.Value);
            Worksheet sheet = (Worksheet)wb.Worksheets.get_Item(1);

            sheet.Rows[1].WrapText = false;
            //sheet.Rows(1).WrapText  = false;  //自动换行设置   
            //sheet.Rows(1).Font.Size=18;//设置第一行的字体大小   
            //sheet.Rows(1).Interior.ColorIndex=2;//设置第一行背景色    
            //sheet.Rows(1).Font.ColorIndex=1;//设置第一行字体色      
            //sheet.Range(sheet.Cells(1,1),sheet.Cells(1,7)).mergecells=true;//第一行1到7单元格合并   
            //sheet.Columns("A").ColumnWidth = 35;//设置列宽   
            //sheet.Columns("B").ColumnWidth = 35;
            //sheet.Columns("A:B").ColumnWidth =35;//另一种设置列宽的方式   
            //sheet.Rows(1).RowHeight = 35;//设置行高   
            //sheet.Rows(1).Font.Name="黑体";//设置字体   
            //sheet.Columns.AutoFit;//所有列自适应宽度   
            //水平对齐方式(貌似-4108为水平居中)   
            //sheet.Range( sheet.Cells(1,1),sheet.Cells(1,5)).HorizontalAlignment =-4108;   
            //垂直对齐方式   
            //sheet.Range( sheet.Cells(1,1),sheet.Cells(1,5)).VerticalAlignment  =-4108;   
            //根据Borders()中参数值设置各个方向边距，1,2,3,4--->top,buttom,left,right   
            //sheet.Range( sheet.Cells(2,1),sheet.Cells(1,5)).Borders(1).Weight = 2; 
        }

    }
}
