namespace Blackjack
{
    internal class Card
    {
        public int Value;
        private string Name;
        public string Suit;

        public static Card GetCardValue(int cardValue)
        {
            return cardValue switch
            {
                0 => new Card() { Name = "Two", Value = 2 },
                1 => new Card() { Name = "Three", Value = 3 },
                2 => new Card() { Name = "Four", Value = 4 },
                3 => new Card() { Name = "Five", Value = 5 },
                4 => new Card() { Name = "Six", Value = 6 },
                5 => new Card() { Name = "Seven", Value = 7 },
                6 => new Card() { Name = "Eight", Value = 8 },
                7 => new Card() { Name = "Nine", Value = 9 },
                8 => new Card() { Name = "Ten", Value = 10 },
                9 => new Card() { Name = "Jack", Value = 10 },
                10 => new Card() { Name = "Queen", Value = 10 },
                11 => new Card() { Name = "King", Value = 10 },
                12 => new Card() { Name = "Ace", Value = 11 },
                _ => new Card() { Name = "Two", Value = 2 },
            };
        }

        public override string ToString()
        {
            return Name + " of " + Suit;
        }
    }
}
