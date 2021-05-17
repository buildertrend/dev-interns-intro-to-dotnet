using System;

namespace Blackjack
{

	public enum Card
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
		Jack = 11,
		Queen = 12,
		King = 13,
		Ace = 14
	}


	//TODO: finish card deal
	public class CardDeal
    {
		public Card getRandom()
        {
			Array values = Enum.GetValues(typeof(Card));
			Random random = new Random();
			Card randomCard = (Card)values.GetValue(random.Next(values.Length));
			return randomCard;
        }
    }
}
