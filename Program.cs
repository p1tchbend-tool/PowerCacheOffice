using System;
using System.IO;
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

        public static void CopyAll(string sourcePath, string destinationPath)
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
    }
}
