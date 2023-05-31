using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    partial class Program
    {
        static Deck deck = new Deck();

        static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception)
                {
                    Console.WriteLine("hi");
                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    dealerCards[0] = deck.draw();
                    dealerCards[1] = deck.draw();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    playerCards[0] = deck.draw();
                    playerCards[1] = deck.draw();

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;


                    //TODO: The dealer is dealt one card face up, one card face down.
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
                    if (playerTotal > dealerTotal && playerTotal <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                    }
                }

                /* END GAME LOOP */

                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("The dealer's first card is: {0}.", dealerCards[0].Name);
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your player total is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        // <summary>
        // Checks if there are ny aces in the hand. These are automatically set to a value of 11 (hard ace),
        // but can be set to a 1 to prevent the player from busting.
        // </summary>
        private static void AlterAces()
        {
            bool alteredFlag = false;

            playerTotal = 0;
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (playerCards[i] == null) { continue; }
                if (playerCards[i].Equals(new Card() { Name = "Ace", Value = 11 }))
                {
                    // Switch the ace to soft.
                    playerCards[i] = new Card() { Name = "Ace", Value = 1 };
                    playerTotal += 1;
                    Console.WriteLine("Your card #{0} has been changed from a hard ace to a soft ace.", i);
                    alteredFlag = true;
                }
                else
                {
                    playerTotal += playerCards[i].Value;
                }
            }
            if (alteredFlag)
            {
                Console.WriteLine("Your new hand total is: {0}", playerTotal);
            }
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = deck.draw();
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("Your card is a(n) {0} and your new total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            //Is this true? I don't think it is.
            if (playerTotal.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
            {
                AlterAces();
                // Still check to see if we're over 21.
                if (playerTotal > 21)
                {
                    Console.WriteLine("You busted! Sorry! The dealer's total was {0}", dealerTotal);
                    return;
                }
            }

            // We're safe. Ask to hit again if desired.
            do
            {
                Console.WriteLine("Would you like to hit or stay? h for hit s for stay");
                playerChoice = Console.ReadLine().ToUpper();
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
            if (playerChoice.ToUpper() == "H")
            {
                Hit();
            }
        }

        static void PlayAgain()
        {
            //Loop until they make a valid choice
            do
            {
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            if (playAgain.Equals("Y"))
            {
                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();
                dealerTotal = 0;
                playerCardCount = 1;
                playerTotal = 0;
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
    }
}