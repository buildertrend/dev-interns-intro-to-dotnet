﻿using System;

namespace Blackjack
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly Card[,] playerCards = new Card[4, 11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        static int playerCount = 0;
        static Player[] player = new Player[4];

        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        //static bool player = true;
        static bool dealer = false;
        static int dealerFaceUp = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {

            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.Write("Welcome to Blackjack - are you ready to play?");
                Console.ForegroundColor = colors[10];
                Console.Write(" (Y)es", colors);
                Console.ForegroundColor = colors[12];
                Console.WriteLine(" (N)o", colors);
                Console.ResetColor();

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    DealDealer();
                    DealDealer();

                    Console.WriteLine("How many players? (1-4)");
                    var pCount = Console.ReadLine();
                    int playerCount = Int32.Parse(pCount);

                    Console.WriteLine("The dealer was dealt the card {0}", dealerCards[0].Name);

                    for (int i = 0; i < playerCount; i++)
                    {
                        player[i] = new Player();
                        DealPlayer(i);
                        DealPlayer(i);
                        Console.ForegroundColor = colors[10];
                        Console.Write("Player{0}", i, colors);
                        Console.ResetColor();
                        Console.WriteLine(" was dealt the cards: {0} and {1} ", player[i].getCard(0).Name, player[i].getCard(1).Name);
                    }

                    for (int i = 0; i < playerCount; i++)
                    {
                        Console.WriteLine("");
                        playerTotal = player[i].getTotal();

                        if (playerTotal == 21)
                        {
                            Console.WriteLine("Player{0} got Blackjack!", i);
                        }
                        else
                        {
                            do
                            {
                                Console.ForegroundColor = colors[10];
                                Console.Write("Player{0}", i, colors);
                                Console.ResetColor();
                                Console.Write(": Would you like to");
                                Console.Write(": Would you like to");
                                Console.ForegroundColor = colors[10];
                                Console.Write(" (H)it", colors);
                                Console.ResetColor();
                                Console.Write(" or");
                                Console.ForegroundColor = colors[12];
                                Console.Write(" (S)tay", colors);
                                Console.ResetColor();
                                Console.WriteLine("? Your current total is {1}", i, playerTotal);

                                playerChoice = Console.ReadLine().ToUpper();
                            }
                            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                        }

                        if (playerChoice.Equals("H"))
                        {
                            //hit will get them a card / check the total and ask for another hit
                            Hit(i);
                        }

                        if (playerChoice.Equals("S"))
                        {
                            /*if (i == playerCount - 1)
                            {
                                Console.WriteLine("The dealer's second card is {0}, dealer's total is {1}", dealerCards[1].Name, dealerTotal);
                                for (int j = 0; j < playerCount; j++)
                                {
                                    playerTotal = player[j].getTotal();

                                    if (playerTotal > 21)
                                    {
                                        Console.ForegroundColor = colors[10];
                                        Console.Write("Player{0}", j, colors);
                                        Console.ResetColor();
                                        Console.WriteLine(" has a total of {0} and busted", playerTotal);
                                    }

                                    else if (playerTotal < dealerTotal)
                                    {
                                        Console.ForegroundColor = colors[10];
                                        Console.Write("Player{0}", j, colors);
                                        Console.ResetColor();
                                        Console.WriteLine(" has a total of {0} and lost to the dealer", playerTotal);
                                    }

                                    else if (playerTotal > dealerTotal)
                                    {
                                        Console.ForegroundColor = colors[10];
                                        Console.Write("Player{0}", j, colors);
                                        Console.ResetColor();
                                        Console.WriteLine(" has a total of {0} and won the dealer", playerTotal);
                                    }

                                    else
                                    {
                                        Console.ForegroundColor = colors[10];
                                        Console.Write("Player{0}", j, colors);
                                        Console.ResetColor();
                                        Console.WriteLine(" has a total of {0} and tied the dealer", playerTotal);
                                    }
                                }
                            }*/
                        }
                    }

                    for (int j = 0; j < playerCount; j++)
                    {
                        playerTotal = player[j].getTotal();

                        if (playerTotal > 21)
                        {
                            Console.ForegroundColor = colors[10];
                            Console.Write("Player{0}", j, colors);
                            Console.ResetColor();
                            Console.WriteLine(" has a total of {0} and busted", playerTotal);
                        }

                        else if (playerTotal < dealerTotal)
                        {
                            Console.ForegroundColor = colors[10];
                            Console.Write("Player{0}", j, colors);
                            Console.ResetColor();
                            Console.WriteLine(" has a total of {0} and lost to the dealer", playerTotal);
                        }

                        else if (playerTotal > dealerTotal)
                        {
                            Console.ForegroundColor = colors[10];
                            Console.Write("Player{0}", j, colors);
                            Console.ResetColor();
                            Console.WriteLine(" has a total of {0} and won the dealer", playerTotal);
                        }

                        else
                        {
                            Console.ForegroundColor = colors[10];
                            Console.Write("Player{0}", j, colors);
                            Console.ResetColor();
                            Console.WriteLine(" has a total of {0} and tied the dealer", playerTotal);
                        }
                    }

                    //DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
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
            Console.WriteLine("You were dealt the cards: {0} and {1} ", player[0].getCard(0).Name, player[0].getCard(1).Name);
            Console.WriteLine("Your player total is {0}, dealercard 1 is {0}", player[0].getTotal(), dealerCards[0].Name);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(int index)
        {
            playerCardCount = player[index].getCardCount();
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            //DealCard(player);
            DealPlayer(index);

            playerTotal = player[index].getTotal();

            Console.WriteLine("Your card is a(n) {0} and your new total is {1}. ", player[index].getCard(player[index].getCardCount() -1).Name, playerTotal);

            //Is this true? I don't think it is.
            if (playerTotal == 21)
            {
                Console.WriteLine("Player{0} got Blackjack! The dealer's total was {1}. ", index, dealerTotal);

            }
            else if (playerTotal > 21)
            {
                Console.WriteLine("Player{0} is busted! Sorry!", index);

            }
            else if (playerTotal < 21)
            {
                do
                {
                    Console.Write("Would you like to hit or stay?");

                    Console.ForegroundColor = colors[10];
                    Console.Write(" (h) for hit", colors);
                    Console.ForegroundColor = colors[12];
                    Console.Write(" (s) for stay", colors);
                    Console.ResetColor();
                    Console.WriteLine(", dealer's face up card is {0}", dealerCards[0].Name);

                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(index);
                }
            }
        }

        static Card DealDealer()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            
            dealerCards[dealerCardCount] = GetCardValue(cardValue);
            dealerTotal += cardValue;
            dealerCardCount++;
            dealerFaceUp = dealerCards[0].Value;

            return GetCardValue(cardValue);
        }

        static Card DealPlayer(int index)
        {
            int cardValue = cardRandomizer.Next(1, 14);

            player[index].addCard(GetCardValue(cardValue));

            return GetCardValue(cardValue);
        }

        static Card GetCardValue(int cardValue)
        {
            return cardValue switch
            {
                1 => new Card() { Name = "Two", Value = 2 },
                2 => new Card() { Name = "Three", Value = 3 },
                3 => new Card() { Name = "Four", Value = 4 },
                4 => new Card() { Name = "Five", Value = 5 },
                5 => new Card() { Name = "Six", Value = 6 },
                6 => new Card() { Name = "Seven", Value = 7 },
                7 => new Card() { Name = "Eight", Value = 8 },
                8 => new Card() { Name = "Nine", Value = 9 },
                9 => new Card() { Name = "Ten", Value = 10 },
                10 => new Card() { Name = "Jack", Value = 10 },
                11 => new Card() { Name = "Queen", Value = 10 },
                12 => new Card() { Name = "King", Value = 10 },
                13 => new Card() { Name = "Ace", Value = 11 },
                _ => new Card() { Name = "Two", Value = 2 },
            };
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