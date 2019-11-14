using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

    public class SqlHelper2
    {
        public static string ConnectionString =  ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        /// <summary>
        /// SqlHelper 的摘要说明
        /// </summary>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 100;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }


        /// <summary>
        /// SqlHelper 的摘要说明
        /// </summary>
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, param);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cmd.Parameters.Clear();
                return dataTable;
            }
        }
        public static DataTable ExecuteTable(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cmd.Parameters.Clear();
                return dataTable;
            }
        }
        public static DataTable ExecuteSqlQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, sql, null);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cmd.Parameters.Clear();
                return dataTable;
            }
        }

        /// <summary>
        /// SqlHelper 的摘要说明
        /// </summary>
        public static DataSet ExecuteDataset(string ConnectionString, CommandType cmdType, string cmdText, SqlParameter[] param)
        {
            return ExecuteDataset(cmdType, cmdText, param);

        }
        public static DataSet ExecuteDataset(CommandType cmdType, string cmdText, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, param);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet DataSet = new DataSet();
                adapter.Fill(DataSet);
                cmd.Parameters.Clear();
                return DataSet;
            }
        }
        public static DataSet ExecuteDataset(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                cmd.Parameters.Clear();
                return dataSet;
            }
        }

        /// <summary>
        /// SqlHelper 的摘要说明
        /// </summary>
        public static int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(ConnectionString, CommandType.Text, cmdText, null);
        }
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(ConnectionString, cmdType, cmdText, null);
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }


        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(ConnectionString, cmdType, cmdText, commandParameters);
        }
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return num;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return num;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return num;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// SqlHelper 的摘要说明
        /// </summary>
        public static SqlDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(ConnectionString, CommandType.Text, cmdText, null);
        }
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            return ExecuteReader(ConnectionString, cmdType, cmdText, null);
        }
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(ConnectionString, cmdType, cmdText, commandParameters);
        }
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader2;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {
                conn.Close();
                throw;
            }
            return reader2;
        }

        /// <summary>
        /// SqlHelper 的摘要说明
        /// </summary>
        public static object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(ConnectionString, CommandType.Text, cmdText, null);
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(ConnectionString, cmdType, cmdText, null);
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, cmdType, cmdText, commandParameters);
        }
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object obj2 = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return obj2;
        }
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return obj2;
            }
        }


        public static void ExecuteSqlNoneQueryForGO(string sql)
        {
            int num2;
            int startIndex = 0;
            string str = "\r\nGO\r\n";
        Label_0008:
            num2 = sql.IndexOf(str, startIndex);
            int length = ((num2 > startIndex) ? num2 : sql.Length) - startIndex;
            string cmdText = sql.Substring(startIndex, length);
            if (cmdText.Trim().Length > 0)
            {
                ExecuteNonQuery(cmdText);
            }
            if (num2 != -1)
            {
                startIndex = num2 + str.Length;
                if (startIndex < sql.Length)
                {
                    goto Label_0008;
                }
            }
        }

        public static int DeleteByWhere(string UspName, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.StoredProcedure, UspName, param);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return num;
            }
        }
    }