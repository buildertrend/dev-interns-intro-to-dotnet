using BlackjackUpdated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackUpdated
{
    internal class Player
    {
        public Card[] cards = new Card[11];
        public int total = 0;
        public int cardCount = 1; // do we really need this?
        public bool busted = false;
    }
}
