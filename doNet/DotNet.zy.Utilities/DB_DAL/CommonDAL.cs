using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DotNet.zy.Utilities;

/// <summary>
///CommonDAL 的摘要说明
/// </summary>
public class CommonDAL
{
    #region 获取服务器时间
    /// <summary>
    /// 获取服务器时间
    /// </summary>
    /// <param name="str">要输出的格式</param>
    /// <returns></returns>
    public static string GetServerDate(string str)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select GETDATE()");

        DateTime newTime = Convert.ToDateTime(SqlHelper.GetSqlValue(strSql.ToString()));
        return DateFormat.DateTransform(newTime, str);
    }
    /// <summary>
    /// 获取服务器完整时间
    /// </summary>
    /// <returns></returns>
    public static string GetServerAllDate()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select GETDATE()");

        return SqlHelper.GetSqlValue(strSql.ToString());
    }
    #endregion

    #region 查询表的总行数(简单sql)
    /// <summary>
    /// 查询表的总行数(简单sql)
    /// </summary>
    /// <param name="tabName"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public static int GetColSum(string tabName, string strWhere)
    {
        string sql = "select count(*) from " + tabName + " where " + strWhere;
        return int.Parse(SqlHelper.GetSqlValue(sql));
    }
    #endregion

    #region 分页查询数据(简单sql)
    /// <summary>
    /// 分页查询数据（简单aql）
    /// </summary>
    /// <param name="tabName"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public static DataTable GetNowPageTable(string tblName, string strGetFields, string fldName, string strWhere, string startIndex, string endindex)
    {
        SqlParameter[] cmdParms = new SqlParameter[]{
         new SqlParameter("@tblName", tblName),
         new SqlParameter("@strGetFields", strGetFields),
         new SqlParameter("@fldName", fldName),
         new SqlParameter("@strWhere", strWhere),
         new SqlParameter("@startIndex", startIndex),
         new SqlParameter("@endIndex", endindex)
     };
        return SqlHelper.ExecProcSqlReturnDB_Param("SP_Pagin_Number", cmdParms);
    }
    #endregion

    #region 查询表的总行数(复杂sql)
    /// <summary>
    /// 查询表的总行数（复杂sql）
    /// </summary>
    /// <param name="sqlSelectTable">完整的sql语句 直接查询Table</param>
    /// <returns></returns>
    public int GetColSum2(string sqlSelectTable)
    {
        string sql = "select count(*) from (" + sqlSelectTable + ") as tmp";
        return int.Parse(SqlHelper.GetSqlValue(sql));
    }
    #endregion

    #region 分页查询数据(复杂sql)
    /// <summary>
    /// 分页查询数据(复杂sql)
    /// </summary>
    /// <returns></returns>
    public DataTable GetNowPageTable(string sql, string startIndex, string endindex)
    {
        string sql_Data = "select * from (" + sql + ") as temp where rowID between " + startIndex + " and " + endindex;
        DataTable db = SqlHelper.GetSqlDataTable(sql_Data);
        return db;
    }
    #endregion



    #region 由数据集DataSet自动生成Insert的SQL语句数据
    /// <summary>
    /// 由数据集DataSet自动生成Insert的SQL语句数据；
    /// </summary>
    /// <param name="ds">数据集</param>
    /// <param name="TableName">表名</param>
    /// <returns></returns>
    public static ArrayList DataSetToArrayList(DataSet ds, String TableName)
    {
        ArrayList allSql = new ArrayList();

        string FieldAll = "";
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            FieldAll = FieldAll + ds.Tables[0].Columns[i].ColumnName.ToString() + ",";
        }
        FieldAll = FieldAll.Substring(0, FieldAll.Length - 1);//去掉最后一个“，”

        DataView dv = ds.Tables[0].DefaultView;
        string ValueAll = "";
        for (int n = 0; n < dv.Count; n++)
        {
            for (int m = 0; m < ds.Tables[0].Columns.Count; m++)
            {
                switch (dv[n][m].GetType().ToString())
                {
                    case "System.DateTime":
                        ValueAll += "'" + (Convert.ToDateTime(dv[n][m])).ToString("yyyy-MM-dd") + "'";
                        ValueAll += ",";
                        break;
                    case "System.String":
                        ValueAll += "'" + dv[n][m].ToString() + "'";
                        ValueAll += ",";
                        break;
                    case "System.Int32":
                        ValueAll += Convert.ToInt32(dv[n][m]);
                        ValueAll += ",";
                        break;
                    case "System.Single":
                        ValueAll += Convert.ToSingle(dv[n][m]);
                        ValueAll += ",";
                        break;
                    case "System.Double":
                        ValueAll += Convert.ToDouble(dv[n][m]);
                        ValueAll += ",";
                        break;
                    case "System.Decimal":
                        ValueAll += Convert.ToDecimal(dv[n][m]);
                        ValueAll += ",";
                        break;
                    default:
                        ValueAll += "'" + dv[n][m].ToString() + "'";
                        ValueAll += ",";
                        break;
                }
            }
            ValueAll = ValueAll.Substring(0, ValueAll.Length - 1); //去掉最后一个“，”
            allSql.Add("insert into " + TableName + " (" + FieldAll + ") values(" + ValueAll + ");");
            ValueAll = "";
        }
        return allSql;
    }
    #endregion


    #region 获取某表中的总记录数

    /// <summary>
    /// 获取某表中的总记录数
    /// </summary>
    /// <param name="tablename">表名</param>
    /// <returns></returns>
    public static int GetRecordCount(string tablename)
    {
        string s = "select count(*) from {0}";
        s = string.Format(s, tablename);
        return Convert.ToInt32(SqlEasy.ExecuteScalar(s));
    }

    public static int GetRecordCount(string tablename, string where)
    {
        string s = "select count(*) from {0} ";
        s = string.Format(s, tablename);
        if (!string.IsNullOrEmpty(where))
            s += " where " + where;
        return Convert.ToInt32(DotNet.zy.Utilities.SqlHelper3.ExecuteScalar(SqlEasy.connString, CommandType.Text, s));
    }

    public static int GetRecordCount(string connString, string tablename, string where)
    {
        string s = "select count(*) from {0} ";
        s = string.Format(s, tablename);
        if (!string.IsNullOrEmpty(where))
            s += " where " + where;
        return Convert.ToInt32(DotNet.zy.Utilities.SqlHelper3.ExecuteScalar(connString, CommandType.Text, s));
    }


    #endregion

    #region 根据条件获取指定表中的数据
    /// <summary>
    /// 根据条件获取指定表中的数据
    /// </summary>
    /// <param name="tablename">表名</param>
    /// <param name="where">条件</param>
    /// <returns></returns>
    public static DataTable GetDataTable(string tablename, string where)
    {
        string s = "select * from " + tablename;
        if (where != "")
        {
            s += " where " + where;
        }
        return DotNet.zy.Utilities.SqlHelper3.ExecuteDataset(SqlEasy.connString, CommandType.Text, s).Tables[0];
    }


    #endregion

    #region 根据ID 获取一行数据

    /// <summary>
    /// 根据主键Id,获取一行数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="keyName">主键名称</param>
    /// <param name="value">值</param>
    /// <param name="msg">返回信息</param>
    /// <returns></returns>
    public static DataRow GetADataRow(string tableName, string keyName, string value, out string msg)
    {
        try
        {
            string s = "select * from @table where @keyname=@value";
            SqlParameter[] sp ={new SqlParameter("@table",tableName),
                    new SqlParameter("@keyname",keyName),
                    new SqlParameter("@value",value)
                };

            DataTable dt = SqlEasy.ExecuteDataTable(s, sp);

            if (dt.Rows.Count > 0)
            {
                msg = "OK";
                return dt.Rows[0];
            }
            else
            {
                msg = "";
                return null;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return null;
        }
    }
    #endregion

    #region  增加Sql参数
    /// <summary>
    /// 增加sql参数并返回
    /// </summary>
    /// <param name="arguments">参数列表(格式:@name,@sex,@email......)</param>
    /// <param name="param">参数对应值</param>
    /// <returns></returns>
    public static SqlParameter[] AddSqlParameters(string arguments, params object[] param)
    {
        string[] args = arguments.Split(',');
        if (args.Length == 0)
            throw new ArgumentNullException("arguments", "参数个数为空!");
        if (args.Length != param.Length)
            throw new ArgumentNullException("arguments", "参数个数与赋值参数个数不相等!");
        SqlParameter[] para = new SqlParameter[args.Length];
        for (int i = 0; i < para.Length; i++)
        {
            para[i] = new SqlParameter(args[i], param[i]);
        }
        return para;
    }

    /// <summary>
    /// 增加赋值sql参数并返回
    /// </summary>
    /// <param name="sqls">sql赋值可变对象</param>
    /// <param name="arguments">参数列表(格式:@name,@sex,@email......)</param>
    /// <param name="param">参数对应值</param>
    /// <returns></returns>
    public static List<SqlParameter> AddSqlParameters(ref StringBuilder sqls, string arguments, params object[] param)
    {
        string[] args = arguments.Split(',');
        if (args.Length == 0)
            throw new ArgumentNullException("arguments", "参数个数为空!");
        if (args.Length != param.Length)
            throw new ArgumentNullException("arguments", "参数个数与赋值参数个数不相等!");
        List<SqlParameter> para = new List<SqlParameter>();
        for (int i = 0; i < args.Length; i++)
        {
            if (param[i] == null)
                continue;
            if (param[i] is int && ConvertHelper.CastInt(param[i]) == 0)
                continue;
            if (param[i] is string && string.IsNullOrEmpty(param[i].ToString()))
                continue;
            if (param[i] is DateTime && ConvertHelper.SafeCastDateTime(param[i]).Date == DateTime.MinValue.Date)
                continue;
            sqls.Append(string.Format("[{0}]={1},", args[i].TrimStart('@'), args[i]));
            para.Add(new SqlParameter(args[i], param[i]));
        }
        return para;
    }

    /// <summary>
    /// 赋值时给指定泛型及可变串追加指定字段及指定Int值
    /// </summary>
    /// <param name="para">sql参数泛型</param>
    /// <param name="sqls">sql可变字段</param>
    /// <param name="field">字段名称</param>
    /// <param name="fieldvalue">字段值</param>
    public static void AddSetIntSqlParameter(List<SqlParameter> para, StringBuilder sqls, string field, int fieldvalue)
    {
        if (ConvertHelper.CastInt(fieldvalue) > 0)
        {
            sqls.Append(string.Format("[{0}]=@{0},", field));
            para.Add(new SqlParameter(string.Format("@{0}", field), fieldvalue));
        }
    }

    /// <summary>
    /// 赋值时给指定泛型及可变串追加指定字段及指定String值
    /// </summary>
    /// <param name="para">sql参数泛型</param>
    /// <param name="sqls">sql可变字段</param>
    /// <param name="field">字段名称</param>
    /// <param name="fieldvalue">字段值</param>
    public static void AddSetStringSqlParameter(List<SqlParameter> para, StringBuilder sqls, string field, string fieldvalue)
    {
        if (!string.IsNullOrEmpty(fieldvalue))
        {
            sqls.Append(string.Format("[{0}]=@{0},", field));
            para.Add(new SqlParameter(string.Format("@{0}", field), fieldvalue));
        }
    }

    /// <summary>
    /// 赋值时给指定泛型及可变串追加指定字段及指定日期String值
    /// </summary>
    /// <param name="para">sql参数泛型</param>
    /// <param name="sqls">sql可变字段</param>
    /// <param name="field">字段名称</param>
    /// <param name="fieldvalue">字段值</param>
    public static void AddSetDateSqlParameter(List<SqlParameter> para, StringBuilder sqls, string field, DateTime fieldvalue)
    {
        if (!string.IsNullOrEmpty(fieldvalue.ToString()))
        {
            if (ConvertHelper.SafeCastDateTime(fieldvalue) != DateTime.MinValue)
            {
                sqls.Append(string.Format("[{0}]=@{0},", field));
                para.Add(new SqlParameter(string.Format("@{0}", field), fieldvalue));
            }
        }
    }

    /// <summary>
    /// 指定条件时给指定泛型及可变串追加指定字段及指定Int值
    /// </summary>
    /// <param name="para">sql参数泛型</param>
    /// <param name="sqls">sql可变字段</param>
    /// <param name="field">字段名称</param>
    /// <param name="fieldvalue">字段值</param>
    public static void AddWhereIntSqlParameter(List<SqlParameter> para, StringBuilder sqls, string field, int fieldvalue)
    {
        if (ConvertHelper.CastInt(fieldvalue) > 0)
        {
            sqls.Append(string.Format("[{0}]=@{0} and ", field));
            para.Add(new SqlParameter(string.Format("@{0}", field), fieldvalue));
        }
    }

    /// <summary>
    /// 指定条件时给指定泛型及可变串追加指定字段及指定String值
    /// </summary>
    /// <param name="para">sql参数泛型</param>
    /// <param name="sqls">sql可变字段</param>
    /// <param name="field">字段名称</param>
    /// <param name="fieldvalue">字段值</param>
    public static void AddWhereStringSqlParameter(List<SqlParameter> para, StringBuilder sqls, string field, string fieldvalue)
    {
        if (!string.IsNullOrEmpty(fieldvalue))
        {
            sqls.Append(string.Format("[{0}]=@{0} and ", field));
            para.Add(new SqlParameter(string.Format("@{0}", field), fieldvalue));
        }
    }
    #endregion


    #region 获取指定表中指定字段的最大值
    /// <summary>
    /// 获取指定表中指定字段的最大值
    /// </summary>
    /// <param name="tableName">表名称</param>
    /// <param name="field">字段</param>
    /// <returns>Return Type:Int</returns>
    public static int GetMaxID(string tableName, string field)
    {
        string s = "select Max(@field) from @tablename";
        SqlParameter[] para = { new SqlParameter("@field", field), new SqlParameter("@tablename", tableName) };
        object obj = DotNet.zy.Utilities.SqlHelper3.ExecuteScalar(SqlEasy.connString, CommandType.Text, s, para);
        int i = Convert.ToInt32(obj == DBNull.Value ? "0" : obj);
        return i;
    }
    #endregion

    #region 合并指定表并返回
    /// <summary>
    /// 合并指定表并返回
    /// </summary>
    /// <param name="dt">原始表</param>
    /// <param name="DataTables">可变表参</param>
    /// <returns></returns>
    public static DataTable MergeDataTable(DataTable dt, params DataTable[] DataTables)
    {
        if (DataTables.Length == 0)
            return dt;
        foreach (DataTable table in DataTables)
            dt.Merge(table);
        return dt;
    }
    #endregion

    #region 分页获取数据DataTable， 存储过程
    /// <summary>
    ///分页获取数据DataTable， 存储过程
    /// </summary>
    /// <param name="tblName"></param>
    /// <param name="strGetFields"></param>
    /// <param name="fldName"></param>
    /// <param name="strWhere"></param>
    /// <param name="startIndex"></param>
    /// <param name="endindex"></param>
    /// <returns></returns>
    public DataTable GetDataTableOfPageIndex(string tblName, string strGetFields, string fldName, string strWhere, int startIndex, int endindex)
    {
        SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@tblName",tblName),
                new SqlParameter("@strGetFields",strGetFields),
                new SqlParameter("@fldName",fldName),
                new SqlParameter("@strWhere",strWhere),
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@endindex",endindex)
             };
        DataTable db = SqlHelper.ExecProcSqlReturnDB_Param("SP_Pagin_Number", parms);
        return db;
    }
    #endregion


    #region 取得服务器所有数据库名称
    /// <summary>
    /// 取得服务器所有数据库名称
    /// </summary>
    /// <returns></returns>
    private static ArrayList GetAllDatabasesNames()
    {
        ArrayList strArray = new ArrayList();
        string sSQL = "SELECT name FROM Master..SysDatabases ORDER BY Name";
        SqlDataReader dr = SqlHelper.GetSqlDataReader(sSQL);//修改，需要自己传递ConnectionString
        while (dr.Read())
        {
            strArray.Add(dr[0].ToString());
        }
        return strArray;
    }
    #endregion

    #region 取得服务器所有数据库中所有的数据表名
    /// <summary>
    /// 取得服务器所有数据库中所有的数据表名
    /// </summary>
    /// <returns></returns>
    private static ArrayList GetAllTableNames()
    {
        ArrayList strArray = new ArrayList();
        string sSQL = "SELECT name FROM sysobjects WHERE xtype = 'U' AND OBJECTPROPERTY (id, 'IsMSShipped') = 0";
        SqlDataReader dr = SqlHelper.GetSqlDataReader(sSQL);
        while (dr.Read())
        {
            strArray.Add(dr[0].ToString());
        }
        return strArray;
    }
    #endregion

    #region 得到服务器某个数据库的表集合
    /// <summary>
    /// 得到服务器某个数据库的表集合
    /// </summary>
    /// <param name="database"></param>
    /// <returns></returns>
    public DataTable GetTableData(string database)
    {
        string strSql = "SELECT Name FROM [" + database + "]..SysObjects Where XType='U' ORDER BY Name ";
        DataTable dt = SqlHelper.GetSqlDataTable( strSql);

        return dt;
    }
    #endregion

    #region 得到某张表的列集合
    /// <summary>
    /// 得到某张表的列集合
    /// </summary>
    /// <param name="database"></param>
    /// <returns></returns>
    public DataTable GetColumns(string database, string tableName)
    {
        string strSql = @"SELECT col.name from [{0}].sys.columns as col join [{1}].sys.tables  as tab
								on col.object_id=tab.object_id where tab.name='{2}'";
        strSql = string.Format(strSql, database, database, tableName);

        DataTable dt = SqlHelper.GetSqlDataTable(strSql);

        return dt;
    }
    #endregion
}

