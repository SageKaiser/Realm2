using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Enemy
    {
        public string name;
        public int hp, maxhp, atk, def, spd, intl, level;
        public List<string> abilities;
        public List<StatusEffect> effects = new List<StatusEffect>();
        public bool canAttack = true, canBeHit = true, canHeal = true;
        public virtual string Attack(Player player)
        {
            return "";
        }
        public void DropLoot()
        {
        }
    }
    public class Slime : Enemy
    {
        public Slime()
        {
            name = "Slime";
            level = Program.main.player.level;
            maxhp = 9 + level;
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
            string used = abilities[Program.main.rand.Next(0, abilities.Count)];
            switch(used)
            {
                case "Attack":
                    player.hp -= d.roll(1, atk) - player.def;
                    break;
                case "Sticky Smash":
                    player.hp -= d.roll(2, atk) - player.def;
                    break;
            }
            return used;
        }
    }
}
