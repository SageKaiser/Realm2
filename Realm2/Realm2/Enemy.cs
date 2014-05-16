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
                    DropLoot();
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
        public virtual int Attack(Player player, out string used)
        {
            used =  "";
            return 0;
        }
        public void DropLoot()
        {
            Program.main.player.xp += Program.random.Next(1, 4 + level);
            Program.main.player.xp += Program.random.Next(3, 6 + level);
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
        public override int Attack(Player player, out string used)
        {
            Dice d = new Dice();
            int dmg = 0;
            used = abilities[Program.random.Next(0, abilities.Count)];
            switch(used)
            {
                case "Attack":
                    dmg = Math.Max(d.roll(1, atk) - player.def, 1);
                    break;
                case "Sticky Smash":
                    dmg = Math.Max(d.roll(2, atk) - player.def, 1);
                    break;
            }
            return dmg;
        }
    }
}
