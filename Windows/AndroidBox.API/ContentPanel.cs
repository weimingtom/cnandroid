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
        /// ���ݲ���
        /// </summary>
        protected cndroidService service;
        /// <summary>
        /// ���Ͻ�5������ͼ��
        /// </summary>
        private List<PictureBoxTip> tips = new List<PictureBoxTip>();
        /// <summary>
        /// ������Ϣ
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
        /// ������ҳ
        /// </summary>
        private void LoadIndex()
        {
            string path = CurrentDirectory + CurrentConfig.HtmlRoot + "index.html";
            if (File.Exists(path))
                webBrowser1.Url = new Uri(path);
            else
                webBrowser1.Document.Body.InnerHtml = "<center><h1>Android���ĺϼ��ͻ���</h1><center>";
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

            //���¼�
            pbtBack.OnClick += new EventHandler(pbtBack_OnClick);
            pbtForward.OnClick += new EventHandler(pbtForward_OnClick);

            new Thread(delegate()
            {
                //���ز��
                parant.labState.Text = "���ڼ���...";
                UpdateTree(service.GetTree());

                //Ϊ������׼������
                UpdateSearch("");

            }).Start();

            //��������
            //OnLoadUpdate();
        }



        /// <summary>
        /// ���ݸ��º󴥷�
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

        #region ������¼�

        #region Events

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
                service.CreateTreeNodeRecursive(node);
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
                //���������Ϣ
                if (!string.IsNullOrEmpty(article.AuthorId))
                {
                    Author author = service.GetAuthorById(article.AuthorId);
                    //��������
                    setArticleDetail(author.About, global::AndroidBox.API.Properties.Resources.user_business_boss, ref i, "");
                    //���߲���
                    setArticleDetail(author.Blog, global::AndroidBox.API.Properties.Resources.blog, ref i, author.Blog);
                }
                //�����ĵ���ַ
                setArticleDetail(article.Online, global::AndroidBox.API.Properties.Resources.link, ref i, article.Online);
                //WORD����
                setArticleDetail(article.WordUrl, global::AndroidBox.API.Properties.Resources.page_white_word, ref i, article.WordUrl);
                //��������
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
                    parant.labState.Text = "��������...";

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
        /// ȫ������
        /// </summary>
        private void AllFind(string key)
        {
            ResultCount = 1;
            IsStopSearch = false;
            parant.labState.Text = "����ȫ������...";
            UpdateSearchText("ȡ��");
            UpdateSearchTextState(false);
            ClearWebBrowserTextForSearch("<div></div>");//��Ϊ����ҳ��ı�ʶ�����ֱ�ӵ��������

            CurrentConfig.IsSearchOver = false;
            IDictionary<string, string> paths = service.GetAllPath(CurrentConfig);
            foreach (string path in paths.Keys)
            {
                parant.labState.Text = "���ڼ����ļ�" + path;
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
            CurrentConfig.IsSearchOver = true;

            //ȫ���������
            parant.labState.Text = "";
            UpdateSearchText("ȫ�ļ���");
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
                    //��̬�����ӽڵ�
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
                txtSearch.AutoCompleteCustomSource = service.GetAllName(CurrentConfig);
                Application.DoEvents();
                //labState.Text = "����׼�����...";
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
        /// ���LOGO ���ڷ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.cnblogs.com/over140/archive/2010/10/26/1861234.html");
        }

        #endregion

        #region IUserControl ��Ա

        public WebBrowser GetBody()
        {
            return this.webBrowser1;
        }

        #endregion

        /// <summary>
        /// ��������
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
