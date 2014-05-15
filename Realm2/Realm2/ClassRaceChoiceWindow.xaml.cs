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
            //update the description and set the player's race
            raceDesc.Text = ((Race)raceBox.SelectedItem).desc + "\r\n\r\n" + ((Race)raceBox.SelectedItem).racialTrait;
            Program.main.player.pRace = (Race)raceBox.SelectedItem;
            //if both boxes have been filled, allow the button to be pressed
            if (classBox.SelectedIndex != -1)
                button.IsEnabled = true;
        }

        private void classBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classDesc.Text = ((PlayerClass)classBox.SelectedItem).desc;
            Program.main.player.pClass = (PlayerClass)classBox.SelectedItem;
            if (raceBox.SelectedIndex != -1)
                button.IsEnabled = true;
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
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            raceBox.ItemsSource = Program.main.mainRaceList;
            classBox.ItemsSource = Program.main.mainClassList;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                //update all stats and write required information
                Program.main.write("You have chosen to be a(n) ", "Black");
                Program.main.write(raceBox.SelectedItem + " " + classBox.SelectedItem, "MediumOrchid", true);
                Program.main.player.atk = 1 + Program.main.player.pRace.atk_init;
                Program.main.player.def = 1 + Program.main.player.pRace.def_init;
                Program.main.player.spd = 1 + Program.main.player.pRace.spd_init;
                Program.main.player.intl = 1 + Program.main.player.pRace.int_init;
                Program.main.player.maxhp = 10 + Program.main.player.pRace.hp_init;
                Program.main.player.hp = Program.main.player.maxhp;
                Program.main.writeStats();
                Program.main.mainWindow.IsEnabled = true;
                //write the current place name and description
                Program.main.write("Current Place: " + Program.main.currentplace.name, "Black");
                Program.main.write(Program.main.currentplace.desc, "Black");
                if (Program.main.player.pRace is Lycanthrope)
                {
                    //TODO: wolfey stuff
                }
                Program.main.gm = GameState.Main;
            }
            //this will only happen if the main window is closed during the class and race choice
            catch (NullReferenceException) { }
        }
    }
}
