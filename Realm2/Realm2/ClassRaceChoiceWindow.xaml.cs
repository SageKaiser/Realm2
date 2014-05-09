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
using System.Windows.Shapes;

namespace Realm2
{
    /// <summary>
    /// Interaction logic for ClassRaceChoiceWindow.xaml
    /// </summary>
    public partial class ClassRaceChoiceWindow : Window
    {
        public ClassRaceChoiceWindow()
        {
            InitializeComponent();
        }

        private void raceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            raceDesc.Text = ((Race)raceBox.SelectedItem).desc;
            Program.main.player.pRace = (Race)raceBox.SelectedItem;
        }

        private void classBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classDesc.Text = ((PlayerClass)classBox.SelectedItem).desc;
            Program.main.player.pClass = (PlayerClass)classBox.SelectedItem;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (raceBox.SelectedItem == null || classBox.SelectedItem == null)
            {
                e.Cancel = true;
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (raceBox.SelectedItem != null && classBox.SelectedItem != null)
            {
                Program.main.write("You have chosen to be a ", "Black");
                Program.main.write(raceBox.SelectedItem + " " + classBox.SelectedItem, "CadetBlue", true);
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            raceBox.ItemsSource = Program.main.mainRaceList;
            classBox.ItemsSource = Program.main.mainClassList;
        }
    }
}
