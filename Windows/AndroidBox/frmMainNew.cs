using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using AndroidBox.Model;
using AndroidBox.Interface;

namespace AndroidBox
{
    public partial class frmMainNew : Form
    {
        private cndroidService service = new cndroidService();

        private List<PlugConfig> configs = cndroidService.GetCachePlugs();
       

        public frmMainNew()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainNew_Load(object sender, EventArgs e)
        {
            //����Ч��
            SystemUtils.AnimateWindow(this.Handle, 800, SystemUtils.AW_CENTER);

            //����������
            BoxUpdate.Update();

            //�����ռ�����Ϣ
            UpdateInboxState();

            //���ز��
            //tabControl1.TabPages.Clear();
            foreach (PlugConfig cfg in configs)
            {
                tabControl1.TabPages.Add(cfg.Name);
            }

            Assembly.LoadFrom("AndroidBox.Common.dll");

            //������ҳ����
            //webBrowser1
            setTab(0);
        }

        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isTrueExit)
            {
                //SystemUtils.AnimateWindow(this.Handle, 800, SystemUtils.AW_SLIDE | SystemUtils.AW_HIDE | SystemUtils.AW_BLEND);
                this.Hide();
                this.notifyIcon1.Visible = true;
                e.Cancel = true;
            }
        }

        private void setTab(int tab_index)
        {
            if (tab_index > -1 && tabControl1.TabPages.Count > 0)
            {
                TabPage tab = tabControl1.TabPages[tab_index];
                Control.ControlCollection controls = tab.Controls;
                if (controls.Count == 0)
                {
                    string dll = configs[tab_index].DllName;
                    string typeName = "";
                    try
                    {
                        Assembly amy = Assembly.LoadFrom(dll);
                        typeName = dll.Replace(".dll", "") + ".ContentPanel";
                        UserControl control = (UserControl)amy.CreateInstance(typeName);
                        control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                        control.Width = tab.Width;
                        control.Height = tab.Height;
                        controls.Add(control);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("�������ʧ��!" + ex.Message + "|" + typeName + ":" + ex.Source + ":" + ex.StackTrace);
                    }

                }
                else
                {
                    controls[0].Width = tab.Width;
                    controls[0].Height = tab.Height;
                }
            }
        }


        /// <summary>
        /// �л�Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTab(tabControl1.SelectedIndex);
        }

        /// <summary>
        /// �����ռ���״̬
        /// </summary>
        public void UpdateInboxState()
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
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private bool isTrueExit = false;

        private void �˳�ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            isTrueExit = true;
            this.Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.notifyIcon1.Visible = false;
        }

        private void �˳�_Click(object sender, EventArgs e)
        {
            isTrueExit = true;
            this.Close();
        }


        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            isTrueExit = true;
            this.Close();
        }

        /// <summary>
        /// �շ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inbox_ButtonClick(object sender, EventArgs e)
        {
            WebBrowser browser = null;
            Control CurrentControl = tabControl1.SelectedTab.Controls[0];
            if (CurrentControl is IUserControl)
                browser = (CurrentControl as IUserControl).GetBody();
            frmMessage inbox = new frmMessage(browser);
            inbox.FormClosing += new FormClosingEventHandler(delegate
            {
                UpdateInboxState();
            });
            inbox.ShowDialog();
        }

        private void labState_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPlugManage plug = new frmPlugManage();
            plug.FormClosing += new FormClosingEventHandler(delegate
            {
                //����Ƿ�����˲��
                if (plug.HasChanged)
                {
                    List<PlugConfig> configs = cndroidService.GetCachePlugs();
                    this.configs = configs;
                    //ɾ�����
                    while (tabControl1.TabPages.Count > 0)
                    {
                        tabControl1.TabPages.RemoveAt(0);
                    }
                    //��Ӳ��
                    foreach (PlugConfig cfg in configs)
                    {
                        tabControl1.TabPages.Add(cfg.Name);
                    }
                }
            });
            plug.ShowDialog();
        }



    }
}