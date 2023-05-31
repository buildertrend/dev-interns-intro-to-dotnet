using Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackUpdated
{
    class Card
    {
        public Suit Suit;
        public Face Face;
        
        public int getValue(int currentTotal)
        {
            int x = (int)this.Face switch{
                0 => 11,
                1 => 2,
                2 => 3,
                3 => 4,
                4 => 5,
                5 => 6,
                6 => 7,
                7 => 8,
                8 => 9,
                9 => 10,
                10 => 10,
                11 => 10,
                12 => 10,
                _ => 1
            };
            if (currentTotal > 10 && x == 11)
            {
                return 1;
            }
            return x;
        }
    }

}
