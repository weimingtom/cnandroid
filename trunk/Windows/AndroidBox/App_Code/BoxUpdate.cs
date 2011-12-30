using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;
using AndroidBox.Model;

namespace AndroidBox
{
    internal class BoxUpdate
    {
        /// <summary>
        /// AndroidBox.exe版本
        /// </summary>
        public const string ANDROIBOX_VERSION = "0.0.1.0";
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();


        /// <summary>
        /// 更新
        /// </summary>
        internal static void Update()
        {
            //仅在联网的情况下进行更新
            if (SystemUtils.IsConnected())
            {
                new Thread(delegate()
                {
                    try
                    {
                        //检测更新程序是否有更新
                        UpboxUpdate();
                        //检测软件是否有更新
                        if (!SelfUpdate())
                        {

                        }
                    }
                    catch
                    {

                    }
                }).Start();
            }
        }

        /// <summary>
        /// 软件本身更新
        /// </summary>
        /// <returns></returns>
        internal static bool SelfUpdate()
        {
            UBox box = UBox.GetBoxUpInfo();
            //配置信息更新

            //软件更新
            if (ANDROIBOX_VERSION != box.SOFT_VERSION)
            {
                DialogResult dialogResult = MessageBox.Show("Android中文合集有新的版本，确定更新吗?", "客户端新版本", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    //传入参数 下载地址和文件名
                    System.Diagnostics.Process.Start("UpBox.exe", box.SOFT_UPDATE_DOWNLOAD_URL1 + " " + box.SOFT_UPDATE_DOWNLOAD_URL1_Name);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新更新文件
        /// </summary>
        internal static void UpboxUpdate()
        {
            UBox box = UBox.GetBoxUpInfo();
            string UPBOX_VERSION = cndroidService.GetLocalUpboxVesion();
            if (string.IsNullOrEmpty(UPBOX_VERSION) || UPBOX_VERSION != box.SOFT_VERSION)
            {
                WebClient wClient = new WebClient();
                try
                {
                    wClient.DownloadFile(box.UPBOX_UPDATE_DOWNLOAD_URL, CurrentDirectory + @"\" + box.UPBOX_UPDATE_DOWNLOAD_URL_Name);
                    wClient.Dispose();
                    //设置更新Upbox.exe的版本
                    cndroidService.SetLocalUpboxVesion(box.SOFT_VERSION);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
