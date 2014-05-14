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
            StatWindow sw = new StatWindow();
            sw.Owner = this;

            sw.Show();
            bw.Show();
            this.Activate();
            Init i = new Init();
            i.Initialize(this, bw, crcw, sw);
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
            if (!Program.bypassConfirmation)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to exit? The game will not be saved.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                    foreach (Window w in this.OwnedWindows)
                        w.Close();
                }
                else
                    e.Cancel = true;
            }
        }

        private void mainText_GotFocus(object sender, RoutedEventArgs e)
        {
            inputText.Focus();
        }

        private void statText_GotFocus(object sender, RoutedEventArgs e)
        {
            inputText.Focus();
        }

        private void mainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            inputText.Focus();
        }

        private void mainWindow_Activated(object sender, EventArgs e)
        {
            inputText.Focus();
        }

        private void mainWindow_StateChanged(object sender, EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.WindowState = this.WindowState;
            }
        }
    }
}
