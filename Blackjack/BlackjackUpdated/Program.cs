﻿using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
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
            Console.BackgroundColor = ConsoleColor.DarkGreen;
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
                    ////Currently, just get a value between 16-21 for the dealer
                    //dealerTotal = cardRandomizer.Next(15, 22);
                    dealerCards[0] = Card.DealCard();
                    dealerCards[1] = Card.DealCard();
                    playerCards[0] = Card.DealCard();
                    playerCards[1] = Card.DealCard();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;
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
                //do
                //{
                if (playerTotal == 21)
                {
                    Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                    Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                    PlayAgain();
                }
                else
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                //}
                //while (!playerChoice.Equals("H") && !playerChoice.Equals("H"));

                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit();
                }

                if (playerChoice.Equals("S"))
                {
                    Stay();
                }

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
            Console.WriteLine("You were dealt the cards : {0} of {1} and {2} of {3} ", playerCards[0].Name, playerCards[0].Suit, playerCards[1].Name, playerCards[1].Suit);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            Console.WriteLine("The dealer was dealt a(n): {0} of {1}", dealerCards[0].Name, dealerCards[0].Suit);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = Card.DealCard();
            if (playerCards[playerCardCount].Name == "Ace")
            {
                if(21 - playerTotal < 11)
                {
                    playerCards[playerCardCount].Value = 1;
                }
                else
                {
                    playerCards[playerCardCount].Value = 11;
                }
            }
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} of {1} and your new Total is {2}. ", playerCards[playerCardCount].Name, playerCards[playerCardCount].Suit, playerTotal);

            if(dealerTotal < 17)
            {
                dealerCards[dealerCardCount] = Card.DealCard();
                dealerTotal += dealerCards[dealerCardCount].Value;
            }

            //Is this true? I don't think it is.
            if (playerTotal.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            else if (playerTotal < 21)
            {
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
        }

        static void Stay()
        {
            if (playerTotal > dealerTotal && playerTotal <= 21)
            {
                Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
            }
            else if (playerTotal < dealerTotal && dealerTotal < 21)
            {
                Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
            }
            else if (dealerTotal > 21)
            {
                Console.WriteLine("Congrats! You won the game! The dealer busted with a total of {0}!", dealerTotal);
            }
        }

        //TODO: Move this class to it's own file.
        

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