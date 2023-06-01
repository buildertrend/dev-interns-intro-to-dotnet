using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Deck
    {

        public List<Card> Cards;
        public string[] suits = { "Diamond", "Spade", "Heart", "Club" };

        //Populate the Deck with 4 of each card in GetCardValue
        public Deck()
        {
            Reset();
            Shuffle();
        }

        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => Guid.NewGuid())
                         .ToList();
        }

        public void Reset()
        {
            Cards = new List<Card>();
            for (int i = 1; i < 14; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card c = Card.GetCardValue(i);
                    String s = suits[j];
                    Cards.Add(new Card() { Name = c.Name, Value = c.Value, Suit = suits[j] });
                    //Cards.Add(Card.GetCardValue(i));
                }
            }
        }

        public Card TakeCard()
        {
            var card = Cards.FirstOrDefault();
            Cards.Remove(card);
            return card;
        }
    }
}
