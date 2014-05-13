using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Dice
    {
        /// <summary>
        /// Rolls numdice number of dice with numsides sides.
        /// </summary>
        /// <param name="numdice">the number of dice to be rolled.</param>
        /// <param name="numsides">the number of sides of the dice.</param>
        /// <returns>the result of the roll.</returns>
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
        /// <summary>
        /// Calculates the chance of missing based on target speed.
        /// </summary>
        /// <param name="spd">The speed of the target for the ability.</param>
        /// <returns>true if it hits, false if it misses.</returns>
        public bool misschance(int spd)
        {
            int misschance = roll(1, 101 - (spd * 3));
            if (misschance == 1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
