using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using AndroidBox.API.Model;
using AndroidBox;

namespace AndroidBox.API
{
    public class UpdateContent
    {
        private PlugConfig cfg;
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        /// <summary>
        /// �����ļ��е�KEY
        /// </summary>
        private const string UPDATE_KEY = "CONTENT_UPDATETIME1";

        public UpdateContent(PlugConfig cfg)
        {
            this.cfg = cfg;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Update(EventHandler UpEvent, cndroidService service)
        {
            Update(UpEvent, service, "Android����API", "Android_API");
        }

        public void Update(EventHandler UpEvent, cndroidService service, string DirName, string PlugName)
        {
            //��������������½��и���
            if (SystemUtils.IsConnected())
            {
                new Thread(delegate()
                {
                    try
                    {
                        ContentUpdate(UpEvent, service, DirName, PlugName);
                    }
                    catch
                    {

                    }
                }).Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UpEvent"></param>
        /// <param name="DirName">�����Ŀ¼���ƣ��硰Android_API��</param>
        /// <param name="PlugName">����������֣��硰����API��</param>
        internal void ContentUpdate(EventHandler UpEvent, cndroidService service, string DirName, string PlugName)
        {
            //��ȡ����˰汾��Ϣ
            UBox box = UBox.GetBoxUpInfo();
            string server_last_updatetime = box.GetUrl(box.Maps[UPDATE_KEY]);
            //��ȡ�������һ�θ���ʱ��
            string local_last_updatetime = service.GetLastUpdatetime();

            //����Ƿ��и���
            if (local_last_updatetime != server_last_updatetime)
            {
                //��Ҫ���µ�
                WebClient wClient = new WebClient();

                if (UpEvent != null)
                    UpEvent("���ڸ��� " + PlugName + " ����...", null);

                try
                {
                    //���ظ����ļ�
                    string db = CurrentDirectory + @"\" + DirName + @"\db.dat";
                    string tmpDB = db + ".tmp";
                    //if (!File.Exists(oldUp))
                    //    newUp = oldUp;

                    wClient.DownloadFile(cfg.ServerDatabase, tmpDB);

                    //�ɿ⸲���¿�
                    Thread.Sleep(1);
                    File.Copy(tmpDB, db, true);//���Ǿɿ�
                    Thread.Sleep(1);
                    File.Delete(tmpDB);//ɾ��
                    Thread.Sleep(1);

                    //�������б�
                    List<Download> list = service.GetDownloadList(local_last_updatetime);
                    string date = DateTime.Now.ToShortDateString();
                    foreach (Download down in list)
                    {
                        try
                        {
                            string path = down.Path;
                            string fileName = StringUtils.GetFileName(path);
                            string cachePath = CurrentDirectory + cfg.HtmlRoot;
                            if (path.EndsWith(".jpg") || path.EndsWith(".png"))
                            {
                                cachePath += "image\\";
                            }
                            else
                                fileName = down.FileName;
                            if (UpEvent != null)
                                UpEvent("���ڸ��� " + down.Name + " ......", null);
                            cachePath += fileName;
                            wClient.DownloadFile(path, cachePath);
                            //������־
                            if (fileName.EndsWith(".htm") || fileName.EndsWith(".html"))
                                service.InsertLog(cfg.Name, down.Name, cachePath, date);
                        }
                        catch
                        {
                            //�������ִ����һ������
                            continue;
                        }
                        //
                    }

                    //���ɿ��滻���¿�
                    if (UpEvent != null)
                        UpEvent(cfg, null);

                    UpEvent("", null);
                }
                catch (Exception ex)
                {
                    //labState.Text = ex.Message;
                    if (UpEvent != null)
                        UpEvent(ex.Message, null);
                }

                wClient.Dispose();
            }
        }
    }
}
