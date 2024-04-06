using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal class LaunchMenuItem : ToolStripMenuItem
    {
        public LaunchMenuItem(string itemPath, Form1 mainForm, Form3 launchForm)
        {
            this.Text = Path.GetFileName(itemPath);

            if (Directory.Exists(itemPath))
            {
                this.DropDownItems.Add("");
                this.ForeColor = Color.FromArgb(26, 13, 171);
            }
            else
            {
                this.ForeColor = Color.FromArgb(0, 0, 0);
            }

            this.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackColor = Color.FromArgb(243, 243, 243);

            this.DropDownOpening += (s, e) =>
            {
                if (!Directory.Exists(itemPath)) return;
                this.DropDownItems.Clear();

                try
                {
                    var folders = Directory.GetDirectories(itemPath);
                    var files = Directory.GetFiles(itemPath);
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

                    if (items.Length != 0)
                    {
                        this.DropDownItems.AddRange(items);
                    }
                    else
                    {
                        this.DropDownItems.Add(new LaunchMenuItem("(なし)", mainForm, launchForm));
                    }
                }
                catch
                {
                    this.DropDownItems.Add(new LaunchMenuItem("(なし)", mainForm, launchForm));
                }
            };

            this.MouseUp += (s, e) =>
            {
                if (e.Button != MouseButtons.Left) return;
                if (itemPath == "(なし)") return;

                mainForm.OpenRecentFile(itemPath);
                launchForm.Visible = false;
            };
        }
    }
}
