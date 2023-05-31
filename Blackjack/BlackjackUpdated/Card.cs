namespace BlackjackUpdated
{
    internal class Card
    {

        public int Value { get; set; }
        public string Name { get; set; }

        public Card(int value, string name)
        {
            Value = value;
            Name = name;
        }


        public static Card GetCardValue(int cardValue)
        {
            switch (cardValue)
            {
                case 1:
                    return new Card(2, "Two");
                case 2:
                    return new Card(3, "Three");
                case 3:
                    return new Card(4, "Four");
                case 4:
                    return new Card(5, "Five");
                case 5:
                    return new Card(6, "Six");
                case 6:
                    return new Card(7, "Seven");
                case 7:
                    return new Card(8, "Eight");
                case 8:
                    return new Card(9, "Nine");
                case 9:
                    return new Card(10, "Ten");
                case 10:
                    return new Card(10, "Jack");
                case 11:
                    return new Card(10, "Queen");
                case 12:
                    return new Card(10, "King");
                case 13:
                    return new Card(11, "Ace");
                default:
                    return new Card(2, "Two");
            }
        
        }

    }
}
