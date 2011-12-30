namespace AndroidBox.Common
{
    partial class TabBodyControl
    {

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabBodyControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logo = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pbtForward = new AndroidBox.Common.PictureBoxTip();
            this.pbtBack = new AndroidBox.Common.PictureBoxTip();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pbCloseTree = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseTree)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Panel2.Controls.Add(this.pbtForward);
            this.splitContainer1.Panel2.Controls.Add(this.pbtBack);
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Panel2.Controls.Add(this.pbCloseTree);
            this.splitContainer1.Size = new System.Drawing.Size(691, 311);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // logo
            // 
            this.logo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Location = new System.Drawing.Point(6, 216);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(217, 92);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logo.TabIndex = 3;
            this.logo.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(223, 210);
            this.treeView1.TabIndex = 2;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(265, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 23);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "全文搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearch.Location = new System.Drawing.Point(63, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(196, 21);
            this.txtSearch.TabIndex = 14;
            // 
            // pbtForward
            // 
            this.pbtForward.Image = global::AndroidBox.Common.Properties.Resources.forward;
            this.pbtForward.Location = new System.Drawing.Point(45, 5);
            this.pbtForward.Margin = new System.Windows.Forms.Padding(0);
            this.pbtForward.Name = "pbtForward";
            this.pbtForward.Size = new System.Drawing.Size(16, 16);
            this.pbtForward.TabIndex = 12;
            this.pbtForward.TabStop = false;
            this.pbtForward.ToolTipText = "下一页";
            this.pbtForward.Url = null;
            // 
            // pbtBack
            // 
            this.pbtBack.Image = global::AndroidBox.Common.Properties.Resources.back;
            this.pbtBack.Location = new System.Drawing.Point(28, 5);
            this.pbtBack.Margin = new System.Windows.Forms.Padding(0);
            this.pbtBack.Name = "pbtBack";
            this.pbtBack.Size = new System.Drawing.Size(16, 16);
            this.pbtBack.TabIndex = 13;
            this.pbtBack.TabStop = false;
            this.pbtBack.ToolTipText = "上一页";
            this.pbtBack.Url = null;
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(4, 26);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(452, 280);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // pbCloseTree
            // 
            this.pbCloseTree.Image = global::AndroidBox.Common.Properties.Resources.expand;
            this.pbCloseTree.Location = new System.Drawing.Point(3, 4);
            this.pbCloseTree.Name = "pbCloseTree";
            this.pbCloseTree.Size = new System.Drawing.Size(22, 19);
            this.pbCloseTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCloseTree.TabIndex = 6;
            this.pbCloseTree.TabStop = false;
            this.pbCloseTree.Click += new System.EventHandler(this.pbCloseTree_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tree_collapse.gif");
            this.imageList1.Images.SetKeyName(1, "tree_expand.gif");
            this.imageList1.Images.SetKeyName(2, "tree_ner.gif");
            // 
            // TabBodyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TabBodyControl";
            this.Size = new System.Drawing.Size(691, 311);
            this.Load += new System.EventHandler(this.TabBodyControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.TreeView treeView1;
        protected System.Windows.Forms.WebBrowser webBrowser1;
        protected System.Windows.Forms.PictureBox pbCloseTree;
        protected System.Windows.Forms.PictureBox logo;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.TextBox txtSearch;
        protected PictureBoxTip pbtForward;
        protected PictureBoxTip pbtBack;
        protected System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.IContainer components;
    }
}
