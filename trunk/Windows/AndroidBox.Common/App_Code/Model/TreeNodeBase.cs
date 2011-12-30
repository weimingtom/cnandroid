using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidBox.Common.Model
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNodeBase
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string Id;
        /// <summary>
        /// 标题
        /// </summary>
        public string Name;
        /// <summary>
        /// 文章内容链接
        /// </summary>
        public string ContentUrl;
        /// <summary>
        /// 目录Id
        /// </summary>
        public string ParentId;
        /// <summary>
        /// 是否是目录
        /// </summary>
        public bool IsRoot;
    }
}
