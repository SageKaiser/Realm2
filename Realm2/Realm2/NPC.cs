using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    /// <summary>
    /// An interface containing the core of an NPC
    /// </summary>
    public interface INPC
    {
        void Interact();
    }
    public class Merchant : INPC
    {
        MerchantWindow mw;
        //the items the Merchant has for sale
        ObservableCollection<Item> itemsForSale;
        /// <summary>
        /// Creates a new Merchant
        /// </summary>
        /// <param name="forsale">the Items the Merchant is selling.</param>
        public Merchant(ObservableCollection<Item> forsale)
        {
            mw = new MerchantWindow();
            //set the merchantName label to a random name
            mw.merchantName.Content += ToString();
            itemsForSale = forsale;
            mw.forsaleBox.ItemsSource = itemsForSale;
        }
        public override string ToString()
        {
            //get a random name
            return new List<string>() { "Clark", "Rosie", "Nick", "Carter", "Charlie", "Lauren", "Steve", "Isabel", "Connor", "Alex" }[Program.random.Next(0, 10)];
        }
        public void Interact()
        {
            //bind the ListBox with the Items for sale to the class member
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
            //get a random name
            return new List<string>() { "Jean-Philippe", "Jacques-Cartier", "Bill", "Hank", "Ernie", "Margie", "Claude", "Gomie", "Chambers", "Takeshi" }[Program.random.Next(0, 10)];
        }
        public void Interact()
        {
            ikw.Show();
        }
    }
}
