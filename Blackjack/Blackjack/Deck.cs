using Blackjack;
using System;
using System.Collections.Generic;

namespace Blackjack
{
	public class Deck
	{
		private static Random cardRandomizer = new Random();

		private List<Card> deck;
		public Deck()
		{
			RefillDeck();
		}

		public void RefillDeck()
        {
			deck = new List<Card>();
			for (int i = 0; i < 4; i++)
			{
				int value = 2;
				deck.Add(new Card { Name = Rank.TWO, Value = value++ });
				deck.Add(new Card { Name = Rank.THREE, Value = value++ });
				deck.Add(new Card { Name = Rank.FOUR, Value = value++ });
				deck.Add(new Card { Name = Rank.FIVE, Value = value++ });
				deck.Add(new Card { Name = Rank.SIX, Value = value++ });
				deck.Add(new Card { Name = Rank.SEVEN, Value = value++ });
				deck.Add(new Card { Name = Rank.EIGHT, Value = value++ });
				deck.Add(new Card { Name = Rank.NINE, Value = value++ });
				deck.Add(new Card { Name = Rank.TEN, Value = value });
				deck.Add(new Card { Name = Rank.JACK, Value = value });
				deck.Add(new Card { Name = Rank.QUEEN, Value = value });
				deck.Add(new Card { Name = Rank.KING, Value = value });
				deck.Add(new Card { Name = Rank.ACE, Value = value + 1});
			}
		}

		public Card DealCard()
		{
			Card card = deck[0];
			deck.RemoveAt(0);
			return card;
		}

		public void Shuffle()
		{
			// Randomly move each card to a different position and swap it.
			for (int i = 0; i < deck.Count; i++)
			{
				int cardIndex = cardRandomizer.Next(0, deck.Count);
				Card swappedCard = deck[i];
				deck[i] = deck[cardIndex];
				deck[cardIndex] = swappedCard;
			}
		}

		public void DisplayDeck()
		{
			foreach (Card card in deck)
			{
				Console.WriteLine("{0}", card);
			}
			Console.WriteLine("Size == {0}", deck.Count);
		}
	}
}