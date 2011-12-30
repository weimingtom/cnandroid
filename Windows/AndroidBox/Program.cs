using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace AndroidBox
{
    static class Program
    {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        private const int SW_HIDE = 0;           //隐藏窗口，活动状态给令一个窗口 
        private const int SW_SHOWNORMAL = 1;     //用原来的大小和位置显示一个窗口，同时令其进入活动状态 
        private const int SW_SHOWMINIMIZED = 2;  //最小化窗口，并将其激活 
        private const int SW_SHOWMAXIMIZED = 3;  //最大化窗口，并将其激活 
        private const int SW_SHOWNOACTIVATE = 4; //用最近的大小和位置显示一个窗口，同时不改变活动窗口 
        private const int SW_RESTORE = 9;        //用原来的大小和位置显示一个窗口，同时令其进入活动状态 
        private const int SW_SHOWDEFAULT = 10;   //根据默认创建窗口时的样式来显示


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMain());

            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "AndroidBox", out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmMainNew());//frmMain
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName("AndroidBox"))
                    {
                        //if (process.Id != current.Id)
                        //{
                        SetForegroundWindow(process.MainWindowHandle);
                        ShowWindowAsync(process.MainWindowHandle, SW_SHOWMAXIMIZED);
                        break;
                        //}
                    }
                }
            }
        }
    }
}