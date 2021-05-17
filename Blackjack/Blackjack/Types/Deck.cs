using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Types
{
    class Deck
    {
        public Stack<Card> Cards;

        public Deck()
        {
            Cards = new Stack<Card>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    Cards.Push(new Card() { Suit = suit, Rank = rank });
                }
            }
        }

        public void Shuffle()
        {
        
        }

        public Card Deal()
        {
            return Cards.Pop();
        }
    }
}
