using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MerchantWindow.xaml
    /// </summary>
    public partial class MerchantWindow : Window
    {
        ObservableCollection<Item> fs;
        public MerchantWindow()
        {
            InitializeComponent();
            fs = getRelevantItemSet();
            merchantName.Content += new List<string>() { "Clark", "Rosie", "Nick", "Carter", "Charlie", "Lauren", "Steve", "Isabel", "Connor", "Alex" }[Program.random.Next(0, 10)];

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bpBox.ItemsSource = Program.main.player.backpack;
            forsaleBox.ItemsSource = fs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Program.main.player.Purchase((Item)forsaleBox.SelectedItem);
        }

        private void sellButton_Click(object sender, RoutedEventArgs e)
        {
            Program.main.player.g += Convert.ToInt32(((Item)bpBox.SelectedItem).value * .4);
            Program.main.player.backpack.Remove((Item)bpBox.SelectedItem);
            Program.main.writeStats();
        }
        /// <summary>
        /// Gets a collection of Items relevant to the Player's level.
        /// </summary>
        /// <returns>An ObservableCollection of relevant Items.</returns>
        public ObservableCollection<Item> getRelevantItemSet()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            //gets a random number of Items between 1 and 5.
            int numberOfItems = Program.random.Next(1, 6);
            for (int i = 0; i < numberOfItems; i++)
            {

            }
            return items;
        }
    }
}
