using System;
using Blackjack.Types;

namespace Blackjack
{
    class Program
    {

        //users to store the player choice (hit or stay)

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

                    GameState.player.DealCard(GameState.deck.Deal());
                    GameState.player.DealCard(GameState.deck.Deal());

                    GameState.dealer.DealCard(GameState.deck.Deal());
                    GameState.dealer.DealCard(GameState.deck.Deal());

                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }


                GameState.player.TakeTurn();
               
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Dealer's hand is: {0}", GameState.dealer.DisplayHand());
            Console.WriteLine("Your hand is: {0}", GameState.player.DisplayHand());
        }

        static void PlayAgain()
        {
            Console.WriteLine("Would you like to play again? (Y)es or (N)o?");

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

                //TODO just reset players/decks, dont create new ones
                GameState.player = new Player() { Name = "Player", Type = PlayerType.Human };
                GameState.dealer = new Player() { Name = "Dealer", Type = PlayerType.Dealer };
                GameState.deck = new Deck();
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