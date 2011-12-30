using AndroidBox.Common;
namespace AndroidBox.API
{
    partial class ContentPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentPanel));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logo = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pbAbout = new AndroidBox.Common.PictureBoxTip();
            this.pbCloseTree = new System.Windows.Forms.PictureBox();
            this.pbBlog = new AndroidBox.Common.PictureBoxTip();
            this.pbOnline = new AndroidBox.Common.PictureBoxTip();
            this.pbWord = new AndroidBox.Common.PictureBoxTip();
            this.pbtForward = new AndroidBox.Common.PictureBoxTip();
            this.pbtBack = new AndroidBox.Common.PictureBoxTip();
            this.pbDownload = new AndroidBox.Common.PictureBoxTip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.logo);
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
            this.splitContainer1.Size = new System.Drawing.Size(691, 311);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 11;
            // 
            // logo
            // 
            this.logo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(4, 217);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(218, 92);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logo.TabIndex = 2;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(3, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(219, 212);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(262, 1);
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
            this.txtSearch.Size = new System.Drawing.Size(195, 21);
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
            this.webBrowser1.Size = new System.Drawing.Size(456, 285);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // pbAbout
            // 
            this.pbAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAbout.Image = global::AndroidBox.API.Properties.Resources.user_business_boss;
            this.pbAbout.Location = new System.Drawing.Point(425, 4);
            this.pbAbout.Margin = new System.Windows.Forms.Padding(0);
            this.pbAbout.Name = "pbAbout";
            this.pbAbout.Size = new System.Drawing.Size(16, 16);
            this.pbAbout.TabIndex = 9;
            this.pbAbout.TabStop = false;
            this.pbAbout.ToolTipText = "关于译者";
            this.pbAbout.Url = null;
            this.pbAbout.Visible = false;
            // 
            // pbCloseTree
            // 
            this.pbCloseTree.Image = global::AndroidBox.API.Properties.Resources.collapse;
            this.pbCloseTree.Location = new System.Drawing.Point(1, 3);
            this.pbCloseTree.Name = "pbCloseTree";
            this.pbCloseTree.Size = new System.Drawing.Size(22, 19);
            this.pbCloseTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCloseTree.TabIndex = 2;
            this.pbCloseTree.TabStop = false;
            this.pbCloseTree.Click += new System.EventHandler(this.pbCloseTree_Click);
            // 
            // pbBlog
            // 
            this.pbBlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBlog.Image = global::AndroidBox.API.Properties.Resources.blog;
            this.pbBlog.Location = new System.Drawing.Point(402, 4);
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
            this.pbOnline.Image = global::AndroidBox.API.Properties.Resources.link;
            this.pbOnline.Location = new System.Drawing.Point(379, 4);
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
            this.pbWord.Image = global::AndroidBox.API.Properties.Resources.page_white_word;
            this.pbWord.Location = new System.Drawing.Point(356, 4);
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
            this.pbtForward.Image = global::AndroidBox.API.Properties.Resources.forward;
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
            this.pbtBack.Image = global::AndroidBox.API.Properties.Resources.back;
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
            this.pbDownload.Image = global::AndroidBox.API.Properties.Resources.attach;
            this.pbDownload.Location = new System.Drawing.Point(331, 4);
            this.pbDownload.Margin = new System.Windows.Forms.Padding(0);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(16, 16);
            this.pbDownload.TabIndex = 8;
            this.pbDownload.TabStop = false;
            this.pbDownload.ToolTipText = "下载示例代码";
            this.pbDownload.Url = null;
            this.pbDownload.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tree_collapse.gif");
            this.imageList1.Images.SetKeyName(1, "tree_expand.gif");
            this.imageList1.Images.SetKeyName(2, "tree_ner.gif");
            // 
            // ContentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ContentPanel";
            this.Size = new System.Drawing.Size(691, 311);
            this.Load += new System.EventHandler(this.ContentPanel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private PictureBoxTip pbAbout;
        private System.Windows.Forms.PictureBox pbCloseTree;
        private PictureBoxTip pbBlog;
        private PictureBoxTip pbOnline;
        private PictureBoxTip pbWord;
        private PictureBoxTip pbtForward;
        private PictureBoxTip pbtBack;
        private PictureBoxTip pbDownload;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox logo;

    }
}
