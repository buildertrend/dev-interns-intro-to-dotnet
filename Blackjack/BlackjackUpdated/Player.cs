using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public enum State 
        {
            NEW,
            DECISION,
            DONE,
            BUST,
            BLACKJACK
        }
    internal class Player
    {
        public State state = 0;
        public int total = 0;
        public List<Card> playerCards = new List<Card>();
        public int cardAmount = 0;

        
    }
}
