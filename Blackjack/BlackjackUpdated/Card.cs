using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Card
    {
        public string Name;
        public int Value;
        public string Suit;

        internal Card(string Name, int Value, string Suit) {
            this.Name = Name;
            this.Value = Value;
            this.Suit = Suit;

        }

    }
}
