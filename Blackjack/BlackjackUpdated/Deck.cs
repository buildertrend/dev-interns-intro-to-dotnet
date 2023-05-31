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
            return card;
        }

 
    }
}
