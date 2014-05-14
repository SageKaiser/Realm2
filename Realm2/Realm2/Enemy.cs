using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Realm2
{
    public class Enemy
    {
        public string name;
        public int maxhp, atk, def, spd, intl, level;
        private int HP;
        public int hp
        {
            get { return HP; }
            set
            {
                HP = value;
                if (HP <= 0)
                {
                    Program.main.write(Program.main.player.name + " has defeated ", "Orange");
                    Program.main.write(name, "LawnGreen", true);
                    Program.main.write("!", "Orange", true);
                    Program.main.mainWindow.IsEnabled = true;
                    foreach (Window w in Program.main.mainWindow.OwnedWindows)
                        if (w is CombatWindow)
                            w.Close();
                }
            }
        }
        //abilities that the Enemy can use
        public List<string> abilities;
        //List<T> containing all of the current status effects
        public List<StatusEffect> effects = new List<StatusEffect>();
        public bool canAttack = true, canBeHit = true, canHeal = true;
        public virtual string Attack(Player player)
        {
            return "";
        }
        public void DropLoot()
        {
            //TODO: Implement the droploot function
        }
    }
    public class Slime : Enemy
    {
        public Slime()
        {
            name = "Slime";
            level = Math.Max(Program.random.Next(Program.main.player.level - 3, Program.main.player.level + 4), 1);
            maxhp = 2 + level;
            hp = maxhp;
            atk = 4 + level;
            spd = 2 + level;
            def = 3 + level;
            intl = 0 + level;
            abilities = new List<string>() { "Attack", "Sticky Smash" };
        }
        public override string Attack(Player player)
        {
            Dice d = new Dice();
            string used = abilities[Program.random.Next(0, abilities.Count)];
            switch(used)
            {
                case "Attack":
                    player.hp -= Math.Max(d.roll(1, atk) - player.def, 1);
                    break;
                case "Sticky Smash":
                    player.hp -= Math.Max(d.roll(2, atk) - player.def, 1);
                    break;
            }
            return used;
        }
    }
}
