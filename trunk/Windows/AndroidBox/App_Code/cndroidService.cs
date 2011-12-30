using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using AndroidBox.Model;

namespace AndroidBox
{
    /// <summary>
    /// 处理业务，包括数据库存储
    /// </summary>
    public class cndroidService
    {

        #region Member Variable

        private const string SQL_INSERT_TEXT = "INSERT INTO tree(name,path,parent_id,type) VALUES(?,?,?,?);";
        private const string SQL_IDENDITY = "select last_insert_rowid()";

        #endregion

        #region Config

        /// <summary>
        /// 获取缓存的插件
        /// </summary>
        /// <returns></returns>
        public static List<PlugConfig> GetCachePlugs()
        {
            List<PlugConfig> result = new List<PlugConfig>();
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(SqliteHelper.SYSTEM_DB, "SELECT DirName,[index],updatetime,tab_name,enable FROM tabs WHERE enable = 1 order by [index]");
            while (sdr.Read())
            {
                PlugConfig cfg = new PlugConfig();
                cfg.DllName = SqliteHelper.DBToString(sdr.GetString(0));
                cfg.TabIndex = sdr.GetInt32(1);
                cfg.Updatetime = sdr.IsDBNull(2) ? "" : SqliteHelper.DBToString(sdr.GetString(2));
                cfg.Name = SqliteHelper.DBToString(sdr.GetString(3));
                cfg.Enable = "1".Equals(SqliteHelper.DBToString(sdr.GetString(4)));
                result.Add(cfg);
            }
            sdr.Close();
            return result;
        }

        /// <summary>
        /// 更新插件状态
        /// </summary>
        public void UpdatePlugEnable(string id, string enable)
        {
            SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "UPDATE tabs SET enable = ? WHERE id = ?", enable, id);
        }

        /// <summary>
        /// 更新插件名称
        /// </summary>
        public void UpdatePlugTitle(string id, string title)
        {
            SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "UPDATE tabs SET tab_name = ? WHERE id = ?", title, id);
        }

        /// <summary>
        /// 更新插件顺序
        /// </summary>
        public void UpdatePlugIndex(string id, string index)
        {
            SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "UPDATE tabs SET [index] = ? WHERE id = ?", index, id);
        }

        public DataTable GetAllPlugs()
        {
            return SqliteHelper.ExecuteDataset(SqliteHelper.SYSTEM_DB, "SELECT * FROM tabs").Tables[0];
        }

        public static string GetRemoteVesionPath()
        {
            return SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(SqliteHelper.SYSTEM_DB, "SELECT value FROM config WHERE key='SOFT_UPDATE' limit 1"));
        }

        public static string GetRemoteSoftPath()
        {
            return SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(SqliteHelper.SYSTEM_DB, "SELECT value FROM config WHERE key='SOFT_UPDATE_URL' limit 1"));
        }

        public static string GetRemoteDBPath()
        {
            return SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(SqliteHelper.SYSTEM_DB, "SELECT value FROM config WHERE key='DB_UPDATE_URL' limit 1"));
        }

        public static string GetLocalUpboxVesion()
        {
            return SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(SqliteHelper.SYSTEM_DB, "SELECT value FROM config WHERE key='UPBOX_VERSION' limit 1"));
        }

        public static void SetLocalUpboxVesion(string vesion)
        {
            string id = SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(SqliteHelper.SYSTEM_DB, "SELECT id FROM config WHERE key='UPBOX_VERSION' limit 1"));
            if (string.IsNullOrEmpty(id))
            {
                SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "INSERT INTO config(key,value) VALUES('UPBOX_VERSION',?)", vesion);
            }
            else
            {
                SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "UPDATE config SET key='UPBOX_VERSION' WHERE value=?", vesion);
            }
        }

        /// <summary>
        /// 更新Tab数据更新时间
        /// </summary>
        /// <param name="DirName"></param>
        /// <param name="Updatetime"></param>
        public static void SetTabUpdatetime(string DirName, string Updatetime)
        {
            SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "UPDATE tabs SET updatetime=? WHERE DirName=?", Updatetime, DirName);
        }

        #endregion

        #region Log

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="date"></param>
        /// <param name="article_id"></param>
        public void InsertLog(string type, string message, string src, string date)
        {
            SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "INSERT INTO [log](type,message,src,date,state) VALUES(?,?,?,?,0)", type, message, src, date);
        }

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLog()
        {
            return SqliteHelper.ExecuteDataset(SqliteHelper.SYSTEM_DB, "SELECT id,type,message,src,date,case  when state=0 then '未读' when state=1 then '已读' end as state FROM [log] ORDER BY date DESC").Tables[0];
        }

        /// <summary>
        /// 更新日志阅读状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public void UpdateLogState(string id, string state)
        {
            SqliteHelper.ExecuteNonQuery(SqliteHelper.SYSTEM_DB, "UPDATE [log] SET state=? WHERE id=?", state, id);
        }

        /// <summary>
        /// 判断今日是否有更新
        /// </summary>
        /// <returns></returns>
        public bool IsTodayNewLog()
        {
            string result = SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(SqliteHelper.SYSTEM_DB, "SELECT COUNT(id) FROM [log] WHERE state = 0 AND date = ?", DateTime.Now.ToShortDateString()));
            return !string.IsNullOrEmpty(result) && result != "0";
        }

        #endregion

        #region Tools

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="command"></param>
        /// <param name="p">name,path,parent_id,type 0目录 1文件</param>
        private void PrepareCommand(SQLiteCommand command, params object[] p)
        {
            command.Parameters.Clear();
            if (p != null)
            {
                foreach (object parm in p)
                    command.Parameters.AddWithValue(string.Empty, parm);
            }
        }




        #endregion

    }
}
