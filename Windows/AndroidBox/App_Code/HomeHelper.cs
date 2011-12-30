using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AndroidBox
{
    internal class HomeHelper
    {
        public const string Name = "index.htm";

        /// <summary>
        /// 初始化界面
        /// </summary>
        public static void Init()
        {
            if (!IsExits())
            {
                try
                {
                    //File.AppendAllText(path, HTML);
                    WebClient wClient = new WebClient();
                    //wClient.DownloadFileAsync("http://androidbox.sinaapp.com/about.htm", path);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns>true存在false不存在</returns>
        private static bool IsExits()
        {
            string path = Directory.GetCurrentDirectory() + @"\" + Name;
            return File.Exists(path);
        }

        public static void LoadHomeHtml(WebBrowser browser)
        {
            //if (IsExits())
            //{
            //    browser.Navigate(new Uri(path));
            //}
            //else
            //{

            //}
        }
    }
}
