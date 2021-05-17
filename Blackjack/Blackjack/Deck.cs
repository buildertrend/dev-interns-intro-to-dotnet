using Blackjack;
using System;
using System.Collections.Generic;

public class Deck
{
	private static Random cardRandomizer = new Random();

	List<Card> deck = new List<Card>();
	public Deck()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 1; j < 14; j++)
			{
				deck.Add(Program.GetCardValue(j));
			}
		}
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
		foreach (Card card in deck) {
			Console.WriteLine("{0}", card);
		}
		Console.WriteLine("Size == {0}", deck.Count);
	}
}
