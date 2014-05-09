using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
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
        public bool Execute(NPC npc)
        {
            npc.Interact();
            return true;
        }
        public bool Execute(Library l)
        {
            LibraryWindow lw = new LibraryWindow(l);
            lw.Show();
            return true;
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
            if (Program.main.player.position.y == prevposy && Program.main.player.position.x == prevposx)
                return false;
            else
                return true;
        }
    }
    public class Ability : Command
    {
        public enum type
        {
            Physical,
            Magical
        }
        public type Type;
        public int manacost;
        public override bool Execute(object target)
        {
            return false;
        }
    }
    public class Cleanse : Ability
    {
        public Cleanse()
        {
            Type = type.Magical;
            manacost = 0;
        }
        public override bool Execute(object target)
        {
            Player p = target as Player;
            for (int i = p.effects.Count; i > -1; i-- )
                if (p.effects[i].isNegative)
                    p.effects.RemoveAt(i);
            return true;
        }
    }
}
