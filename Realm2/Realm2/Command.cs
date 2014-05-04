﻿using System;
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
}