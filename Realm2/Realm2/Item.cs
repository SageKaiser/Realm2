using System;
using System.Collections.Generic;

namespace Realm2
{
    public enum Slot
    {
        Primary,
        Secondary, 
        Armor,
        Accessory
    }
    public enum WeaponType
    {
        Longsword,
        Shortsword,
        Glove,
        Mace, 
        Dagger,
        Staff,
        Book
    }
    public class Item
    {
        public Slot slot;
        public WeaponType wt;
        public string name, desc;
        public int atkbuff, defbuff, spdbuff, intlbuff, tier, value;
        public float multiplier;
    }
}