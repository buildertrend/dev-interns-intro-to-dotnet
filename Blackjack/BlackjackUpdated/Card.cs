using System.Security.Cryptography.X509Certificates;

namespace BlackjackUpdated
{
    enum Suits { 
        Diamonds, Hearts, Clubs, Spades
    }

    class Card
    {
        static Random cardRandomizer = new Random();
        public int Value;
        public string Name;
        public string Suit;

        public static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            //playerTotal += cardValue;
            return GetCardValue(cardValue);
        }

        public static Card GetCardValue(int cardValue)
        {
            //Suits validSuits = new[] { Suits.Clubs, Suits.Hearts, Suits.Diamonds, Suits.Spades };
            //Random rnd = new Random();
            //Suits selectedSuits = validSuits[rnd.Next(validSuits.Length)];
            Suits someSuit = Suits.Diamonds | Suits.Hearts | Suits.Clubs | Suits.Spades;

            var someSuit2 = new List<Suits>();

            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                if (someSuit.HasFlag(suit))
                {
                    someSuit2.Add(suit);
                }
            }

            Random rnd = new Random();
            Suits selectedSuit = someSuit2[rnd.Next(someSuit2.Count)];
            string suitString = selectedSuit.ToString();
            return cardValue switch
            {
                1 => new Card() { Name = "Two", Value = 2, Suit = suitString },
                2 => new Card() { Name = "Three", Value = 3, Suit = suitString },
                3 => new Card() { Name = "Four", Value = 4, Suit = suitString },
                4 => new Card() { Name = "Five", Value = 5, Suit = suitString },
                5 => new Card() { Name = "Six", Value = 6, Suit = suitString },
                6 => new Card() { Name = "Seven", Value = 7, Suit = suitString },
                7 => new Card() { Name = "Eight", Value = 8, Suit = suitString },
                8 => new Card() { Name = "Nine", Value = 9, Suit = suitString },
                9 => new Card() { Name = "Ten", Value = 10, Suit = suitString },
                10 => new Card() { Name = "Jack", Value = 10, Suit = suitString },
                11 => new Card() { Name = "Queen", Value = 10, Suit = suitString },
                12 => new Card() { Name = "King", Value = 10, Suit = suitString },
                13 => new Card() { Name = "Ace", Value = 11, Suit = suitString },
                _ => new Card() { Name = "Two", Value = 2, Suit = suitString },
            };
        }
    }
}
