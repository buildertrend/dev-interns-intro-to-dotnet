using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace BlackjackUpdated
{
    class Deck
    {
        public List<Card> cardDeck = new List<Card>();
        static string[] suites = { "Hearts", "Spades", "Clubs", "Diamonds" };
        static string[] names = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace"};
        static int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
        public List<Card> dealtCards = new List<Card>();

        public Deck()
        {
            for (int i = 0; i < 4; i++)
            {
                string s = suites[i];

                for (int j = 0; j < 13; j++)
                {
                    string n = names[j] + " of " + s;
                    Card c = new Card(values[j], n, s);
                    cardDeck.Add(c);
                }
            }
        }

        public void shuffleDeck()
        {
            Random rand = new Random();
            cardDeck = cardDeck.OrderBy(_ => rand.Next()).ToList();

        }

        public Card dealCard()
        {
            Card dealtCard = cardDeck[0];
            dealtCards.Add(dealtCard);
            cardDeck.RemoveAt(0);
            return dealtCard;
        }
    }
}
