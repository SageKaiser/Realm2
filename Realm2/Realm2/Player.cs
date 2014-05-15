using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Realm2
{
    public class Player
    {
        public int mana, maxmana, maxhp, spd, basespeed, atk, baseattack, intl, baseintl, def, basedef, g, level, xp_next, reputation;
        public Position position;
        private int HP, XP;
        public int hp
        {
            get { return HP; }
            set
            {
                //check if the Player is dead. If he is, do dead-y things
                HP = value;
                if (HP <= 0)
                {
                    //TODO: revenant stuff
                    Program.main.gm = GameState.Dead;
                    Program.main.write("You have died. Press enter to exit.", "Red");
                    Program.main.focusWindow();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (!(window is MainWindow))
                            window.Close();
                    }
                }
                //if the Player's current hp is greater than the max hp, set the hp to maxhp
                if (HP > maxhp)
                    HP = maxhp;
            }
        }
        public int xp
        {
            get { return XP; }
            set
            {
                if (XP <= xp_next)
                {
                    //if you have more xp than is required to level up, save that value
                    int xp_overlap = XP > xp_next ? XP - xp_next : 0;
                    //set the xp to the overlap
                    XP = xp_overlap;
                    level++;
                    //level-up code.
                    xp_next = level >= 10 ? 62 + (level - 10) * 7 : (level >= 5 ? 17 + (level - 5) * 3 : 17);
                    //add three mana every level and restore mana on levelup
                    maxmana += 3;
                    mana = maxmana;
                    //boost all stats
                    maxhp += 3 + pClass.hpperlvl;
                    hp += 3 + pClass.hpperlvl;
                    atk += 1 + pClass.atkperlvl;
                    def += 1 + pClass.defperlvl;
                    spd += 1 + pClass.spdperlvl;
                }
            }
        }
        public string name;
        //Lazy<Item> is so that we don't have to make each one a new Item() and then check if they're null when they're referenced
        public Lazy<Item> primary, secondary, armor, accessory;
        public ObservableCollection<Item> backpack;
        //class and race
        public PlayerClass pClass;
        public Race pRace;
        public bool canAttack = true, canBeHit = true, canHeal = true;
        public List<Ability> combatAbilities;
        public List<StatusEffect> effects;
        public Player()
        {
            level = 1;
            //set initial xp required
            xp_next = 17;
            //set initial mana
            maxmana = 10;
            mana = maxmana;
            backpack = new ObservableCollection<Item>();
            position = new Position();
            //initial ability
            combatAbilities = new List<Ability>() { new Attack() };
            effects = new List<StatusEffect>();
        }
        public bool LearnAbility(Ability a)
        {
            //if the Player doesn't already know that ability, learn it
            if (!combatAbilities.Contains(a))
            {
                combatAbilities.Add(a);
                Program.main.write("You learned " + a.name + "!", "Aqua");
                return true;
            }
            else
            {
                Program.main.write("You have already learned that ability.", "Red");
                return false;
            }
        }
        public bool Purchase(int cost)
        {
            //check if the Player can afford it, if he can, subtract that much gold
            if (g >= cost)
            {
                g -= cost;
                return true;
            }
            else
                return false;
        }

        public bool Purchase(Item i)
        {
            //same as previous, but for buying items instead of undescribed things
            if (g >= i.value)
            {
                g -= i.value;
                backpack.Add(i);
                return true;
            }
            else
                return false;
        }
    }
}
