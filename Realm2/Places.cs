using System;
using System.Collections.Generic;
using System.Linq;

namespace Realm2
{
    public class Place
    {
        public int q;

        public Map globals = new Map();

        public List<Enemy> enemylist = new List<Enemy>();

        public string name, desc;

        public bool is_npc_active = false;

        private Merchant m;

        public virtual Enemy getEnemyList()
        {
            List<Enemy> templist = new List<Enemy>();
            templist.Add(new Slime());
            if (Main.Player.level >= 3)
                templist.Add(new Goblin());
            if (Main.Player.level >= 5)
                templist.Add(new Bandit());
            if (Main.Player.level >= 10)
                templist.Add(new Drake());
            int randint = Main.rand.Next(1, templist.Count + 1);

            return templist[randint - 1];
        }
        public Place()
        {
            desc = "You are smack-dab in the middle of nowhere.";
            name = "Wilderness";
        }
        public void CreateRandomNPC()
        {
            desc = "You run into a travelling merchant. He has a few pieces of gear for sale.";
            List<Item> tempitemlist = new List<Item>(), forsale = new List<Item>();
            foreach (Item i in Main.MainItemList)
                if (i.tier >= 5)
                    tempitemlist.Add(i);
            for (int i = 0; i < 4; i++)
                forsale.Add(tempitemlist[Main.rand.Next(0, tempitemlist.Count - 1)]);
            m = new Merchant(Map.GetNPCName(), "Hello. Have a look at my wares,", "The merchant thanks you.", "You leave.", new Dictionary<char, Item>() { { '1', forsale[0] }, { '2', forsale[1] }, { '3', forsale[2] }, { '4', forsale[3] } });
        }
    }
    public class WKingdom : Place
    {

    }
    public class IllusionForest : Place
    {

    }
    public class Seaport : Place
    {

    }
    public class Riverwell : Place
    {

    }
    public class Valleyburg : Place
    {

    }
    public class NKingdom : Place
    {

    }
    public class MagicCity : Place
    {

    }
    public class SKingdom : Place
    {

    }
    public class NMtns : Place
    {

    }
    public class CentralKingdom : Place
    {

    }
    public class Newport : Place
    {

    }
    public class TwinPaths : Place
    {

    }
    public class FrozenFjords : Place
    {

    }
    public class Coaltown : Place
    {

    }
    public class Nomad : Place
    {

    }
    public class EKingdom : Place
    {

    }
    public class Ravenkeep : Place
    {

    }
}

