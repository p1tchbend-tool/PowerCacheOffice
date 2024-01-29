using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace PowerCacheOffice
{
    public class App : WindowsFormsApplicationBase
    {
        public App() : base()
        {
            this.EnableVisualStyles = true;
            this.IsSingleInstance = true;
            this.MainForm = new Form1();
            this.StartupNextInstance += (s, e) =>
            {
                try
                {
                    ((Form1)this.MainForm).OpenFile(e.CommandLine.ToArray());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
            };
        }
        
        protected override void OnRun()
        {
            try
            {
                ((Form1)this.MainForm).OpenFile(this.CommandLineArgs.ToArray());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
            base.OnRun();
        }
    }
}
