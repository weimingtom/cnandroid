using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidBox.Common.Model
{
    /// <summary>
    /// ���ڵ�
    /// </summary>
    public class TreeNodeBase
    {
        /// <summary>
        /// Ψһ���
        /// </summary>
        public string Id;
        /// <summary>
        /// ����
        /// </summary>
        public string Name;
        /// <summary>
        /// ������������
        /// </summary>
        public string ContentUrl;
        /// <summary>
        /// Ŀ¼Id
        /// </summary>
        public string ParentId;
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public bool IsRoot;
    }
}
