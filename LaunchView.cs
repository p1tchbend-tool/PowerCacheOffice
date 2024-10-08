﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal class LaunchView : ListView
    {
        public event EventHandler OnItemChanged = delegate { };
        public event EventHandler OnLaunch = delegate { };
        public class LaunchEventArgs : EventArgs
        {
            public string Path;
            public LaunchEventArgs(string path)
            {
                Path = path;
            }
        }

        private static ListViewItem dragItem = null;
        private static LaunchView dragItemParent = null;

        private ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        private ImageList imageList = new ImageList();
        private bool isLeave = false;

        protected override void OnNotifyMessage(Message m)
        {
            const int WM_ERASEBKGND = 0x14;
            if (m.Msg != WM_ERASEBKGND) base.OnNotifyMessage(m);
        }

        public LaunchView(Form1 mainForm, Form3 launchForm) 
        {
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(32, 32);

            var toolStripMenuItem1 = new ToolStripMenuItem("パスのコピー");
            toolStripMenuItem1.Font = new Font("メイリオ", 9);
            toolStripMenuItem1.BackColor = Color.FromArgb(243, 243, 243);
            toolStripMenuItem1.Click += (s, eventArgs) =>
            {
                if (this.SelectedItems.Count == 1) Clipboard.SetText(this.SelectedItems[0].Tag.ToString());
            };
            contextMenuStrip.Items.Add(toolStripMenuItem1);

            var toolStripMenuItem2 = new ToolStripMenuItem("場所を開く");
            toolStripMenuItem2.Font = new Font("メイリオ", 9);
            toolStripMenuItem2.BackColor = Color.FromArgb(243, 243, 243);
            toolStripMenuItem2.Click += (s, eventArgs) =>
            {
                if (this.SelectedItems.Count == 1)
                {
                    var path = string.Empty;
                    try
                    {
                        path = Path.GetDirectoryName(this.SelectedItems[0].Tag.ToString());
                    }
                    catch { }
                    OnLaunch(this, new LaunchEventArgs(path));
                }
            };
            contextMenuStrip.Items.Add(toolStripMenuItem2);

            var toolStripMenuItem3 = new ToolStripMenuItem("メニュー展開");
            toolStripMenuItem3.Font = new Font("メイリオ", 9);
            toolStripMenuItem3.BackColor = Color.FromArgb(243, 243, 243);
            toolStripMenuItem3.Click += (s, e) =>
            {
                if (this.SelectedItems.Count != 1) return;
                try
                {
                    var path = this.SelectedItems[0].Tag.ToString();
                    if (!Directory.Exists(path)) path = Path.GetDirectoryName(path);

                    var folders = Directory.GetDirectories(@path);
                    var files = Directory.GetFiles(@path);
                    var items = new LaunchMenuItem[folders.Length + files.Length];

                    for (int i = 0; i < folders.Length; i++)
                    {
                        items[i] = new LaunchMenuItem(folders[i], mainForm, launchForm);
                    }

                    int num = 0;
                    for (int i = folders.Length; i < folders.Length + files.Length; i++)
                    {
                        items[i] = new LaunchMenuItem(files[num], mainForm, launchForm);
                        num++;
                    }

                    var menu = new ContextMenuStrip();
                    menu.Items.AddRange(items);
                    menu.Show(new Point(Cursor.Position.X + 1, Cursor.Position.Y + 1));
                }
                catch { }
            };
            contextMenuStrip.Items.Add(toolStripMenuItem3);

            var toolStripMenuItem4 = new ToolStripMenuItem("削除");
            toolStripMenuItem4.Font = new Font("メイリオ", 9);
            toolStripMenuItem4.BackColor = Color.FromArgb(243, 243, 243);
            toolStripMenuItem4.Click += (s, eventArgs) =>
            {
                if (this.SelectedItems.Count == 1)
                {
                    var selectedItem = this.SelectedItems[0];

                    var result = MessageBox.Show("選択したアイテムを削除しますか？", Program.AppName, MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes) return;

                    imageList.Images.RemoveAt(selectedItem.Index);
                    this.Items.Remove(selectedItem);
                    for (int i = 0; i < this.Items.Count; i++) this.Items[i].ImageIndex = i;

                    OnItemChanged(this, EventArgs.Empty);
                }
            };
            contextMenuStrip.Items.Add(toolStripMenuItem4);

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            this.Font = new Font("メイリオ", 9);
            this.MultiSelect = false;
            this.HideSelection = false;
            this.Cursor = Cursors.Hand;
            this.AllowDrop = true;
            this.LargeImageList = imageList;

            this.Enter += (s, eventArgs) => isLeave = false;
            this.Leave += (s, eventArgs) => isLeave = true;

            this.ItemDrag += (s, eventArgs) =>
            {
                dragItem = (ListViewItem)eventArgs.Item;
                dragItemParent = this;

                string[] files = { dragItem.Tag.ToString() };
                var dataObject = new DataObject(DataFormats.FileDrop, files);
                this.DoDragDrop(dataObject, DragDropEffects.Copy);

                dragItem = null;
                dragItemParent = null;
            };

            this.DragEnter += (s, eventArgs) => eventArgs.Effect = DragDropEffects.All;
            this.DragOver += (s, eventArgs) =>
            {
                var point = this.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                if (!this.ClientRectangle.Contains(point)) isLeave = true;

                var item = this.GetItemAt(point.X, point.Y);
                if (item != null && !item.Selected)
                {
                    item.Selected = true;
                    item.Focused = true;
                }
            };

            this.DragDrop += (s, eventArgs) =>
            {
                if (!eventArgs.Data.GetDataPresent(DataFormats.FileDrop)) return;

                var files = (string[])eventArgs.Data.GetData(DataFormats.FileDrop, false);
                if (files.Length != 1) return;
                if (!File.Exists(files[0]) && !Directory.Exists(files[0])) return;

                var item = new ListViewItem(Path.GetFileName(files[0]));
                item.Tag = files[0];

                var point = this.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                var selectedItem = this.GetItemAt(point.X, point.Y);

                if (selectedItem == null)
                {
                    imageList.Images.Add(GetIconImageFromPath(files[0]));
                    this.Items.Add(item);
                }
                else
                {
                    var index = selectedItem.Index;
                    if (dragItem != null && dragItemParent == this && index > dragItem.Index) index++;

                    var bitmaps = new List<Bitmap>();
                    foreach (var bitmap in imageList.Images) bitmaps.Add((Bitmap)bitmap);
                    bitmaps.Insert(index, GetIconImageFromPath(files[0]));
                    imageList.Images.Clear();
                    imageList.Images.AddRange(bitmaps.ToArray());

                    var listViewItems = new List<ListViewItem>();
                    foreach (var listViewItem in this.Items) listViewItems.Add((ListViewItem)listViewItem);
                    listViewItems.Insert(index, item);
                    this.Items.Clear();
                    this.Items.AddRange(listViewItems.ToArray());
                }

                if (dragItem != null)
                {
                    dragItemParent.LargeImageList.Images.RemoveAt(dragItem.Index);
                    dragItemParent.Items.Remove(dragItem);
                    dragItemParent = null;
                    dragItem = null;
                }

                for (int i = 0; i < this.Items.Count; i++) this.Items[i].ImageIndex = i;
                OnItemChanged(this, EventArgs.Empty);
            };

            this.MouseMove += (s, eventArgs) =>
            {
                var item = this.GetItemAt(eventArgs.Location.X, eventArgs.Location.Y);
                if (item != null && item.Selected == false)
                {
                    item.Selected = true;
                    item.Focused = true;
                }
            };

            this.MouseClick += (s, eventArgs) =>
            {
                if (isLeave) return;
                if (this.SelectedItems.Count != 1) return;
                if (!this.ClientRectangle.Contains(eventArgs.Location)) return;

                if (eventArgs.Button == MouseButtons.Right)
                {
                    contextMenuStrip.Show(this.PointToScreen(eventArgs.Location));
                }
                else
                {
                    OnLaunch(this, new LaunchEventArgs(this.SelectedItems[0].Tag.ToString()));
                }
            };
        }

        public void SetItems(List<string> largeImageListAsBase64Strings, List<string> paths)
        {
            this.Items.Clear();
            imageList.Images.Clear();

            paths.ForEach(path =>
            {
                var item = new ListViewItem(Path.GetFileName(path));
                item.Tag = path;
                this.Items.Add(item);
            });

            largeImageListAsBase64Strings.ForEach(base64String =>
            {
                imageList.Images.Add(GetBitmapFromBase64String(base64String));
            });

            for (int i = 0; i < this.Items.Count; i++) this.Items[i].ImageIndex = i;
        }

        public List<string> GetLargeImageListAsBase64Strings()
        {
            var list = new List<string>();
            foreach (var bitmap in imageList.Images) list.Add(GetBase64String((Bitmap)bitmap));
            return list;
        }

        public List<string> GetPaths()
        {
            var list = new List<string>();
            foreach (var item in this.Items) list.Add(((ListViewItem)item).Tag.ToString());
            return list;
        }

        private string GetBase64String(Bitmap bitmap)
        {
            if (bitmap == null) return string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                byte[] bites = ms.ToArray();
                return Convert.ToBase64String(bites);
            }
        }

        private Bitmap GetBitmapFromBase64String(string str)
        {
            if (string.IsNullOrEmpty(str)) return new Bitmap(1, 1);

            byte[] bytes = Convert.FromBase64String(str);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return new Bitmap(ms);
            }
        }

        private Bitmap GetIconImageFromPath(string path)
        {
            var bmp = new Bitmap(1, 1);
            if (string.IsNullOrEmpty(path)) return bmp;

            try
            {
                WindowsShellApi.SHFILEINFO shinfo = new WindowsShellApi.SHFILEINFO();
                IntPtr hImg = WindowsShellApi.SHGetFileInfo(
                    path, 0, out shinfo, (uint)Marshal.SizeOf(typeof(WindowsShellApi.SHFILEINFO)), WindowsShellApi.SHGFI.SHGFI_SYSICONINDEX);

                WindowsShellApi.IImageList imglist = null;
                WindowsShellApi.SHGetImageList(WindowsShellApi.SHIL.SHIL_EXTRALARGE, ref WindowsShellApi.IID_IImageList, out imglist);

                IntPtr hicon = IntPtr.Zero;
                imglist.GetIcon(shinfo.iIcon, (int)WindowsShellApi.ImageListDrawItemConstants.ILD_TRANSPARENT, ref hicon);

                Icon myIcon = Icon.FromHandle(hicon);
                bmp = myIcon.ToBitmap();
                NativeMethods.DestroyIcon(myIcon.Handle);
            }
            catch { }
            return bmp;
        }
    }
}
