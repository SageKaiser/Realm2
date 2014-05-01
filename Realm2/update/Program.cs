using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace update
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Realm2.exe";
                File.Delete(path);
                File.Copy(temppath, path);
                Process.Start(path);
                Environment.Exit(0);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Realm 2 Has Encountered an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
