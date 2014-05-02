using System;
using System.Collections.Generic;

namespace Realm2
{
    public class Item
    {
        public string name, desc;
        public int atkbuff, defbuff, spdbuff, intlbuff, tier, slot, value;
        public float multiplier;
        //1 for primary 2 for secondary 3 for armor 4 for Accessory
        public Item()
        {
            atkbuff = 0;
            defbuff = 0;
            spdbuff = 0;
            intlbuff = 0;
            multiplier = 1;
        }
    }
}