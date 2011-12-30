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
        /// ������ɫ��ʾ����ƥ����
        /// </summary>
        private const string RED = @"<span style='color: red;'>{0}</span>";
        private const string FIND = @"<p text-indent:21.0pt'>{0}    <a href=""{1}"">{2}</a>         {3}<br />{4}</p>";

        /// <summary>
        /// ��ʼ������
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
        /// <param name="key">�����ؼ���</param>
        /// <param name="index">�������ƥ��λ��</param>
        /// <param name="href">���ӵ�ַ</param>
        /// <param name="hrefText">����</param>
        /// <param name="category">���������API</param>
        /// <param name="text">ƥ��������</param>
        /// <returns></returns>
        public static string BuildSearchResultItem(string key, int index, string href, string hrefText, string category, string text)
        {
            return string.Format(FIND, index + ".", href.Replace(@"\", @"/"), hrefText, category, text.Replace("<", "&lt;").Replace(">", "&gt;").Replace(key, string.Format(RED, key)));
        }

    }
}
