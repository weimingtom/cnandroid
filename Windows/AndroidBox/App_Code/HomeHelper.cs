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
        /// ��ʼ������
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
        /// �Ƿ����
        /// </summary>
        /// <returns>true����false������</returns>
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
