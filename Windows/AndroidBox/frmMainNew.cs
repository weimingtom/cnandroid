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
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainNew_Load(object sender, EventArgs e)
        {
            //动画效果
            SystemUtils.AnimateWindow(this.Handle, 800, SystemUtils.AW_CENTER);

            //检查软件更新
            BoxUpdate.Update();

            //更新收件箱信息
            UpdateInboxState();

            //加载插件
            //tabControl1.TabPages.Clear();
            foreach (PlugConfig cfg in configs)
            {
                tabControl1.TabPages.Add(cfg.Name);
            }

            Assembly.LoadFrom("AndroidBox.Common.dll");

            //加载首页内容
            //webBrowser1
            setTab(0);
        }

        /// <summary>
        /// 窗体关闭
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

                        MessageBox.Show("插件加载失败!" + ex.Message + "|" + typeName + ":" + ex.Source + ":" + ex.StackTrace);
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
        /// 切换Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTab(tabControl1.SelectedIndex);
        }

        /// <summary>
        /// 更新收件箱状态
        /// </summary>
        public void UpdateInboxState()
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
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private bool isTrueExit = false;

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void 退出_Click(object sender, EventArgs e)
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
        /// 收发件箱
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
        /// 插件管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 插件管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPlugManage plug = new frmPlugManage();
            plug.FormClosing += new FormClosingEventHandler(delegate
            {
                //检测是否更改了插件
                if (plug.HasChanged)
                {
                    List<PlugConfig> configs = cndroidService.GetCachePlugs();
                    this.configs = configs;
                    //删除插件
                    while (tabControl1.TabPages.Count > 0)
                    {
                        tabControl1.TabPages.RemoveAt(0);
                    }
                    //添加插件
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