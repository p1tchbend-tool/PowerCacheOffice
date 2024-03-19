using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public class LaunchView : ListView
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

        public LaunchView() 
        {
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(48, 48);

            var toolStripMenuItem1 = new ToolStripMenuItem("パスのコピー");
            toolStripMenuItem1.Font = new Font("メイリオ", 9);
            toolStripMenuItem1.Click += (s, eventArgs) =>
            {
                if (this.SelectedItems.Count == 1) Clipboard.SetText(this.SelectedItems[0].Tag.ToString());
            };
            contextMenuStrip.Items.Add(toolStripMenuItem1);

            var toolStripMenuItem2 = new ToolStripMenuItem("場所を開く");
            toolStripMenuItem2.Font = new Font("メイリオ", 9);
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

            var toolStripMenuItem3 = new ToolStripMenuItem("削除");
            toolStripMenuItem3.Font = new Font("メイリオ", 9);
            toolStripMenuItem3.Click += (s, eventArgs) =>
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
            contextMenuStrip.Items.Add(toolStripMenuItem3);

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
                    imageList.Images.Add(ResizeBitmap(GetIconImageFromPath(files[0]), 48, 48));
                    this.Items.Add(item);
                }
                else
                {
                    var index = selectedItem.Index;
                    if (dragItem != null && dragItemParent == this && index > dragItem.Index) index++;

                    var bitmaps = new List<Bitmap>();
                    foreach (var bitmap in imageList.Images) bitmaps.Add((Bitmap)bitmap);
                    bitmaps.Insert(index, ResizeBitmap(GetIconImageFromPath(files[0]), 48, 48));
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
                imageList.Images.Add(GetBitmapFromtBase64String(base64String));
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

        private Bitmap GetBitmapFromtBase64String(string str)
        {
            if (string.IsNullOrEmpty(str)) return new Bitmap(1, 1);

            byte[] bytes = Convert.FromBase64String(str);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return new Bitmap(ms);
            }
        }

        private Bitmap GetIconImageFromPath(string str)
        {
            Bitmap bmp = new Bitmap(1, 1);
            if (string.IsNullOrEmpty(str)) return bmp;

            try
            {
                NativeMethods.SHFILEINFO shinfo = new NativeMethods.SHFILEINFO();
                IntPtr hSuccess = NativeMethods.SHGetFileInfo(
                    @str, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), NativeMethods.SHGFI_ICON | NativeMethods.SHGFI_LARGEICON);
                if (hSuccess == IntPtr.Zero) return bmp;

                Icon icon = Icon.FromHandle(shinfo.hIcon);
                bmp = icon.ToBitmap();
                NativeMethods.DestroyIcon(icon.Handle);
            }
            catch { }
            return bmp;
        }

        private Bitmap ResizeBitmap(Bitmap original, int width, int height)
        {
            if (original == null) return null;

            float scale = Math.Min((float)width / original.Width, (float)height / original.Height);
            int scaleWidth = (int)(original.Width * scale);
            int scaleHeight = (int)(original.Height * scale);

            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(original, new Rectangle((width - scaleWidth) / 2, (height - scaleHeight) / 2, scaleWidth, scaleHeight));
            }
            return result;
        }
    }
}
