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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cost = Program.main.player.level < 5 ? 10 : Program.main.player.level * 2;
            if (Program.main.player.Purchase(cost))
            {
                costBox.Text = "Your health and mana have been restored.";
            }
        }
    }
}
