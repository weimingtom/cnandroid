using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AndroidBox.Common
{
    /// <summary>
    /// ͨ������չʾģ��
    /// ������νṹ���ұ���ʾ����
    /// </summary>
    public partial class TabBodyControl : UserControl
    {
        public TabBodyControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBodyControl_Load(object sender, EventArgs e)
        {
            //�������б�ͼ��
            treeView1.ImageList = GetTreeImageList();

            //���¼�
            pbtBack.OnClick += new EventHandler(pbtBack_OnClick);
            pbtForward.OnClick += new EventHandler(pbtForward_OnClick);

        }

        /// <summary>
        /// ��������������
        /// </summary>
        protected virtual void AsyncLoad()
        {
            //��ʼ������
            new Thread(delegate()
            {
                //���ز��
                //labState.Text = "���ڼ���...";
                UpdateTree(GetTree());

                //��ʼ������
                InitSearch();

            }).Start();
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCloseTree_Click(object sender, EventArgs e)
        {
            if (this.splitContainer1.Panel1Collapsed)
            {
                this.splitContainer1.Panel1Collapsed = false;
                pbCloseTree.Image = global::AndroidBox.Common.Properties.Resources.collapse;
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = true;
                pbCloseTree.Image = global::AndroidBox.Common.Properties.Resources.expand;
            }
        }

        #region ���б����

        private void UpdateTree(TreeNode[] nodes)
        {
            treeView1.Invoke((MethodInvoker)delegate
            {
                this.treeView1.Nodes.Clear();
                this.treeView1.Nodes.AddRange(nodes);
                Application.DoEvents();
            });
        }

        /// <summary>
        /// ��ȡ�����б�����
        /// </summary>
        /// <returns></returns>
        public virtual TreeNode[] GetTree()
        {
            return null;
        }

        /// <summary>
        /// ��ȡ���ؼ�Ĭ��ͼƬ״̬�飨0Ŀ¼������1Ŀǰչ����2��ͨ�ڵ㣩
        /// </summary>
        /// <returns></returns>
        public virtual ImageList GetTreeImageList()
        {
            return this.imageList1;
        }

        #region ���ڵ�����/չ��

        /// <summary>
        /// �ڵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// �ڵ�չ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// �����ڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// ˫���ڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// ��������չ��
        /// </summary>
        /// <param name="node"></param>
        private void setNodeImageState(TreeNode node)
        {
            //�ж��ӽڵ����Ŀ
            if (node.GetNodeCount(false) > 0)
            {
                if (node.IsExpanded)
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                }
                else
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                }
            }
            else
            {
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
            }
        }

        #endregion

        #endregion

        #region ��������

        #region ����һҳ����

        /// <summary>
        /// ��һҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbtBack_OnClick(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbtForward_OnClick(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        #endregion

        #endregion

        #region �������

        /// <summary>
        /// Ϊ�Զ����׼������������
        /// </summary>
        /// <returns></returns>
        public virtual AutoCompleteStringCollection GetAutoCompleteForSearch()
        {
            return null;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void InitSearch()
        {
            AutoCompleteStringCollection acsc = GetAutoCompleteForSearch();
            if (acsc != null)
            {
                txtSearch.Invoke((MethodInvoker)delegate
                {
                    AutoCompleteStringCollection result = new AutoCompleteStringCollection();
                    txtSearch.AutoCompleteCustomSource = acsc;
                    Application.DoEvents();
                });
            }
        }

        #endregion


    }
}
