using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AndroidBox
{
    public static class SystemUtils
    {

        /// <summary>
        /// 检查网络是否连接
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            int I = 0;
            bool state = InternetGetConnectedState(out I, 0);
            return state;
        }

        /// <summary>
        /// 检查网络是否连接
        /// </summary>
        /// <param name="lpdwFlags"></param>
        /// <param name="dwReserved"></param>
        /// <returns></returns>
        [DllImport("wininet.dll")]
        public static extern bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);


        /// <summary>
        /// 动画效果
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="dwTime"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        /// <summary>
        /// 从左向右显示
        /// </summary>
        public const int AW_HOR_POSITIVE = 0x0001;//
        /// <summary>
        /// 从右向左显示
        /// </summary>
        public const int AW_HOR_NEGATIVE = 0x0002;//
        /// <summary>
        /// 从上到下显示
        /// </summary>
        public const int AW_VER_POSITIVE = 0x0004;//
        /// <summary>
        /// 从下到上显示
        /// </summary>
        public const int AW_VER_NEGATIVE = 0x0008;//
        /// <summary>
        /// 从中间向四周
        /// </summary>
        public const int AW_CENTER = 0x0010;//
        public const int AW_HIDE = 0x10000;
        /// <summary>
        /// 普通显示
        /// </summary>
        public const int AW_ACTIVATE = 0x20000;//
        public const int AW_SLIDE = 0x40000;
        /// <summary>
        /// 透明渐变显示效果
        /// </summary>
        public const int AW_BLEND = 0x80000;//

        //Form Load事件：
        //            //动画由小渐大,现在取消
        //            AnimateWindow(this.Handle, 500, AW_CENTER | AW_ACTIVATE);

        //             //普通显示
        //                    AnimateWindow(Handle, 1000, AW_ACTIVATE);

        //               //从左向右显示
        //                    AnimateWindow(Handle, 1000, AW_HOR_POSITIVE);
        //                //从右向左显示
        //                    AnimateWindow(Handle, 1000, AW_HOR_NEGATIVE);

        //                //从上到下显示
        //                    AnimateWindow(Handle, 1000, AW_VER_POSITIVE);

        //                //从下到上显示
        //                    AnimateWindow(Handle, 1000, AW_VER_NEGATIVE);

        //                //透明渐变显示
        //                    AnimateWindow(Handle, 1000, AW_BLEND);

        //                //从中间向四周
        //                    AnimateWindow(Handle, 1000, AW_CENTER);

        //                //左上角伸展
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_POSITIVE | AW_VER_POSITIVE);

        //                //左下角伸展
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_POSITIVE | AW_VER_NEGATIVE);

        //                //右上角伸展
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_NEGATIVE | AW_VER_POSITIVE);

        //                //右下角伸展
        //                    AnimateWindow(Handle, 1000, AW_SLIDE | AW_HOR_NEGATIVE | AW_VER_NEGATIVE);

        //可以通过组合出不同的效果
        //例子：//退出效果 向上移出窗体
        //        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        //        {
        //            AnimateWindow(this.Handle, 800, AW_SLIDE | AW_HIDE | AW_VER_NEGATIVE);
        //        }
        //通过变换AnimateWindow（AW_SLIDE | AW_HIDE |？）中问号的值！移动方向参考最上面的具体说明。
        //祝你编程愉快！
    }
}
