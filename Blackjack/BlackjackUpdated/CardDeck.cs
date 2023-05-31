using Blackjack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackUpdated
{
    
    class CardDeck
    {
        List<Card> cardsList;
        private static Random rng = new Random();
        public CardDeck()
        {
            this.cardsList = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Face face in Enum.GetValues(typeof(Face)))
                {
                    cardsList.Add(new Card() { Suit = suit, Face = face });
                }
            }
        }

        public void shuffle()
        {
            cardsList = cardsList.OrderBy(a => rng.Next()).ToList();
        }

        public Card drawCard()
        {
            Card x = cardsList.ElementAt(cardsList.Count -1);
            cardsList.RemoveAt(cardsList.Count - 1);
            return x;
        }

        public int numCards()
        {
            return cardsList.Count;
        }
    }

}
