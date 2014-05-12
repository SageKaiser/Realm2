using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public interface INPC
    {
        void Interact();
    }
    public class Merchant : INPC
    {
        MerchantWindow mw;
        ObservableCollection<Item> itemsForSale;
        public Merchant(ObservableCollection<Item> forsale)
        {
            mw = new MerchantWindow();
            mw.merchantName.Content += ToString();
            itemsForSale = forsale;
            mw.forsaleBox.ItemsSource = itemsForSale;
        }
        public override string ToString()
        {
            return new List<string>() { "Clark", "Rosie", "Nick", "Carter", "Charlie", "Lauren", "Steve", "Isabel", "Connor", "Alex" }[Program.main.rand.Next(0, 10)];
        }
        public void Interact()
        {
            mw.forsaleBox.ItemsSource = itemsForSale;
            mw.Show();
        }
    }
    public class Innkeeper : INPC
    {
        InnkeeperWindow ikw;
        public Innkeeper()
        {
            ikw = new InnkeeperWindow();
            ikw.innName.Content += ToString();
            ikw.costBox.Text = "Would you like to stay at the inn for " + (Program.main.player.level < 5 ? 10 : Program.main.player.level * 2) + " gold?";
        }
        public override string ToString()
        {
            return new List<string>() { "Jean-Philippe", "Jacques-Cartier", "Bill", "Hank", "Ernie", "Margie", "Claude", "Gomie", "Chambers", "Takeshi" }[Program.main.rand.Next(0, 10)];
        }
        public void Interact()
        {
            ikw.Show();
        }
    }
}
