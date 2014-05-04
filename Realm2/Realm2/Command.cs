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
        public List<string> synonyms;

        public virtual bool Execute(object target)
        {
            return false;
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
