using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Player
    {
        public string name;
        public List<Card> cards;

        public int getTotal()
        {
            int sum = 0;
            foreach (Card card in cards){
                sum += card.Value;   
            }
            return sum;
        }
    }
}
