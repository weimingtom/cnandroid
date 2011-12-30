using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AndroidBox
{
    /// <summary>
    /// 插件管理
    /// </summary>
    public partial class frmPlugManage : Form
    {
        #region Member Variable
        
        /// <summary>
        /// 数据库服务对象
        /// </summary>
        private cndroidService service = new cndroidService();
        /// <summary>
        /// 缓存数据集
        /// </summary>
        private DataTable dt;
        /// <summary>
        /// 是否修改了数据
        /// </summary>
        public bool HasChanged = false;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmPlugManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPlugManage_Load(object sender, EventArgs e)
        {
            dt = service.GetAllPlugs();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        /// <summary>
        /// 启用复选框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["id"]);
            switch (e.ColumnIndex)
            {
                case 3://是否启用
                    string enable = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["enable"]) == "1" ? "0" : "1";
                    service.UpdatePlugEnable(id, enable);
                    dt = service.GetAllPlugs();
                    dataGridView1.DataSource = dt;
                    HasChanged = true;
                    break;
            }
        }

        /// <summary>
        /// 单元格编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string id = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["id"]);
            switch (e.ColumnIndex)
            {
                case 0://重命名
                    string title = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["tab_name"]);
                    service.UpdatePlugIndex(id, title);
                    HasChanged = true;
                    break;
                case 2://Tab顺序
                    string index = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["index"]);
                    service.UpdatePlugIndex(id, index);
                    HasChanged = true;
                    break;
            }
        }

        /// <summary>
        /// 扫描插件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanPlugs_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //取消输入
            e.Cancel = false;
            MessageBox.Show("请输入数字","Tab顺序");
        }

    }
}