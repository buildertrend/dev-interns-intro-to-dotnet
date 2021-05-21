using System;
using System.Collections.Generic;

namespace Blackjack
{
    class BlackjackMain
    {
        // Deck of cards for the drawing pile
        private static Deck deck;

        // List of all players in the game
        private static List<Player> players;

        // Cards that the player has been dealt
//        private static Hand playerHand;

        // Cards that the dealer has been dealt
        private static Hand dealerHand;

        // Stores whether the player wants to (H)it or (S)tay
        private static string playerChoice = "";

        // Stores whether the player wants to play another round or not (Y or N)
        private static string playAgain = "";

        public static void Main(string[] args)
        {
            // Initialize playAgain to be "Yes" for the first round
            playAgain = "Y";

            // Initialize the deck 
            deck = new Deck();

            while (playAgain.ToUpper().Equals("Y"))
            {
                // Reset the game (deck and hands)
                ResetGame();

                // Loop through each player to take their turn
                foreach (Player player in players)
                {
                    Hand currentHand = player.Hand;
                    
                    Console.WriteLine(player.Name);

                    // Start the game
                    StartGame(currentHand);

                    // Main Game Loop
                    MainGameLoop(currentHand);

                    // Ask the player to play again
                    PlayAgain();
                }
            }
        }

        private static void StartGame(Hand playerHand)
        {
            BlackjackConsoleColor.WriteLineOption(
                new string[] { "Welcome to Blackjack - are you ready to play? ", " or " },
                new string[] { "(Y)es", "(N)o" }
            );
            var decision = BlackjackConsoleColor.ReadLine().ToUpper();

            if (decision.Equals("Y"))
            {
                DisplayWelcomeMessage(playerHand);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private static void MainGameLoop(Hand playerHand)
        {
            do
            {
                BlackjackConsoleColor.WriteLineOption(
                    new string[] { "Would you like to ", " or " },
                    new string[] { "(H)it", "(S)tay" }
                );
                playerChoice = BlackjackConsoleColor.ReadLine().ToUpper();
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
            
  
            if (playerChoice.Equals("H"))
            {
                Hit(playerHand);
            }
            if (playerChoice.Equals("S")) 
            {
                Stay(playerHand);
            }

            BlackjackConsoleColor.WriteLineValue(
                new string[] { "Player total == " },
                new string[] { playerHand.getTotal().ToString() }
            );
            BlackjackConsoleColor.WriteLineValue(
                new string[] { "Dealer total == " },
                new string[] { dealerHand.getTotal().ToString() }
            );
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage(Hand playerHand)
        {
            // Display player cards
            BlackjackConsoleColor.WriteLineValue(
                new string[] { "You were dealt the cards : ", " and " }, 
                new string[] { playerHand.getCard(0).Name.ToString(), playerHand.getCard(1).Name.ToString() }
            );

            // Display player total
            BlackjackConsoleColor.WriteLineValue(
                new string[] { "Your playerTotal is " },
                new string[] { playerHand.getTotal().ToString() }
            );

            // Display dealer card
            BlackjackConsoleColor.WriteLineValue(
                new string[] { "Dealer's first card: " },
                new string[] { dealerHand.getCard(0).Name.ToString() }
            );
        }

        private static void Hit(Hand playerHand)
        {
            playerHand.addCard(deck.DealCard());
            BlackjackConsoleColor.WriteLineValue(
                new string[] { "Your card is a(n) ", " and your new Total is ", "." },
                new string[] { playerHand.getCard(playerHand.getHandSize() - 1).Name.ToString(), playerHand.getTotal().ToString() }
            );

            if (playerHand.getTotal() == 21)
            {
                //Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerHand.getTotal());
                BlackjackConsoleColor.WriteLineValue(
                    new string[] { "You got Blackjack! The dealer's Total was " },
                    new string[] { dealerHand.getTotal().ToString() }
                );
            }
            else if (playerHand.getTotal() > 21)
            {
                //Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerHand.getTotal());
                BlackjackConsoleColor.WriteLineValue(
                    new string[] { "You busted! Sorry! The dealer's Total was " },
                    new string[] { dealerHand.getTotal().ToString() }
                );
            }
            else if (playerHand.getTotal() < 21)
            {
                // Prompt the user to enter a choice (H or S) until a valid choice is entered
                do
                {
                    BlackjackConsoleColor.WriteLineOption(
                        new string[] { "Would you like to ", " or " },
                        new string[] { "(H)it", "(S)tay" }
                    );
                    playerChoice = BlackjackConsoleColor.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(playerHand);
                }
            }
        }

        private static void Stay(Hand playerHand)
        {
            // Check to make sure the player is <= 21
            if (playerHand.getTotal() <= 21)
            {
                // Check to make sure player's total is larger OR if the dealer busted
                if (playerHand.getTotal() > dealerHand.getTotal() || dealerHand.getTotal() > 21)
                {
                    //Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerHand.getTotal());
                    BlackjackConsoleColor.WriteLineValue(
                        new string[] { "Congrats! You won the game! The dealer's total is " },
                        new string[] { dealerHand.getTotal().ToString() }
                    );
                }
                else
                {
                    //Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerHand.getTotal());
                    BlackjackConsoleColor.WriteLineValue(
                        new string[] { "Sorry, you lost! The dealer's total was " },
                        new string[] { dealerHand.getTotal().ToString() }
                    );
                }
            }
            else if (playerHand.getTotal() <= dealerHand.getTotal())
            {
                // otherwise, verify player got a score <= the dealer's score
                //Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerHand.getTotal());
                BlackjackConsoleColor.WriteLineValue(
                    new string[] { "Sorry, you lost! The dealer's total was " },
                    new string[] { dealerHand.getTotal().ToString() }
                );
            }

        }

        private static void PlayAgain()
        {
            // Prompt user for choice
            BlackjackConsoleColor.WriteLineOption(
                new string[] { "Would you like to play again? ", " or " },
                new string[] { "(Y)es", "(N)o" }
            );

            //Loop until they make a valid choice
            do
            {
                playAgain = BlackjackConsoleColor.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            if (playAgain.Equals("Y"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Press enter to restart the game!");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.ReadLine();
                Console.Clear();

                ResetGame();
            }
            else if (playAgain.Equals("N"))
            {
                // Require the user to hit enter once more to exit the game
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

        private static void ResetPlayers()
        {
            players = new List<Player>();
            int numberPlayers = 0;

            // Continuously ask user to enter the number of players until a valid number is entered
            do
            {
                BlackjackConsoleColor.WriteLineOption(
                    new string[] { "How many players would you like? " },
                    new string[] { "[1-7]" }
                );

                // Grab number of players from the console
                try
                {
                    numberPlayers = Int32.Parse(BlackjackConsoleColor.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number. [1-7]");
                }
            } while (numberPlayers < 1 || numberPlayers > 7);
            for (int i = 0; i < numberPlayers; i++)
            {
                string name = "Player " + (i + 1);
                players.Add(new Player() { Name = name });
            }
        }

        private static void ResetGame()
        {
            ResetPlayers();

            // Refill and reshuffle the entire deck
            deck.RefillDeck();
            deck.Shuffle();

            // Deal a hand of 2 cards to both the player and the dealer
            foreach (Player player in players)
            {
                player.Hand = new Hand(2, deck);
            }
            dealerHand = new Hand(2, deck);

            // Keep dealing cards to the dealer until his score is more than 17
            while (dealerHand.getTotal() < 17)
            {
                dealerHand.addCard(deck.DealCard());
            }
        }
    }
}