using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Card
    {
        public int Value;
        public string Name;
        public string Suit;
    }

    internal class Player
    {
        public int PlayerID;
        public List<Card> PlayerCards;
        public int PlayerTotal;

        public Player(int playerID, List<Card> playerCards, int playerTotal) {
            PlayerID = playerID;
            PlayerCards = playerCards;
            PlayerTotal = playerTotal;
        }
    }

    internal class CardDeck
    {
        public Stack<Card> DeckOfCards;
        private Random cardRandomizer;

        public CardDeck() 
        {
            DeckOfCards = CreateDeckOfCards();
            cardRandomizer = new Random();
        }
        public void ShuffleCards() 
        {
            List<Card> cardList = DeckOfCards.ToList();
            int n = cardList.Count;

            while (n > 1)
            {
                n--;
                int k = cardRandomizer.Next(n + 1);
                Card temp = cardList[k];
                cardList[k] = cardList[n];
                cardList[n] = temp;
            }

            DeckOfCards = new Stack<Card>(cardList);
        }

        public Card DealCard()
        {
            if (DeckOfCards.Count < 1)
            {
                DeckOfCards = CreateDeckOfCards();
                ShuffleCards();
            }
            Card card = DeckOfCards.Pop();
            /*playerTotal += cardValue;*/
            return GetCardValue(card.Value);
        }

        public Card GetCardValue(int cardValue)
        {
            return cardValue switch
            {
                1 => new Card() { Name = "Two", Value = 2 },
                2 => new Card() { Name = "Three", Value = 3 },
                3 => new Card() { Name = "Four", Value = 4 },
                4 => new Card() { Name = "Five", Value = 5 },
                5 => new Card() { Name = "Six", Value = 6 },
                6 => new Card() { Name = "Seven", Value = 7 },
                7 => new Card() { Name = "Eight", Value = 8 },
                8 => new Card() { Name = "Nine", Value = 9 },
                9 => new Card() { Name = "Ten", Value = 10 },
                10 => new Card() { Name = "Jack", Value = 10 },
                11 => new Card() { Name = "Queen", Value = 10 },
                12 => new Card() { Name = "King", Value = 10 },
                13 => new Card() { Name = "Ace", Value = 11 },
                _ => new Card() { Name = "Two", Value = 2 },
            };
        }

        public Stack<Card> CreateDeckOfCards()
        {
            Stack<Card> deckOfCards = new Stack<Card>();
            string[] suits = { "Spades", "Clubs", "Hearts", "Diamonds" };
            foreach (var suit in suits)
            {
                for (int i = 1; i <= 13; i++)
                {
                    var card = GetCardValue(i);
                    deckOfCards.Push(item: new Card()
                    {
                        Name = card.Name,
                        Value = card.Value,
                        Suit = suit
                    });
                }
            }
            return deckOfCards;
        }
    }
}
