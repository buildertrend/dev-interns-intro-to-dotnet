namespace BlackjackUpdated
{
    // Player class that stores the necessary values for a player
    class Player
    {
        public Card[] playerCards;
        int playerTotal;
        public int playerCardCount;

        public Player()
        {
            playerTotal = 0;
            playerCardCount = 0;
            playerCards = new Card[11];

        }

        public void AddToTotal(int value)
        {
            playerTotal += value;
        }

        public int getPlayerTotal()
        {
            return playerTotal;
        }

        public void AddToPlayerCardCount()
        {
            playerCardCount++;
        }

        public int GetPlayerCardCount()
        {
            return playerCardCount;
        }

        public void AddCardToHand(Card card)
        {
            playerCards[playerCardCount] = card;
            AddToPlayerCardCount();
            AddToTotal(card.Value);
        }

        public void ResetPlayer()
        {
            playerTotal = 0;
            playerCardCount = 0;
            playerCards = new Card[11];
        }

        public Card GetLastCard()
        {
            return playerCards[playerCards.Length - 1];
        }
    }
}
