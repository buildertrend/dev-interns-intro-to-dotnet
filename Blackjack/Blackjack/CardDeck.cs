using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class CardDeck
    {
        private Card[] cards = new Card[52];
        static Random cardRandomizer = new Random();
        private static int cardsDrawn = 0;

        public CardDeck(){
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cardRandomizer.Next(0, 13);
                    cards[i*13+j] = GetCardValue(j+1, intToSuit(i));
                }
            }

            cards = cards.OrderBy(item => cardRandomizer.Next()).ToArray();
        }

        public String intToSuit(int index)
        {
            switch (index)
            {
                case 0:
                    return "Club";
                    break;
                case 1:
                    return "Heart";
                    break;
                case 2:
                    return "Spade";
                    break;
                case 3:
                    return "Diamond";
                    break;
            }
            return "";
        }

        public Card drawCard()
        {
            if (cardsDrawn > 51)
            {
                return new Card() { Name = "*NO CARDS LEFT IN DECK*", Value = 100, Suit = "notValid" };
            }
            int cardIndex = cardRandomizer.Next(0, 52 - cardsDrawn);
            Card temp = cards[cardIndex];
            cards[cardIndex] = cards[52 - cardsDrawn - 1];
            cards[52 - cardsDrawn - 1] = temp;
            cardsDrawn++;
            return temp;
        }

        public int cardsUsed()
        {
            return cardsDrawn;
        }

        static Card GetCardValue(int cardValue, String suit)
        {
            return cardValue switch
            {
                1 => new Card() { Name = "Two", Value = 2, Suit = suit },
                2 => new Card() { Name = "Three", Value = 3, Suit = suit },
                3 => new Card() { Name = "Four", Value = 4, Suit = suit },
                4 => new Card() { Name = "Five", Value = 5, Suit = suit },
                5 => new Card() { Name = "Six", Value = 6, Suit = suit },
                6 => new Card() { Name = "Seven", Value = 7, Suit = suit },
                7 => new Card() { Name = "Eight", Value = 8, Suit = suit },
                8 => new Card() { Name = "Nine", Value = 9, Suit = suit },
                9 => new Card() { Name = "Ten", Value = 10, Suit = suit },
                10 => new Card() { Name = "Jack", Value = 10, Suit = suit },
                11 => new Card() { Name = "Queen", Value = 10, Suit = suit },
                12 => new Card() { Name = "King", Value = 10, Suit = suit },
                13 => new Card() { Name = "Ace", Value = 11, Suit = suit },
                _ => new Card() { Name = "Two", Value = 2, Suit = suit },
            };
        }
    }
}
