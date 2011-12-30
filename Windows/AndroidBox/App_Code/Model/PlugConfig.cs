using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace AndroidBox.Model
{
    public class PlugConfig
    {
        /// <summary>
        /// �������
        /// </summary>
        public string Name;
        /// <summary>
        /// �������
        /// </summary>
        public string DllName;
        ///// <summary>
        ///// ���ݿ�����
        ///// </summary>
        //public string Database;
        ///// <summary>
        ///// �����Ŀ¼
        ///// </summary>
        //public string HtmlRoot;
        /// <summary>
        /// �����ҳ
        /// </summary>
        //public string Index;
        ///// <summary>
        ///// Զ�����ݿ��ļ�������ͬ��
        ///// </summary>
        //public string ServerDatabase;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string Updatetime;
        /// <summary>
        /// Tabλ��
        /// </summary>
        public int TabIndex;
        ///// <summary>
        ///// ���µ�ַ
        ///// </summary>
        //public string UpdateUrl;

        //public string DatabasePath; 

        ///// <summary>
        ///// �Ƿ�������
        ///// </summary>
        //public bool IsSearchOver = true;
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool Enable;

        public override bool Equals(object obj)
        {
            if (obj is PlugConfig)
            {
                PlugConfig cfg = obj as PlugConfig;
                if (cfg.DllName == this.DllName && cfg.TabIndex == this.TabIndex && cfg.Enable == this.Enable)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}


///// <summary>
///// �ӵ�ǰ���Ŀ¼�²����ı�
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