using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace AndroidBox.API
{
    public class SqliteHelper : AndroidBox.SqliteHelper
    {
        //private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private static string DB = @"\Android_API\db.dat";

        public override string GetDatabasePath()
        {
            return DB;
        }

        ///// <summary>
        ///// 获得连接对象
        ///// </summary>
        ///// <returns></returns>
        //public static SQLiteConnection GetSQLiteConnection()
        //{
        //    return new SQLiteConnection("Data Source=" + CurrentDirectory + SYSTEM_DB);//@"Data Source=cndroid.db");
        //}

        //private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        //{

        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();
        //    cmd.Parameters.Clear();
        //    cmd.Connection = conn;
        //    cmd.CommandText = cmdText;

        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandTimeout = 30;

        //    if (p != null)
        //    {
        //        foreach (object parm in p)
        //            cmd.Parameters.AddWithValue(string.Empty, parm);
        //        //for (int i = 0; i < p.Length; i++)
        //        //    cmd.Parameters[i].Value = p[i];
        //    }
        //}

        //public static DataSet ExecuteDataset(string cmdText, params object[] p)
        //{
        //    DataSet ds = new DataSet();
        //    SQLiteCommand command = new SQLiteCommand();
        //    using (SQLiteConnection connection = GetSQLiteConnection())
        //    {
        //        PrepareCommand(command, connection, cmdText, p);
        //        SQLiteDataAdapter da = new SQLiteDataAdapter(command);
        //        da.Fill(ds);
        //    }

        //    return ds;
        //}

        ///// <summary>
        ///// 返回受影响的行数
        ///// </summary>
        ///// <param name="cmdText">a</param>
        ///// <param name="commandParameters">传入的参数</param>
        ///// <returns></returns>
        //public static int ExecuteNonQuery(string cmdText, params object[] p)
        //{
        //    SQLiteCommand command = new SQLiteCommand();

        //    using (SQLiteConnection connection = GetSQLiteConnection())
        //    {
        //        PrepareCommand(command, connection, cmdText, p);
        //        return command.ExecuteNonQuery();
        //    }
        //}

        ///// <summary>
        ///// 返回SqlDataReader对象
        ///// </summary>
        ///// <param name="cmdText"></param>
        ///// <param name="commandParameters">传入的参数</param>
        ///// <returns></returns>
        //public static SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        //{
        //    ExecuteReader(DB, cmdText, p);
        //}

        ///// <summary>
        ///// 返回结果集中的第一行第一列，忽略其他行或列
        ///// </summary>
        ///// <param name="cmdText"></param>
        ///// <param name="commandParameters">传入的参数</param>
        ///// <returns></returns>
        //public static object ExecuteScalar(string cmdText, params object[] p)
        //{
        //    SQLiteCommand cmd = new SQLiteCommand();

        //    using (SQLiteConnection connection = GetSQLiteConnection())
        //    {
        //        PrepareCommand(cmd, connection, cmdText, p);
        //        return cmd.ExecuteScalar();
        //    }
        //}

        ///// <summary>
        ///// 处理NULL情况
        ///// </summary>
        ///// <param name="val"></param>
        ///// <returns></returns>
        //public static string DBToString(object val)
        //{
        //    return DBToString(val, "");
        //}

        //public static string DBToString(object val, string defVal)
        //{
        //    if (val == DBNull.Value || val == null || string.IsNullOrEmpty(val.ToString()))
        //        return defVal;
        //    else
        //        return val.ToString();
        //}

    }
}
