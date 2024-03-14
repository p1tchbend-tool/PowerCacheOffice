using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, Keys KEY);

        [DllImport("user32.dll")]
        public static extern int UnregisterHotKey(IntPtr HWnd, int ID);

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref bool pvAttribute, uint cbAttribute);
        public static readonly int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static readonly int SW_RESTORE = 9;
    }
}
