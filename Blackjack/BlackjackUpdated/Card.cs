 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            Value = GetValue(rank);
            Name = $"{rank} of {suit}";
        }

        private int GetValue(Rank rank)
        {
            return rank switch
            {
                Rank.Two => 2,
                Rank.Three => 3,
                Rank.Four => 4,
                Rank.Five => 5,
                Rank.Six => 6,
                Rank.Seven => 7,
                Rank.Eight => 8,
                Rank.Nine => 9,
                Rank.Ten => 10,
                Rank.Jack => 10,
                Rank.Queen => 10,
                Rank.King => 10,
                Rank.Ace => 11,
                _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
            };
        }

        private static Random random = new Random();

        public static Card DealCard(List<Card> deck)
        {
            int index = random.Next(deck.Count);
            Card card = deck[index];
            deck.RemoveAt(index);
            return card;
        }

        public static List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    deck.Add(new Card(suit, rank));
                }
            }
            return deck;
        }
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

}
