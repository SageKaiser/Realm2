using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Player
    {
        //enum Class
        //{
        //    Knight,
        //    Lancer,
        //    Monk,
        //    Mage,
        //    Jester,
        //    Ranger,
        //    Rogue,
        //    BladeDancer,
        //    Ninja,
        //    Dreadknight
        //}

        public int mana, maxmana, hp, maxhp, spd, basespeed, atk, baseattack, intl, baseintl, def, basedef, g, level, xp, xp_next, reputation;
        public string name;
        public Item primary = new Item(), secondary = new Item(), armor = new Item(), accessory = new Item();
        public List<Item> backpack;
        public PlayerClass pClass;
        public Race pRace;
        public bool canAttack, canBeHit = true, canHeal = true;
        public List<Ability> combatAbilities;
        public List<StatusEffect> effects;
    }
    public class PlayerClass
    {
        public int hpperlvl, atkperlvl, defperlvl, spdperlvl, intperlvl;

        public string desc, name;

        public Dictionary<Ability, int> abilities;

        public Tuple<PlayerClass, PlayerClass> descdendants;
    }
    public class Race
    {
        public int hp_init, atk_init, def_init, spd_init, int_init;

        public string desc, racialTrait;

        Ability racialAbility;
    }
    #region Races
    public class Human : Race
    {
        public Human()
        {
            hp_init = 2;
            atk_init = 2;
            def_init = 2;
            spd_init = 2;
            int_init = 2;
            desc = "Humans are a versatile people that rely on the ability to be good in many aspects. Humans are also valued for their ability to pull through even in the toughest situations.";
            racialTrait = "Spark of Hope: Humans get a huge stat buff against enemies stronger than they are."; 
        }
    }
    public class Elf : Race
    {
        public Elf()
        {
            spd_init = 5;
            int_init = 5;
            desc = "Elf make up for their lack of strength in wisdom. Elves are quick learners and are usually adept at scholarly endeavours.";
            racialTrait = "Keen Shot: Elves never miss.";
        }
    }
    public class Dwarf : Race
    {
        public Dwarf()
        {
            hp_init = 5;
            def_init = 5;
            desc = "Dwarves are a people much smaller than humans. Known for their reliability and incredible metalworking, they dwell in mountains where resources and gold are abundant.";
            racialTrait = "Reinforced Armor: Dwarves receive 15% less damage from all sources. At critical HP, this reduction increases to 50%";
        }
    }
    public class Orc : Race
    {
        public Orc()
        {
            hp_init = 6;
            atk_init = 4;
            desc = "Orcs are a hardy race of creatures capable of incredible feats of strength.";
            racialTrait = "Enrage: Orcs receive bonus Attack points the more Health they are missing.";
        }
    }
    public class Lycanthrope : Race
    {
        public Lycanthrope()
        {
            atk_init = 5;
            spd_init = 5;
            desc = "Lycanthropes are berserkers that have always been associated with the phases of the moon. They are said to obtain wolf-like traits when the moon is at its fullest.";
            racialTrait = "Full Moon: Lycanthropes take their wolf form gaining additional speed and attack.\r\nNew Moon: Lycanthropes return to their human form.";
        }
    }
    public class Halfdragon : Race
    {
       public Halfdragon()
        {
           def_init = 7;
           atk_init = 3;
           desc = "Halfdragons are a rare hybrid between human and dragon. Half dragons have properties from both species.";
           racialTrait = "Burning Scales: Physical attacks ignite enemies.";
        }
    }
    public class Revenant : Race
    {
        public Revenant()
        {
            hp_init = -2;
            spd_init = 4;
            desc = "Born from an unholy pact between a human and a demon, revenants are aberrations that should not exist in this world.";
            racialTrait = "Death Defied: Once per battle, Revenants revive themselves with half HP and three turns to exact revenge.";
        } 
    } 
    public class Djinn : Race 
    {
        public Djinn()
        {
            int_init = 10;
            desc = "Djinn are creatures born of the desire for power and arcane magic. The first Djinn are milennia old, these beings were made from the corruption of magic on the human soul.";
            racialTrait = "Power Tap: Magic attacks lower enemy defense.";
        }
    }
    public class Vampire : Race
    {
        public Vampire()
        {
            hp_init = 4;
            atk_init = 3;
            spd_init = 3;
            desc = "Vampires are naturally immortal creatures that survive off of the life force of other beings.";
            racialTrait = "Life Drain: All attacks heal Vampires for a fraction of the damage dealt";
        }
    }
    public class Demon : Race
    {
        public Demon()
        {
            hp_init = -1;
            def_init = -1;
            atk_init = 8;
            spd_init = 4;
            desc = "Demons are evil and twisted human souls escaped from the clutches of Hell. They have no recollection of what it means to be human, and as a result, not a shred of humanity remains with them.";
            racialTrait = "Subjugate: Demons deal 33% increased to damage to targets with negative status effects.";
        }
    }
    #endregion
    #region Classes
    public class Knight : PlayerClass    
    {
        public Knight()
        {
            name = "Knight";
            hpperlvl = 1;
            atkperlvl = 1;
            defperlvl = 1;
            desc = "Knights specialize in powerful Physical attacks and durability in battle.";
            abilities = new Dictionary<Ability, int>() { };
            descdendants = new Tuple<PlayerClass, PlayerClass>(new Gladiator(), new Paladin());
        }
        public class Gladiator : Knight
        {
            public Gladiator()
            {
                name = "Knight[Gladiator]";
                atkperlvl = 2;
                spdperlvl = 1;
                desc = "The Gladiator discipline causes Knights to become faster and stronger.";
                abilities = new Dictionary<Ability, int>() { };
            }
        }
        public class Paladin : Knight
        {
            public Paladin()
            {
                name = "Knight[Paladin]";
                hpperlvl = 2;
                defperlvl = 2;
                desc = "Paladins are holy warriors that specialize in defense.";
                abilities = new Dictionary<Ability, int>() { {new Cleanse(), 7} };
            }
        }
    }
    public class Lancer : PlayerClass
    {
        public Lancer()
        {
            name = "Lancer";
            atkperlvl = 2;
            defperlvl = 1;
            desc = "Lancers realy on their ability to make all-or-nothing attacks on their eneimies.";
            abilities = new Dictionary<Ability, int>() { };
        }
        public class Dragoon : Lancer
        {
            public Dragoon()
            {
                name = "Lancer[Dragoon]";
                spdperlvl = 3;
                desc = "The Dragoon path relies on lightning fast multi-strikes.";
                abilities = new Dictionary<Ability, int>() { };
            }
        }
        public class Valkyrie : Lancer
        {
            public Valkyrie()
            {
                name = "Lancer[Valkyrie]";
                atkperlvl = 3;
                desc = "The Valkyrie stirkes with fiery fury, rendering targets incapable of healing.";
                abilities = new Dictionary<Ability, int>() { };
            }
        }
    }
    public class Brawler : PlayerClass
    {

    }
    #endregion
}
