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
    }
}
