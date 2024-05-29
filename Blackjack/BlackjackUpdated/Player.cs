using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Player
    {
        readonly Card[] playerCards;
        int playerTotal;
        int playerCardCount;

        internal Player(Card card1, Card card2) {
            playerCards = new Card[11];
            playerCards[0] = card1;
            playerCards[1] = card2;
            playerTotal = card1.Value + card2.Value;
            playerCardCount = 2;
        }

        public void SetPlayerTotal(int cardValue) 
        {
            playerTotal += cardValue;
        }

        public int GetPlayerTotal() 
        {
            return playerTotal;
        }

        public void incrementPlayerCardCount() 
        {
            playerCardCount++;
        }

        public int GetPlayerCardCount() 
        {
            return playerCardCount;
        }

        public Card[] GetPlayerCardsList() 
        {
            return playerCards;
        }



    }
}
