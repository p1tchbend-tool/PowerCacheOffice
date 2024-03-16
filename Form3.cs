using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form3 : Form
    {
        public string SelectedFile {  get; set; }
        private ListViewItem dragItem = null;

        public Form3(List<string> recentFiles, bool isDarkMode)
        {
            SelectedFile = string.Empty;

            InitializeComponent();
            Program.SortTabIndex(this);
            Program.ChangeDarkMode(this, isDarkMode);

            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (var item in recentFiles.AsEnumerable().Reverse())
            {
                listBox1.Items.Add(item);
            }
            listBox1.EndUpdate();

            for (int i = 0; i < 5;  i++)
            {
                var listViewItem = new ListViewItem(Path.GetFileName(@"C:\Users\yuta\repository\Neon\MyToolStripMenuItem.cs"));
                listViewItem.Tag = @"C:\Users\yuta\repository\Neon\MyToolStripMenuItem.cs";
                imageList1.Images.Add(GetIconImageFromPath(@"C:\Users\yuta\repository\Neon\MyToolStripMenuItem.cs"));
                listViewItem.ImageIndex = imageList1.Images.Count - 1;
                listView1.Items.Add(listViewItem);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listView1.ItemDrag += (s, eventArgs) =>
            {
                dragItem = (ListViewItem)eventArgs.Item;
                string[] files = { dragItem.Tag.ToString() };
                var dataObject = new DataObject(DataFormats.FileDrop, files);
                listBox1.DoDragDrop(dataObject, DragDropEffects.Copy);
            };

            listView1.DragEnter += (s, eventArgs) => eventArgs.Effect = DragDropEffects.All;
            listView1.DragOver += (s, eventArgs) =>
            {
                var point = listView1.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                var item = listView1.GetItemAt(point.X, point.Y);
                if (item != null && !item.Selected) item.Selected = true;
            };

            listView1.DragDrop += (s, eventArgs) =>
            {
                if (!eventArgs.Data.GetDataPresent(DataFormats.FileDrop)) return;

                var files = (string[])eventArgs.Data.GetData(DataFormats.FileDrop, false);
                if (files.Length != 1) return;
                if (!File.Exists(files[0]) && !Directory.Exists(files[0])) return;

                var item = new ListViewItem(Path.GetFileName(files[0]));
                item.Tag = files[0];

                var point = listView1.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                var selectedItem = listView1.GetItemAt(point.X, point.Y);

                if (selectedItem == null)
                {
                    imageList1.Images.Add(GetIconImageFromPath(files[0]));
                    listView1.Items.Add(item);
                }
                else
                {
                    var index = selectedItem.Index;
                    if (dragItem != null && index > dragItem.Index) index++;

                    var bitmaps = new List<Bitmap>();
                    foreach (var bitmap in imageList1.Images) bitmaps.Add((Bitmap)bitmap);
                    bitmaps.Insert(index, GetIconImageFromPath(files[0]));
                    imageList1.Images.Clear();
                    imageList1.Images.AddRange(bitmaps.ToArray());

                    var listViewItems = new List<ListViewItem>();
                    foreach (var listViewItem in listView1.Items) listViewItems.Add((ListViewItem)listViewItem);
                    listViewItems.Insert(index, item);
                    listView1.Items.Clear();
                    listView1.Items.AddRange(listViewItems.ToArray());
                }

                if (dragItem != null)
                {
                    imageList1.Images.RemoveAt(dragItem.Index);
                    listView1.Items.Remove(dragItem);
                    dragItem = null;
                }

                for (int i = 0; i < listView1.Items.Count; i++) listView1.Items[i].ImageIndex = i;
            };

            listView1.MouseMove += (s, eventArgs) =>
            {
                var item = listView1.GetItemAt(eventArgs.Location.X, eventArgs.Location.Y);
                if (item != null && item.Selected == false) item.Selected = true;
            };

            listView1.MouseUp += (s, eventArgs) =>
            {
                if (listView1.SelectedItems.Count != 1) return;

                if (eventArgs.Button == MouseButtons.Right)
                {
                    // contextMenuStrip1.Show(listBox1.PointToScreen(eventArgs.Location));
                }
                else
                {
                    // SelectedFile = listView1.SelectedItems[0].Tag.ToString();
                    // this.Close();
                }
            };

            listBox1.MouseMove += (s, eventArgs) =>
            {
                var index = listBox1.IndexFromPoint(eventArgs.Location);
                if (index != ListBox.NoMatches) listBox1.SelectedIndex = index;

                if (listBox1.SelectedItem != null && eventArgs.Button == MouseButtons.Left)
                {
                    string[] files = { listBox1.SelectedItem.ToString() };
                    var dataObject = new DataObject(DataFormats.FileDrop, files);
                    listBox1.DoDragDrop(dataObject, DragDropEffects.Copy);
                };
            };

            listBox1.MouseUp += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;
                if (!listBox1.ClientRectangle.Contains(listBox1.PointToClient(Cursor.Position))) return;

                if (eventArgs.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(listBox1.PointToScreen(eventArgs.Location));
                }
                else
                {
                    SelectedFile = listBox1.SelectedItem.ToString();
                    this.Close();
                }
            };

            toolStripMenuItem1.Click += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;

                Clipboard.SetText(listBox1.SelectedItem.ToString());
                this.Close();
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
