using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using AndroidBox.Model;

namespace AndroidBox
{
    public sealed class AddinHelper
    {
        private static List<PlugConfig> configs;
        public static List<PlugConfig> GetPlugs()
        {
            if (configs == null)
            {
                configs = new List<PlugConfig>();
                DirectoryInfo currDir = new DirectoryInfo(Directory.GetCurrentDirectory());
                DirectoryInfo[] folders = currDir.GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    FileInfo[] files = folder.GetFiles("config.xml");
                    if (files != null && files.Length > 0)
                    {
                        PlugConfig config = ParseXML(files[0].FullName);
                        if (config != null)
                            configs.Add(config);
                    }
                }
            }
            return configs;
        }

        private static PlugConfig ParseXML(string xmlpath)
        {
            PlugConfig config = null;
            XmlDocument Document = new XmlDocument();
            try
            {
                Document.Load(xmlpath);
                XmlElement element = Document["Plug"];
                config = new PlugConfig();
                config.Name = element["Name"].InnerText.Trim();
                //config.Database = element["Database"].InnerText.Trim();
                config.HtmlRoot = element["HtmlRoot"].InnerText.Trim();
                config.Index = element["Index"].InnerText.Trim();
                config.ServerDatabase = element["ServerDatabase"].InnerText.Trim();
            }
            catch (Exception)
            {
                config = null;
            }
            return config;
        }

        /// <summary>
        /// 1=0.1
        /// </summary>
        internal const int BOX_VERISION = 1;
    }

}
