using System;

namespace Blackjack
{
	public class Card
	{
		public Suit cardSuit { get; set; }
		public Rank cardRank { get; set; }

		//(int) Card.Rank.Ace
		public enum Rank
		{
			two = 2,
			three = 3,
			four = 4,
			five = 5,
			six = 6,
			seven = 7,
			eight = 8,
			nine = 9,
			ten = 10,
			Jack = 10,
			Queen = 10,
			King = 10,
			Ace = 11
		}
		public enum Suit
        {
			Spade,
			Club,
			Heart,
			Diamond
        }
	}


}
