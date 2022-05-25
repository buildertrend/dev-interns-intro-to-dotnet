using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Card
    {
        public int Value;
        public string Name;
        public string Suit;

        public String getName()
        {
            if (Suit.Equals("notValid"))
            {
                return Name;
            }
            return Name + String.Format(" of {0}", Suit);
        }
    }
}
