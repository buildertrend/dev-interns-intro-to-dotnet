using System;
using System.Collections.Generic;
namespace Blackjack
{
    partial class Program
    {
        static Random cardRandomizer = new Random();
        static readonly List<Card> playerCardsL = new List<Card>();
        static readonly List<Card> dealerCardsL = new List<Card>();

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
                    //Deal cards to dealer and player
                    dealerCardsL.Add(DealCard());
                    dealerCardsL.Add(DealCard());
                    playerCardsL.Add(DealCard());
                    playerCardsL.Add(DealCard());

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
                    Console.WriteLine("The Dealer card has a(n) {0}. Would you like to (H)it or (S)tay?", dealerCardsL[0].Name);
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
                    if (GetTotal(playerCardsL) > GetTotal(dealerCardsL) && GetTotal(playerCardsL) <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", GetTotal(dealerCardsL));
                    }
                    else if (GetTotal(playerCardsL) < GetTotal(dealerCardsL))
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", GetTotal(dealerCardsL));
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
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCardsL[0].Name, playerCardsL[1].Name);
            Console.WriteLine("Your player Total is {0} ", GetTotal(playerCardsL));
        }

        static void Hit()
        {
            playerCardsL.Add(DealCard());
            //Checks if player woulod normally bust with an Ace, converts Ace to value of 1 if possible
            for (int card = 0; card <= playerCardsL.Count; card++)
            {
                if (GetTotal(playerCardsL) > 21 && playerCardsL[card - 1].Value == 11)
                {
                    playerCardsL[card].Value = 1;
                }
            }
            
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCardsL[playerCardsL.Count - 1].Name, GetTotal(playerCardsL));

            //Is this true? I don't think it is.
            if (GetTotal(playerCardsL) == 21)
            {
                Console.WriteLine("Yuo got Blackjack! The dealer's Total was {0}. ", GetTotal(dealerCardsL));

            }
            else if (GetTotal(playerCardsL) > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", GetTotal(dealerCardsL));

            }
            else if (GetTotal(playerCardsL) < 21)
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

        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
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
                dealerCardsL.Clear();
                playerCardsL.Clear();
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
            //method to sum up all cards in a given hand
        static int GetTotal(List<Card> hand)
        {
            int sum = 0;
            for(int c = 0; c < hand.Count; c++)
            {
                sum += hand[c].Value;
            }
            return sum;
        }
    }
}