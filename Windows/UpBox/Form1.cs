using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace UpBox
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// UpBox.exe�汾
        /// </summary>
        private const string UPBOX_VERSION = "0.0.0.6";

        string[] args;
        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private WebClient wClient;
        private string CurrentDirectory = Directory.GetCurrentDirectory();
        private string path = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //args = new string[] { "http://files.cnblogs.com/over140/AndroidBox.apk", "http://files.cnblogs.com/over140/AndroidBoxData.zip" };
            if (args == null || args.Length != 2)
            {
                this.Close();
            }
            //else if (args.Length == 1)
            //{
            //    path = args[0];
            //}
            //if (string.IsNullOrEmpty(path))
            //{
            //    MessageBox.Show("�뱣�����糩ͨ���ٴ����и��³����Բ����뷢�ʼ���over140@gmail.com", "���´���", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            KillProcess();
            path = args[0];
            wClient = new WebClient();
            wClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wClient_DownloadProgressChanged);
            wClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wClient_DownloadFileCompleted);
            wClient.DownloadDataAsync(new Uri(path));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (wClient != null)
            {
                wClient.CancelAsync();
            }
            this.Close();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wClient_DownloadFileCompleted(object sender, DownloadDataCompletedEventArgs e)    //���ؽ���
        {
            try
            {
                KillProcess();

                if (path.EndsWith("AndroidBox.1.0.apk"))
                {
                    //���DLL
                    string api_path = CurrentDirectory + @"\AndroidBox.API.dll";
                    string dev_path = CurrentDirectory + @"\AndroidBox.DevGuide.dll";
                    string common_path = CurrentDirectory + @"\AndroidBox.Common.dll";
                    string db_path = CurrentDirectory + @"\Data.dat";
                    WebClient wClient = new WebClient();
                    try
                    {
                        string path_base = "http://files.cnblogs.com/over140/";
                        wClient.DownloadFile(path_base + "AndroidBox.API.dll.zip", api_path);
                        wClient.DownloadFile(path_base + "AndroidBox.Common.dll.zip", dev_path);
                        wClient.DownloadFile(path_base + "AndroidBox.DevGuide.dll.zip", common_path);
                        wClient.DownloadFile(path_base + "Data.1.0.zip", db_path);
                        wClient.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                }
                //

                //�������ļ�
                FileStream fs = new FileStream(CurrentDirectory + @"\AndroidBox.exe", FileMode.Create, FileAccess.Write);
                fs.Write(e.Result, 0, e.Result.Length);
                fs.Close();
                //���ݿ��ļ�
                //wClient.DownloadFile(args[1], CurrentDirectory + @"\Data.dat");
                Thread.Sleep(100);
                System.Diagnostics.Process.Start("AndroidBox.exe", "");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            this.Close();
        }

        private void KillProcess()
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //�õ����д򿪵Ľ���
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName("AndroidBox"))
                {
                    //if (!thisproc.CloseMainWindow())
                    //{
                    thisproc.Kill();
                    //}
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wClient != null)
            {
                try
                {
                    wClient.Dispose();
                    wClient = null;
                }
                catch
                {

                }
            }
        }
    }
}