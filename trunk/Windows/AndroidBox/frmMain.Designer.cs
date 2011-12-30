namespace AndroidBox
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labState = new System.Windows.Forms.ToolStripStatusLabel();
            this.inbox = new System.Windows.Forms.ToolStripSplitButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pbCloseTree = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbAbout = new AndroidBox.PictureBoxTip();
            this.pbBlog = new AndroidBox.PictureBoxTip();
            this.pbOnline = new AndroidBox.PictureBoxTip();
            this.pbWord = new AndroidBox.PictureBoxTip();
            this.pbtForward = new AndroidBox.PictureBoxTip();
            this.pbtBack = new AndroidBox.PictureBoxTip();
            this.pbDownload = new AndroidBox.PictureBoxTip();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseTree)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(2, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(200, 312);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tree_collapse.gif");
            this.imageList1.Images.SetKeyName(1, "tree_expand.gif");
            this.imageList1.Images.SetKeyName(2, "tree_ner.gif");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(202, 21);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(194, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "中文API";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(194, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "开发者指南";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labState,
            this.inbox});
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(707, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labState
            // 
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(671, 17);
            this.labState.Spring = true;
            this.labState.Text = "labState";
            this.labState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inbox
            // 
            this.inbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.inbox.DropDownButtonWidth = 0;
            this.inbox.Image = global::AndroidBox.Properties.Resources.box;
            this.inbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inbox.Name = "inbox";
            this.inbox.Size = new System.Drawing.Size(21, 20);
            this.inbox.ToolTipText = "日志";
            this.inbox.ButtonClick += new System.EventHandler(this.inbox_ButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel2.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Panel2.Controls.Add(this.pbAbout);
            this.splitContainer1.Panel2.Controls.Add(this.pbCloseTree);
            this.splitContainer1.Panel2.Controls.Add(this.pbBlog);
            this.splitContainer1.Panel2.Controls.Add(this.pbOnline);
            this.splitContainer1.Panel2.Controls.Add(this.pbWord);
            this.splitContainer1.Panel2.Controls.Add(this.pbtForward);
            this.splitContainer1.Panel2.Controls.Add(this.pbtBack);
            this.splitContainer1.Panel2.Controls.Add(this.pbDownload);
            this.splitContainer1.Size = new System.Drawing.Size(707, 338);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(310, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "全文搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearch.Location = new System.Drawing.Point(61, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(247, 21);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            this.txtSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSearch_MouseDown);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 23);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(492, 312);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // pbCloseTree
            // 
            this.pbCloseTree.Image = global::AndroidBox.Properties.Resources.collapse;
            this.pbCloseTree.Location = new System.Drawing.Point(1, 3);
            this.pbCloseTree.Name = "pbCloseTree";
            this.pbCloseTree.Size = new System.Drawing.Size(22, 19);
            this.pbCloseTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCloseTree.TabIndex = 2;
            this.pbCloseTree.TabStop = false;
            this.pbCloseTree.Click += new System.EventHandler(this.pbCloseTree_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(707, 25);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout,
            this.退出ToolStripMenuItem1});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 21);
            this.toolStripMenuItem1.Text = "帮助(H)";
            this.toolStripMenuItem1.ToolTipText = "日志";
            // 
            // menuAbout
            // 
            this.menuAbout.Image = global::AndroidBox.Properties.Resources.information;
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(195, 22);
            this.menuAbout.Text = "关于Android中文合集";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Android中文合集";
            this.notifyIcon1.BalloonTipTitle = "Android中文合集";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Android中文合集";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // pbAbout
            // 
            this.pbAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAbout.Image = global::AndroidBox.Properties.Resources.user_business_boss;
            this.pbAbout.Location = new System.Drawing.Point(473, 4);
            this.pbAbout.Margin = new System.Windows.Forms.Padding(0);
            this.pbAbout.Name = "pbAbout";
            this.pbAbout.Size = new System.Drawing.Size(16, 16);
            this.pbAbout.TabIndex = 9;
            this.pbAbout.TabStop = false;
            this.pbAbout.ToolTipText = "关于译者";
            this.pbAbout.Url = null;
            this.pbAbout.Visible = false;
            // 
            // pbBlog
            // 
            this.pbBlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBlog.Image = global::AndroidBox.Properties.Resources.blog;
            this.pbBlog.Location = new System.Drawing.Point(450, 4);
            this.pbBlog.Margin = new System.Windows.Forms.Padding(0);
            this.pbBlog.Name = "pbBlog";
            this.pbBlog.Size = new System.Drawing.Size(16, 16);
            this.pbBlog.TabIndex = 8;
            this.pbBlog.TabStop = false;
            this.pbBlog.ToolTipText = "点击访问译者博客";
            this.pbBlog.Url = null;
            this.pbBlog.Visible = false;
            // 
            // pbOnline
            // 
            this.pbOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOnline.Image = global::AndroidBox.Properties.Resources.link;
            this.pbOnline.Location = new System.Drawing.Point(427, 4);
            this.pbOnline.Margin = new System.Windows.Forms.Padding(0);
            this.pbOnline.Name = "pbOnline";
            this.pbOnline.Size = new System.Drawing.Size(16, 16);
            this.pbOnline.TabIndex = 8;
            this.pbOnline.TabStop = false;
            this.pbOnline.ToolTipText = "点击访问在线API中文文档(原版风格，由于服务器在国外可能访问较慢)";
            this.pbOnline.Url = null;
            this.pbOnline.Visible = false;
            // 
            // pbWord
            // 
            this.pbWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWord.Image = global::AndroidBox.Properties.Resources.page_white_word;
            this.pbWord.Location = new System.Drawing.Point(404, 4);
            this.pbWord.Margin = new System.Windows.Forms.Padding(0);
            this.pbWord.Name = "pbWord";
            this.pbWord.Size = new System.Drawing.Size(16, 16);
            this.pbWord.TabIndex = 8;
            this.pbWord.TabStop = false;
            this.pbWord.ToolTipText = "下载本文(Word格式)";
            this.pbWord.Url = null;
            this.pbWord.Visible = false;
            // 
            // pbtForward
            // 
            this.pbtForward.Image = global::AndroidBox.Properties.Resources.forward;
            this.pbtForward.Location = new System.Drawing.Point(43, 4);
            this.pbtForward.Margin = new System.Windows.Forms.Padding(0);
            this.pbtForward.Name = "pbtForward";
            this.pbtForward.Size = new System.Drawing.Size(16, 16);
            this.pbtForward.TabIndex = 8;
            this.pbtForward.TabStop = false;
            this.pbtForward.ToolTipText = "下一页";
            this.pbtForward.Url = null;
            // 
            // pbtBack
            // 
            this.pbtBack.Image = global::AndroidBox.Properties.Resources.back;
            this.pbtBack.Location = new System.Drawing.Point(26, 4);
            this.pbtBack.Margin = new System.Windows.Forms.Padding(0);
            this.pbtBack.Name = "pbtBack";
            this.pbtBack.Size = new System.Drawing.Size(16, 16);
            this.pbtBack.TabIndex = 8;
            this.pbtBack.TabStop = false;
            this.pbtBack.ToolTipText = "上一页";
            this.pbtBack.Url = null;
            // 
            // pbDownload
            // 
            this.pbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownload.Image = global::AndroidBox.Properties.Resources.attach;
            this.pbDownload.Location = new System.Drawing.Point(379, 4);
            this.pbDownload.Margin = new System.Windows.Forms.Padding(0);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(16, 16);
            this.pbDownload.TabIndex = 8;
            this.pbDownload.TabStop = false;
            this.pbDownload.ToolTipText = "下载示例代码";
            this.pbDownload.Url = null;
            this.pbDownload.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 391);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Android中文合集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseTree)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel labState;
        private PictureBoxTip pbBlog;
        private PictureBoxTip pbAbout;
        private PictureBoxTip pbWord;
        private PictureBoxTip pbOnline;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private PictureBoxTip pbDownload;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pbCloseTree;
        private System.Windows.Forms.ToolStripSplitButton inbox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private PictureBoxTip pbtForward;
        private PictureBoxTip pbtBack;
    }
}

