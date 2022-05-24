namespace Blackjack
{
    internal class Player
    {
        private int playerNum;
        private Card[] cards;
        private int totalPoints;
        private int cardCount;

        public Player(int playerNum)
        {
            this.playerNum = playerNum;
            this.Reset();
        }

        public void Reset()
        {
            this.cards = new Card[11];
            this.totalPoints = 0;
            this.cardCount = 0;
        }

        public void AddCardToHand(Card card)
        {
            this.cards[cardCount] = card;
            this.cardCount++;
            this.totalPoints += card.Value;
        }

        public int GetPlayerNum()
        {
            return this.playerNum;
        }

        public Card[] GetCards()
        {
            return this.cards;
        }

        public Card GetMostRecentCard()
        {
            return this.cards[this.cardCount - 1];
        }

        public int GetTotalPoints()
        {
            return this.totalPoints;
        }
    }
}
