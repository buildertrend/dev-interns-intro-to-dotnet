using BlackjackUpdated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlackjackUpdated.Card;

namespace BlackjackUpdated
{
    internal class Shoe
    {
        public List<Card> Cards = new List<Card>();

        public Shoe(int decks)
        {
            for (int deck = 0; deck < decks; deck++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    for (int num = 1; num < 14; num++)
                    {
                        Card card = GetCardValue(num);
                        card.Suit = suit;
                        Cards.Add(card);
                    }
                }
            }
            Cards.Shuffle();
        }

        public Card Draw()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }

    static class MyExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
