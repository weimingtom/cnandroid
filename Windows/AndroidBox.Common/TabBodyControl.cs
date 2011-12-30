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
    /// 通用文章展示模板
    /// 左边树形结构，右边显示文章
    /// </summary>
    public partial class TabBodyControl : UserControl
    {
        public TabBodyControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabBodyControl_Load(object sender, EventArgs e)
        {
            //设置树列表图标
            treeView1.ImageList = GetTreeImageList();

            //绑定事件
            pbtBack.OnClick += new EventHandler(pbtBack_OnClick);
            pbtForward.OnClick += new EventHandler(pbtForward_OnClick);

        }

        /// <summary>
        /// 加载树和搜索框
        /// </summary>
        protected virtual void AsyncLoad()
        {
            //初始化搜索
            new Thread(delegate()
            {
                //加载插件
                //labState.Text = "正在加载...";
                UpdateTree(GetTree());

                //初始化搜索
                InitSearch();

            }).Start();
        }

        /// <summary>
        /// 收缩导航栏
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

        #region 树列表相关

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
        /// 获取树形列表数据
        /// </summary>
        /// <returns></returns>
        public virtual TreeNode[] GetTree()
        {
            return null;
        }

        /// <summary>
        /// 获取树控件默认图片状态组（0目录收缩，1目前展开，2普通节点）
        /// </summary>
        /// <returns></returns>
        public virtual ImageList GetTreeImageList()
        {
            return this.imageList1;
        }

        #region 树节点收缩/展开

        /// <summary>
        /// 节点收缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// 节点展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// 单击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// 双击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            setNodeImageState(e.Node);
        }

        /// <summary>
        /// 根据搜索展开
        /// </summary>
        /// <param name="node"></param>
        private void setNodeImageState(TreeNode node)
        {
            //判断子节点的数目
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

        #region 浏览器相关

        #region 上下一页导航

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbtBack_OnClick(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        /// <summary>
        /// 下一页
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

        #region 搜索相关

        /// <summary>
        /// 为自动完成准备下拉框数据
        /// </summary>
        /// <returns></returns>
        public virtual AutoCompleteStringCollection GetAutoCompleteForSearch()
        {
            return null;
        }

        /// <summary>
        /// 初始化搜索
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
