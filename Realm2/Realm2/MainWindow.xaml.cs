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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Realm2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BackpackWindow bw = new BackpackWindow();
            bw.Owner = this;
            ClassRaceChoiceWindow crcw = new ClassRaceChoiceWindow();
            crcw.Owner = this;

            bw.Show();
            this.Activate();
            Init i = new Init();
            i.Initialize(this, bw, crcw);
        }

        private void inputText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Program.main.HandleInput(this.inputText.Text);
                this.inputText.Text = String.Empty;
            }
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window w in this.OwnedWindows)
                w.Close();
        }
    }
}
