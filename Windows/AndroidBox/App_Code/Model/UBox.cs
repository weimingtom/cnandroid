using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.CompilerServices;

namespace AndroidBox
{
    public class UBox
    {
        //��ǰ�ļ���
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        //�����ļ�λ��
        private const string url = @"http://files.cnblogs.com/over140/UBox.xml";
        //������Ϣ
        public Dictionary<string, string> Maps = new Dictionary<string, string>();

        private static UBox box;
        /// <summary>
        /// ����汾
        /// </summary>
        public string SOFT_VERSION
        {
            get
            {
                return Maps["SOFT_VERSION"];
            }
        }

        /// <summary>
        /// ���³��� ���ص�ַ
        /// </summary>
        internal string UPBOX_UPDATE_DOWNLOAD_URL
        {
            get
            {
                return GetUrl(Maps["UPBOX_UPDATE_DOWNLOAD_URL"]);
            }
        }

        /// <summary>
        /// ���³��� ���ص�ַ
        /// </summary>
        internal string UPBOX_UPDATE_DOWNLOAD_URL_Name
        {
            get
            {
                return GetName(Maps["UPBOX_UPDATE_DOWNLOAD_URL"]);
            }
        }

        /// <summary>
        /// ����汾 ���ص�ַ
        /// </summary>
        internal string SOFT_UPDATE_DOWNLOAD_URL1
        {
            get
            {
                return GetUrl(Maps["SOFT_UPDATE_DOWNLOAD_URL1"]);
            }
        }

        /// <summary>
        /// ����汾 ���ص�ַ
        /// </summary>
        internal string SOFT_UPDATE_DOWNLOAD_URL1_Name
        {
            get
            {
                return GetName(Maps["SOFT_UPDATE_DOWNLOAD_URL1"]);
            }
        }

        /// <summary>
        /// ��ȡ����������
        /// UPBOX_UPDATE_DOWNLOAD_URL:http://files.cnblogs.com/over140/UpBox.0.4.apk|UpBox.exe
        /// </summary>
        /// <returns></returns>
        public string GetName(string value)
        {
            return value.Split('|')[1];
        }

        /// <summary>
        /// ��ȡURL����
        /// UPBOX_UPDATE_DOWNLOAD_URL:http://files.cnblogs.com/over140/UpBox.0.4.apk|UpBox.exe
        /// </summary>
        /// <returns></returns>
        public string GetUrl(string value)
        {
            return value.Split('|')[0];
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static UBox GetBoxUpInfo()
        {
            if (box == null)
            {
                box = new UBox();
            }
            return box;
        }

        private UBox()
        {
            try
            {
                WebClient wClient = new WebClient();
                string localTmpFile = CurrentDirectory + "\\UBox.tmp";
                wClient.DownloadFile(url, localTmpFile);
                wClient.Dispose();

                using (StreamReader sr = new StreamReader(localTmpFile, Encoding.Default))
                {
                    string line;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                            continue;

                        //string[] items = line.Split(':');
                        int index = line.IndexOf(":");
                        if (index != -1)
                        {
                            Maps.Add(line.Substring(0, index), line.Substring(index + 1));
                        }
                    }
                    sr.Close();
                }
                Thread.Sleep(1);
                File.Delete(localTmpFile);//ɾ����ʱ�ļ�
            }
            catch
            {

            }
        }
    }
}
