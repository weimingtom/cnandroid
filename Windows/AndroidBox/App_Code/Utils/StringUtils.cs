using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidBox
{
    public static class StringUtils
    {
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";

            int startIndex = path.LastIndexOf('/');
            if (startIndex < 0 || startIndex + 1 > path.Length)
                return "";
            return path.Substring(startIndex + 1);
        }
    }
}
