using System;
using System.Collections.Generic;
using System.Linq;
using static Blackjack.Card;

namespace Blackjack
{
	/// <summary>
	/// Contains a list that stores a set of Card.
	/// </summary>
	public class Deck
	{
		public List<Card> cardDeck = new List<Card>();

		public List<Card> CreateDeck()
        {
			foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
				foreach (Rank rank in Enum.GetValues(typeof(Rank)))
				{
					cardDeck.Add(new Card() { cardSuit = suit, cardRank = rank });
				}
			}
            return cardDeck;
        }

		public List<Card> ShuffleDeck(List<Card> cardDeck)
        {
			return cardDeck.OrderBy(x => Guid.NewGuid()).ToList();
        }

		public Card Deal()
        {
			int last = cardDeck.Count;
			return cardDeck.RemoveAt(last);
        }
	}
}
