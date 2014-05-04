using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Dice
    {
        public int roll(int numdice, int numsides)
        {
            int total = 0;
            Random rand = new Random();
            for (int i = 0; i < numdice; i++)
            {
                total += rand.Next(1, Math.Abs(numsides + 1));
            }
            return total;
        }
        public bool misschance(int spd)
        {
            int misschance = Dice.roll(1, 101 - (spd * 3));
            if (misschance == 1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
