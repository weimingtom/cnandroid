using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace AndroidBox.API.Model
{
    public class PlugConfig
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name;
        ///// <summary>
        ///// 插件目录名称
        ///// </summary>
        //public string DirName;
        ///// <summary>
        ///// 数据库名称
        ///// </summary>
        //public string Database;
        /// <summary>
        /// 插件根目录
        /// </summary>
        public string HtmlRoot;
        /// <summary>
        /// 插件首页
        /// </summary>
        public string Index;
        /// <summary>
        /// 远程数据库文件，用于同步
        /// </summary>
        public string ServerDatabase;
        /// <summary>
        /// 更新时间
        /// </summary>
        //public string Updatetime;
        /// <summary>
        /// Tab位置
        /// </summary>
        //public int TabIndex;
        ///// <summary>
        ///// 更新地址
        ///// </summary>
        //public string UpdateUrl;

        //public string DatabasePath; 

        /// <summary>
        /// 文章标题
        /// </summary>
        public List<string> ArticleNames;

        /// <summary>
        /// 是否检索完毕
        /// </summary>
        public bool IsSearchOver = true;

        /// <summary>
        /// 检测更新并下载最新的
        /// </summary>
        /// <returns></returns>
        public void Update()
        {
            //if (string.IsNullOrEmpty(UpdateUrl))
            //    return;


            //WebClient wClient = new WebClient();
            //string tmpDB = Database + ".tmp";
            //try
            //{
            //    //下载待更新列表
            //    wClient.DownloadFile(ServerDatabase, Directory.GetCurrentDirectory() + tmpDB);

            //    //查找服务器最后一次更新时间
            //    //
            //}
            //catch
            //{

            //}
        }
    }
}


///// <summary>
///// 从当前插件目录下查找文本
///// </summary>
///// <param name="key"></param>
///// <returns></returns>
//public List<string> FindFullText(string key)
//{
//    List<string> result = new List<string>();
//    IDictionary<string, string> paths = new cndroidService().GetAllPath(this);
//    foreach (string path in paths.Keys)
//    {
//        if (IsStopFind)
//            break;
//        try
//        {
//            string fullPath = CurrentDirectory + path;
//            using (StreamReader sr = new StreamReader(fullPath))
//            {
//                string line;
//                while ((line = sr.ReadLine()) != null)
//                {
//                    int index = line.IndexOf(key);
//                    if (index > -1)
//                    {
//                        result.Add(string.Format(FIND, Name, fullPath, paths[path], line.Replace(key, string.Format(RED, key))));
//                    }
//                }
//                sr.Close();
//            }
//        }
//        catch
//        {
//            continue;
//        }

//    }
//    return result;
//}

//private const string BR = "<br />";
//private const string HREF = @"<a href=""{0}"">{1}</a>";
//private const string RED = @"<span style='background:#D9D9D9'>{0}</span>";
//private const string FIND = @"<p>{0}    <a href=""{1}"">{2}</a><br />{3}</p>";
//private readonly static string CurrentDirectory = Directory.GetCurrentDirectory();