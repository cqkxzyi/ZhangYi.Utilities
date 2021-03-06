﻿/*mysql 数据库操作方法
 * 
 * 版权所有：2010张毅
 * 开发部门：IT 
 * 程序负责：zhangyi
 * 电话：13594663608
 * 其他联系：重庆市、深圳市
 * Email：kxyi-lover@163.com
 * MSN：10011
 * QQ:284124391
 * 
 * 开发时间：2012年2月28日
 * 声明：仅限于您自己使用，不得进行商业传播，违者必究！
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace DotNet.zy.Utilities
{
    public class ExecMySql
    {
        public static string connstr()
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings["connstring"];
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 执行无需返回结果的sql语句
        /// </summary>
        /// <param name="SqlStr">sql语句</param>
        /// <returns>返回相应的比数</returns>
        public static int SqlExecNoquery(string SqlStr)
        {
            #region
            MySqlConnection conn = new MySqlConnection(connstr());
            int Result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlStr, conn);
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ErrorLog.sendLog(e, "");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return Result;
            #endregion
        }

        /// <summary>
        /// 执行无需返回结果、带参数的sql语句
        /// </summary>
        /// <param name="SqlStr">sql语句</param>
        /// <param name="parm">参数组</param>
        /// <returns></returns>
        public static int SqlExecNoquery(string SqlStr, params MySqlParameter[] cmdParms)
        {
            #region
            int Result = 0;
            MySqlConnection conn = new MySqlConnection(connstr());

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(SqlStr, conn);
            foreach (MySqlParameter pam in cmdParms)
            {
                cmd.Parameters.Add(pam);
            }
            try
            {
                Result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ErrorLog.sendLog(e, "");
                Result = 0;
            }
            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
                conn.Dispose();
            }
            return Result;
            #endregion
        }


        /// <summary>
        /// 执行返回ds的查询
        /// </summary>
        /// <param name="SqlStr">sql语句</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string SqlStr)
        {
            #region
            MySqlConnection conn = new MySqlConnection(connstr());
            DataSet ds = new DataSet();

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(SqlStr, conn);

                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception e)
            {
                ErrorLog.sendLog(e, "");
            }
            finally
            {
                conn.Dispose();
            }
            return ds;
            #endregion
        }

        /// <summary>
        /// 执行返回ds的查询
        /// </summary>
        /// <param name="SqlStr">sql语句</param>
        /// <param name="sqlconn">数据库连接字符串</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string SqlStr, string sqlconn)
        {
            #region
            MySqlConnection conn = new MySqlConnection(sqlconn);
            DataSet ds = new DataSet();

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(SqlStr, conn);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception e)
            {
                ErrorLog.sendLog(e, "");
            }
            finally
            {
                conn.Dispose();
            }
            return ds;
            #endregion
        }

        /// <summary>
        /// 执行带参数的查询，返回ds
        /// </summary>
        /// <param name="SqlStr">sql查询语句</param>
        /// <param name="cmdParms">参数组</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string SqlStr, params MySqlParameter[] cmdParms)
        {
            #region
            MySqlConnection conn = new MySqlConnection(connstr());
            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(SqlStr, conn);
            da.SelectCommand.Parameters.Clear();
            foreach (MySqlParameter pam in cmdParms)
            {
                da.SelectCommand.Parameters.Add(pam);
            }

            try
            {
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ErrorLog.sendLog(e, "");
            }
            finally
            {
                da.Dispose();
                conn.Dispose();
            }
            return ds;
            #endregion
        }

        #region  2.0 方法

        /// <summary>
        /// 执行sql语句，返回实体对象组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public static List<T> GetTableList<T>(string SqlStr)
        {
            DataTable dt = GetDataSet(SqlStr).Tables[0];
            if (dt == null)
                return null;
            List<T> _t = new List<T>();
            Type _type = typeof(T);

            foreach (DataRow dr in dt.Rows)
            {
                _t.Add((T)Activator.CreateInstance(_type, new object[] { dr }));
            }
            return _t;
        }

        /// <summary>
        /// 执行带参数的Sql语句，返回实体对象组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static List<T> GetTableList<T>(string SqlStr, params MySqlParameter[] cmdParms)
        {
            DataTable dt = GetDataSet(SqlStr, cmdParms).Tables[0];
            if (dt == null)
                return null;
            List<T> _t = new List<T>();
            Type _type = typeof(T);

            foreach (DataRow dr in dt.Rows)
            {
                _t.Add((T)Activator.CreateInstance(_type, new object[] { dr }));
            }
            return _t;
        }

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
            MySqlParameter[] pams = new MySqlParameter[ORMap.Count];
            string insertsql1 = "", insertsql2 = "", updatesql = "";
            int i = 0;
            foreach (string[] mapstr in ORMap)
            {
                pams[i] = new MySqlParameter("?" + mapstr[0], _type.GetProperty(mapstr[1]).GetValue(_myclass, null));
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
                        insertsql2 += "?" + mapstr[0];

                    }
                    else
                    {
                        if (updatesql != "")
                            updatesql += ",";
                        updatesql += mapstr[0] + "=?" + mapstr[0];
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
                sqlstr = "update " + tablename + " set " + updatesql + " where " + strpk + "=?" + strpk;
            }

            //执行查询
            if (SqlExecNoquery(sqlstr, pams) > 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}
