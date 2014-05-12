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
    /// Interaction logic for BackpackWindow.xaml
    /// </summary>
    public partial class BackpackWindow : Window
    {
        public BackpackWindow()
        {
            InitializeComponent();
        }

        private void itemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item i = (Item)itemBox.SelectedItem;
            string itemcolor = "";
            switch(i.tier)
            {
                case 1:
                    itemcolor = "Gray";
                    break;
                case 2:
                    itemcolor = "Yellow";
                    break;
                case 3:
                    itemcolor = "Blue";
                    break;
                case 4:
                    itemcolor = "Green";
                    break;
                case 5:
                    itemcolor = "Orange";
                    break;
                case 6:
                    itemcolor = "Red";
                    break;
                case 7:
                    itemcolor = "Purple";
                    break;
            }
            itemDesc.AppendText(i.name + "\r\n", itemcolor, true);
            itemDesc.AppendText(i.desc, "Black");
            itemDesc.AppendText("Attack buff: " + i.atkbuff, "SeaGreen");
            itemDesc.AppendText("Defense buff: " + i.defbuff, "SeaGreen");
            itemDesc.AppendText("Speed buff: " + i.spdbuff, "SeaGreen");
            itemDesc.AppendText("Intelligence buff: " + i.intlbuff, "SeaGreen");
            itemDesc.AppendText("Value: " + i.value, "Gold");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
