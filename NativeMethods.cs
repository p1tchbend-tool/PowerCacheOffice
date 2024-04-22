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

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        public static readonly uint SHGFI_ICON = 0x100;
        public static readonly uint SHGFI_LARGEICON = 0x0;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern bool SetThreadDpiAwarenessContext(IntPtr dpiFlag);
        public static readonly IntPtr DPI_AWARENESS_INVALID = new IntPtr(-1);

        [DllImport("user32.dll")]
        public static extern bool SetProcessDpiAwarenessContext(IntPtr dpiFlag);
        public static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = new IntPtr(-3);
    }
}
