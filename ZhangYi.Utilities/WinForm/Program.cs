using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using CSharp.示例窗口;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using DotNet.zy.Utilities;
using System.Diagnostics;

namespace CSharp通用方法
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new 子窗口显示样式());

            程序自带 test = new 程序自带();
            //test.Test();
        }
    }

    #region 利用NPOI2.0操作Xls文件
    /// <summary>
    /// 利用NPOI2.0操作Xls文件
    /// </summary>
    public class ImportXlsToDataTable
    {
        private void Exec()
        {
            InitializeWorkbook(@"xls\曼娅奴店铺.xlsx");
            ConvertToDataTable();
        }

        //HSSFWorkbook hssfworkbook;
        //HSSFSheet sheet;
        //HSSFRow row
        XSSFWorkbook hssfworkbook;

        #region 读取文件

        void InitializeWorkbook(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new XSSFWorkbook(file);
            }
        }
        #endregion

        #region 将文件转换成DataTable

        void ConvertToDataTable()
        {
            XSSFSheet sheet = (XSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();
            for (int j = 0; j < 5; j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
            }

            while (rows.MoveNext())
            {
                XSSFRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    XSSFCell cell = (XSSFCell)row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            SetData(dt);
        }
        #endregion

        #region 具体业务方法

        public void SetData(DataTable dt)
        {
            string name = "", province = "", city = "";
            string sql = "", provinceid = "", cityid = "";
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                name = dt.Rows[i][0].ToString();
                province = dt.Rows[i][1].ToString();
                city = dt.Rows[i][2].ToString();

                //判断省份是否存在,不存在则需要添加
                sql = " select id from T_Area where name='" + province + "' and (ParentID is null or ParentID=0)";
                provinceid = SqlHelper.GetSqlValue(sql);
                if (provinceid == "")
                {
                    provinceid = SqlHelper.GetSqlValue("insert into T_Area values(null,'" + province + "',0,'import',GETDATE(),'import',GETDATE());select @@IDENTITY AS ID");
                }
                //判断城市是否存在，不存在则需要添加
                sql = " select id from T_Area where name='" + city + "' and isnull(ParentID,0)!=0";
                cityid = SqlHelper.GetSqlValue(sql);
                if (cityid == "")
                {
                    cityid = SqlHelper.GetSqlValue(" insert into T_Area values(" + provinceid + ",'" + city + "',0,'import',GETDATE(),'import',GETDATE());select @@IDENTITY AS ID");
                }

                //向数据库插入门店数据
                sql = " insert into T_Stores(StoreName,ProvinceID,CityID,StorePhone,StoreAddress)  values('" + name + "'," + provinceid + "," + cityid + ",'" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][3].ToString() + "')";
                SqlHelper.ExecuteSqlNoQuery(sql);

            }
        }
        #endregion


        //switch(cell.CellType)
        //{
        //    case HSSFCellType.BLANK:
        //        dr[i] = "[null]";
        //        break;
        //    case HSSFCellType.BOOLEAN:
        //        dr[i] = cell.BooleanCellValue;
        //        break;
        //    case HSSFCellType.NUMERIC:
        //        dr[i] = cell.ToString();    //This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number.
        //        break;
        //    case HSSFCellType.STRING:
        //        dr[i] = cell.StringCellValue;
        //        break;
        //    case HSSFCellType.ERROR:
        //        dr[i] = cell.ErrorCellValue;
        //        break;
        //    case HSSFCellType.FORMULA:
        //    default:
        //        dr[i] = "="+cell.CellFormula;
        //        break;
        //}
    }
    #endregion

    public class 执行时间记录方法
    {
        //执行时间记录方法
        public void StopwatchTest()
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();
            //具体执行代码
            stw.Stop();
            NewLife.Log.XTrace.WriteLine(string.Format("2.2共执行时间:{0}:{1}:{2}.{3}", stw.Elapsed.Hours, stw.Elapsed.Minutes, stw.Elapsed.Seconds, stw.Elapsed.Milliseconds));
        }
    }

}
