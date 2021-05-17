using System;

namespace Blackjack
{
    class Program
    {
        private static Deck deck;
        private static Hand playerHand;
        private static Hand dealerHand;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            while (playAgain.ToUpper() == "Y")
            {
                deck = new Deck();
                ResetGame();

                //StartGame
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                do
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit();
                }

                if (playerChoice.Equals("S"))
                {
                    if (playerHand.getTotal() > dealerHand.getTotal() && playerHand.getTotal() <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerHand.getTotal());
                    }
                    else if (playerHand.getTotal() <= dealerHand.getTotal())
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerHand.getTotal());
                    }
                }

                Console.WriteLine("Player total == {0}", playerHand.getTotal());
                Console.WriteLine("Dealer total == {0}", dealerHand.getTotal());

                /* END GAME LOOP */

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerHand.getCard(0).Name, playerHand.getCard(1).Name);
            Console.WriteLine("Your playerTotal is {0} ", playerHand.getTotal());
            Console.WriteLine("Dealer's first card: {0}", dealerHand.getCard(0).Name);
        }

        private static void Hit()
        {
            playerHand.addCard(deck.DealCard());
            Console.WriteLine("Your card is a(n) {0} and your new Total is {1}. ", playerHand.getCard(playerHand.getHandSize() - 1).Name, playerHand.getTotal());

            if (playerHand.getTotal() == 21)
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerHand.getTotal());

            }
            else if (playerHand.getTotal() > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerHand.getTotal());

            }
            else if (playerHand.getTotal() < 21)
            {
                do
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit();
                }
            }
        }

        static void PlayAgain()
        {
            //Loop until they make a valid choice
            do
            {
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            if (playAgain.Equals("Y"))
            {
                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();

                ResetGame();
            }
            else if (playAgain.Equals("N"))
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Read();
                    Environment.Exit(0);
                }
            }
        }

        private static void ResetGame()
        {
            // Refill and reshuffle the entire deck
            deck.RefillDeck();
            deck.Shuffle();

            // Deal a hand of 2 cards to both the player and the dealer
            playerHand = new Hand(2, deck);
            dealerHand = new Hand(2, deck);

            // Keep dealing cards to the dealer until his score is more than 17
            while (dealerHand.getTotal() < 17)
            {
                dealerHand.addCard(deck.DealCard());
            }
        }
    }
}