using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 操作Excel(COM组件Microsoft Excel 11.0的方式)
    /// </summary>
    public class ExcelByCom3
    {
        private int _ReturnStatus;
        private string _ReturnMessage;
        private const string CREATE_ERROR = "Can not create excel file, may be your computer has not installed excel!";
        private const string IMPORT_ERROR = "Your excel file is open, please save and close it.";
        private const string EXPORT_ERROR = "This is an error that the excel file may be open when it be exported. /n";


        public int ReturnStatus
        {
            get { return _ReturnStatus; }

        }

        public string ReturnMessage
        {
            get { return _ReturnMessage; }
        }


        /// <summary>
        /// Excel文件的导出
        /// 其中每个工作表为一个DataTable
        /// </summary>
        /// <param name="fileName">Excel文件的完整路径</param>
        /// <returns></returns>
        public DataSet ImportExcel(string fileName)
        {
            Application xlApp = new Application();

            if (xlApp == null)
            {
                _ReturnStatus = -1;

                _ReturnMessage = CREATE_ERROR;

                return null;
            }

            Workbook workbook;
            try
            {
                workbook = xlApp.Workbooks.Open(fileName, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, 1, 0);
            }

            catch
            {
                _ReturnStatus = -1;
                _ReturnMessage = IMPORT_ERROR;
                return null;
            }

            int n = workbook.Worksheets.Count;

            string[] SheetSet = new string[n];

            System.Collections.ArrayList al = new System.Collections.ArrayList();

            for (int i = 1; i <= n; i++)
            {
                SheetSet[i - 1] = ((Worksheet)workbook.Worksheets[i]).Name;
            }

            workbook.Close(null, null, null);

            xlApp.Quit();

            if (workbook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
            }

            if (xlApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }

            GC.Collect();

            DataSet ds = new DataSet();

            string connStr = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + fileName + ";Extended Properties=Excel 8.0";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbDataAdapter da;

                for (int i = 1; i <= n; i++)
                {
                    string sql = "select * from [" + SheetSet[i - 1] + "$] ";
                    da = new OleDbDataAdapter(sql, conn);
                    da.Fill(ds, SheetSet[i - 1]);
                    da.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            return ds;
        }



        /// <summary>
        /// Excel导入
        /// 每个Datatable为一个工作表，工作表的名字为Datatable的名字
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="saveFileName">要保存的Excel文件的完整路径</param>
        /// <returns></returns>
        public bool ExportExcel(DataSet ds, string saveFileName)
        {
            if (ds == null)
            {
                _ReturnStatus = -1;
                _ReturnMessage = "The DataSet is null!";
                return false;
            }
            bool fileSaved = false;

            Application xlApp = new Application();

            if (xlApp == null)
            {
                _ReturnStatus = -1;
                _ReturnMessage = CREATE_ERROR;
                return false;
            }

            Workbooks workbooks = xlApp.Workbooks;
            Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            for (int j = ds.Tables.Count - 1; j >= 0; j--)
            {
                System.Data.DataTable dt = ds.Tables[j];
                Worksheet worksheet = (Worksheet)workbook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                worksheet.Name = ds.Tables[j].TableName.ToString();
                worksheet.Cells.Font.Size = 10;
                Range range;

                long totalCount = dt.Rows.Count;
                long rowRead = 0;
                float percent = 0;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                    range = (Range)worksheet.Cells[1, i + 1];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                }

                for (int r = 0; r < dt.Rows.Count; r++)
                {

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();

                    }

                    rowRead++;

                    percent = ((float)(100 * rowRead)) / totalCount;

                }



                range = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]);

                range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, null);

                if (dt.Rows.Count > 0)
                {

                    range.Borders[XlBordersIndex.xlInsideHorizontal].ColorIndex = XlColorIndex.xlColorIndexAutomatic;

                    range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;

                    range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;

                }

                if (dt.Columns.Count > 1)
                {

                    range.Borders[XlBordersIndex.xlInsideVertical].ColorIndex = XlColorIndex.xlColorIndexAutomatic;

                    range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;

                    range.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;

                }



                if (range != null)
                {

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                    range = null;

                }

                if (worksheet != null)
                {

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

                    worksheet = null;

                }

            }



            ((Worksheet)workbook.Worksheets[ds.Tables.Count + 1]).Delete();



            if (saveFileName != "")
            {

                try
                {

                    workbook.Saved = true;

                    System.Reflection.Missing miss = System.Reflection.Missing.Value;

                    workbook.SaveAs(saveFileName, XlFileFormat.xlExcel8, miss, miss, miss, miss,

                        XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);

                    fileSaved = true;

                }

                catch (Exception ex)
                {

                    fileSaved = false;

                    _ReturnStatus = -1;

                    _ReturnMessage = EXPORT_ERROR + ex.Message;

                }

            }

            else
            {

                fileSaved = false;

            }





            if (workbook != null)
            {

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);

                workbook = null;

            }

            if (workbooks != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                workbooks = null;
            }

            xlApp.Application.Workbooks.Close();
            xlApp.Quit();

            if (xlApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }

            GC.Collect();
            return fileSaved;
        }

    }
}
