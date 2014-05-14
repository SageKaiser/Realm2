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
    /// Interaction logic for InnkeeperWindow.xaml
    /// </summary>
    public partial class InnkeeperWindow : Window
    {
        public InnkeeperWindow()
        {
            InitializeComponent();
            innName.Content += new List<string>() { "Jean-Philippe", "Jacques-Cartier", "Bill", "Hank", "Ernie", "Margie", "Claude", "Gomie", "Chambers", "Takeshi" }[Program.random.Next(0, 10)];
            costBox.Text = "Would you like to stay at the inn for " + (Program.main.player.level < 5 ? 10 : Program.main.player.level * 2) + " gold?";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cost = Program.main.player.level < 5 ? 10 : Program.main.player.level * 2;
            if (Program.main.player.Purchase(cost))
            {
                costBox.Text = "Your health and mana have been restored.";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
