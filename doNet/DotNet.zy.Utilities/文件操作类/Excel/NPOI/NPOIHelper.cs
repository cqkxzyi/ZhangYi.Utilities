using DotNet.zy.Utilities;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNet.zy.Utilities.文件操作类.Excel.NPOI
{
    public class NPOIHelper
    {
        #region 由Excel导入DataTable
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="sheetName">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns></returns>
        public static DataTable ImportDataTableFromExcel(string fileName, int sheetIndex, int headerRowIndex)
        {
            try
            {
                DataTable dt;
                string strExtension = Path.GetExtension(fileName).ToLower();
                if (strExtension == ".xls")
                {
                    dt = ImportDataTableFromXls(fileName, sheetIndex, headerRowIndex);
                }
                else
                {
                    dt = ImportDataTableFromXlsx(fileName, sheetIndex, headerRowIndex);
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogWriter.Write("由Excel导入DataTable发生错误");
                LogWriter.Write(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region xls导入DataTable
        /// <summary>
        /// xls导入DataTable
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="sheetName">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromXls(string fileName, int sheetIndex, int headerRowIndex)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }

            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(sheetIndex);
            HSSFRow headerRow = (HSSFRow)sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
            DataTable table = new DataTable();

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i + 1;
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
                {
                    // 如果遇到第一个空行，则不再继续向后读取
                    break;
                }
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    HSSFCell cell = (HSSFCell)row.GetCell(j);
                    dataRow[j] = cell;

                    //读取Excel格式，根据格式读取数据类型
                    //switch (cell.CellType)
                    //{
                    //    case CellType.Blank: //空数据类型处理
                    //        dataRow[j] = "";
                    //        break;
                    //    case CellType.String: //字符串类型
                    //        dataRow[j] = cell.StringCellValue;
                    //        break;
                    //    case CellType.Numeric: //数字类型                                   
                    //        if (DateUtil.IsValidExcelDate(cell.NumericCellValue))
                    //        {
                    //            dataRow[j] = cell.DateCellValue;
                    //        }
                    //        else
                    //        {
                    //            dataRow[j] = cell.NumericCellValue;
                    //        }
                    //        break;
                    //    case CellType.Formula:
                    //        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                    //        dataRow[j] = e.Evaluate(cell).StringValue;
                    //        break;
                    //    default:
                    //        dataRow[j] = "";
                    //        break;
                    //}
                }
                table.Rows.Add(dataRow);
            }
            workbook = null;
            sheet = null;
            return table;
        }
        #endregion

        #region xlsx导入DataTable
        /// <summary>
        /// xlsx导入DataTable
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="sheetName">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromXlsx(string fileName, int sheetIndex, int headerRowIndex)
        {
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }

            XSSFSheet sheet = (XSSFSheet)workbook.GetSheetAt(sheetIndex);
            DataTable table = new DataTable();
            XSSFRow headerRow = (XSSFRow)sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i + 1;
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                XSSFRow row = (XSSFRow)sheet.GetRow(i);
                if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
                {
                    // 如果遇到第一个空行，则不再继续向后读取
                    break;
                }
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    dataRow[j] = (XSSFCell)row.GetCell(j);
                }
                table.Rows.Add(dataRow);
            }
            workbook = null;
            sheet = null;
            return table;
        }
        #endregion
    }
}
