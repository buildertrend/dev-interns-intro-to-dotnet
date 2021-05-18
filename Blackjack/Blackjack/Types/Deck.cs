using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.Types
{
    public class Deck
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

            Shuffle();
        }

        public void Shuffle()
        {
            Random random = new Random();
            Cards = new Stack<Card>(Cards.OrderBy(c => random.Next()));
        }

        public Card Deal()
        {
            return Cards.Pop();
        }
    }
}
