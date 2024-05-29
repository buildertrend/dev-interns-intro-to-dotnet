using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{

    public class Card
    {
        public int Value;
        public string Name;
        public string Suit;

    }
    public class Deck
    {
        private List<Card> _deck { get; set; } = new List<Card>();
        public List<Card> deck => _deck;

        public List<Card> BuildDeck()
        {
            //List<Card> deck = new List<Card>();

            List<string> suits = new List<string> {"Hearts", "Diamonds", "Clubs", "Spades" };
            

            List<string> names = new List<string>
            {
                "Ace",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine",
                "Ten",
                "Jack",
                "Queen",
                "King",
            };
            
            foreach (string suit in suits)
            {
                foreach (string name in names)
                {
                    Card temp = new Card();
                    temp.Name = name;
                    temp.Suit = suit;
                    temp.Value = GetCardValue(name);
                    _deck.Add(temp);
                }
            }


            return deck;
        }
        static int GetCardValue(string name)
        {
            return name switch
            {
                "Ace" => 11,
                "Two" => 2,
                "Three" => 3,
                "Four" => 4,
                "Five" => 5,
                "Six" => 6,
                "Seven" => 7,
                "Eight" => 8,
                "Nine" => 9,
                "Ten" => 10,
                "Jack" => 10,
                "Queen" => 10,
                "King" => 10,
            };
           
        }
        public void shuffleDeck()
        {
            List<Card> newDeck = new List<Card>();
            Random cardPicker = new Random();

            while (_deck.Count() > 0)
            {
                int card = cardPicker.Next(0, _deck.Count());
                newDeck.Add(_deck[card]);
                _deck.Remove(_deck[card]);

            }

            _deck = newDeck;
        }


    }

    
}
