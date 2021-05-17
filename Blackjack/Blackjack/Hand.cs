using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Hand
    {
        private List<Card> hand;
        
        public Hand()
        {
            hand = new List<Card>();
        }

        public Hand(int numCards, Deck deck)
        {
            hand = new List<Card>();

            // Adds numCards amount of random cards
            addCard(deck.DealCard());
            addCard(deck.DealCard());
        }

        public void addCard(Card card)
        {
            hand.Add(card);
        }

        public Card getCard(int index)
        {
            return hand[index];
        }

        public int getTotal()
        {
            int total = 0;
            foreach (Card card in hand) {
                total += card.Value;
            }
            return total;
        }

        public int getHandSize()
        {
            return hand.Count;
        }
    }
}
