using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Player
    {
        enum Class
        {
            Knight,
            Paladin,
            Monk,
            ElementalMage,
            ArcaneMage,
            VoidMage,
            Rogue,
            BladeDancer,
            Ninja,
            Dreadknight
        }
        enum Race
        {
            Human,
            Elf,
            Dwarf,
            Orc, 
            Lycanthrope,
            HalfDragon,
            Halfling,
            Djinn,
            Vampire,
            Demon
        }

        public int hp, maxhp, spd, atk, intl, def, g, level, xp, xp_next, fire, guard, reputation;

        public string pclass, race, name;

        public Item primary = new Item(), secondary = new Item(), armor = new Item(), accessory = new Item();

        public List<Item> backpack;

        public bool on_fire = false, cursed = false, stunned = false, guarded = false, blinded = false, phased = false;
    }
}
