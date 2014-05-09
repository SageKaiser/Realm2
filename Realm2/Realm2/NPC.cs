using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public abstract class NPC
    {
        public string name;
        public abstract void Interact();
    }
    public class Merchant : NPC
    {
        MerchantWindow mw;
        ObservableCollection<Item> itemsForSale;
        public Merchant(ObservableCollection<Item> forsale)
        {
            mw = new MerchantWindow();
            itemsForSale = forsale;
            mw.forsaleBox.ItemsSource = itemsForSale;
            name = new List<string>() { "Clark", "Rosie", "Nick", "Carter", "Charlie", "Lauren", "Steve", "Isabel", "Connor", "Alex" }[Program.main.rand.Next(0, 10)];
        }
        public override void Interact()
        {
            mw.forsaleBox.ItemsSource = itemsForSale;
            mw.Show();
        }
    }
    public class Innkeeper : NPC
    {
        InnkeeperWindow ikw;
        public Innkeeper()
        {
            ikw = new InnkeeperWindow();
            name = new List<string>() { "Jean-Philippe", "Jacques-Cartier", "Bill", "Hank", "Ernie", "Margie", "Claude", "Gomie", "Chambers", "Takeshi" }[Program.main.rand.Next(0, 10)];
        }
        public override void Interact()
        {
            ikw.Show();
        }
    }
}
