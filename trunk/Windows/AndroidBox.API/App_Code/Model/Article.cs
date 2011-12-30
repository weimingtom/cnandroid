using System;
using System.Collections.Generic;
using System.Text;
using AndroidBox.Model;
using AndroidBox.Common.Model;

namespace AndroidBox.API
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : TreeNodeBase
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string Version;
        /// <summary>
        /// WORD下载链接
        /// </summary>
        public string WordUrl;
        /// <summary>
        /// 译者Id
        /// </summary>
        public string AuthorId;
        /// <summary>
        /// 在线中文文档地址
        /// </summary>
        public string Online;
        /// <summary>
        /// 示例代码
        /// </summary>
        public string CodeUrl;
        ///// <summary>
        ///// 缓存 0 未缓存/待下载 1 已缓存/已下载 2待更新
        ///// </summary>
        //public string Cache;
        /// <summary>
        /// 是否允许手动更新
        /// </summary>
        public bool IsManualUpdate = false;
    }
}
