namespace BlackjackUpdated
{
    //Deck is a list of cards that contains methods necceary to play a card game
    public class Deck
    {
        public Deck()
        {
            Reset();
        }

        public List<Card> Cards { get; set; }

        //This will be used for card counting
        Dictionary<CardNumber, int> pulledCards; 

        public void Reset()
        {
            Cards = Enumerable.Range(1, 4)
               .SelectMany(s => Enumerable.Range(1, 13)
                                   .Select(c => new Card()
                                   {
                                       suit = (Suit)s,
                                       cardNumber = (CardNumber)c
                                   }
                                           )
                           )
                  .ToList();

            pulledCards = new Dictionary<CardNumber, int>();
        }

        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => Guid.NewGuid())
                         .ToList();
        }

        public Card TakeCard()
        {
            var card = Cards.FirstOrDefault();
            Cards.Remove(card);
            AddCardsToPulled(card.cardNumber);
            return card;
        }

        public int CardsLeft() { 
            return Cards.Count;
        }

        public void AddCardsToPulled(CardNumber cardnumber)
        {

            if (pulledCards.TryGetValue(cardnumber, out var pulled))
            {
                pulledCards.Add(cardnumber, pulled + 1);
            }
            else
            {
                pulledCards.Add(cardnumber, 1);
            }

        }


    }
}
