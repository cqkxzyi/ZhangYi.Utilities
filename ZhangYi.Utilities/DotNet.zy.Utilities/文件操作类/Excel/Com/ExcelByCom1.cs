using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace DotNet.zy.Utilities
{
    #region 测试例子
    /// <summary>
    /// 测试例子
    /// </summary>
    class test
    {
        static void TestMain()
        {
            ExcelByCom1 mexc = new ExcelByCom1();
            mexc.CreateExceFile();
            mexc.insertRow(1);//注意从1开始的   
            int col = (256 * 256 * 255) + (256 * 192) + 192;//颜色   
            mexc.SetValue(1, 1, "张三", new System.Drawing.Font("Arial", 18), col, "center");//设置单元格内容和样式
            mexc.MergeCell(1, 1, 4, 1);//合并单元格   

            for (int i = 2; i <= 101; i++)
            {
                mexc.insertRow(i);//插入行   
                for (int j = 1; j <= 4; j++)//注意从1开始的   
                {
                    mexc.SetValue(i, j, i + "--" + j);//设置单元格内容   
                }
            }

            mexc.SaveAs("demo.xls");//保存文件的文件名 
            mexc.KillSpecialExcel();  //关闭Excel进程
        }
    }
    #endregion

    /// <summary>
    /// 操作Excel(COM组件Microsoft Excel 11.0的方式)
    /// </summary>
    public class ExcelByCom1
    {
        #region field字段
        /// <summary>
        /// 忽略的参数OLENULL
        /// </summary>
        private object miss = Missing.Value;    
        /// <summary>
        /// Excel应用程序实例
        /// </summary>
        private Application m_objExcel;   
        /// <summary>
        /// 工作表集合
        /// </summary>
        private Workbooks m_objBooks;
        /// <summary>
        /// 当前操作的工作表
        /// </summary>
        private Workbook m_objBook;
        /// <summary>
        /// 当前操作的表格
        /// </summary>
        private Worksheet sheet;  
        #endregion

        #region property属性
        /// <summary>
        /// 当前操作的表格
        /// </summary>
        public Worksheet CurrentSheet
        {
            get
            {
                return sheet;
            }
            set
            {
                this.sheet = value;
            }
        }
        /// <summary>
        /// 工作表集合
        /// </summary>
        public Workbooks CurrentWorkBooks
        {
            get
            {
                return this.m_objBooks;
            }
            set
            {
                this.m_objBooks = value;
            }
        }
        /// <summary>
        /// 当前操作的工作表
        /// </summary>
        public Workbook CurrentWorkBook
        {
            get
            {
                return this.m_objBook;
            }
            set
            {
                this.m_objBook = value;
            }
        }

        #endregion

        #region 列标号，Excel最大列数是256
        /// <summary>   
        /// 列标号，Excel最大列数是256   
        /// </summary>   
        private string[] ALists = new string[] {   
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",   
            "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",   
            "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",   
            "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",   
            "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ",   
            "EA", "EB", "EC", "ED", "EE", "EF", "EG", "EH", "EI", "EJ", "EK", "EL", "EM", "EN", "EO", "EP", "EQ", "ER", "ES", "ET", "EU", "EV", "EW", "EX", "EY", "EZ",   
            "FA", "FB", "FC", "FD", "FE", "FF", "FG", "FH", "FI", "FJ", "FK", "FL", "FM", "FN", "FO", "FP", "FQ", "FR", "FS", "FT", "FU", "FV", "FW", "FX", "FY", "FZ",   
            "GA", "GB", "GC", "GD", "GE", "GF", "GG", "GH", "GI", "GJ", "GK", "GL", "GM", "GN", "GO", "GP", "GQ", "GR", "GS", "GT", "GU", "GV", "GW", "GX", "GY", "GZ",   
            "HA", "HB", "HC", "HD", "HE", "HF", "HG", "HH", "HI", "HJ", "HK", "HL", "HM", "HN", "HO", "HP", "HQ", "HR", "HS", "HT", "HU", "HV", "HW", "HX", "HY", "HZ",   
            "IA", "IB", "IC", "ID", "IE", "IF", "IG", "IH", "II", "IJ", "IK", "IL", "IM", "IN", "IO", "IP", "IQ", "IR", "IS", "IT", "IU", "IV"   
        };
        #endregion

        #region construct构造函数

        /// <summary>   
        /// 构建ExcelClass类   
        /// </summary>   
        public ExcelByCom1()
        {  
            this.m_objExcel = new Application();
        }
        /// <summary>   
        /// 构建ExcelClass类   
        /// </summary>   
        /// <param name="objExcel">Excel.Application</param>   
        public ExcelByCom1(Application objExcel)
        {
            this.m_objExcel = objExcel;
        }

        #endregion


        #region 给单元格赋值 ******************

        /// <summary>   
        /// 给单元格赋值1   
        /// </summary>
        /// <param name="x">列号</param>   
        /// <param name="y">行号</param> 
        /// <param name="align">对齐（CENTER、LEFT、RIGHT）</param>   
        /// <param name="text">值</param>   
        public void SetValue(int x, int y, string align, string text)
        {
            Range range = sheet.get_Range(this.GetAix(x, y), miss);
            range.set_Value(miss, text);
            if (align.ToUpper() == "CENTER")
            {
                range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }
            if (align.ToUpper() == "LEFT")
            {
                range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            }
            if (align.ToUpper() == "RIGHT")
            {
                range.HorizontalAlignment = XlHAlign.xlHAlignRight;
            }
        }

        /// <summary>   
        /// 给单元格赋值2   
        /// </summary>
        /// <param name="x">列号</param>   
        /// <param name="y">行号</param>   
        /// <param name="text">值</param>   
        public void SetValue(int x, int y, string text)
        {
            Range range = sheet.get_Range(this.GetAix(x, y), miss);
            range.set_Value(miss, text);
            range.EntireColumn.AutoFit(); //设置单元格自适应宽度  

            //设置单元格边框  
            range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, null);

        }

        /// <summary>   
        /// 给单元格赋值3   
        /// </summary>
        /// <param name="x">列号</param>   
        /// <param name="y">行号</param>    
        /// <param name="text">值</param>   
        /// <param name="font">字符格式</param>   
        /// <param name="color">颜色</param>   
        public void setValue(int x, int y, string text, System.Drawing.Font font, int color)
        {
            this.SetValue(x, y, text);
            Range range = sheet.get_Range(this.GetAix(x, y), miss);
            range.Font.Size = font.Size;
            range.Font.Bold = font.Bold;
            //这里是int型的颜色   
            range.Font.Color = ColorTranslator.ToOle(ColorTranslator.FromWin32(color));
            range.Font.Name = font.Name;
            range.Font.Italic = font.Italic;
            range.Font.Underline = font.Underline;

        }

        /// <summary>   
        /// 给单元格赋值4   
        /// </summary>
        /// <param name="x">列号</param>   
        /// <param name="y">行号</param>   
        /// <param name="text">值</param>   
        /// <param name="font">字符格式</param>   
        /// <param name="color">颜色</param>   
        /// <param name="align">对齐（CENTER、LEFT、RIGHT）</param>  
        public void SetValue(int x, int y, string text, System.Drawing.Font font, int color, string align)
        {
            this.SetValue(x, y, text);
            Range range = sheet.get_Range(this.GetAix(x, y), miss);
            range.Font.Size = font.Size;
            range.Font.Bold = font.Bold;
            //这里是int型的颜色   
            range.Font.Color = ColorTranslator.ToOle(ColorTranslator.FromWin32(color));
            range.Font.Name = font.Name;
            range.Font.Italic = font.Italic;
            range.Font.Underline = font.Underline;

            if (align.ToUpper() == "CENTER")
            {
                range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }
            if (align.ToUpper() == "LEFT")
            {
                range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            }
            if (align.ToUpper() == "RIGHT")
            {
                range.HorizontalAlignment = XlHAlign.xlHAlignRight;
            }
        }
        #endregion ~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>   
        /// 获取描述区域的字符(横纵坐标)   
        /// </summary>   
        /// <param name="x">列</param>   
        /// <param name="y">行</param>   
        /// <returns></returns>   
        public string GetAix(int x, int y)
        {
            if (x > 256) { return ""; }
            string s = "";
            s = s + ALists[x - 1].ToString();
            s = s + y.ToString();
            return s;
        }

        /// <summary>   
        /// 把剪切内容粘贴到当前区域   
        /// </summary>   
        public void Paste()
        {
            string s = "a,b,c,d,e,f,g";
            sheet.Paste(sheet.get_Range(this.GetAix(10, 10), miss), s);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        public void MergeCell(int x1, int y1, int x2, int y2)
        {
            Range range = sheet.get_Range(this.GetAix(x1, y1), this.GetAix(x2, y2));
            range.Merge(true);
        }

        #region 获取单元格对象
        
        /// <summary>
        /// 获取单元格对象
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public Range GetRange(int x1, int y1, int x2, int y2)
        {
            Range range = sheet.get_Range(this.GetAix(x1, y1), this.GetAix(x2, y2));
            return range;
        }
        #endregion

        #region 获取单元格值
        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <returns></returns>
        public object GetRangeVal()
        {
            Range range = sheet.Cells[1, 1];   //获取A1单元格
            range = sheet.Cells[1, "A"];       //获取A1单元格
            range = sheet.Cells["A", "A"];     //获取A1单元格
            range = sheet.Cells["A", 1];       //获取A1单元格

            object obj = range.Value;//日期和货币格式才使用这个属性
            obj = range.NumberFormat;
            obj = range.Text;
            return obj;
        }
        #endregion


        /// <summary>   
        /// 设置边框   
        /// </summary>  
        public void SetBorder(int x1, int y1, int x2, int y2, int Width)
        {
            Range range = sheet.get_Range(this.GetAix(x1, y1), this.GetAix(x2, y2));
            range.Borders.Weight = Width;
        }


        /// <summary>   
        /// 打开Excel文件   
        /// </summary>   
        /// <param name="filename">路径</param>   
        public void OpenExcelFile(string filename)
        {
            UserControl(false);

            m_objExcel.Workbooks.Open(filename, miss, miss, miss, miss, miss, miss, miss,miss, miss, miss, miss, miss, miss, miss);
            m_objBooks = (Workbooks)m_objExcel.Workbooks;
            m_objBook = m_objExcel.ActiveWorkbook;
            sheet = (Worksheet)m_objBook.ActiveSheet;
        }

        /// <summary>
        /// 创建Book、sheet
        /// </summary>
        public void CreateExceFile()
        {
            UserControl(false);
            m_objBooks = (Workbooks)m_objExcel.Workbooks;
            m_objBook = (Workbook)(m_objBooks.Add(miss));
            sheet = (Worksheet)m_objBook.ActiveSheet;
        }

        /// <summary>
        /// 设置Exce属性
        /// </summary>
        /// <param name="usercontrol"></param>
        public void UserControl(bool usercontrol)
        {
            if (m_objExcel == null)
            { return; }
            m_objExcel.UserControl = usercontrol;
            m_objExcel.DisplayAlerts = usercontrol;
            m_objExcel.Visible = usercontrol;
        }

        /// <summary>   
        /// 插入新行   
        /// </summary>   
        /// <param name="y">模板行号</param>   
        public void insertRow(int y)
        {
            Range range = sheet.get_Range(GetAix(1, y), GetAix(255, y));
            range.Copy(miss);
            range.Insert(XlDirection.xlDown, miss);
            range.get_Range(GetAix(1, y), GetAix(255, y));
            range.Select();

            sheet.Paste(miss, miss);
        }

        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="FileName"></param>
        public void SaveAs(string FileName)
        {
            string path = System.IO.Path.GetTempPath() + FileName;

            //如果存在先删除  
            if (File.Exists(path))
            {
                File.Delete(path);
            }

             //重新生成  
             m_objBook.SaveAs(path, miss, miss, miss, miss,
             miss, XlSaveAsAccessMode.xlNoChange,
             XlSaveConflictResolution.xlLocalSessionChanges,miss, miss, miss, miss);
             //m_objBook.Close(false, miss, miss);   

            //释放所有资源 zhangl  
            ReleaseExcel();

            //以"流"的方式输出  
            if (File.Exists(path))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "gb2312";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                //Response.AddHeader("Content-Length", new FileInfo(filename).Length);  
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.WriteFile(path);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
            }
        }
        #endregion


        #region 释放所有资源
        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void ReleaseExcel()
        {
            m_objExcel.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject((object)m_objExcel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject((object)m_objBooks);
            System.Runtime.InteropServices.Marshal.ReleaseComObject((object)m_objBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject((object)sheet);

            sheet = null;
            m_objBook = null;
            m_objBooks = null;
            m_objExcel = null;

            GC.Collect(0);
        }
        #endregion


        #region 关闭创建的Excel进程

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// 关闭创建的Excel进程1 
        /// 推荐这个方法
        /// </summary>
        public void KillSpecialExcel()
        {
            try
            {
                if (m_objExcel != null)
                {
                    int lpdwProcessId;
                    GetWindowThreadProcessId(new IntPtr(m_objExcel.Hwnd), out lpdwProcessId);
                    System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Excel Process Error:" + ex.Message);
            }
        }

        /// <summary>
        /// 关闭Excel进程2
        /// </summary>
        /// <returns></returns>
        public bool KillAllExcel()
        {
            try
            {
                if (m_objExcel != null) // isRunning是判断xlApp是怎么启动的flag.   
                {
                    m_objExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
                    //释放COM组件，其实就是将其引用计数减1   
                    //System.Diagnostics.Process theProc;   
                    foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        //先关闭图形窗口。如果关闭失败...有的时候在状态里看不到图形窗口的excel了，   
                        //但是在进程里仍然有EXCEL.EXE的进程存在，那么就需要杀掉它:p   
                        if (theProc.CloseMainWindow() == false)
                        {
                            theProc.Kill();
                        }
                    }
                    m_objExcel = null;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}