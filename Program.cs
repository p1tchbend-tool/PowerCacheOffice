using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal static class Program
    {
        public static readonly string AppName = "Power Cache Office";

        private static readonly string powerCacheOfficeLogFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\log");

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            NativeMethods.SetThreadDpiAwarenessContext(NativeMethods.DPI_AWARENESS_INVALID);
            NativeMethods.SetProcessDpiAwarenessContext(NativeMethods.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => ErrorHandling(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => ErrorHandling((Exception)e.ExceptionObject);

            try
            {
                App winAppBase = new App();
                winAppBase.Run(args);
            }
            catch (Exception ex) { ErrorHandling(ex); }
        }

        static void ErrorHandling(Exception exception)
        {
            try
            {
                if (!Directory.Exists(powerCacheOfficeLogFolder)) Directory.CreateDirectory(powerCacheOfficeLogFolder);
                File.WriteAllText(
                    Path.Combine(powerCacheOfficeLogFolder, DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N") + ".log"), exception.ToString());

                MessageBox.Show(exception.Message, AppName);
            }
            finally { Environment.Exit(1); }
        }

        public static void CopyFileAndAttributes(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath, true);

            var attributes = File.GetAttributes(sourcePath);
            File.SetAttributes(destinationPath, attributes);

            var creationTime = File.GetCreationTime(sourcePath);
            var lastAccessTime = File.GetLastAccessTime(sourcePath);
            var lastWriteTime = File.GetLastWriteTime(sourcePath);

            File.SetCreationTime(destinationPath, creationTime);
            File.SetLastAccessTime(destinationPath, lastAccessTime);
            File.SetLastWriteTime(destinationPath, lastWriteTime);
        }

        public static void SortTabIndex(Form form)
        {
            var children = new List<Control>();
            foreach (Control child in form.Controls) children.Add(child);
            children.Sort((x, y) =>
            {
                if (x.Top == y.Top) return x.Left.CompareTo(y.Left);
                return x.Top.CompareTo(y.Top);
            });
            for (int i = 0; i < children.Count; i++) children[i].TabIndex = i;
        }

        public static void ChangeDarkMode(Form form, bool enabled)
        {
            NativeMethods.DwmSetWindowAttribute(
                form.Handle, NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE, ref enabled, (uint)Marshal.SizeOf(typeof(bool)));
            if (enabled)
            {
                form.BackColor = Color.FromArgb(33, 33, 33);
                form.ForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                form.BackColor = Color.FromArgb(243, 243, 243);
                form.ForeColor = Color.FromArgb(0, 0, 0);
            }

            ChangeColor(form);
            void ChangeColor(Control ctrl)
            {
                foreach (Control c in ctrl.Controls)
                {
                    try
                    {
                        if (enabled)
                        {
                            c.BackColor = Color.FromArgb(33, 33, 33);
                            c.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            c.BackColor = Color.FromArgb(243, 243, 243);
                            c.ForeColor = Color.FromArgb(0, 0, 0);
                        }
                    }
                    catch { }
                    ChangeColor(c);
                }
            }
        }
    }
}
