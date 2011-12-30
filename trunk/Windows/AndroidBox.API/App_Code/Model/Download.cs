using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidBox.API.Model
{
    public class Download
    {
        public string Id;
        /// <summary>
        /// 文章ID
        /// </summary>
        public string TreeId;
        /// <summary>
        /// 路径
        /// </summary>
        public string Path;
        /// <summary>
        /// 下载文章名称
        /// </summary>
        public string Name;
        /// <summary>
        /// content_url
        /// </summary>
        public string FileName;
    }
}
