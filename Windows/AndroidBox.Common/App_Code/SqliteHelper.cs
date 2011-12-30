using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace AndroidBox.Common
{
    public class SqliteHelper
    {
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        public static string SYSTEM_DB = @"\Data.dat";
        /// <summary>
        /// 获得连接对象
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection GetSQLiteConnection(string database)
        {
            return new SQLiteConnection("Data Source=" + CurrentDirectory + database);//@"Data Source=cndroid.db");
        }

        //public static void SetDB(string path)
        //{
        //    db_path = "Data Source=" + path;
        //}

        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;

            if (p != null)
            {
                foreach (object parm in p)
                    cmd.Parameters.AddWithValue(string.Empty, parm);
                //for (int i = 0; i < p.Length; i++)
                //    cmd.Parameters[i].Value = p[i];
            }
        }

        public static DataSet ExecuteDataset(string connectionString, string cmdText, params object[] p)
        {
            DataSet ds = new DataSet();
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(connectionString))
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds);
            }

            return ds;
        }

        public DataSet ExecuteDataset(string cmdText, params object[] p)
        {
            return ExecuteDataset(GetDatabasePath(), cmdText, p);
        }

        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();

            using (SQLiteConnection connection = GetSQLiteConnection(connectionString))
            {
                PrepareCommand(command, connection, cmdText, p);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="p"></param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string cmdText, params object[] p)
        {
            return ExecuteNonQuery(GetDatabasePath(), cmdText, p);
        }

        public virtual string GetDatabasePath()
        {
            return SYSTEM_DB;
        }

        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static SQLiteDataReader ExecuteReader(string connectionString, string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = GetSQLiteConnection(connectionString);
            try
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            return ExecuteReader(GetDatabasePath(), cmdText, p);
        }

        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, string cmdText, params object[] p)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection connection = GetSQLiteConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, p);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, params object[] p)
        {
            return ExecuteScalar(GetDatabasePath(), cmdText, p);
        }


        /// <summary>
        /// 处理NULL情况
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DBToString(object val)
        {
            return DBToString(val, "");
        }

        /// <summary>
        /// 处理NULL情况
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defVal">默认值</param>
        /// <returns></returns>
        public static string DBToString(object val, string defVal)
        {
            if (val == DBNull.Value || val == null || string.IsNullOrEmpty(val.ToString()))
                return defVal;
            else
                return val.ToString();
        }

        /// <summary>
        /// 处理NULL情况
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int DBToInt(object val)
        {
            return DBToInt(val, 0);
        }

        /// <summary>
        /// 处理NULL情况
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static int DBToInt(object val, int defVal)
        {
            if (val == DBNull.Value || val == null || string.IsNullOrEmpty(val.ToString()))
                return defVal;

            int result;
            if (!int.TryParse(val.ToString(), out result))
                result = defVal;

            return result;
        }

    }
}

//public static DataRow ExecuteDataRow(string cmdText, params object[] p)
//{
//    DataSet ds = ExecuteDataset(cmdText, p);
//    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        return ds.Tables[0].Rows[0];
//    return null;
//}


///// <summary>
///// 分页
///// </summary>
///// <param name="recordCount"></param>
///// <param name="pageIndex"></param>
///// <param name="pageSize"></param>
///// <param name="cmdText"></param>
///// <param name="countText"></param>
///// <param name="p"></param>
///// <returns></returns>
//public static DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, params object[] p)
//{
//    if (recordCount < 0)
//        recordCount = int.Parse(ExecuteScalar(countText, p).ToString());

//    DataSet ds = new DataSet();

//    SQLiteCommand command = new SQLiteCommand();
//    using (SQLiteConnection connection = GetSQLiteConnection())
//    {
//        PrepareCommand(command, connection, cmdText, p);
//        SQLiteDataAdapter da = new SQLiteDataAdapter(command);
//        da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
//    }
//    return ds;
//}
