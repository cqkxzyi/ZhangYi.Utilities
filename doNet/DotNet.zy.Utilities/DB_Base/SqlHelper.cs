using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;

namespace DotNet.zy.Utilities
{
   /// <summary>
   /// 链接数据库所有方法
   /// </summary>
   public class SqlHelper
   {
       /// <summary>
       /// 链接字符串
       /// </summary>
      public static string ConnectionString = ConfigurationManager.ConnectionStrings["huniConn"].ToString();

      public static bool isError = IsEnrollAA.IsEnroll();

      #region 判断是否存在 返回bool  public static Boolean SqlCheckIsExists(string SQLString)
      /// <summary>
      /// 判断是否存在 返回bool
      /// </summary>
      /// <param name="SQLString"></param>
      /// <returns></returns>
      public static Boolean SqlCheckIsExists(string SQLString)
      {
         bool returnbool;
         using (SqlConnection conn = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, conn))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  conn.Open();
                  SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                  if (sqlDataReader.Read())
                  {
                     returnbool = true;
                  }
                  else
                  {
                     returnbool = false;
                  }
                  return returnbool;
               }
               catch (Exception ex)
               {
                  conn.Close();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 判断是否存在 [带参数] 返回bool  public static Boolean SqlCheckIsExists(string SQLString, SqlParameter[] param)
      /// <summary>
      /// 判断是否存在 [带参数] 返回bool
      /// </summary>
      /// <param name="SQLString">SQL</param>
      /// <param name="param">参数集合</param>
      /// <returns></returns>
      public static Boolean SqlCheckIsExists(string SQLString, SqlParameter[] param)
      {
         bool returnbool = false;
         using (SqlConnection conn = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, conn))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  conn.Open();
                  cmd.Parameters.AddRange(param); //添加参数集
                  SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                  if (sqlDataReader.Read())
                  {
                     returnbool = true;
                  }
                  else
                  {
                     returnbool = false;
                  }
                  return returnbool;
               }
               catch (Exception ex)
               {
                  conn.Close();
                  JsHelper.MsgBox_GoHistory(ex.Message, -1);
                  return returnbool;
               }
            }
         }
      }
      #endregion

      #region 返回单个值  public static string GetSqlValue(string SQLString)
      /// <summary>
      /// 返回单个值
      /// </summary>
      /// <param name="SQLString"></param>
      /// <returns></returns>
      public static string GetSqlValue(string SQLString)
      {
         using (SqlConnection conn = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, conn))
            {
               cmd.CommandTimeout = 10;
               conn.Open();
               try
               {
                  object obj = cmd.ExecuteScalar();
                  if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                  {
                     return "";
                  }
                  else
                  {
                     return obj.ToString();
                  }
               }
               catch (Exception ex)
               {
                  conn.Close();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 返回单个值 [带参数]  public static string GetSqlValue_Param(string SQLString, SqlParameter[] param)
      /// <summary>
      /// 返回单个值 [带参数]
      /// </summary>
      /// <param name="SQLString"></param>
      /// <param name="param"></param>
      /// <returns></returns>
      public static string GetSqlValue_Param(string SQLString, SqlParameter[] param)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               DataSet ds = new DataSet();

               cmd.CommandTimeout = 10;
               connection.Open();
               cmd.Parameters.AddRange(param); //添加参数集
               try
               {
                  object obj = cmd.ExecuteScalar();
                  if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                  {
                     return "";
                  }
                  else
                  {
                     return obj.ToString();
                  }
               }
               catch (Exception ex)
               {
                  connection.Close();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 执行SQL语句 无返回值  public static void ExecuteSqlNoQuery(string SQLString)
      /// <summary>
      /// 执行SQL语句 无返回值
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static void ExecuteSqlNoQuery(string SQLString)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.ExecuteNonQuery();
               }
               catch (Exception ex)
               {
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 执行SQL语句 [带参数] 无返回值  public static void ExecSqlNoQuery_Param(string SQLString, SqlParameter[] param)
      /// <summary>
      /// 执行SQL语句 [带参数] 无返回值
      /// </summary>
      /// <param name="SQLString">SQL</param>
      /// <param name="param">参数集合</param>
      public static void ExecSqlNoQuery_Param(string SQLString, SqlParameter[] param)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.Parameters.AddRange(param); //添加参数集
                  cmd.ExecuteNonQuery();
               }
               catch (Exception ex)
               {
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 执行SQL语句 [带参数] 返回Bool public static bool ExecSqlReturn_Param(string SQLString, SqlParameter[] param)
      /// <summary>
      /// 执行SQL语句 [带参数] 返回Bool
      /// </summary>
      /// <param name="SQLString">SQL</param>
      /// <param name="param">参数集合</param>
      public static bool ExecSqlReturn_Param(string SQLString, SqlParameter[] param)
      {
         bool returnBool = false;
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.Parameters.AddRange(param); //添加参数集
                  cmd.ExecuteNonQuery();
                  returnBool = true;
               }
               catch (Exception ex)
               {
                  returnBool = false;
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
         return returnBool;
      }
      #endregion

      #region 执行SQL语句 返回Bool public static bool ExecSqlReturn(string SQLString)
      /// <summary>
      /// 执行SQL语句 返回是否成功
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static bool ExecSqlReturn(string SQLString)
      {
         bool returnBool = false;
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.ExecuteNonQuery();
                  returnBool = true;
               }
               catch (Exception ex)
               {
                  returnBool = false;
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
         return returnBool;
      }
      #endregion

      #region 执行SQL语句 [事务] 返回Bool public static bool ExecSqlNoQuery_Tran(string SQLString)
      /// <summary>
      /// 执行SQL语句 [事务] 返回是否成功
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static bool ExecSqlNoQuery_Tran(string SQLString)
      {
         bool returnBool = false;
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               cmd.CommandTimeout = 10;
               connection.Open();
               SqlTransaction tran = connection.BeginTransaction();
               try
               {
                  cmd.Transaction = tran;
                  cmd.ExecuteNonQuery();
                  tran.Commit();        //提交事务;
                  returnBool = true;
               }
               catch (Exception ex)
               {
                  tran.Rollback();   //回滚事务
                  connection.Close();
                  connection.Dispose();
                  throw ex;
               }
            }
         }
         return returnBool;
      }
      #endregion

      #region 执行SQL语句 [事务] [带参数] 返回Bool ExecSql_Tran_Param(string SQLString,SqlParameter[] param)
      /// <summary>
      /// 行SQL语句 [事务] [带参数] 返回是否成功
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static bool ExecSql_Tran_Param(string SQLString,SqlParameter[] param)
      {
         bool returnBool = false;
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               cmd.CommandTimeout = 10;
               connection.Open();
               SqlTransaction tran = connection.BeginTransaction();
               try
               {

                  cmd.Transaction = tran;
                  cmd.Parameters.AddRange(param); //添加参数集
                  cmd.ExecuteNonQuery();
                  tran.Commit();        //提交事务;
                  returnBool = true;
               }
               catch (Exception ex)
               {
                  tran.Rollback();   //回滚事务
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
         return returnBool;
      }
      #endregion

      #region 执行SQL语句 [事务] [带参数] [返回单个值] ExecSql_Tran_Param(string SQLString,SqlParameter[] param)
      /// <summary>
      /// 执行SQL语句 [事务] [带参数] [返回单个值]
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>returnStr</returns>
      public static string ExecSql_Tran_Param_retnStr(string SQLString, SqlParameter[] param)
      {
         string returnStr = "";
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               cmd.CommandTimeout = 10;
               connection.Open();
               SqlTransaction tran = connection.BeginTransaction();
               try
               {
                  cmd.Transaction = tran;
                  cmd.Parameters.AddRange(param); //添加参数集
                  object obj = cmd.ExecuteScalar();
                  if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                  {
                     returnStr= "";
                  }
                  else
                  {
                     returnStr= obj.ToString();
                  }
                  tran.Commit();        //提交事务;
               }
               catch (Exception ex)
               {
                  tran.Rollback();   //回滚事务
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
         return returnStr;
      }
      #endregion

      #region 返回DataSet  public static DataSet GetSqlDataSet(string SQLString)
      /// <summary>
      ///返回DataSet
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static DataSet GetSqlDataSet(string SQLString)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  DataSet ds = new DataSet();

                  cmd.CommandTimeout = 10;
                  connection.Open();
                  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                  adapter.Fill(ds, "ds");
                  return ds;
               }
               catch (Exception ex)
               {
                  connection.Close();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 返回DataTable   public static DataTable GetSqlDataTable(string SQLString)
      /// <summary>
      /// 返回DataTable
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static DataTable GetSqlDataTable(string SQLString)
      {
         if (!IsEnrollAA.IsEnroll())
         {
            return new DataTable();
         }
         else
         {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
               using (SqlCommand cmd = new SqlCommand(SQLString, connection))
               {
                  DataSet ds = new DataSet();

                  cmd.CommandTimeout = 10;
                  connection.Open();
                  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                  adapter.Fill(ds, "ds");
                  return ds.Tables[0];
               }

            }
         }
      }
      #endregion

      #region 返回DataTable [带参数]  public static DataTable GetSqlDataTable_Param(string SQLString, SqlParameter[] param)
      /// <summary>
      /// 返回DataTable [带参数]
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static DataTable GetSqlDataTable_Param(string SQLString, SqlParameter[] param)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               DataSet ds = new DataSet();

               cmd.CommandTimeout = 10;
               connection.Open();
               cmd.Parameters.AddRange(param); //添加参数集
               SqlDataAdapter adapter = new SqlDataAdapter(cmd);
               adapter.Fill(ds, "ds");
               adapter.Dispose();
               return ds.Tables[0];
            }

         }
      }
      #endregion



      #region 返回DataReader  public static SqlDataReader GetSqlDataReader(string SQLString)
      /// <summary>
      /// 返回DataReader
      /// </summary>
      /// <param name="connectionString"></param>
      /// <param name="cmdType"></param>
      /// <param name="cmdText"></param>
      /// <param name="commandParameters"></param>
      /// <returns></returns>
      public static SqlDataReader GetSqlDataReader(string SQLString)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            try
            {
               using (SqlCommand cmd = new SqlCommand(SQLString, connection))
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                  return sqlDataReader;
               }

            }
            catch (Exception ex)
            {
               connection.Close();
               throw new Exception(ex.Message);
            }
         }
      }
      #endregion

      #region 执行多条SQL语句，SQL语句的哈希表  public static void ExecuteSqlTran(Hashtable SQLStringList)
      /// <summary>
      /// 执行多条SQL语句，SQL语句的哈希表 
      /// </summary>
      /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
      public static void ExecuteSqlTran(Hashtable SQLStringList)
      {
         using (SqlConnection conn = new SqlConnection(ConnectionString))
         {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
               SqlCommand cmd = new SqlCommand();
               try
               {
                  //循环
                  foreach (DictionaryEntry myDE in SQLStringList)
                  {
                     string cmdText = myDE.Key.ToString();
                     SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                     PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                     int val = cmd.ExecuteNonQuery();
                     cmd.Parameters.Clear();

                     trans.Commit();
                  }
               }
               catch
               {
                  trans.Rollback();
                  throw;
               }
            }
         }
      }
      private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
      {
         if (conn.State != ConnectionState.Open)
            conn.Open();
         cmd.Connection = conn;
         cmd.CommandText = cmdText;
         if (trans != null)
            cmd.Transaction = trans;
         cmd.CommandType = CommandType.Text;//cmdType;
         if (cmdParms != null)
         {
            foreach (SqlParameter parm in cmdParms)
               cmd.Parameters.Add(parm);
         }
      }

      #endregion

      #region  存储过程 [带参数] 无返回值  public static void ExecProcSqlNoQuery_Param(string SQLString, SqlParameter[] parms)
      /// <summary>
      /// 存储过程 [带参数] 无返回值
      /// </summary>
      /// <param name="SQLString">查询语句</param>
      /// <returns>DataSet</returns>
      public static void ExecProcSqlNoQuery_Param(string SQLString, SqlParameter[] parms)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddRange(parms);  //添加参数集
                  cmd.ExecuteNonQuery();
               }
               catch (Exception ex)
               {
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region  存储过程 [带参数] 返回DataTable  public static DataTable ExecProcSqlReturnDB_Param(string SQLString, SqlCommand outcmd, SqlParameter[] parms)
      /// <summary>
      /// 存储过程 [带参数] 返回DataTable
      /// </summary>
      /// <param name="SQLString">存储过程名称</param>
      /// <param name="outcmd">返回的参数</param>
      /// <param name="parms">传入的参数列表</param>
      public static DataTable ExecProcSqlReturnDB_Param(string SQLString, SqlParameter[] parms)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddRange(parms);  //添加参数集

                  DataSet ds = new DataSet();
                  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                  adapter.Fill(ds, "ds");
                  adapter.Dispose();
                  return ds.Tables[0];
               }
               catch (Exception ex)
               {
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region  存储过程 [带参数] 返回单个值  public static DataTable ExecProcSqlReturnVal_Param(string SQLString, SqlCommand outcmd, SqlParameter[] parms)
      /// <summary>
      /// 存储过程 [带参数] 返回单个值
      /// </summary>
      /// <param name="SQLString">存储过程名称</param>
      /// <param name="outcmd">返回的参数</param>
      /// <param name="parms">传入的参数列表</param>
      public static string ExecProcSqlReturnVal_Param(string SQLString, SqlParameter[] parms)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddRange(parms);  //添加参数集

                 object obj = cmd.ExecuteScalar();
                 if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                 {
                    return  "";
                 }
                 else
                 {
                    return obj.ToString();
                 }
               }
               catch (Exception ex)
               {
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 存储过程 [带参数] 返回sqlComm参数  public static void ExecProcSqlReturn_Param(string SQLString, out SqlCommand outcmd, SqlParameter[] parms)
      /// <summary>
      /// 存储过程 [带参数] 返回sqlComm参数
      /// </summary>
      /// <param name="SQLString">存储过程名称</param>
      /// <param name="outcmd">返回的参数</param>
      /// <param name="parms">传入的参数列表</param>
      public static void ExecProcSqlReturn_Param(string SQLString, out SqlCommand outcmd, SqlParameter[] parms)
      {
         using (SqlConnection connection = new SqlConnection(ConnectionString))
         {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
               try
               {
                  cmd.CommandTimeout = 10;
                  connection.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddRange(parms);  //添加参数集

                  cmd.ExecuteNonQuery();
                  outcmd = cmd;
               }
               catch (Exception ex)
               {
                  connection.Close();
                  connection.Dispose();
                  throw new Exception(ex.Message);
               }
            }
         }
      }
      #endregion

      #region 将DataRow转化成实体对象  public static T CreateObject<T>(DataRow dr)
      /// <summary>
      /// 将DataRow 转化成实体对象
      /// </summary>
      /// <typeparam name="T">实体类</typeparam>
      /// <param name="dr">数据行</param>
      /// <returns></returns>
      public static T CreateObject<T>(DataRow dr)
      {
         if (dr == null)
            return default(T);
         object objDataFieldAttribute = null;
         Type _type = typeof(T);
         T MyClass = Activator.CreateInstance<T>();
         PropertyInfo[] _infolist = _type.GetProperties();

         string ColmunName = "";
         foreach (PropertyInfo _info in _infolist)
         {
            ColmunName = _info.Name;
            objDataFieldAttribute = _info.GetCustomAttributes(typeof(DataFieldAttribute), false);
            if (objDataFieldAttribute != null)
            {
               DataFieldAttribute[] colattrib = (DataFieldAttribute[])objDataFieldAttribute;
               if (colattrib.Length > 0)
               {
                  ColmunName = colattrib[0].FieldName;
               }
            }

            try
            {
               if (dr.Table.Columns.IndexOf(ColmunName) >= 0)
               {
                  _info.SetValue(MyClass, dr[ColmunName], null);
               }
            }
            catch { }
         }
         return MyClass;
      }
      #endregion

      #region 将DataTable转化成实体对象 public static List<T> CreateObject<T>(DataTable dt)
      /// <summary>
      /// 将DataTable转化成实体对象
      /// </summary>
      /// <typeparam name="T">实体类</typeparam>
      /// <param name="dt">数据表</param>
      /// <returns></returns>
      public static List<T> CreateObject<T>(DataTable dt)
      {
         if (dt == null)
         {
            return null;
         }
         List<T> tlist = new List<T>();

         object objDataFieldAttribute = null;
         Type _type = typeof(T);
         PropertyInfo[] _infolist = _type.GetProperties();

         string ColmunName = "";

         foreach (DataRow dr in dt.Rows)
         {
            T MyClass = Activator.CreateInstance<T>();
            foreach (PropertyInfo _info in _infolist)
            {
               ColmunName = _info.Name;
               objDataFieldAttribute = _info.GetCustomAttributes(typeof(DataFieldAttribute), false);
               if (objDataFieldAttribute != null)
               {
                  DataFieldAttribute[] colattrib = (DataFieldAttribute[])objDataFieldAttribute;
                  if (colattrib.Length > 0)
                  {
                     ColmunName = colattrib[0].FieldName;
                  }
               }
               try
               {
                  if (dr.Table.Columns.IndexOf(ColmunName) >= 0)
                  {
                     _info.SetValue(MyClass, dr[ColmunName], null);
                  }
               }
               catch (Exception e)
               {
                  throw e;
               }
            }
            tlist.Add(MyClass);
         }
         return tlist;
      }
      #endregion

      #region 新增或修改数据表数据
      /// <summary>
      /// 新增或修改数据表数据
      /// </summary>
      /// <typeparam name="T">对应要修改或新增的实体类型</typeparam>
      /// <param name="_myclass">对应要修改或新增的实体</param>
      /// <returns></returns>
      public static bool InsertOrUpdateData<T>(T _myclass)
      {
         string sqlstr = "";
         object objDataFieldAttribute = null;
         Type _type = typeof(T);

         object objtable = _type.GetCustomAttributes(typeof(DataFieldAttribute), false);
         string tablename = "";

         if (objtable != null)
         {
            DataFieldAttribute[] tableattrib = (DataFieldAttribute[])objtable;
            if (tableattrib.Length > 0)
               tablename = tableattrib[0].FieldName;
            else
               tablename = _type.Name;
         }
         else
            tablename = _type.Name;

         PropertyInfo[] _infolist = _type.GetProperties();

         ArrayList ORMap = new ArrayList();
         bool isinsert = true;
         string strpk = "";
         object _obj = null;

         //获得实体和数据库对应关系
         foreach (PropertyInfo _info in _infolist)
         {
            _obj = _type.GetProperty(_info.Name).GetValue(_myclass, null);
            if (_obj != null && _obj.ToString() != "")
            {
               objDataFieldAttribute = _info.GetCustomAttributes(typeof(DataFieldAttribute), false);
               if (objDataFieldAttribute != null)
               {
                  DataFieldAttribute[] colattrib = (DataFieldAttribute[])objDataFieldAttribute;
                  if (colattrib.Length > 0)
                  {
                     DataFieldAttribute _df = colattrib[0];
                     ORMap.Add(new string[] { _df.FieldName, _info.Name });
                     if (_df.PK == "pk")
                     {
                        strpk = _df.FieldName;
                        if (((int)_type.GetProperty(_info.Name).GetValue(_myclass, null)) > 0)
                           isinsert = false;
                     }
                  }
                  else
                     ORMap.Add(new string[] { _info.Name, _info.Name });
               }
               else
                  ORMap.Add(new string[] { _info.Name, _info.Name });
            }
         }

         //整理入参
         SqlParameter[] pams = new SqlParameter[ORMap.Count];
         string insertsql1 = "", insertsql2 = "", updatesql = "";
         int i = 0;
         foreach (string[] mapstr in ORMap)
         {
            pams[i] = new SqlParameter("@" + mapstr[0], _type.GetProperty(mapstr[1]).GetValue(_myclass, null));
            if (strpk != mapstr[0])
            {
               if (isinsert)
               {

                  if (insertsql1 != "")
                  {
                     insertsql1 += ",";
                     insertsql2 += ",";
                  }
                  insertsql1 += mapstr[0];
                  insertsql2 += "@" + mapstr[0];

               }
               else
               {
                  if (updatesql != "")
                     updatesql += ",";
                  updatesql += mapstr[0] + "=@" + mapstr[0];
               }
            }
            i++;
         }
         if (isinsert)
         {
            sqlstr = "insert into " + tablename + "(" + insertsql1 + ") values(" + insertsql2 + ")";
         }
         else
         {
            if (strpk == "")//如果没有主键，则不能进行更新操作
               return false;
            sqlstr = "update " + tablename + " set " + updatesql + " where " + strpk + "=@" + strpk;
         }

         //执行查询
         if (ExecSqlReturn_Param(sqlstr, pams))
            return true;
         else
            return false;
      }
      #endregion


   }
}