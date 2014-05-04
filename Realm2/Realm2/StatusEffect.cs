using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public bool isPlayer, isNegative = true;
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
        public virtual void Expire() { }
    }
    public class OnFire : StatusEffect
    {
        int power;
        public OnFire(int Turns, int Power, CombatWindow cw)
        {
            turns = Turns;
            power = Power;
            window = cw;
            if (isPlayer)
                cw.combatText.AppendText(p.name + " has been set ablaze!", "Crimson");
            else
                cw.combatText.AppendText(e.name + " has been set ablaze!", "Aqua");
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
            if (isPlayer)
                cw.combatText.AppendText(p.name + " was stunned for " + turns + " turns!", "Crimson");
            else
                cw.combatText.AppendText(e.name + " was stunned for " + turns + " turns!", "Aqua");
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
        public override void Expire()
        {
            if (isPlayer)
                p.canAttack = true;
            else
                e.canAttack = true;
        }
    }
    public class Cursed : StatusEffect
    {
        CurseType type;
        int strength;
        public Cursed(int Turns, CombatWindow cw, CurseType Type, int degree)
        {
            turns = Turns;
            window = cw;
            type = Type;
            strength = degree;
            if (isPlayer)
                cw.combatText.AppendText(p.name + " has placed a(n) " + Type + " Curse on " + e.name + " for " + turns + " turns!", "Crimson");
            else
                cw.combatText.AppendText(e.name + " has placed a(n) " + Type + " Curse on " + p.name + " for " + turns + " turns!", "Aqua");
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
    public class Untargetable : StatusEffect
    {
        public Untargetable(CombatWindow cw)
        {
            isNegative = false;
            window = cw;
            turns = 1;
            if (isPlayer)
                cw.combatText.AppendText(p.name + " is now untargetable.", "Aqua");
            else
                cw.combatText.AppendText(e.name + " is now untargetable.", "Crimson");
        }
        public override void ApplyEffect(object Target)
        {
            base.ApplyEffect(Target);
            if (isPlayer)
                p.canBeHit = false;
            else
                e.canBeHit = false;
        }
        public override void Expire()
        {
            if (isPlayer)
                p.canBeHit = true;
            else
                e.canBeHit = true;
        }
    }
    public class HealBlock : StatusEffect
    {
        public HealBlock(int Turns, CombatWindow cw)
        {
            turns = Turns;
            window = cw;
            if (isPlayer)
                cw.combatText.AppendText(p.name + " cannot heal for " + turns + " turns!", "Crimson");
            else
                cw.combatText.AppendText(e.name + " cannot heal for " + turns + " turns!", "Aqua");
        }
        public override void ApplyEffect(object Target)
        {
            base.ApplyEffect(Target);
            if (isPlayer)
                p.canHeal = false;
            else
                e.canHeal = false;
        }
        public override void Expire()
        {
            if (isPlayer)
                p.canHeal = true;
            else
                e.canHeal = true;
        }
    }
}
