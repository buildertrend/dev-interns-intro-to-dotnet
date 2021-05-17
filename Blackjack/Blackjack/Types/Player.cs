using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.Types
{
    public enum PlayerType
    {
        Dealer,
        Human,
    }

    class Player
    {
        public string Name;
        public PlayerType Type;
        public List<Card> Hand;

        public Player()
        {
            Hand = new List<Card>();
        }

        public string DisplayHand()
        {
            return $"{String.Join(" + ", Hand.Select(card => card.Rank).ToArray())} = {ScoreHand()}";
        }

        public void DealCard(Card card)
        {
            Hand.Add(card);
        }

        public int ScoreHand()
        {
            return Hand.Aggregate(0, (total, card) => total += Card.GetCardValue(card));
        }
    }
}
