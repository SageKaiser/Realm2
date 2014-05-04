using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class StatusEffect
    {
        public enum CurseType
        {
            Attack,
            Defense,
            Speed,
        }
        public Dice d;
        public int turns;
        public bool isPlayer;
        public Player p;
        public Enemy e;
        public CombatWindow window;

        public virtual void ApplyEffect(object Target)
        {
            turns--;
            p = Target as Player;
            if (p != null)
                isPlayer = true;
            else
            {
                isPlayer = false;
                e = Target as Enemy;
            }
        }
    }
    public class OnFire : StatusEffect
    {
        int power;
        public OnFire(int Turns, int Power, CombatWindow cw)
        {
            turns = Turns;
            power = Power;
            window = cw;
        }
        public override void ApplyEffect(object Target)
        {
            base.ApplyEffect(Target);
            int damage = d.roll(power, 3);
            if (isPlayer)
            {
                p.hp -= damage;
                window.combatText.AppendText("You take " + damage + " fire damage", "Crimson");
            }
            else
            {
                e.hp -= damage;
                window.combatText.AppendText(e.name + " takes " + damage + " fire damage", "Aqua");
            }
        }
    }
    public class Stunned : StatusEffect
    {
        public Stunned(int Turns, CombatWindow cw)
        {
            turns = Turns;
            window = cw;
        }
        public override void ApplyEffect(object Target)
        {
            base.ApplyEffect(Target);
            if (isPlayer)
            {
                p.canAttack = false;
                window.combatText.AppendText("You are stunned!", "Crimson");
            }
            else
            {
                e.canAttack = false;
                window.combatText.AppendText(e.name + " is stunned!", "Aqua");
            }
        }
    }
    public class Curse : StatusEffect
    {
        CurseType type;
        int strength;
        public Curse(int Turns, CombatWindow cw, CurseType Type, int degree)
        {
            turns = Turns;
            window = cw;
            type = Type;
            strength = degree;
        }
        public override void ApplyEffect(object Target)
        {
            base.ApplyEffect(Target);
            if (isPlayer)
            {
                switch(type)
                {
                    case CurseType.Attack:
                        p.atk -= strength;
                        break;
                    case CurseType.Defense:
                        p.def -= strength;
                        break;
                    case CurseType.Speed:
                        p.spd -= strength;
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case CurseType.Attack:
                        e.atk -= strength;
                        break;
                    case CurseType.Defense:
                        e.def -= strength;
                        break;
                    case CurseType.Speed:
                        e.spd -= strength;
                        break;
                }
            }
        }
    }
}
