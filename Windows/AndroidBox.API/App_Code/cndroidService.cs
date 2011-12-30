using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using AndroidBox.API.Model;

namespace AndroidBox.API
{
    /// <summary>
    /// 处理业务，包括数据库存储
    /// </summary>
    public class cndroidService : AndroidBox.cndroidService
    {
        private string DB;
        public cndroidService(string DB)
        {
            this.DB = DB;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public PlugConfig GetConfig()
        {
            PlugConfig cfg = new PlugConfig();
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(DB, "SELECT key,value FROM config");
            while (sdr.Read())
            {
                string key = SqliteHelper.DBToString(sdr.GetString(0));
                string value = SqliteHelper.DBToString(sdr.GetString(1));
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                    continue;
                switch (key)
                {
                    case "Name":
                        cfg.Name = value;
                        break;
                    case "HtmlRoot":
                        cfg.HtmlRoot = value;
                        break;
                    case "Index":
                        cfg.Index = value;
                        break;
                    case "ServerDatabase":
                        cfg.ServerDatabase = value;
                        break;
                }
            }
            sdr.Close();
            return cfg;
        }

        /// <summary>
        /// 获取译者信息
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        public Author GetAuthorById(string AuthorId)
        {
            Author author = new Author();
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(DB, "SELECT * FROM author WHERE id=" + AuthorId);
            if (sdr.Read())
            {
                author.Id = sdr["id"].ToString();
                author.Name = SqliteHelper.DBToString(sdr["name"]);
                author.Blog = SqliteHelper.DBToString(sdr["blog"]);
                author.About = SqliteHelper.DBToString(sdr["about"]);
                author.City = SqliteHelper.DBToString(sdr["city"]);
                author.Twitter = SqliteHelper.DBToString(sdr["twitter"]);
                author.ParticipterDate = SqliteHelper.DBToString(sdr["participte_date"]);
            }
            sdr.Close();
            return author;
        }

        /// <summary>
        /// 获取待下载列表
        /// </summary>
        /// <param name="database"></param>
        /// <param name="updatetime"></param>
        /// <returns></returns>
        public List<Download> GetDownloadList(string updatetime)
        {
            List<Download> result = new List<Download>();
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(DB, "SELECT d.id,d.tree_id,d.path,t.name,t.content_url FROM download d INNER JOIN tree t ON t.id = d.tree_id WHERE t.updatetime > ?", updatetime);
            while (sdr.Read())
            {
                Download down = new Download();
                down.Id = SqliteHelper.DBToString(sdr["id"]);
                down.TreeId = SqliteHelper.DBToString(sdr["tree_id"]);
                down.Path = SqliteHelper.DBToString(sdr["path"]);
                down.Name = SqliteHelper.DBToString(sdr["name"]);
                down.FileName = SqliteHelper.DBToString(sdr["content_url"]);
                result.Add(down);
            }
            sdr.Close();
            return result;
        }

        /// <summary>
        /// 创建树菜单
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public TreeNode[] GetTree()
        {
            DataTable dt = SqliteHelper.ExecuteDataset(DB, "SELECT tree.id,tree.name,tree.content_url,tree.parent_id,tree.isroot,tree.online_url,tree.word_url,tree.code_url,tree.author_id,download.tree_id FROM tree LEFT JOIN download ON tree.id = download.tree_id AND download.path like '%.htm' WHERE tree.parent_id=0 ORDER BY tree.name").Tables[0];
            //获取顶级目录
            //DataRow[] drs = dt.Rows;// dt.Select("parent_id=0", "name");
            TreeNode[] result = new TreeNode[dt.Rows.Count];
            for (int i = 0, j = dt.Rows.Count; i < j; i++)
            {
                DataRow dr = dt.Rows[i];
                Article article = new Article();
                string name = SqliteHelper.DBToString(dr["name"]);
                if (string.IsNullOrEmpty(name))
                    continue;
                article.Id = dr["id"].ToString();
                article.Name = name;
                article.ContentUrl = SqliteHelper.DBToString(dr["content_url"]);
                article.ParentId = SqliteHelper.DBToString(dr["parent_id"], "0");
                article.IsRoot = SqliteHelper.DBToString(dr["isroot"]) == "1";
                article.Online = SqliteHelper.DBToString(dr["online_url"]);
                article.WordUrl = SqliteHelper.DBToString(dr["word_url"]);
                article.CodeUrl = SqliteHelper.DBToString(dr["code_url"]);
                article.AuthorId = SqliteHelper.DBToString(dr["author_id"]);
                article.IsManualUpdate = dr.IsNull("tree_id");
                TreeNode node = new TreeNode();
                node.Text = article.Name;
                //node.ToolTipText = dr[2].ToString();
                node.Name = article.Id; //id
                node.Tag = article; //path

                if (article.IsRoot)
                {
                    node.Nodes.Add("");
                    //CreateTreeNodeRecursive(node, dt);
                    node.ImageIndex = 0;
                }
                else
                    node.ImageIndex = 2;

                result[i] = node;
            }
            return result;
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="database"></param>
        /// <param name="baseNode"></param>
        public void CreateTreeNodeRecursive(TreeNode baseNode)
        {
            DataTable dt = SqliteHelper.ExecuteDataset(DB, "SELECT tree.id,tree.name,tree.content_url,tree.parent_id,tree.isroot,tree.online_url,tree.word_url,tree.code_url,tree.author_id,download.tree_id FROM tree LEFT JOIN download ON tree.id = download.tree_id AND download.path like '%.htm' WHERE tree.parent_id='" + baseNode.Name + "' ORDER BY tree.name").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode();
                Article article = new Article();
                string name = SqliteHelper.DBToString(dr["name"]);
                if (string.IsNullOrEmpty(name))
                    continue;
                article.Id = dr["id"].ToString();
                article.Name = name;
                article.ContentUrl = SqliteHelper.DBToString(dr["content_url"]);
                article.ParentId = SqliteHelper.DBToString(dr["parent_id"], "0");
                article.IsRoot = SqliteHelper.DBToString(dr["isroot"]) == "1";
                article.Online = SqliteHelper.DBToString(dr["online_url"]);
                article.WordUrl = SqliteHelper.DBToString(dr["word_url"]);
                article.CodeUrl = SqliteHelper.DBToString(dr["code_url"]);
                article.AuthorId = SqliteHelper.DBToString(dr["author_id"]);
                //node.ToolTipText = dr[2].ToString();
                node.Text = article.Name;
                node.Name = article.Id; //id
                node.Tag = article;  //path

                if (article.IsRoot)
                {
                    node.Nodes.Add("");
                    node.ImageIndex = 0;
                }
                else
                    node.ImageIndex = 2;

                //CreateTreeNodeRecursive(node, dataSource);
                baseNode.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 取得所有文章的名字
        /// </summary>
        /// <param name="cfgs"></param>
        /// <returns></returns>
        public AutoCompleteStringCollection GetAllName(PlugConfig cfg)
        {
            AutoCompleteStringCollection result = new AutoCompleteStringCollection();
            List<string> names = new List<string>();
            int preCount = result.Count;
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(DB, "SELECT name FROM tree");
            while (sdr.Read())
            {
                string name = SqliteHelper.DBToString(sdr[0]);
                if (!string.IsNullOrEmpty(name))
                    names.Add(name);
            }
            sdr.Close();
            cfg.ArticleNames = names;
            result.AddRange(names.ToArray());
            return result;
        }

        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetAllPath(PlugConfig cfg)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(DB, "SELECT content_url,name FROM tree");
            while (sdr.Read())
            {
                result.Add(SqliteHelper.DBToString(sdr.GetString(0)), SqliteHelper.DBToString(sdr.GetString(1)));
            }
            sdr.Close();
            return result;
        }

        /// <summary>
        /// 获取最后一次更新的时间
        /// </summary>
        /// <returns></returns>
        public string GetLastUpdatetime()
        {
            return SqliteHelper.DBToString(SqliteHelper.ExecuteScalar(DB, "SELECT max(updatetime) FROM tree"));
        }
    }
}
