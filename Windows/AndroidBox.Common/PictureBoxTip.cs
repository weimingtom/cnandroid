using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AndroidBox.Common
{
    public partial class PictureBoxTip : UserControl, ISupportInitialize
    {
        public new event EventHandler OnClick;
        ToolTip toolTip = new ToolTip();

        public PictureBoxTip()
        {
            InitializeComponent();
            // 
        }


        private void PictureBoxTip_Load(object sender, EventArgs e)
        {
            this.Width = 16;
            this.Height = 16;
            toolTip.ShowAlways = true;
            this.pictureBox1.MouseLeave += new EventHandler(PictureBoxTip_MouseLeave);
            //this.pictureBox1.MouseHover += new EventHandler(PictureBoxTip_MouseEnter);
            this.pictureBox1.MouseEnter += new EventHandler(PictureBoxTip_MouseEnter);
        }

        #region ISupportInitialize 成员

        public void BeginInit()
        {
            this.Width = 16;
            this.Height = 16;
        }

        public void EndInit()
        {

        }

        #endregion

        #region Events

        //private void PictureBoxTip_Resize(object sender, EventArgs e)
        //{
        //    this.pictureBox1.Width = this.Width;
        //    this.pictureBox1.Height = this.Height;
        //}



        /// <summary>
        /// 离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxTip_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(this.pictureBox1);
        }

        private void PictureBoxTip_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show(ToolTipText, this.pictureBox1);
        }


        #endregion

        #region Protery

        private Image image;
        /// <summary>
        /// 图片
        /// </summary>
        [Localizable(true)]
        [Bindable(true)]
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                this.pictureBox1.Image = image;
                this.Width = image.Width;
                this.Height = image.Height;
            }
        }

        private string text;
        /// <summary>
        /// 提示信息
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [DefaultValue("")]
        public string ToolTipText
        {
            get  
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        private string url;
        /// <summary>
        /// 译者博客
        /// </summary>
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        #endregion

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(Url))
                System.Diagnostics.Process.Start(Url);
            else if (OnClick != null)
                OnClick(sender, e);
        }

    }
}
