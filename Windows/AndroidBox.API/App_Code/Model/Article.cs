using System;
using System.Collections.Generic;
using System.Text;
using AndroidBox.Model;
using AndroidBox.Common.Model;

namespace AndroidBox.API
{
    /// <summary>
    /// ����
    /// </summary>
    public class Article : TreeNodeBase
    {
        /// <summary>
        /// �汾
        /// </summary>
        public string Version;
        /// <summary>
        /// WORD��������
        /// </summary>
        public string WordUrl;
        /// <summary>
        /// ����Id
        /// </summary>
        public string AuthorId;
        /// <summary>
        /// ���������ĵ���ַ
        /// </summary>
        public string Online;
        /// <summary>
        /// ʾ������
        /// </summary>
        public string CodeUrl;
        ///// <summary>
        ///// ���� 0 δ����/������ 1 �ѻ���/������ 2������
        ///// </summary>
        //public string Cache;
        /// <summary>
        /// �Ƿ������ֶ�����
        /// </summary>
        public bool IsManualUpdate = false;
    }
}
