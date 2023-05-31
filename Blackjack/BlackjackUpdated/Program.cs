using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;
﻿using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;

namespace BlackjackUpdated
{
    class Program
    {
        static Deck cards = new Deck();
        public static Random cardRandomizer = new Random();
        static ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        static readonly Card[,] playerCards = new Card[5, 11];
        static int[] playerTotal = new int[5];
        static int[] playerCardCount = { 1, 1, 1, 1, 1 };
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static int playerCount = 0;
        public static int hiLo = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            Console.BackgroundColor = colors[11];
            Console.ForegroundColor = colors[13];
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

                Console.WriteLine("Please input the number of players who will be playing: (Between 1 and 5)");
                playerCount = int.Parse(Console.ReadLine());
                Console.WriteLine("Beginning game with {0} players", playerCount);

                 
                if (decision == "Y")
                {
                    cards.shuffleDeck();
                    //Deal the dealer two cards before dealing the player their cards
                    dealerCards[0] = cards.dealCard();
                    dealerCards[1] = cards.dealCard();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    for (int i = 0; i < playerCount; i++)
                    {
                        playerCards[i, 0] = cards.dealCard();
                        playerCards[i, 1] = cards.dealCard();

                        playerTotal[i] += playerCards[i, 0].Value;
                        playerTotal[i] += playerCards[i, 1].Value;
                    }




                    //The dealer is dealt one card face up, one card face down.
                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                for (int i = 0; i < playerCount; i++)
                {
                    do
                    {
                        countCards(i);
                        
                        Console.WriteLine("Player {0}, would you like to (H)it or (S)tay?", i+1);
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (playerChoice.Equals("H") && !playerChoice.Equals("H"));

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit(i);
                    }

                    if (playerChoice.Equals("S"))

                    {
                        if (playerTotal[i] > dealerTotal && playerTotal[i] <= 21)
                        {
                            Console.WriteLine("Congrats Player {1}! You won the game!", dealerTotal, i+1);
                        }
                        else if (playerTotal[i] < dealerTotal)
                        {
                            Console.WriteLine("Sorry Player {1}, you lost!", dealerTotal, i+1);
                        }
                        else if (playerTotal[i] == dealerTotal)
                        {
                            Console.WriteLine("Player {0}, you tied!", i+1);
                        }
                    }

                    /* END GAME LOOP */

                }
                Console.WriteLine("The dealer total was {0}", dealerTotal);
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }
                    

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Player {2}, you were dealt the cards : {0} and {1} ", playerCards[i, 0].Name, playerCards[i, 1].Name, i+1);
                Console.WriteLine("Player {1}, your player total is {0} ", playerTotal[i], i+1);
            }              
            //Inform the player the value of the dealer's visible card.
            Console.WriteLine("The dealer's visible card is: {0} ", dealerCards[0].Name);
        }


        static void Hit(int playerNum)
        {
            playerCardCount[playerNum] += 1;
            playerCards[playerNum, playerCardCount[playerNum]] = cards.dealCard();
            playerTotal[playerNum] += playerCards[playerNum, playerCardCount[playerNum]].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerNum, playerCardCount[playerNum]].Name, playerTotal[playerNum]);

            //Is this true? I don't think it is.
            if (playerTotal[playerNum].Equals(21))
            {
                Console.WriteLine("Player {0}, you got Blackjack!", playerNum+1);

            }
            else if (playerTotal[playerNum] > 21)
            {
                Console.WriteLine("Player {0}, you busted! Sorry!", playerNum+1);

            }
            else if (playerTotal[playerNum] < 21)
            {
                do
                {
                    countCards(playerNum);
                    Console.WriteLine("Player {0}, would you like to hit or stay? h for hit s for stay", playerNum+1);
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(playerNum);
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
                dealerTotal = 0;
                for (int i = 0; i < playerCount; i++)
                {
                    playerCardCount[i] = 1;
                }
                playerTotal = new int[5];
                cards = new Deck();
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

        static void countCards(int playerNum)
        {
            int counting = 0;
            for (int i = 0; i < cards.dealtCards.Count; i++)
            {
                if (cards.dealtCards[i].Value <= 6 && cards.dealtCards[i].Value >= 2)
                {
                    counting += 1;
                }
                else if (cards.dealtCards[i].Value <= 11 && cards.dealtCards[i].Value >= 10)
                {
                    counting -= 1;
                }
                else
                {
                    counting += 0;
                }
            }
            hiLo = counting;
            if (hiLo < 0 || playerTotal[playerNum] < 18) 
            {
                Console.WriteLine("Based on the cards that have been dealt, the recommended move is to stay.");
            }
            else if (hiLo == 0)
            {
                Console.WriteLine("Based on the cards that have been dealt, the recommended move is to stay or hit.");
            }
            else if (hiLo > 0 || playerTotal[playerNum] < 10)
            {
                Console.WriteLine("Based on the cards that have been dealt, the recommended move is to hit.");
            }
        }
        }
    }
