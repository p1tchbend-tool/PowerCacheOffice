using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class HotKey : Form
    {
        public static readonly int MOD_KEY_NONE = 0;
        public static readonly int MOD_KEY_ALT = 1;
        public static readonly int MOD_KEY_CONTROL = 2;
        public static readonly int MOD_KEY_SHIFT = 4;
        public event EventHandler OnHotKey;

        private readonly int WM_HOTKEY = 0x0312;
        private List<int> ids = new List<int>();

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_HOTKEY) OnHotKey(this, new HotKeyEventArgs((int)m.WParam));
        }

        public bool Add(int modKey, Keys key, int id)
        {
            if (NativeMethods.RegisterHotKey(this.Handle, id, modKey, key) != 0)
            {
                if (!ids.Contains(id)) ids.Add(id);
                return true;
            }
            return false;
        }

        public bool Remove(int id)
        {
            if (NativeMethods.UnregisterHotKey(this.Handle, id) != 0)
            {
                if (ids.Contains(id)) ids.Remove(id);
                return true;
            }
            return false;
        }

        public void RemoveAll()
        {
            ids.ForEach(x => NativeMethods.UnregisterHotKey(this.Handle, x));
            ids = new List<int>();
        }

        protected override void Dispose(bool disposing)
        {
            ids.ForEach(x => NativeMethods.UnregisterHotKey(this.Handle, x));

            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        public class HotKeyEventArgs : EventArgs
        {
            public int Id;
            public HotKeyEventArgs(int id)
            {
                Id = id;
            }
        }

        public HotKey()
        {
            InitializeComponent();
        }

        private void HotKey_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
