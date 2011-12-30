using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using AndroidBox.Model;
using System.Runtime.InteropServices;
using AndroidBox.Html;
namespace AndroidBox
{
    public partial class frmMain : Form
    {
        #region Member Variable

        /// <summary>
        /// 
        /// </summary>
        private cndroidService service = new cndroidService();
        private int tabIndex = -1;
        private List<PlugConfig> configs = null;
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private List<PictureBoxTip> tips = new List<PictureBoxTip>();


        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "<center><h1>Android中文合集客户端</h1><center>";
            tips.Add(pbAbout);
            tips.Add(pbBlog);
            tips.Add(pbOnline);
            tips.Add(pbWord);
            tips.Add(pbDownload);
            treeView1.ImageList = this.imageList1;
            tabControl1.TabPages.Clear();
            new Thread(delegate()
            {
                //加载插件
                labState.Text = "正在加载插件...";
                //插件第一个插件的树
                configs = cndroidService.GetCachePlugs();

                foreach (PlugConfig cfg in configs)
                {
                    AddTab(cfg.Name);
                }
                //SetFirstTab("");
                setTab(0);

                //设置搜索
                UpdateSearch("");
            }).Start();

            //启动软件时同步一下
            BoxUpdate.Update(new EventHandler(Refresh));

            //webBrowser1.Url = new Uri(CurrentDirectory + "\\index.html");

            //更新消息
            UpdateInboxState();

            //准备搜索界面
            HtmlSearch.Init();

            //准备
            pbtBack.OnClick += new EventHandler(pbtBack_OnClick);
            pbtForward.OnClick += new EventHandler(pbtForward_OnClick);
        }

        private void Refresh(object sender, EventArgs e)
        {
            if (sender != null)
            {
                PlugConfig cfg = sender as PlugConfig;
                if (cfg != null)
                {
                    if (cfg.TabIndex == tabIndex)
                    {
                        SetControlTab(tabIndex + "");
                        UpdateInboxState();
                        labState.Text = "";
                    }
                }
                else
                {
                    string msg = sender.ToString();
                    labState.Text = msg;
                }
            }

        }

        private void setTab(int tabIndex)
        {
            if (configs == null)
                return;
            PlugConfig cfg = configs[tabIndex];
            //加载插件
            labState.Text = "正在加载" + cfg.Name + "...";

            UpdateTree(service.GetTree(cfg.Database));

            UpdateWebIndex(cfg.HtmlRoot + cfg.Index);
        }

        #region 线程更新UI

        ///// <summary>
        ///// 异步委托
        ///// </summary>
        ///// <param name="path"></param>
        //private delegate void AsyncExecute(string path);
        delegate void ChangeState(bool state);
        delegate void SetStateDelegate(string text);
        delegate void SetTreeDelegate(TreeNode[] nodes);
        delegate void UpdateTreeDelegate(TreeNode node);

        /// <summary>
        /// 添加Tab
        /// </summary>
        /// <param name="text"></param>
        private void AddTab(string text)
        {
            if (this.tabControl1.InvokeRequired)
                this.Invoke(new SetStateDelegate(AddTab), new object[] { text });
            else
            {
                tabControl1.TabPages.Add(text);
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 更新插件首页
        /// </summary>
        /// <param name="url"></param>
        private void UpdateWebIndex(string url)
        {
            if (this.webBrowser1.InvokeRequired)
                this.Invoke(new SetStateDelegate(UpdateWebIndex), new object[] { url });
            else
            {
                webBrowser1.Url = new Uri(CurrentDirectory + url);
                Application.DoEvents();
            }
        }

        private void Close(string text)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetStateDelegate(Close), new object[] { text });
            else
            {
                this.Close();
            }
        }

        #endregion

        #region Events


        #region treeView1 树 相关事件

        /// <summary>
        /// 隐藏左边的树菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCloseTree_Click(object sender, EventArgs e)
        {
            if (this.splitContainer1.Panel1Collapsed)
            {
                this.splitContainer1.Panel1Collapsed = false;
                pbCloseTree.Image = global::AndroidBox.Properties.Resources.collapse;
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = true;
                pbCloseTree.Image = global::AndroidBox.Properties.Resources.expand;
            }
        }

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
                service.CreateTreeNodeRecursive(configs[tabIndex].Database, node);
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

        private PlugConfig CurrentConfig
        {
            get
            {
                return configs[tabControl1.SelectedIndex];
            }
        }


        private void setArticle(TreeNode node, Article article)
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
                Author author = service.GetAuthorById(CurrentConfig.Database, article.AuthorId);
                //关于译者
                setArticleDetail(author.About, global::AndroidBox.Properties.Resources.user_business_boss, ref i, "");
                //译者博客
                setArticleDetail(author.Blog, global::AndroidBox.Properties.Resources.blog, ref i, author.Blog);
            }
            //在线文档地址
            setArticleDetail(article.Online, global::AndroidBox.Properties.Resources.link, ref i, article.Online);
            //WORD下载
            setArticleDetail(article.WordUrl, global::AndroidBox.Properties.Resources.page_white_word, ref i, article.WordUrl);
            //代码下载
            setArticleDetail(article.CodeUrl, global::AndroidBox.Properties.Resources.attach, ref i, article.CodeUrl);
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



        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        /// <summary>
        /// 更新收件箱状态
        /// </summary>
        private void UpdateInboxState()
        {
            if (service.IsTodayNewLog())
            {
                inbox.Image = global::AndroidBox.Properties.Resources.lamp;
                inbox.ToolTipText = "有新的内容更新!";
            }
            else
            {
                inbox.Image = global::AndroidBox.Properties.Resources.box;
                inbox.ToolTipText = "收件箱";
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inbox_ButtonClick(object sender, EventArgs e)
        {
            frmMessage inbox = new frmMessage(this.webBrowser1);
            inbox.FormClosing += new FormClosingEventHandler(delegate
            {
                UpdateInboxState();
            });
            inbox.ShowDialog();
        }

        #endregion


        /// <summary>
        /// 重新设置内容区域大小
        /// </summary>
        private void ResetContent()
        {
            //this.gbContentContainer.Width = this.Width - this.gbContentContainer.Left - 18;
        }

        #region 同步

        /// <summary>
        /// 软件更新
        /// </summary>
        public void SoftUpdate()
        {


            //
        }

        public void Sync()
        {

        }

        #endregion

        #region Tools



        #endregion

        private bool isTrueExit = false;

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isTrueExit)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
                e.Cancel = true;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isTrueExit = true;
            this.Close();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.notifyIcon1.Visible = false;
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            isTrueExit = true;
            this.Close();
        }

        #region tabControl

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            tabIndex = tabControl1.SelectedIndex;
            if (tabIndex == -1)
                tabIndex = 0;
            treeView1.Nodes.Clear();
            setTab(tabIndex);
        }


        /// <summary>
        /// 设置第一个Tab为首个Tab
        /// </summary>
        /// <param name="text"></param>
        private void SetControlTab(string text)
        {
            if (this.tabControl1.InvokeRequired)
                this.Invoke(new SetStateDelegate(SetControlTab), new object[] { text });
            else
            {
                //tabControl1.TabIndex = ;
                tabControl1.SelectTab(int.Parse(text));
                //tabControl1.TabPages[].Select();
                Application.DoEvents();
            }
        }

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

        #region 线程更新UI

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
                labState.Text = "";
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

        public void AppendSearchResult(string text)
        {
            if (this.webBrowser1.InvokeRequired)
                this.Invoke(new SetStateDelegate(AppendSearchResult), new object[] { text });
            else
            {
                //if (!webBrowser1.DocumentText.StartsWith("<div></div>"))
                //{
                //    IsStopSearch = true;
                //    return;
                //}

                //webBrowser1.DocumentText += "<p />" + text;
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
                //webBrowser1.Navigate("about:blank");
                //HtmlDocument doc = this.webBrowser1.Document;
                //doc.Write(String.Empty);
                //webBrowser1.Document.OpenNew(true);
                //webBrowser1.Document.Body.InnerText = "";
                webBrowser1.Url = new Uri(CurrentDirectory + "/search.htm");
                SearchHtml = "";

                //webBrowser1.N += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                Application.DoEvents();
            }
        }

        //    private void webBrowser1_Navigating(object sender,
        //WebBrowserNavigatingEventArgs e)
        //    {
        //        Uri a = e.Url;
        //    }

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
                txtSearch.AutoCompleteCustomSource = service.GetAllName(configs);
                Application.DoEvents();
                //labState.Text = "搜索准备完毕...";
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
                    labState.Text = "正在搜索...";

                    bool searchTitle = false;
                    foreach (PlugConfig cfg in configs)
                    {
                        if (cfg.ArticleNames != null && cfg.ArticleNames.Contains(str))
                        {
                            if (cfg.TabIndex != tabIndex)
                                SetControlTab(cfg.TabIndex + "");
                            //setTab(cfg.TabIndex);
                            TreeNode node = FindNode(this.treeView1.Nodes, str);
                            if (node != null)
                            {
                                UpdateTreeSelected(node);
                                searchTitle = true;
                            }
                            break;
                        }
                    }
                    if (searchTitle)
                        labState.Text = "";
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
            if (configs == null)
            {
                labState.Text = "";
                return;
            }
            ResultCount = 1;
            IsStopSearch = false;
            labState.Text = "正在全文搜索...";
            UpdateSearchText("取消");
            UpdateSearchTextState(false);
            ClearWebBrowserTextForSearch("<div></div>");//作为搜索页面的标识，如果直接点搜索结果

            int count = configs.Count;
            for (int i = 0; i < count; i++)
            {

                configs[i].IsSearchOver = false;
                IDictionary<string, string> paths = new cndroidService().GetAllPath(configs[i]);
                foreach (string path in paths.Keys)
                {
                    labState.Text = "正在检索文件" + path;
                    if (IsStopSearch)
                        break;
                    try
                    {
                        string fullPath = CurrentDirectory + configs[i].HtmlRoot + path;
                        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                int index = line.IndexOf(key, StringComparison.OrdinalIgnoreCase);
                                if (index > -1)
                                {
                                    AppendSearchResult(HtmlSearch.BuildSearchResultItem(key, ResultCount, fullPath, paths[path], configs[i].Name, line));
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
                configs[i].IsSearchOver = true;
                //检测是否都已经完成
                foreach (PlugConfig cfg in configs)
                {
                    if (!cfg.IsSearchOver)
                        return;
                }

                //全部检索完毕
                labState.Text = "";
                UpdateSearchText("全文检索");
                UpdateSearchTextState(true);
                IsStopSearch = true;
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
                    TreeNode has = FindNode(node.Nodes, str);
                    if (has != null)
                        return has;
                }
            }
            return null;
        }

        #endregion

        #endregion

        //C# MD5文件校验
        //http://blog.csdn.net/chuangxin/archive/2010/02/28/5333376.aspx

    }
}