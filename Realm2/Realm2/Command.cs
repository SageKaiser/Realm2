using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    //base Command class
    public class Command
    {
        public string name;

        public virtual bool Execute(object target)
        {
            return false;
        }
    }
    public class interact : Command
    {
        public interact()
        {
            name = "interact";
        }
    }
    public class go : Command
    {
        public go()
        {
            name = "go";
        }
        public bool Execute(Direction d)
        {
            int prevposx = Program.main.player.position.x;
            int prevposy = Program.main.player.position.y;
            switch(d)
            {
                case Direction.north:
                    Program.main.player.position.y += 1;
                    break;
                case Direction.south:
                    Program.main.player.position.y -= 1;
                    break;
                case Direction.east:
                    Program.main.player.position.x += 1;
                    break;
                case Direction.west:
                    Program.main.player.position.x -= 1;
                    break;
            }
            //if the player went nowhere, return false
            if (Program.main.player.position.y == prevposy && Program.main.player.position.x == prevposx)
                return false;
            else
                return true;
        }
    }
    public enum type
    {
        Physical,
        Magical
    }
    public enum targetType
    {
        Self,
        Enemy
    }
    public class Ability
    {
        public type Type;
        public targetType TargetType;
        public Dice d = new Dice();
        public string name;
        public int manacost;
        public virtual bool Execute(object target)
        {
            return false;
        }
        public override string ToString()
        {
            return name;
        }
    }
    public class Attack : Ability
    {
        public Attack()
        {
            name = "Basic Attack";
            Type = type.Physical;
            TargetType = targetType.Enemy;
            manacost = 0;
        }
        public override bool Execute(object target)
        {
            ((Enemy)target).hp -= Math.Max((d.roll(1, Program.main.player.atk) + 4) - ((Enemy)target).def, 1);
            return true;
        }
    }
    public class EnergyBlast : Ability
    {
        public EnergyBlast()
        {
            name = "Energy Blast";
            Type = type.Magical;
            TargetType = targetType.Enemy;
            manacost = 1;
        }
        public override bool Execute(object target)
        {
            ((Enemy)target).hp -= Math.Max((d.roll(1, Program.main.player.intl) + 2) - (((Enemy)target).def / 2), 1);
            return true;
        }
    }
    public class Cleanse : Ability
    {
        public Cleanse()
        {
            name = "Cleanse";
            Type = type.Magical;
            TargetType = targetType.Self;
            manacost = 0;
        }
        public override bool Execute(object target)
        {
            Player p = target as Player;
            //remove all debuffs from the Player
            for (int i = p.effects.Count; i > -1; i-- )
                if (p.effects[i].isNegative)
                    p.effects.RemoveAt(i);
            return true;
        }
    }
}
