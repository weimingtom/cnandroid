using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AndroidBox
{
    public static class SystemUtils
    {

        /// <summary>
        /// ��������Ƿ�����
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            int I = 0;
            bool state = InternetGetConnectedState(out I, 0);
            return state;
        }

        /// <summary>
        /// ��������Ƿ�����
        /// </summary>
        /// <param name="lpdwFlags"></param>
        /// <param name="dwReserved"></param>
        /// <returns></returns>
        [DllImport("wininet.dll")]
        public static extern bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);


        /// <summary>
        /// ����Ч��
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="dwTime"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        /// <summary>
        /// ����������ʾ
        /// </summary>
        public const int AW_HOR_POSITIVE = 0x0001;//
        /// <summary>
        /// ����������ʾ
        /// </summary>
        public const int AW_HOR_NEGATIVE = 0x0002;//
        /// <summary>
        /// ���ϵ�����ʾ
        /// </summary>
        public const int AW_VER_POSITIVE = 0x0004;//
        /// <summary>
        /// ���µ�����ʾ
        /// </summary>
        public const int AW_VER_NEGATIVE = 0x0008;//
        /// <summary>
        /// ���м�������
        /// </summary>
        public const int AW_CENTER = 0x0010;//
        public const int AW_HIDE = 0x10000;
        /// <summary>
        /// ��ͨ��ʾ
        /// </summary>
        public const int AW_ACTIVATE = 0x20000;//
        public const int AW_SLIDE = 0x40000;
        /// <summary>
        /// ͸��������ʾЧ��
        /// </summary>
        public const int AW_BLEND = 0x80000;//

        //Form Load�¼���
        //            //������С����,����ȡ��
        //            AnimateWindow(this.Handle, 500, AW_CENTER | AW_ACTIVATE);

        //             //��ͨ��ʾ
        //                    AnimateWindow(Handle, 1000, AW_ACTIVATE);

        //               //����������ʾ
        //                    AnimateWindow(Handle, 1000, AW_HOR_POSITIVE);
        //                //����������ʾ
        //                    AnimateWindow(Handle, 1000, AW_HOR_NEGATIVE);

        //                //���ϵ�����ʾ
        //                    AnimateWindow(Handle, 1000, AW_VER_POSITIVE);

        //                //���µ�����ʾ
        //                    AnimateWindow(Handle, 1000, AW_VER_NEGATIVE);

        //                //͸��������ʾ
        //                    AnimateWindow(Handle, 1000, AW_BLEND);

        //                //���м�������
        //                    AnimateWindow(Handle, 1000, AW_CENTER);

        //                //���Ͻ���չ
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_POSITIVE | AW_VER_POSITIVE);

        //                //���½���չ
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_POSITIVE | AW_VER_NEGATIVE);

        //                //���Ͻ���չ
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_NEGATIVE | AW_VER_POSITIVE);

        //                //���½���չ
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_NEGATIVE | AW_VER_NEGATIVE);

        //����ͨ����ϳ���ͬ��Ч��
        //���ӣ�//�˳�Ч�� �����Ƴ�����
        //        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        //        {
        //            AnimateWindow(this.Handle, 800, AW_SLIDE | AW_HIDE | AW_VER_NEGATIVE);
        //        }
        //ͨ���任AnimateWindow��AW_SLIDE | AW_HIDE |�������ʺŵ�ֵ���ƶ�����ο�������ľ���˵����
        //ף������죡
    }
}
