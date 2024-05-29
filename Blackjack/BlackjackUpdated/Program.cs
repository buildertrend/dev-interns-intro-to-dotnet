using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;
using static BlackjackUpdated.Card;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static bool playing = true;
        static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;

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
                catch (Exception ex)
                {
                    Console.WriteLine("hi");
                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    // dealerTotal = cardRandomizer.Next(15, 22);
                    dealerCards[0] = Card.DealCard();
                    dealerCards[1] = Card.DealCard();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    playerCards[0] = Card.DealCard();
                    playerCards[1] = Card.DealCard();

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;

                    //TODO: The dealer is dealt one card face up, one card face down.
                    DisplayWelcomeMessage();

                    if (playerTotal.Equals(21))
                    {
                        Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

                    }
                }
                else
                {
                    Console.WriteLine("Exiting due to invaid keypress...");
                    Environment.Exit(0);
                }

                /* START GAME LOOP */

                while (playing)
                {
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

                    // TODO: If hit then stay, doesn't show ending message.
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
                        playing = false;
                    }
                    /* END GAME LOOP */
                }

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("The dealer shows their first card, a(n) {0}", dealerCards[0].Name);
            Console.WriteLine("Their current known total is {0}", dealerCards[0].Value);

            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = DealCard();
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            // else if (playerTotal < 21)
            // {
                // do
                // {
                //     Console.WriteLine("Would you like to hit or stay? h for hit s for stay");
                //     playerChoice = Console.ReadLine().ToUpper();
                // }
                // while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                // if (playerChoice.ToUpper() == "H")
                // {
                //     Hit();
                // }
            // }
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