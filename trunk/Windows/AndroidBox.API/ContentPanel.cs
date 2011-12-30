using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AndroidBox.API.Model;
using System.Threading;
using AndroidBox.Html;
using AndroidBox.Interface;
using AndroidBox.Common;

namespace AndroidBox.API
{
    public partial class ContentPanel : UserControl, IUserControl
    {
        #region Member Variable

        private string DB = @"\Android_API\db.dat";

        /// <summary>
        /// 数据操作
        /// </summary>
        protected cndroidService service;
        /// <summary>
        /// 右上角5个功能图标
        /// </summary>
        private List<PictureBoxTip> tips = new List<PictureBoxTip>();
        /// <summary>
        /// 配置信息
        /// </summary>
        protected PlugConfig CurrentConfig;
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        private frmMainNew parant;

        #endregion

        public ContentPanel()
        {
            InitializeComponent();

            service = new cndroidService(GetDB());
            CurrentConfig = service.GetConfig();
        }

        /// <summary>
        /// 加载首页
        /// </summary>
        private void LoadIndex()
        {
            string path = CurrentDirectory + CurrentConfig.HtmlRoot + "index.html";
            if (File.Exists(path))
                webBrowser1.Url = new Uri(path);
            else
                webBrowser1.Document.Body.InnerHtml = "<center><h1>Android中文合集客户端</h1><center>";
        }

        private void ContentPanel_Load(object sender, EventArgs e)
        {
            LoadIndex();

            this.parant = (frmMainNew)FindForm();

            treeView1.ImageList = this.imageList1;

            tips.Add(pbAbout);
            tips.Add(pbBlog);
            tips.Add(pbOnline);
            tips.Add(pbWord);
            tips.Add(pbDownload);

            //绑定事件
            pbtBack.OnClick += new EventHandler(pbtBack_OnClick);
            pbtForward.OnClick += new EventHandler(pbtForward_OnClick);

            new Thread(delegate()
            {
                //加载插件
                parant.labState.Text = "正在加载...";
                UpdateTree(service.GetTree());

                //为下拉框准备数据
                UpdateSearch("");

            }).Start();

            //更新内容
            //OnLoadUpdate();
        }



        /// <summary>
        /// 内容更新后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Refresh(object sender, EventArgs e)
        {
            if (sender != null)
            {
                PlugConfig cfg = sender as PlugConfig;
                if (cfg != null)
                {
                    parant.UpdateInboxState();
                    parant.labState.Text = "";
                }
                else
                {
                    string msg = sender.ToString();
                    parant.labState.Text = msg;
                }
            }

        }

        #region 树相关事件

        #region Events

        /// <summary>
        /// 节点折叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            Article article = (Article)node.Tag;
            setNodeImageState(node, article);
        }

        /// <summary>
        /// 节点展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            Article article = (Article)node.Tag;
            setNodeImageState(node, article);

            //动态加载子节点
            if (article.IsRoot && (node.Nodes.Count == 1 && string.IsNullOrEmpty(node.Nodes[0].Text)))
            {
                node.Nodes.Clear();
                service.CreateTreeNodeRecursive(node);
            }
        }

        /// <summary>
        /// 单击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            Article article = (Article)node.Tag;
            setArticle(node, article);
            setNodeImageState(node, article);
        }

        /// <summary>
        /// 双击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            Article article = (Article)node.Tag;
            setArticle(node, article);
            setNodeImageState(node, article);
        }

        #endregion


        private void pbCloseTree_Click(object sender, EventArgs e)
        {
            if (this.splitContainer1.Panel1Collapsed)
            {
                this.splitContainer1.Panel1Collapsed = false;
                pbCloseTree.Image = global::AndroidBox.API.Properties.Resources.collapse;
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = true;
                pbCloseTree.Image = global::AndroidBox.API.Properties.Resources.expand;
            }
        }


        #region Tools


        private void setNodeImageState(TreeNode node, Article article)
        {
            if (article.IsRoot)
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

        private void setArticle(TreeNode node, Article article)
        {
            if (!string.IsNullOrEmpty(article.ContentUrl))
            {
                webBrowser1.Navigate(new Uri(CurrentDirectory + CurrentConfig.HtmlRoot + article.ContentUrl));
                //webBrowser1.Url = new Uri(CurrentDirectory + CurrentConfig.HtmlRoot + article.ContentUrl);

                for (int ind = 0; ind < 5; ind++)
                {
                    tips[ind].Visible = false;
                }

                int i = 0;
                //译者相关信息
                if (!string.IsNullOrEmpty(article.AuthorId))
                {
                    Author author = service.GetAuthorById(article.AuthorId);
                    //关于译者
                    setArticleDetail(author.About, global::AndroidBox.API.Properties.Resources.user_business_boss, ref i, "");
                    //译者博客
                    setArticleDetail(author.Blog, global::AndroidBox.API.Properties.Resources.blog, ref i, author.Blog);
                }
                //在线文档地址
                setArticleDetail(article.Online, global::AndroidBox.API.Properties.Resources.link, ref i, article.Online);
                //WORD下载
                setArticleDetail(article.WordUrl, global::AndroidBox.API.Properties.Resources.page_white_word, ref i, article.WordUrl);
                //代码下载
                setArticleDetail(article.CodeUrl, global::AndroidBox.API.Properties.Resources.attach, ref i, article.CodeUrl);
            }
        }

        private void setArticleDetail(string toolTip, Image img, ref int index, string url)
        {
            if (!string.IsNullOrEmpty(toolTip))
            {
                PictureBoxTip pbt = tips[index];
                pbt.ToolTipText = toolTip;
                pbt.Image = img;
                pbt.Visible = true;
                if (!string.IsNullOrEmpty(url))
                    pbt.Url = url;
                index++;
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

        #region 变量

        /// <summary>
        /// 搜索终止搜索
        /// </summary>
        private bool IsStopSearch = true;
        /// <summary>
        /// 搜索结果HTML存放
        /// </summary>
        private string SearchHtml = "";
        /// <summary>
        /// 匹配记录行数
        /// </summary>
        private volatile int ResultCount = 1;

        #endregion

        #region webBrowser处理

        /// <summary>
        /// 加载搜索结果，由于模板页是空的，所有返回时已经清空，需要重新加载搜索结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (IsStopSearch && webBrowser1.Url.ToString().EndsWith("/search.htm") && !string.IsNullOrEmpty(SearchHtml))
            {
                webBrowser1.Document.Body.InnerHtml += SearchHtml;
            }
        }

        #endregion

        #region txtSearch 文本框

        /// <summary>
        /// 获得焦点全选内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_MouseDown(object sender, MouseEventArgs e)
        {
            string str = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(str))
                return;
            else
            {
                txtSearch.SelectAll();
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if ("取消" == btnSearch.Text)
            {
                IsStopSearch = true;
            }
            else
            {
                string str = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(str))
                    return;
                new Thread(delegate()
                {
                    AllFind(str);

                }).Start();
            }
            //txtSearch.AutoCompleteCustomSource
        }

        /// <summary>
        /// 选中搜索建议或回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(str))
                    return;
                new Thread(delegate()
                {
                    parant.labState.Text = "正在搜索...";

                    bool searchTitle = false;
                    PlugConfig cfg = CurrentConfig;

                    if (cfg.ArticleNames != null && cfg.ArticleNames.Contains(str))
                    {
                        TreeNode node = FindNode(this.treeView1.Nodes, str);
                        if (node != null)
                        {
                            UpdateTreeSelected(node);
                            searchTitle = true;
                        }
                    }

                    if (searchTitle)
                        parant.labState.Text = "";
                    else
                        AllFind(str);

                }).Start();

            }
        }

        /// <summary>
        /// 全文搜索
        /// </summary>
        private void AllFind(string key)
        {
            ResultCount = 1;
            IsStopSearch = false;
            parant.labState.Text = "正在全文搜索...";
            UpdateSearchText("取消");
            UpdateSearchTextState(false);
            ClearWebBrowserTextForSearch("<div></div>");//作为搜索页面的标识，如果直接点搜索结果

            CurrentConfig.IsSearchOver = false;
            IDictionary<string, string> paths = service.GetAllPath(CurrentConfig);
            foreach (string path in paths.Keys)
            {
                parant.labState.Text = "正在检索文件" + path;
                if (IsStopSearch)
                    break;
                try
                {
                    string fullPath = CurrentDirectory + CurrentConfig.HtmlRoot + path;
                    using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            int index = line.IndexOf(key, StringComparison.OrdinalIgnoreCase);
                            if (index > -1)
                            {
                                AppendSearchResult(HtmlSearch.BuildSearchResultItem(key, ResultCount, fullPath, paths[path], CurrentConfig.Name, line));
                                ResultCount++;
                                //搜索结果不超过一百条
                                if (ResultCount > 100)
                                    break;
                            }
                        }
                        sr.Close();
                    }
                }
                catch
                {
                    continue;
                }
                //搜索结果不超过一百条
                if (ResultCount >= 100)
                    break;
            }
            CurrentConfig.IsSearchOver = true;

            //全部检索完毕
            parant.labState.Text = "";
            UpdateSearchText("全文检索");
            UpdateSearchTextState(true);
            IsStopSearch = true;

        }

        public void AppendSearchResult(string text)
        {
            if (this.webBrowser1.InvokeRequired)
                this.Invoke(new SetStateDelegate(AppendSearchResult), new object[] { text });
            else
            {
                SearchHtml += "<p />" + text;
                webBrowser1.Document.Body.InnerHtml += "<p />" + text;
                Application.DoEvents();
            }
        }

        public void ClearWebBrowserTextForSearch(string text)
        {
            if (this.webBrowser1.InvokeRequired)
                this.Invoke(new SetStateDelegate(ClearWebBrowserTextForSearch), new object[] { text });
            else
            {
                webBrowser1.Url = new Uri(CurrentDirectory + "/search.htm");
                SearchHtml = "";

                Application.DoEvents();
            }
        }

        private TreeNode FindNode(TreeNodeCollection nodes, string str)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == str)
                    return node;
                else if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    Article article = (Article)node.Tag;
                    //动态加载子节点
                    if (article.IsRoot && (node.Nodes.Count == 1 && string.IsNullOrEmpty(node.Nodes[0].Text)))
                    {
                        AddChildNode(node);
                    }

                    TreeNode has = FindNode(node.Nodes, str);
                    if (has != null)
                        return has;
                }
            }
            return null;
        }

        #endregion

        #region delegate

        delegate void ChangeState(bool state);
        delegate void SetStateDelegate(string text);
        delegate void UpdateTreeDelegate(TreeNode node);
        delegate void SetTreeDelegate(TreeNode[] nodes);


        /// <summary>
        /// 更新树控件
        /// </summary>
        /// <param name="nodes"></param>
        private void UpdateTree(TreeNode[] nodes)
        {
            if (this.treeView1.InvokeRequired)
                this.Invoke(new SetTreeDelegate(UpdateTree), new object[] { nodes });
            else
            {
                this.treeView1.Nodes.Clear();
                this.treeView1.Nodes.AddRange(nodes);
                parant.labState.Text = "";
                Application.DoEvents();
            }
        }

        private void UpdateTreeSelected(TreeNode node)
        {
            if (this.treeView1.InvokeRequired)
                this.Invoke(new UpdateTreeDelegate(UpdateTreeSelected), new object[] { node });
            else
            {
                this.treeView1.Focus();
                this.treeView1.SelectedNode = node;
                this.treeView1.SelectedNode.ExpandAll();

                //Article article = (Article)node.Tag;
                //setNodeImageState(node, article);

                treeView1_NodeMouseClick(this.treeView1, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 0, 0));

                Application.DoEvents();
            }
        }

        /// <summary>
        /// 更新搜索框
        /// </summary>
        /// <param name="state"></param>
        private void UpdateSearchState(bool state)
        {
            if (this.btnSearch.InvokeRequired)
                this.Invoke(new ChangeState(UpdateSearchState), new object[] { state });
            else
            {
                btnSearch.Enabled = state;
                Application.DoEvents();
            }
        }

        private void UpdateSearchTextState(bool state)
        {
            if (this.btnSearch.InvokeRequired)
                this.Invoke(new ChangeState(UpdateSearchTextState), new object[] { state });
            else
            {
                txtSearch.Enabled = state;
                Application.DoEvents();
            }
        }

        private void UpdateSearchText(string text)
        {
            if (this.btnSearch.InvokeRequired)
                this.Invoke(new SetStateDelegate(UpdateSearchText), new object[] { text });
            else
            {
                btnSearch.Text = text;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 为搜索做准备
        /// </summary>
        /// <param name="url"></param>
        private void UpdateSearch(string url)
        {
            if (this.txtSearch.InvokeRequired)
                this.Invoke(new SetStateDelegate(UpdateSearch), new object[] { url });
            else
            {
                //txtSearch.AutoCompleteSoukrce
                AutoCompleteStringCollection result = new AutoCompleteStringCollection();
                txtSearch.AutoCompleteCustomSource = service.GetAllName(CurrentConfig);
                Application.DoEvents();
                //labState.Text = "搜索准备完毕...";
            }
        }

        private void AddChildNode(TreeNode node)
        {
            if (this.txtSearch.InvokeRequired)
                this.Invoke(new UpdateTreeDelegate(AddChildNode), new object[] { node });
            else
            {
                node.Nodes.Clear();
                service.CreateTreeNodeRecursive(node);
            }
        }

        #endregion

        /// <summary>
        /// 点击LOGO 关于翻译组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.cnblogs.com/over140/archive/2010/10/26/1861234.html");
        }

        #endregion

        #region IUserControl 成员

        public WebBrowser GetBody()
        {
            return this.webBrowser1;
        }

        #endregion

        /// <summary>
        /// 更新内容
        /// </summary>
        public virtual void OnLoadUpdate()
        {
            new UpdateContent(CurrentConfig).Update(new EventHandler(Refresh), service);
        }

        public virtual string GetDB()
        {
            return DB;
        }
    }
}
