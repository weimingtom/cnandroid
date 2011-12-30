using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AndroidBox
{
    public partial class frmMessage : Form
    {
        private WebBrowser browser;
        private cndroidService service = new cndroidService();
        private DataTable dt;

        public frmMessage(WebBrowser browser)
        {
            InitializeComponent();
            this.browser = browser;
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMessage_Load(object sender, EventArgs e)
        {
            dt = service.GetAllLog();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        /// <summary>
        /// 单击单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string src = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["src"]);
                string state = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["state"]);
                string message = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["message"]);
                //更新阅读状态
                if (state == "0" || state == "未读")
                {
                    string id = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["id"]);
                    service.UpdateLogState(id, "1");
                }
                if (browser != null)
                {
                    //显示详细内容
                    if (!string.IsNullOrEmpty(src))
                    {
                        if (src.EndsWith(".htm") || src.EndsWith(".html") || src.StartsWith("http"))
                        {
                            browser.Url = new Uri(src);
                        }
                        else
                            browser.DocumentText = src;
                    }
                    else
                        browser.DocumentText = message;
                }
                this.Close();

            }
        }
    }
}