using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Player
    {
        private static Card[] cards = new Card[11];
        private static int total = 0;
        private static int cardCount = 0;

        public void addCard(Card newCard)
        {
            cards[cardCount] = newCard;
            total += newCard.Value;
            cardCount++;
        }

        public int getTotal()
        {
            return total;
        }

        public Card getCard(int index)
        {
            return cards[index];
        }

        public int getCardCount()
        {
            return cardCount;
        }
    }
}
