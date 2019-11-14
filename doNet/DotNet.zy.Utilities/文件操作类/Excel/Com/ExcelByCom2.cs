using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 操作Excel(COM组件Microsoft Excel 11.0的方式)
    /// </summary>
    public class ExcelByCom2
    {
        private Application appExcel = null;
        private Workbook workbook = null;
        private Worksheet sheet = null;
        private DateTime dtBefore;
        private DateTime dtAfter;
        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        private string timestamp;
        /// <summary>
        /// 以时间字符串作为保存文件的名称
        /// </summary>
        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        private object mValue = System.Reflection.Missing.Value;
        /// <summary>
        ///是否打开Excel界面 
        /// </summary>
        public bool Visible
        {
            set
            {
                appExcel.Visible = value;
            }
        }
        public ExcelByCom2()
        {
            this.dtBefore = DateTime.Now;
            appExcel = new Application();
            this.dtAfter = DateTime.Now;
            this.timestamp = DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }
        public ExcelByCom2(string strFilePath)
        {
            this.dtBefore = DateTime.Now;
            appExcel = new Application();
            this.dtAfter = DateTime.Now;
            this.workbook = (Workbook)appExcel.Workbooks.Open(strFilePath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
            this.timestamp = DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }
        public void Dispose()
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                sheet = null;
                workbook.Close(false, mValue, mValue);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
                appExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                appExcel = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    if (pro.StartTime > this.dtBefore && pro.StartTime < this.dtAfter)
                    {
                        pro.Kill();
                    }
                }
            }
            System.GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 加载Excel
        /// </summary>
        public void Load()
        {
            if (workbook == null && this.filePath != null)
            {
                workbook = appExcel.Workbooks.Open(this.filePath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
            }
        }
        /// <summary>
        /// 加载Excel
        /// </summary>
        public void Load(string strFilePath)
        {
            if (workbook == null)
            {
                workbook = appExcel.Workbooks.Open(strFilePath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
            }
        }
        /// <summary>
        /// 新建工作表
        /// </summary>
        /// <param name="sheetName"></param>
        public void NewWorkSheet(string sheetName)
        {
            sheet = workbook.Sheets.Add(workbook.Sheets[1], mValue, mValue, mValue);
            sheet.Name = sheetName;
        }
        /// <summary>
        /// 在指定的单元格插入指定的值
        /// </summary>
        /// <param name="strCell">单元格 如"A4"</param>
        /// <param name="objValue">文本 数字等值</param>
        public void WriteCell(string strCell, object objValue)
        {
            sheet.get_Range(strCell, mValue).Value2 = objValue;
        }
        /// <summary>
        /// 在指定Range中插入指定的值
        /// </summary>
        /// <param name="strStartCell">Range的开始单元格</param>
        /// <param name="strEndCell">Range的结束单元格</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteRange(string strStartCell, string strEndCell, object objValue)
        {
            sheet.get_Range(strStartCell, strEndCell).Value2 = objValue;
        }
        /**/
        /// <summary>
        /// 合并单元格，并在合并后的单元格中插入指定的值
        /// </summary>
        /// <param name="strStartCell"></param>
        /// <param name="strEndCell"></param>
        /// <param name="objValue"></param>
        public void WriteAfterMerge(string strStartCell, string strEndCell, object objValue)
        {
            sheet.get_Range(strStartCell, strEndCell).Merge(mValue);
            sheet.get_Range(strStartCell, mValue).Value2 = objValue;
        }

        /**/
        /// <summary>
        /// 在连续单元格中插入一个DataTable中的值
        /// </summary>
        /// <param name="strStartCell">开始的单元格</param>
        /// <param name="dtData">存储数据的DataTable</param>
        public void WriteTable(string strStartCell, System.Data.DataTable dtData)
        {
            object[,] arrData = new object[dtData.Rows.Count, dtData.Columns.Count];

            for (int i = 0; i < dtData.Rows.Count; i++)
                for (int j = 0; j < dtData.Columns.Count; j++)
                    arrData[i, j] = dtData.Rows[i][j];

            sheet.get_Range(strStartCell, this.GetEndCell(strStartCell, dtData.Rows.Count - 1, dtData.Columns.Count - 1)).Value2 = arrData;

            arrData = null;
        }

        /**/
        /// <summary>
        /// 在连续单元格中插入一个DataTable并作超级链接
        /// </summary>
        /// <param name="strStartCell">起始单元格标识符</param>
        /// <param name="dtData">存储数据的DataTable</param>
        /// <param name="strLinkField">链接的地址字段</param>
        /// <param name="strTextField">链接的文本字段</param>
        public void WriteTableAndLink(string strStartCell, System.Data.DataTable dtData, string strLinkField, string strTextField)
        {
            object[,] arrData = new object[dtData.Rows.Count, dtData.Columns.Count - 1];

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                for (int j = 0; j < dtData.Columns.Count; j++)
                {
                    if (j > dtData.Columns.IndexOf(strLinkField))
                        arrData[i, j - 1] = dtData.Rows[i][j];
                    else if (j < dtData.Columns.IndexOf(strLinkField))
                        arrData[i, j] = dtData.Rows[i][j];
                }
            }

            sheet.get_Range(strStartCell, this.GetEndCell(strStartCell, dtData.Rows.Count - 1, dtData.Columns.Count - 2)).Value2 = arrData;

            for (int i = 0; i < dtData.Rows.Count; i++)
                this.AddHyperLink(this.NtoL(this.LtoN(this.GetCellLetter(strStartCell)) + dtData.Columns.IndexOf(strTextField)) + System.Convert.ToString(this.GetCellNumber(strStartCell) + i), dtData.Rows[i][strLinkField].ToString() + ".htm", "点击查看详细", dtData.Rows[i][strTextField].ToString());

            arrData = null;
        }

        /**/
        /// <summary>
        /// 为单元格设置公式
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="strFormula">公式</param>
        public void SetFormula(string strCell, string strFormula)
        {
            sheet.get_Range(strCell, mValue).Formula = strFormula;
        }
        /**/
        /// <summary>
        /// 设置单元格或连续区域的字体为黑体
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public void SetBold(string strCell)
        {
            sheet.get_Range(strCell, mValue).Font.Bold = true;
        }

        /**/
        /// <summary>
        /// 设置连续区域的字体为黑体
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public void SetBold(string strStartCell, string strEndCell)
        {
            sheet.get_Range(strStartCell, strEndCell).Font.Bold = true;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的字体颜色
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public void SetColor(string strCell, System.Drawing.Color clrColor)
        {
            sheet.get_Range(strCell, mValue).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// 设置连续区域的字体颜色
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public void SetColor(string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            sheet.get_Range(strStartCell, strEndCell).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的边框：上下左右都为黑色连续边框
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public void SetBorderAll(string strCell)
        {
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strCell, mValue).Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;


        }

        /**/
        /// <summary>
        /// 设置连续区域的边框：上下左右都为黑色连续边框
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public void SetBorderAll(string strStartCell, string strEndCell)
        {
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;

            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            sheet.get_Range(strStartCell, strEndCell).Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域水平居左
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public void SetHAlignLeft(string strCell)
        {
            sheet.get_Range(strCell, mValue).HorizontalAlignment = XlHAlign.xlHAlignLeft;
        }

        /**/
        /// <summary>
        /// 设置连续区域水平居左
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public void SetHAlignLeft(string strStartCell, string strEndCell)
        {
            sheet.get_Range(strStartCell, strEndCell).HorizontalAlignment = XlHAlign.xlHAlignLeft;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域水平居左
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public void SetHAlignCenter(string strCell)
        {
            sheet.get_Range(strCell, mValue).HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        /**/
        /// <summary>
        /// 设置连续区域水平居中
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public void SetHAlignCenter(string strStartCell, string strEndCell)
        {
            sheet.get_Range(strStartCell, strEndCell).HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域水平居右
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public void SetHAlignRight(string strCell)
        {
            sheet.get_Range(strCell, mValue).HorizontalAlignment = XlHAlign.xlHAlignRight;
        }

        /**/
        /// <summary>
        /// 设置连续区域水平居右
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public void SetHAlignRight(string strStartCell, string strEndCell)
        {
            sheet.get_Range(strStartCell, strEndCell).HorizontalAlignment = XlHAlign.xlHAlignRight;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的显示格式
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="strNF">如"#,##0.00"的显示格式</param>
        public void SetNumberFormat(string strCell, string strNF)
        {
            sheet.get_Range(strCell, mValue).NumberFormat = strNF;
        }

        /**/
        /// <summary>
        /// 设置连续区域的显示格式
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="strNF">如"#,##0.00"的显示格式</param>
        public void SetNumberFormat(string strStartCell, string strEndCell, string strNF)
        {
            sheet.get_Range(strStartCell, strEndCell).NumberFormat = strNF;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的字体大小
        /// </summary>
        /// <param name="strCell">单元格或连续区域标识符</param>
        /// <param name="intFontSize"></param>
        public void SetFontSize(string strCell, int intFontSize)
        {
            sheet.get_Range(strCell, mValue).Font.Size = intFontSize.ToString();
        }

        /**/
        /// <summary>
        /// 设置连续区域的字体大小
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="intFontSize">字体大小</param>
        public void SetFontSize(string strStartCell, string strEndCell, int intFontSize)
        {
            sheet.get_Range(strStartCell, strEndCell).Font.Size = intFontSize.ToString();
        }

        /**/
        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="strColID">列标识，如A代表第一列</param>
        /// <param name="decWidth">宽度</param>
        public void SetColumnWidth(string strColID, double dblWidth)
        {
            ((Range)sheet.Columns.GetType().InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, sheet.Columns, new object[] { (strColID + ":" + strColID).ToString() })).ColumnWidth = dblWidth;
        }

        /**/
        /// <summary>
        /// 为单元格添加超级链接
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="strAddress">链接地址</param>
        /// <param name="strTip">屏幕提示</param>
        /// <param name="strText">链接文本</param>
        public void AddHyperLink(string strCell, string strAddress, string strTip, string strText)
        {
            sheet.Hyperlinks.Add(sheet.get_Range(strCell, mValue), strAddress, mValue, strTip, strText);
        }

        /**/
        /// <summary>
        /// 已知开始的单元格标识，求intR行、intColumn列后的单元格标识
        /// </summary>
        /// <param name="strStartCell">开始单元格标识</param>
        /// <param name="intR">行数</param>
        /// <param name="intC">列数</param>
        /// <returns>单元格标识符结果</returns>
        public string GetEndCell(string strStartCell, int intR, int intC)
        {
            string endcell = string.Empty;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            if (regex.IsMatch(strStartCell))
            {
                endcell = this.NtoL(this.LtoN(regex.Match(strStartCell).Result("${vLetter}")) + intC) + System.Convert.ToString((System.Convert.ToInt32(regex.Match(strStartCell).Result("${vNumber}")) + intR));
            }
            return endcell;

        }

        /**/
        /// <summary>
        /// 获取单元格标识符中的字母
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <returns>单元格标识符对应的字母</returns>
        public string GetCellLetter(string strCell)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            return regex.Match(strCell).Result("${vLetter}");
        }

        /**/
        /// <summary>
        /// 获取单元格标识符中的数字
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public int GetCellNumber(string strCell)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            return System.Convert.ToInt32(regex.Match(strCell).Result("${vNumber}"));
        }


        /**/
        /// <summary>
        /// 另存为xls文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public void SaveAs(string strFilePath)
        {
            workbook.SaveCopyAs(strFilePath);

        }

        /**/
        /// <summary>
        /// 另存为xls文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public void Save()
        {
            workbook.Save();

        }


        /// <summary>
        /// 将Excel中的数据 读入DataTable中
        /// </summary>
        /// <param name="filePath">excel文件的路径</param>
        /// <param name="dtName">datatable的名字</param>
        /// <param name="headName">表头，以逗号分隔</param>
        /// <returns></returns>
        public System.Data.DataTable ReadExcel2DataTable(string filePath, string dtName, string headName)
        {
            workbook = this.appExcel.Workbooks.Open(filePath, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
            sheet = workbook.Worksheets.get_Item(1);
            System.Data.DataTable dt = new System.Data.DataTable(dtName);
            string[] heads = !string.IsNullOrEmpty(headName) ? headName.Split(',') : null;
            if (heads != null)
            {
                for (int i = 0; i < heads.Length; i++)
                {
                    DataColumn column = new DataColumn(heads[i]);
                    //这里没对类型 进行处理
                    dt.Columns.Add(column);
                }
                for (int i = 0; i <= sheet.UsedRange.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < heads.Length; j++)
                    {
                        string cellString = NtoL(j + 1);
                        dr[heads[j]] = sheet.get_Range(cellString + (i + 1)).Value2;

                    }
                    dt.Rows.Add(dr);

                }
            }
            else
            {
                for (int i = 0; i <= sheet.UsedRange.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j <= sheet.UsedRange.Columns.Count; j++)
                    {
                        dr[j + 1] = sheet.get_Range(NtoL(j + 1) + (i + 1)).Value2;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 另存为html文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public void SaveHtml(string strFilePath)
        {
            workbook.SaveAs(strFilePath, XlFileFormat.xlHtml, mValue, mValue, mValue, mValue, XlSaveAsAccessMode.xlNoChange, mValue, mValue, mValue, mValue, mValue);
        }


        /// <summary>
        /// 字母转换为数字，Excel列头，如A-1;AA-27
        /// </summary>
        /// <param name="strLetter">字母</param>
        /// <returns>字母对应的数字</returns>
        private int LtoN(string strLetter)
        {
            int intRtn = 0;

            string strLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (strLetter.Length == 2)
                intRtn += (strLetters.IndexOf(strLetter.Substring(0, 1)) + 1) * 26;

            intRtn += strLetters.IndexOf(strLetter.Substring(strLetter.Length - 1, 1)) + 1;

            return intRtn;

        }


        /// <summary>
        /// 数字转换为字母，Excel列头，如1-A;27-AA
        /// </summary>
        /// <param name="intNumber">数字</param>
        /// <returns>数字对应的字母</returns>
        private string NtoL(int intNumber)
        {
            if (intNumber > 702)
                return String.Empty;

            if (intNumber == 702)
                return "ZZ";

            string strRtn = String.Empty;

            string strLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (intNumber > 26)
                strRtn = strLetters.Substring(intNumber / 26 - 1, 1);

            strRtn += strLetters.Substring((intNumber % 26) - 1, 1);

            return strRtn;
        }

    }
}