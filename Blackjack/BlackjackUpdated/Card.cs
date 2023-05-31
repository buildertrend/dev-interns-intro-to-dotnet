using System.Net.NetworkInformation;
using System.Threading;

namespace BlackjackUpdated
{
    // Card class that stores value and name for a card 
    public class Card
    {
       // public int Value;
       // public string Name;

        public Suit suit { get; set; }
        public CardNumber cardNumber { get; set; }

        public int GetCardValue()
        {
            return (this.cardNumber) switch
            {
                CardNumber.Ace => 11,
                CardNumber.Two => 2,
                CardNumber.Three => 3,
                CardNumber.Four => 4,
                CardNumber.Five => 5,
                CardNumber.Six => 6,
                CardNumber.Seven => 7,
                CardNumber.Eight => 8,
                CardNumber.Nine => 9,
                CardNumber.Ten => 10,
                CardNumber.Jack => 10,
                CardNumber.Queen => 10,
                CardNumber.King => 10,
            };
        
          
        }

    }
}
