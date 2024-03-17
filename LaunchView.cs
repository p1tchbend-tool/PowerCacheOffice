using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public class LaunchView : ListView
    {
        private static ListViewItem dragItem = null;
        private ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        private ImageList imageList = new ImageList();
        private bool isLeave = false;

        public LaunchView() 
        {
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(48, 48);

            var toolStripMenuItem1 = new ToolStripMenuItem("パスのコピー");
            toolStripMenuItem1.Font = new Font("メイリオ", 9);
            toolStripMenuItem1.Click += (s, eventArgs) => { };
            contextMenuStrip.Items.Add(toolStripMenuItem1);

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
                string[] files = { dragItem.Tag.ToString() };
                var dataObject = new DataObject(DataFormats.FileDrop, files);
                this.DoDragDrop(dataObject, DragDropEffects.Copy);
            };

            this.DragEnter += (s, eventArgs) => eventArgs.Effect = DragDropEffects.All;
            this.DragOver += (s, eventArgs) =>
            {
                var point = this.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                if (!this.ClientRectangle.Contains(point)) isLeave = true;

                var item = this.GetItemAt(point.X, point.Y);
                if (item != null && !item.Selected) item.Selected = true;
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
                    if (dragItem != null && index > dragItem.Index) index++;

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
                    imageList.Images.RemoveAt(dragItem.Index);
                    this.Items.Remove(dragItem);
                    dragItem = null;
                }

                for (int i = 0; i < this.Items.Count; i++) this.Items[i].ImageIndex = i;
            };

            this.MouseMove += (s, eventArgs) =>
            {
                var item = this.GetItemAt(eventArgs.Location.X, eventArgs.Location.Y);
                if (item != null && item.Selected == false) item.Selected = true;
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
                    //SelectedFile = this.SelectedItems[0].Tag.ToString();
                    //this.Close();
                }
            };
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
    }
}
