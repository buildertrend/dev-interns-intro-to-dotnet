using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.Types
{
    public enum PlayerType
    {
        Dealer,
        Human,
    }

    public class Player
    {
        public string Name;
        public PlayerType Type;
        public List<Card> Hand;

        public Player()
        {
            Hand = new List<Card>();
        }

        public string DisplayHand()
        {
            return $"{String.Join(" + ", Hand.Select(card => card.Rank).ToArray())} = {ScoreHand()}";
        }

        public void DealCard(Card card)
        {
            Hand.Add(card);
        }

        public int ScoreHand()
        {
            //TODO check for ace
            return Hand.Aggregate(0, (total, card) => total += Card.GetCardValue(card));
        }

        public void TakeTurn()
        {
            if (Type == PlayerType.Human)
            {
                HumanTurn();
            }
            else
            {
                DealerTurn();
            }

        }

        private void HumanTurn()
        {
            string playerChoice;
            do
            {
                Console.WriteLine($"{Name}, would you like to (H)it or (S)tay?");
                playerChoice = Console.ReadLine().ToUpper();
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

            if (playerChoice.Equals("H"))
            {
                Hit();
            }

            if (playerChoice.Equals("S"))
            {
                int playerTotal = ScoreHand();
                int dealerTotal = GameState.dealer.ScoreHand();

                if (playerTotal > dealerTotal && playerTotal <= 21)
                {
                    Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                }
                else if (playerTotal < dealerTotal)
                {
                    Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                }
            }
        }

        private void DealerTurn()
        { 
            //TODO
        }

        public void Hit()
        {
            Card card = GameState.deck.Deal();
            DealCard(card);

            int playerTotal = ScoreHand();
            int dealerTotal = GameState.dealer.ScoreHand();
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", card.Rank, playerTotal);

            if (playerTotal == 21)
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            else if (playerTotal < 21)
            {
                TakeTurn();
            }
        }
    }
}
