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
        public int hp, maxhp, atk, def, spd, intl;
        public List<string> abilities;
        public List<StatusEffect> effects;
        public bool canAttack, canBeHit = true, canHeal = true;
        public void Attack(Player player)
        {
            return;
        }
    }
}
