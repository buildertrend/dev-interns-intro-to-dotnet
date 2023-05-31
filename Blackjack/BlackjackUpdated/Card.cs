using System.Net.NetworkInformation;
using System.Threading;

namespace BlackjackUpdated
{
    // Card class that stores Suit and CardNumber for every card in a deck
    public class Card
    {
       // public int Value;
       // public string Name;

        public Suit suit { get; set; }
        public CardNumber cardNumber { get; set; }

        public int GetCardValue(int currentPlayerTotal)
        {

            if(currentPlayerTotal + 11 > 21 && this.cardNumber == CardNumber.Ace)
            {
                return 1;
            }
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
