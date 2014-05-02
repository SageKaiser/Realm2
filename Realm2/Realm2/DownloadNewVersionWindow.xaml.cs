using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Realm2
{
    /// <summary>
    /// Interaction logic for DownloadNewVersionWindow.xaml
    /// </summary>
    public partial class DownloadNewVersionWindow : Window
    {
        MainWindow w;
        public DownloadNewVersionWindow(MainWindow window)
        {
            w = window;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            File.Delete(temppath);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var assembly = this.GetType().Assembly;
            using (var stream = assembly.GetManifestResourceStream("Realm2.update.exe"))
            {
                using (FileStream file = new FileStream("update.exe", FileMode.Create))
                {
                    stream.CopyTo(file);
                }
                Process.Start("update.exe");
            }
            w.Close();
            this.Close();
            Environment.Exit(0);
        }
    }
}
