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
        None,
        Stick,
        Longsword,
        Shortsword,
        Lance,
        Glove,
        Mace, 
        Dagger,
        Staff,
        Book,
        Bow,
        Gun
    }
    public class Item
    {
        public string desc, name;
        public Slot slot;
        public WeaponType wt;
        public int atkbuff, defbuff, spdbuff, intlbuff, tier, value;
        public Item()
        {
            wt = WeaponType.None;
        }
        public override string ToString()
        {
            return name;
        }
    }
    public class Stick : Item
    {
        public Stick()
        {
            slot = Slot.Primary;
            wt = WeaponType.Stick;
            name = "Stick";
            desc = "An unimpressive wooden stick. It isn't even sharp.";
            tier = 1;
            atkbuff = 1;
            value = 5;
        }
    }
}