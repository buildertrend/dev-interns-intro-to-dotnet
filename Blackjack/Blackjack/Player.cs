using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Player
    {

        public string Name { get; set; }
        public Hand Hand { get; set; }

        public Player()
        {
            Hand = new Hand();
        }
    }
}
