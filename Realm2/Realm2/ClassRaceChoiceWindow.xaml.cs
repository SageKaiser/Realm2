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
            int i = raceBox.SelectedIndex;
            ComboBoxItem cbi = (ComboBoxItem)(raceBox.ItemContainerGenerator.ContainerFromIndex(i));
            Type type = Type.GetType("Realm2." + cbi.Content.ToString().Replace(" ", String.Empty));
            Race race = (Race)Activator.CreateInstance(type);
            raceDesc.Text = race.desc;
            Program.main.player.pRace = race;
        }

        private void classBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = classBox.SelectedIndex;
            ComboBoxItem cbi = (ComboBoxItem)(classBox.ItemContainerGenerator.ContainerFromIndex(i));
            Type type = Type.GetType("Realm2." + cbi.Content.ToString().Replace(" ", String.Empty));
            PlayerClass pc = (PlayerClass)Activator.CreateInstance(type);
            classDesc.Text = pc.desc;
            Program.main.player.pClass = pc;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (raceBox.SelectedItem == null || classBox.SelectedItem == null)
                e.Cancel = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (raceBox.SelectedItem != null && classBox.SelectedItem != null)
                this.Close();
        }
    }
}
