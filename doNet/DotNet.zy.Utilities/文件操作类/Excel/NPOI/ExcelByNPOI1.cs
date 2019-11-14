using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// 使用NPOI组件
    /// 需引入ICSharpCode.SharpZipLib.dll/NPOI.dll/NPOI.OOXML.dll/NPOI.OpenXml4Net.dll/NPOI.OpenXmlFormats.dll
    /// office2007
    /// </summary>
    public class ExcelByNPOI1
    {
        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable Excel2DataTable(string file, string sheetName, string tableName)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //office2003 HSSFWorkbook
                workbook = new XSSFWorkbook(fs);
            }
            ISheet sheet = workbook.GetSheet(sheetName);
            dt = Export2DataTable(sheet, 0, true);
            return dt;

        }
        /// <summary>
        /// 将指定sheet中的数据导入到datatable中
        /// </summary>
        /// <param name="sheet">指定需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在的行号，-1没有列头</param>
        /// <param name="needHeader"></param>
        /// <returns></returns>
        private static DataTable Export2DataTable(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable dt = new DataTable();
            XSSFRow headerRow = null;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as XSSFRow;
                    cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        dt.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as XSSFRow;
                    cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        ICell cell = headerRow.GetCell(i);
                        if (cell == null)
                        {
                            break;//到最后 跳出循环
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            dt.Columns.Add(column);
                        }

                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = HeaderRowIndex + 1; i <= sheet.LastRowNum; i++)
                {
                    XSSFRow row = null;
                    if (sheet.GetRow(i) == null)
                    {
                        row = sheet.CreateRow(i) as XSSFRow;
                    }
                    else
                    {
                        row = sheet.GetRow(i) as XSSFRow;
                    }
                    DataRow dtRow = dt.NewRow();
                    for (int j = row.FirstCellNum; j <= cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Boolean:
                                    dtRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                    break;
                                case CellType.Error:
                                    dtRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                    break;
                                case CellType.Formula:
                                    switch (row.GetCell(j).CachedFormulaResultType)
                                    {

                                        case CellType.Boolean:
                                            dtRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);

                                            break;
                                        case CellType.Error:
                                            dtRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);

                                            break;
                                        case CellType.Numeric:
                                            dtRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);

                                            break;
                                        case CellType.String:
                                            string strFORMULA = row.GetCell(j).StringCellValue;
                                            if (strFORMULA != null && strFORMULA.Length > 0)
                                            {
                                                dtRow[j] = strFORMULA.ToString();
                                            }
                                            else
                                            {
                                                dtRow[j] = null;
                                            }
                                            break;
                                        default:
                                            dtRow[j] = "";
                                            break;
                                    }
                                    break;
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                    {
                                        dtRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                    }
                                    else
                                    {
                                        dtRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                    }
                                    break;
                                case CellType.String:
                                    string str = row.GetCell(j).StringCellValue;
                                    if (!string.IsNullOrEmpty(str))
                                    {

                                        dtRow[j] = Convert.ToString(str);


                                    }
                                    else
                                    {
                                        dtRow[j] = null;
                                    }
                                    break;
                                default:
                                    dtRow[j] = "";
                                    break;
                            }

                        }
                    }
                    dt.Rows.Add(dtRow);
                }

            }
            catch (Exception)
            {

                return null;
            }
            return dt;
        }
        /// <summary>
        /// 将DataTable中的数据导入Excel文件中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        public static void DataTable2Excel(DataTable dt, string file, string sheetName)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow header = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = header.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }
            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            byte[] buffer = stream.ToArray();
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(XSSFCell cell)
        {
            if (cell == null)
            {
                return null;
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;

                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                default:
                    return "=" + cell.StringCellValue;
            }
        }

    }
}
