using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AndroidBox.Html
{
    public class HtmlSearch
    {
        public const string Name = "search.htm";
        private const string HTML = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN"" ""http://www.w3.org/TR/html4/loose.dtd""><html>
<head>
<meta http-equiv=Content-Type content=""text/html; charset=gb2312"">
<title>AndroidBox Search</title>
</head>
<body>  
</body>
</html>
";
        /// <summary>
        /// 高亮红色显示搜索匹配项
        /// </summary>
        private const string RED = @"<span style='color: red;'>{0}</span>";
        private const string FIND = @"<p text-indent:21.0pt'>{0}    <a href=""{1}"">{2}</a>         {3}<br />{4}</p>";

        /// <summary>
        /// 初始化界面
        /// </summary>
        public static void Init()
        {
            string path = Directory.GetCurrentDirectory() + @"\" + Name;
            if (!File.Exists(path))
            {
                try
                {
                    File.AppendAllText(path, HTML);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">搜索关键字</param>
        /// <param name="index">搜索结果匹配位置</param>
        /// <param name="href">链接地址</param>
        /// <param name="hrefText">标题</param>
        /// <param name="category">类别，如中文API</param>
        /// <param name="text">匹配行内容</param>
        /// <returns></returns>
        public static string BuildSearchResultItem(string key, int index, string href, string hrefText, string category, string text)
        {
            return string.Format(FIND, index + ".", href.Replace(@"\", @"/"), hrefText, category, text.Replace("<", "&lt;").Replace(">", "&gt;").Replace(key, string.Format(RED, key)));
        }

    }
}
