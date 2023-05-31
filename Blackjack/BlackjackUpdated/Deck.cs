namespace BlackjackUpdated
{
    public class Card
    {
        public int Value;
        public string Name = "";
        public string Suit = "";

        public override bool Equals(object obj)
        {
            // Check if the object is null or of a different type
            if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        // Cast the object to a Card instance
        Card otherCard = (Card) obj;

        // Compare the value and name properties for equality
        return Value == otherCard.Value && Name == otherCard.Name;
        }

        public override int GetHashCode()
        {
            // Generate a hash code based on the value and name properties
            return HashCode.Combine(Value, Name);
        }
    }

    public enum Suit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }

    public enum Value
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10,
    }

    public class Deck
    {
        public List<Card> cards = new List<Card>();

        public Deck()
        {
            cards = initializeDeck();
            // TODO:
        }

        public List<Card> initializeDeck()
        {
            List<Card> deck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (String name in Enum.GetNames(typeof(Value)))
                {
                    try
                    {
                        int cardValue = (int) Enum.Parse(typeof(Value), name);
                        deck.Add(new Card { Name = name, Suit = suit.ToString(), Value = cardValue});
                    } 
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Caught a translation exception when creating the deck!");
                        continue;
                    }
                }
            }

            return deck;
        }

        public void shuffle()
        {
            // TODO:
        }
    }
}