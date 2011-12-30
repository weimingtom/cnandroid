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
        /// 配置文件中的KEY
        /// </summary>
        private const string UPDATE_KEY = "CONTENT_UPDATETIME1";

        public UpdateContent(PlugConfig cfg)
        {
            this.cfg = cfg;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update(EventHandler UpEvent, cndroidService service)
        {
            Update(UpEvent, service, "Android中文API", "Android_API");
        }

        public void Update(EventHandler UpEvent, cndroidService service, string DirName, string PlugName)
        {
            //仅在联网的情况下进行更新
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
        /// <param name="DirName">插件根目录名称，如“Android_API”</param>
        /// <param name="PlugName">插件中文名字，如“中文API”</param>
        internal void ContentUpdate(EventHandler UpEvent, cndroidService service, string DirName, string PlugName)
        {
            //获取服务端版本信息
            UBox box = UBox.GetBoxUpInfo();
            string server_last_updatetime = box.GetUrl(box.Maps[UPDATE_KEY]);
            //读取本地最后一次更新时间
            string local_last_updatetime = service.GetLastUpdatetime();

            //检测是否有更新
            if (local_last_updatetime != server_last_updatetime)
            {
                //有要更新的
                WebClient wClient = new WebClient();

                if (UpEvent != null)
                    UpEvent("正在更新 " + PlugName + " 内容...", null);

                try
                {
                    //下载更新文件
                    string db = CurrentDirectory + @"\" + DirName + @"\db.dat";
                    string tmpDB = db + ".tmp";
                    //if (!File.Exists(oldUp))
                    //    newUp = oldUp;

                    wClient.DownloadFile(cfg.ServerDatabase, tmpDB);

                    //旧库覆盖新库
                    Thread.Sleep(1);
                    File.Copy(tmpDB, db, true);//覆盖旧库
                    Thread.Sleep(1);
                    File.Delete(tmpDB);//删除
                    Thread.Sleep(1);

                    //待下载列表
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
                                UpEvent("正在更新 " + down.Name + " ......", null);
                            cachePath += fileName;
                            wClient.DownloadFile(path, cachePath);
                            //更新日志
                            if (fileName.EndsWith(".htm") || fileName.EndsWith(".html"))
                                service.InsertLog(cfg.Name, down.Name, cachePath, date);
                        }
                        catch
                        {
                            //出错继续执行下一个下载
                            continue;
                        }
                        //
                    }

                    //将旧库替换成新库
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
