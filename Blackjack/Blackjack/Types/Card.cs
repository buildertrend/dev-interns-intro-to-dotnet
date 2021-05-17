using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Types
{

    public enum CardSuit
    {
        Diamond,
        Club,
        Heart,
        Spade
    }

    public enum CardRank
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

    class Card
    {

        public CardRank Rank;
        public CardSuit Suit;

        public static int GetCardValue(Card card)
        {
            return card.Rank switch
            {
                CardRank.Two =>  2,
                CardRank.Three => 3,
                CardRank.Four => 4,
                CardRank.Five => 5,
                CardRank.Six => 6,
                CardRank.Seven => 7,
                CardRank.Eight => 8,
                CardRank.Nine => 9,
                CardRank.Ten => 10,
                CardRank.Jack => 10,
                CardRank.Queen => 10,
                CardRank.King => 10,
                CardRank.Ace => 11, //TODO handle ace as 1?
                _ => 2, //TODO better default?
            };
        }
    }
}

