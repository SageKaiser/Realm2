using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Player
    {
        public int mana, maxmana, maxhp, spd, basespeed, atk, baseattack, intl, baseintl, def, basedef, g, level, xp_next, reputation;
        public Dictionary<string, Func<bool>> commands;
        public Position position;
        public int hp
        {
            get { return hp; }
            set
            {
                if (hp >= 0)
                    Program.main.gm = Main.GameState.Dead;
                if (hp > maxhp)
                    hp = maxhp;
            }
        }
        public int xp
        {
            get { return xp_next; }
            set
            {
                if (xp <= xp_next)
                {
                    int xp_overlap = xp > xp_next ? xp - xp_next : 0;
                    xp = xp_overlap;
                    level++;
                    xp_next = level >= 10 ? 62 + (level - 10) * 7 : (level >= 5 ? 17 + (level - 5) * 3 : 17);
                    maxmana += 3;
                    mana = maxmana;
                    maxhp += 3 + pClass.hpperlvl;
                    hp += 3 + pClass.hpperlvl;
                    atk += 1 + pClass.atkperlvl;
                    def += 1 + pClass.defperlvl;
                    spd += 1 + pClass.spdperlvl;
                }
            }
        }
        public string name;
        public Lazy<Item> primary, secondary, armor, accessory;
        public ObservableCollection<Item> backpack;
        public PlayerClass pClass;
        public Race pRace;
        public bool canAttack, canBeHit = true, canHeal = true;
        public List<Ability> combatAbilities;
        public List<StatusEffect> effects;
        public Player()
        {
            level = 1;
            xp_next = level >= 10 ? 62 + (level - 10) * 7 : (level >= 5 ? 17 + (level - 5) * 3 : 17);
            backpack = new ObservableCollection<Item>();
            combatAbilities = new List<Ability>();
            effects = new List<StatusEffect>();
        }
        public void LearnAbility(Ability a)
        {
            if (!combatAbilities.Contains(a))
            {
                combatAbilities.Add(a);
                Program.main.write("You learned " + a.name + "!", "Aqua");
            }
            else
                Program.main.write("You have already learned that ability.", "Crimson");
        }
        public bool Purchase(int cost)
        {
            if (g >= cost)
            {
                g -= cost;
                return true;
            }
            else
                return false;
        }

        public bool Purchase(int cost, Item i)
        {
            if (g >= cost)
            {
                g -= cost;
                backpack.Add(i);
                return true;
            }
            else
                return false;
        }
    }
    public class PlayerClass
    {
        public int hpperlvl, atkperlvl, defperlvl, spdperlvl, intperlvl;
        public string desc, name;
        public WeaponType preferredType;
        public List<WeaponType> types = new List<WeaponType>();
        public Dictionary<Ability, int> abilities;

        public bool canEquipItem(Item w)
        {
            if (types.Contains(w.wt) || w.wt == WeaponType.Stick)
                return true;
            else
                return false;
        }
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
            preferredType = WeaponType.Longsword;
            desc = "Knights specialize in powerful Physical attacks and durability in battle.\r\nPreferred weapon type: " + preferredType;
            types = new List<WeaponType>() { WeaponType.Longsword, WeaponType.Shortsword, WeaponType.Mace };
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Gladiator : Knight
    {
        public Gladiator()
        {
            name = "Knight[Gladiator]";
            atkperlvl = 2;
            spdperlvl = 1;
            preferredType = WeaponType.Shortsword;
            desc = "The Gladiator discipline causes Knights to become faster and stronger.\r\nPreferred weapon type: " + preferredType;
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
            preferredType = WeaponType.Mace;
            desc = "Paladins are holy warriors that specialize in defense.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { { new Cleanse(), 7 } };
        }
    }
    public class Lancer : PlayerClass
    {
        public Lancer()
        {
            name = "Lancer";
            atkperlvl = 2;
            defperlvl = 1;
            preferredType = WeaponType.Lance;
            desc = "Lancers realy on their ability to make all-or-nothing attacks on their eneimies.\r\nPreferred weapon type: " + preferredType;
            types = new List<WeaponType>() { WeaponType.Longsword, WeaponType.Shortsword, WeaponType.Lance };
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Dragoon : Lancer
    {
        public Dragoon()
        {
            name = "Lancer[Dragoon]";
            spdperlvl = 3;
            desc = "The Dragoon path relies on lightning fast multi-strikes.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Valkyrie : Lancer
    {
        public Valkyrie()
        {
            name = "Lancer[Valkyrie]";
            atkperlvl = 3;
            desc = "The Valkyrie stirkes with fiery fury, rendering targets incapable of healing.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Brawler : PlayerClass
    {
        public Brawler()
        {
            name = "Brawler";
            atkperlvl = 1;
            defperlvl = 1;
            spdperlvl = 1;
            preferredType = WeaponType.Glove;
            types = new List<WeaponType>() { WeaponType.Glove };
            desc = "A Brawler relies on his speed and the power of his fists to battle.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Monk : Brawler
    {
        public Monk()
        {
            name = "Brawler[Monk]";
            atkperlvl = 4;
            defperlvl = 4;
            spdperlvl = 4;
            types = new List<WeaponType>();
            preferredType = WeaponType.None;
            desc = "A Monk uses on his bare fists to fight, relying in his incredible speed, defense, and power to defeat foes. A Monk cannot equip Primary or Secondary items.";
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Boxer : Brawler
    {
        public Boxer()
        {
            name = "Brawler[Boxer]";
            atkperlvl = 2;
            spdperlvl = 2;
            desc = "A Boxer uses the speed of his powerful punches to defeat his foes.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Mage : PlayerClass
    {
        public Mage()
        {
            name = "Mage";
            defperlvl = 1;
            intperlvl = 2;
            preferredType = WeaponType.Staff;
            types = new List<WeaponType>() { WeaponType.Staff, WeaponType.Book, WeaponType.Dagger };
            desc = "Mages use powerful spells to destory their enemies.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class ArcaneMage : Mage
    {
        public ArcaneMage()
        {
            name = "Mage[Arcane]";
            intperlvl = 3;
            atkperlvl = 1;
            preferredType = WeaponType.Book;
            desc = "Arcane Mages draw power from runes and curses, using knowledge from ancient spellweavers.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class ElementalMage : Mage
    {
        public ElementalMage()
        {
            name = "Mage[Elemental]";
            intperlvl = 3;
            spdperlvl = 1;
            desc = "Elemental Mages use power from nature and the elements to smite their foes.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Ranger : PlayerClass
    {
        public Ranger()
        {
            name = "Ranger";
            spdperlvl = 2;
            atkperlvl = 1;
            preferredType = WeaponType.Bow;
            types = new List<WeaponType>() { WeaponType.Bow, WeaponType.Gun, WeaponType.Dagger };
            desc = "Rangers like to deal damage from afar.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Archer : Ranger
    {
        public Archer()
        {
            name = "Ranger[Archer]";
            spdperlvl = 3;
            atkperlvl = 2;
            preferredType = WeaponType.Bow;
            desc = "Archers use bow and arrow to take down enemies from a long range, while also specialize in dealing critical damage.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Gunslinger : Ranger
    {
        public Gunslinger()
        {
            name = "Ranger[Gunslinger]";
            atkperlvl = 3;
            preferredType = WeaponType.Gun;
            desc = "Gunslingers specialize in the exlusive use of firearms, however they make up for it with the ability to dual-wield.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Rogue : PlayerClass
    {
        public Rogue()
        {
            name = "Rogue";
            atkperlvl = 1;
            spdperlvl = 2;
            preferredType = WeaponType.Shortsword;
            desc = "Rogues are quick swordsman who like to jump in and out of combat.\r\nPreferred weapon type: " + preferredType;
            types = new List<WeaponType>() { WeaponType.Shortsword, WeaponType.Dagger };
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Thief : Rogue
    {
        public Thief()
        {
            name = "Rogue[Thief]";
            spdperlvl = 4;
            preferredType = WeaponType.Dagger;
            types = new List<WeaponType>() { WeaponType.Dagger };
            desc = "Thieves rely solely on speed to strike quickly or escape.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Scout : Rogue
    {
        public Scout()
        {
            name = "Rogue[Scout]";
            spdperlvl = 2;
            atkperlvl = 2;
            desc = "Scouts rely on mobility and rapid damge to execute foes.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class BladeDancer : PlayerClass
    {
        public BladeDancer()
        {
            name = "Blade Dancer";
            atkperlvl = 2;
            spdperlvl = 1;
            preferredType = WeaponType.Longsword;
            types = new List<WeaponType>() { WeaponType.Longsword, WeaponType.Shortsword, WeaponType.Dagger };
            desc = "Blade Dancers combine agility with the strength of their magically augmented strikes to eliminate foes.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Hexblade : BladeDancer
    {
        public Hexblade()
        {
            name = "Blade Dancer[Hexblade]";
            intperlvl = 2;
            preferredType = WeaponType.Shortsword;
            desc = "Hexblades charge the weapon with magical power and use basic arcane magic.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Duelist : BladeDancer
    {
        public Duelist()
        {
            name = "Blade Dancer[Duelist]";
            atkperlvl = 3;
            spdperlvl = 2;
            preferredType = WeaponType.Longsword;
            desc = "Duelists thrive in battle. The longer the fight goes on, thr stronger the duelist becomes.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Assassin : PlayerClass
    {
        public Assassin()
        {
            name = "Assassin";
            atkperlvl = 3;
            preferredType = WeaponType.Dagger;
            types = new List<WeaponType>() { WeaponType.Dagger, WeaponType.Glove, WeaponType.Shortsword };
            desc = "Assassins use incredible bursts of damage to dispatch their targets quickly.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Ninja : Assassin
    {
        public Ninja()
        {
            name = "Assassin[Ninja]";
            spdperlvl = 2;
            preferredType = WeaponType.Shortsword;
            desc = "Ninjas hail from the mystical orient, using deception for most purposes, however in a pinch they can be very effective at fighting as well.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class RiftWalker : Assassin
    {
        public RiftWalker()
        {
            name = "Assassin[Ninja]";
            defperlvl = 1;
            atkperlvl = 1;
            desc = "An assassin who has developed the incredible ability to traverse the void, allowing for breif teleportation and more durability.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class DreadKnight : PlayerClass
    {
        public DreadKnight()
        {
            name = "Dread Knight";
            atkperlvl = 1;
            defperlvl = 1;
            intperlvl = 1;
            preferredType = WeaponType.Mace;
            types = new List<WeaponType>() { WeaponType.Longsword, WeaponType.Shortsword, WeaponType.Mace };
            desc = "Dread Knights draw ther power from the pain and suffering of others, growing stronger as their enemies get weaker.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Necromancer : DreadKnight
    {
        public Necromancer()
        {
            name = "Dread Knight[Necromancer]";
            atkperlvl = 0;
            intperlvl = 3;
            preferredType = WeaponType.Book;
            types = new List<WeaponType>() { WeaponType.Book, WeaponType.Staff };
            desc = "Necromancers cast spells involving death, and use powerful reanimation spells to bring back past foes to fight for them.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    public class Duskblade : DreadKnight
    {
        public Duskblade()
        {
            name = "Dreadknight[Duskblade]";
            atkperlvl = 2;
            defperlvl = 2;
            preferredType = WeaponType.Longsword;
            desc = "Duskblades grow stronger each time they kill an enemy, relying on the shadows to make kills.\r\nPreferred weapon type: " + preferredType;
            abilities = new Dictionary<Ability, int>();
        }
    }
    public class Jester : PlayerClass
    {
        public Jester()
        {
            name = "Jester";
            atkperlvl = 1;
            defperlvl = 1;
            spdperlvl = 1;
            intperlvl = 1;
            hpperlvl = 1;
            preferredType = WeaponType.None;
            types = new List<WeaponType>() { WeaponType.None, WeaponType.Book, WeaponType.Bow, WeaponType.Dagger, WeaponType.Glove, WeaponType.Gun, WeaponType.Lance, WeaponType.Longsword, WeaponType.Mace, WeaponType.Shortsword, WeaponType.Staff };
            desc = "Jesters are perhaps the most enigmatic of classes. None but Jesters really understand the implications of being one.";
            abilities = new Dictionary<Ability, int>() { };
        }
    }
    #endregion
}
