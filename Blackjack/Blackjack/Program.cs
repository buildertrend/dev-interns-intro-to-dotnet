﻿using System;

namespace Blackjack
{
    /// <summary>
    /// Contains flow of program.
    /// </summary>
    class Program
    {        
        static Deck deck = new Deck();

        static Random cardRandomizer = new Random();
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
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {                    
                    //TODO: Player deal card from Deck

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

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            //TODO: create Player that stores current score
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotall is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
            Console.WriteLine("Dealer's hand is: {0}", //player.Hand());
        }

        /// <summary>
        /// 
        /// </summary>
        static void Hit()
        {
            //TODO: implement deal for Deck
            //Card card = deck.DealCard();

            //TODO: create a player that stores current score
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

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
                deck = new Deck();
                //TODO: Create new hands for players
            }
            else if (playAgain.Equals("N"))
            {
                Console.WriteLine("Thanks for playing BT Blackjack!");
                Environment.Exit(0);
            }
        }
    }
}