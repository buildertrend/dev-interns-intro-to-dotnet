using System;

namespace Blackjack
{
    public class Card
    {
        public int Value { get; set; }
        public Rank Name { get; set; }

        public override string ToString()
        {
            return Name + " --- " + Value;
        }
    }

    public enum Color
    {
        RED,
        BLACK
    }

    public enum Suit
    {
        HEARTS,
        DIAMONDS,
        SPADES,
        CLUBS
    }

    public enum Rank
    {
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        ACE
    }
}