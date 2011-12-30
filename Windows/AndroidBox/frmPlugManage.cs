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
    /// �������
    /// </summary>
    public partial class frmPlugManage : Form
    {
        #region Member Variable
        
        /// <summary>
        /// ���ݿ�������
        /// </summary>
        private cndroidService service = new cndroidService();
        /// <summary>
        /// �������ݼ�
        /// </summary>
        private DataTable dt;
        /// <summary>
        /// �Ƿ��޸�������
        /// </summary>
        public bool HasChanged = false;

        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public frmPlugManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��������¼�
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
        /// ���ø�ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["id"]);
            switch (e.ColumnIndex)
            {
                case 3://�Ƿ�����
                    string enable = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["enable"]) == "1" ? "0" : "1";
                    service.UpdatePlugEnable(id, enable);
                    dt = service.GetAllPlugs();
                    dataGridView1.DataSource = dt;
                    HasChanged = true;
                    break;
            }
        }

        /// <summary>
        /// ��Ԫ��༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string id = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["id"]);
            switch (e.ColumnIndex)
            {
                case 0://������
                    string title = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["tab_name"]);
                    service.UpdatePlugIndex(id, title);
                    HasChanged = true;
                    break;
                case 2://Tab˳��
                    string index = SqliteHelper.DBToString(dt.Rows[e.RowIndex]["index"]);
                    service.UpdatePlugIndex(id, index);
                    HasChanged = true;
                    break;
            }
        }

        /// <summary>
        /// ɨ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanPlugs_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// ����У��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //ȡ������
            e.Cancel = false;
            MessageBox.Show("����������","Tab˳��");
        }

    }
}