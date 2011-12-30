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
            webBrowser1.DocumentText = "<center><h1>Android���ĺϼ��ͻ���</h1><center>";
            tips.Add(pbAbout);
            tips.Add(pbBlog);
            tips.Add(pbOnline);
            tips.Add(pbWord);
            tips.Add(pbDownload);
            treeView1.ImageList = this.imageList1;
            tabControl1.TabPages.Clear();
            new Thread(delegate()
            {
                //���ز��
                labState.Text = "���ڼ��ز��...";
                //�����һ���������
                configs = cndroidService.GetCachePlugs();

                foreach (PlugConfig cfg in configs)
                {
                    AddTab(cfg.Name);
                }
                //SetFirstTab("");
                setTab(0);

                //��������
                UpdateSearch("");
            }).Start();

            //�������ʱͬ��һ��
            BoxUpdate.Update(new EventHandler(Refresh));

            //webBrowser1.Url = new Uri(CurrentDirectory + "\\index.html");

            //������Ϣ
            UpdateInboxState();

            //׼����������
            HtmlSearch.Init();

            //׼��
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
            //���ز��
            labState.Text = "���ڼ���" + cfg.Name + "...";

            UpdateTree(service.GetTree(cfg.Database));

            UpdateWebIndex(cfg.HtmlRoot + cfg.Index);
        }

        #region �̸߳���UI

        ///// <summary>
        ///// �첽ί��
        ///// </summary>
        ///// <param name="path"></param>
        //private delegate void AsyncExecute(string path);
        delegate void ChangeState(bool state);
        delegate void SetStateDelegate(string text);
        delegate void SetTreeDelegate(TreeNode[] nodes);
        delegate void UpdateTreeDelegate(TreeNode node);

        /// <summary>
        /// ���Tab
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
        /// ���²����ҳ
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


        #region treeView1 �� ����¼�

        /// <summary>
        /// ������ߵ����˵�
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
        /// �ڵ��۵�
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
        /// �ڵ�չ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            Article article = (Article)node.Tag;
            setNodeImageState(node, article);

            //��̬�����ӽڵ�
            if (article.IsRoot && (node.Nodes.Count == 1 && string.IsNullOrEmpty(node.Nodes[0].Text)))
            {
                node.Nodes.Clear();
                service.CreateTreeNodeRecursive(configs[tabIndex].Database, node);
            }
        }

        /// <summary>
        /// �����ڵ�
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
        /// ˫���ڵ�
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
            //���������Ϣ
            if (!string.IsNullOrEmpty(article.AuthorId))
            {
                Author author = service.GetAuthorById(CurrentConfig.Database, article.AuthorId);
                //��������
                setArticleDetail(author.About, global::AndroidBox.Properties.Resources.user_business_boss, ref i, "");
                //���߲���
                setArticleDetail(author.Blog, global::AndroidBox.Properties.Resources.blog, ref i, author.Blog);
            }
            //�����ĵ���ַ
            setArticleDetail(article.Online, global::AndroidBox.Properties.Resources.link, ref i, article.Online);
            //WORD����
            setArticleDetail(article.WordUrl, global::AndroidBox.Properties.Resources.page_white_word, ref i, article.WordUrl);
            //��������
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
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        /// <summary>
        /// �����ռ���״̬
        /// </summary>
        private void UpdateInboxState()
        {
            if (service.IsTodayNewLog())
            {
                inbox.Image = global::AndroidBox.Properties.Resources.lamp;
                inbox.ToolTipText = "���µ����ݸ���!";
            }
            else
            {
                inbox.Image = global::AndroidBox.Properties.Resources.box;
                inbox.ToolTipText = "�ռ���";
            }
        }

        /// <summary>
        /// ��־
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
        /// �����������������С
        /// </summary>
        private void ResetContent()
        {
            //this.gbContentContainer.Width = this.Width - this.gbContentContainer.Left - 18;
        }

        #region ͬ��

        /// <summary>
        /// �������
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

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void �˳�ToolStripMenuItem1_Click(object sender, EventArgs e)
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
        /// ���õ�һ��TabΪ�׸�Tab
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

        #region �������

        #region ����

        /// <summary>
        /// ������ֹ����
        /// </summary>
        private bool IsStopSearch = true;
        /// <summary>
        /// �������HTML���
        /// </summary>
        private string SearchHtml = "";
        /// <summary>
        /// ƥ���¼����
        /// </summary>
        private volatile int ResultCount = 1;

        #endregion

        #region webBrowser����

        /// <summary>
        /// �����������������ģ��ҳ�ǿյģ����з���ʱ�Ѿ���գ���Ҫ���¼����������
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

        #region �̸߳���UI

        /// <summary>
        /// ����������
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
        /// �������ؼ�
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
        /// Ϊ������׼��
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
                //labState.Text = "����׼�����...";
            }
        }


        #endregion

        #region txtSearch �ı���

        /// <summary>
        /// ��ý���ȫѡ����
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
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if ("ȡ��" == btnSearch.Text)
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
        /// ѡ�����������س�
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
                    labState.Text = "��������...";

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
        /// ȫ������
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
            labState.Text = "����ȫ������...";
            UpdateSearchText("ȡ��");
            UpdateSearchTextState(false);
            ClearWebBrowserTextForSearch("<div></div>");//��Ϊ����ҳ��ı�ʶ�����ֱ�ӵ��������

            int count = configs.Count;
            for (int i = 0; i < count; i++)
            {

                configs[i].IsSearchOver = false;
                IDictionary<string, string> paths = new cndroidService().GetAllPath(configs[i]);
                foreach (string path in paths.Keys)
                {
                    labState.Text = "���ڼ����ļ�" + path;
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
                                    //�������������һ����
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
                    //�������������һ����
                    if (ResultCount >= 100)
                        break;
                }
                configs[i].IsSearchOver = true;
                //����Ƿ��Ѿ����
                foreach (PlugConfig cfg in configs)
                {
                    if (!cfg.IsSearchOver)
                        return;
                }

                //ȫ���������
                labState.Text = "";
                UpdateSearchText("ȫ�ļ���");
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

        //C# MD5�ļ�У��
        //http://blog.csdn.net/chuangxin/archive/2010/02/28/5333376.aspx

    }
}