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
        /// AndroidBox.exe�汾
        /// </summary>
        public const string ANDROIBOX_VERSION = "0.0.1.0";
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();


        /// <summary>
        /// ����
        /// </summary>
        internal static void Update()
        {
            //��������������½��и���
            if (SystemUtils.IsConnected())
            {
                new Thread(delegate()
                {
                    try
                    {
                        //�����³����Ƿ��и���
                        UpboxUpdate();
                        //�������Ƿ��и���
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
        /// ����������
        /// </summary>
        /// <returns></returns>
        internal static bool SelfUpdate()
        {
            UBox box = UBox.GetBoxUpInfo();
            //������Ϣ����

            //�������
            if (ANDROIBOX_VERSION != box.SOFT_VERSION)
            {
                DialogResult dialogResult = MessageBox.Show("Android���ĺϼ����µİ汾��ȷ��������?", "�ͻ����°汾", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    //������� ���ص�ַ���ļ���
                    System.Diagnostics.Process.Start("UpBox.exe", box.SOFT_UPDATE_DOWNLOAD_URL1 + " " + box.SOFT_UPDATE_DOWNLOAD_URL1_Name);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ���¸����ļ�
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
                    //���ø���Upbox.exe�İ汾
                    cndroidService.SetLocalUpboxVesion(box.SOFT_VERSION);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
