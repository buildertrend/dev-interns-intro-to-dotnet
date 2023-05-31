namespace BlackjackUpdated
{
    public class Card
    {
        public int Value;
        public string Name;

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
}