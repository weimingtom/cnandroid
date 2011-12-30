using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace AndroidBox.Common
{
    public class TreeBusiness
    {
        /// <summary>
        /// id,name,content_url,parent_id,isroot
        /// </summary>
        private const string SQL_ALL_TREE = "SELECT * FROM tree WHERE parent_id = 0";

        public virtual TreeNode[] GetTree()
        {

        }


        /// <summary>
        /// ��ȡRSS��Ŀ
        /// </summary>
        /// <returns></returns>
        public TreeNode[] GetTree(bool OnlyRoot)
        {
            DataTable dt = SqliteHelper.ExecuteDataset(Constants.DB_PATH, "SELECT id,name,content_url,parent_id,isroot,updatetime,code,cache_count FROM tree WHERE parent_id=0 ORDER BY id").Tables[0];
            //��ȡ����Ŀ¼
            //DataRow[] drs = dt.Rows;// dt.Select("parent_id=0", "name");
            TreeNode[] result = new TreeNode[dt.Rows.Count];
            for (int i = 0, j = dt.Rows.Count; i < j; i++)
            {
                DataRow dr = dt.Rows[i];
                ArticleColumn article = new ArticleColumn();
                string name = SqliteHelper.DBToString(dr["name"]);
                if (string.IsNullOrEmpty(name))
                    continue;
                article.Id = dr["id"].ToString();
                article.Name = name;
                article.ContentUrl = SqliteHelper.DBToString(dr["content_url"]);
                article.ParentId = SqliteHelper.DBToString(dr["parent_id"], "0");
                article.IsRoot = SqliteHelper.DBToString(dr["isroot"]) == "1";
                article.Updatetime = SqliteHelper.DBToString(dr["updatetime"]);
                article.Code = SqliteHelper.DBToString(dr["code"]);
                article.CacheCount = SqliteHelper.DBToInt(dr["cache_count"]);
                //article.AuthorId = SqliteHelper.DBToString(dr["author_id"]);
                //article.IsManualUpdate = dr.IsNull("tree_id");
                TreeNode node = new TreeNode();
                node.Text = article.Name;
                node.ToolTipText = "F2�༭���ƣ��Ҽ���Ӻ�ɾ��";
                node.Name = article.Id; //id
                node.Tag = article; //path
                node.Expand();
                if (article.IsRoot)
                {
                    //node.Nodes.Add("");
                    CreateTreeNodeRecursive(node);
                    node.ImageIndex = 0;
                }
                else
                    node.ImageIndex = 2;

                result[i] = node;
            }
            return result;
        }



        /// <summary>
        /// ��ȡ�Ӳ˵�
        /// </summary>
        /// <param name="database"></param>
        /// <param name="baseNode"></param>
        public void CreateTreeNodeRecursive(TreeNode baseNode)
        {
            DataTable dt = SqliteHelper.ExecuteDataset(Constants.DB_PATH, "SELECT id,name,content_url,parent_id,isroot,updatetime,code,cache_count FROM tree WHERE parent_id='" + baseNode.Name + "' ORDER BY id").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode();
                ArticleColumn article = new ArticleColumn();
                string name = SqliteHelper.DBToString(dr["name"]);
                if (string.IsNullOrEmpty(name))
                    continue;
                article.Id = dr["id"].ToString();
                article.Name = name;
                article.ContentUrl = SqliteHelper.DBToString(dr["content_url"]);
                article.ParentId = SqliteHelper.DBToString(dr["parent_id"], "0");
                article.IsRoot = SqliteHelper.DBToString(dr["isroot"]) == "1";
                article.Updatetime = SqliteHelper.DBToString(dr["updatetime"]);
                article.Code = SqliteHelper.DBToString(dr["code"]);
                article.CacheCount = int.Parse(SqliteHelper.DBToString(dr["cache_count"]));
                //article.AuthorId = SqliteHelper.DBToString(dr["author_id"]);
                //article.IsManualUpdate = dr.IsNull("tree_id");
                //node.ToolTipText = dr[2].ToString();
                node.Text = article.Name;
                node.Name = article.Id; //id
                node.Tag = article;  //path

                if (article.IsRoot)
                {
                    //node.Nodes.Add("");
                    node.ImageIndex = 0;
                }
                else
                    node.ImageIndex = 2;
                if (article.IsRoot)
                    CreateTreeNodeRecursive(node);
                baseNode.Nodes.Add(node);
            }
        }

        /// <summary>
        /// ��ȡ�� SQL���
        /// </summary>
        /// <returns></returns>
        public virtual string GetTreeSQL()
        {
            return SQL_ALL_TREE;
        }

        /// <summary>
        /// ��ȡ���ݿ�
        /// </summary>
        /// <returns></returns>
        public virtual string GetDB()
        {
            return SqliteHelper.SYSTEM_DB;
        }
    }
}
