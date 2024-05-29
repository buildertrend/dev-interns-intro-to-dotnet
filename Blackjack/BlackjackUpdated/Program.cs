using Blackjack;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        // Initialize a deck
        private static Deck deck = new Deck();

        // Lists to store dealer and player cards
        private static readonly List<Card> dealerCards = new List<Card>();
        private static readonly List<Card> playerCards = new List<Card>();

        // Variables to track totals and card counts
        static int dealerTotal = 0;
        static int playerTotal = 0;
        static int playerCardCount = 0;
        static int dealerCardCount = 0;

        // Flag to indicate if it's the dealer's turn
        static bool dealersTurn = false;

        // Variable to store player's choice (hit or stay)
        static string playerChoice = "";

        // Variable to prompt user to play again
        static string playAgain = "Y";

        static void Main(string[] args)
        {
            // Set up console control handler
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            // Main game loop
            while (playAgain.ToUpper() == "Y")
            {
                // Prompt user to start the game
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write to console: {ex.Message}");
                }

                // Get user's decision to start the game
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    // Prepare deck
                    deck.Populate(); // Populate the deck
                    deck.Shuffle(deck.cards);

                    // Deal initial cards to dealer and player
                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());
                    playerCards.Add(DealCard());
                    playerCards.Add(DealCard());

                    // Calculate initial totals for dealer and player
                    dealerTotal += dealerCards[0].Value + dealerCards[1].Value;
                    playerTotal += playerCards[0].Value + playerCards[1].Value;

                    // Display welcome message and check for initial Blackjack or bust
                    DisplayWelcomeMessage();

                    // Check if player got Blackjack or busted
                    if (playerTotal == 21)
                    {
                        Console.WriteLine("\nYou got Blackjack!");
                        Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                        PlayAgain();
                    }
                    if (playerTotal > 21)
                    {
                        Console.WriteLine("\nYou busted!");
                        Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                        PlayAgain();
                    }
                }
                else
                {
                    // Exit the game if the player chooses not to play
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                do
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?\n");
                    playerChoice = Console.ReadLine().ToUpper();

                    if (playerChoice.Equals("H"))
                    {
                        // Perform a hit
                        Hit();
                    }
                }
                while (!playerChoice.Equals("S") && playerTotal <= 21);

                // If player chooses to stay, it's the dealer's turn
                if (playerChoice.Equals("S"))
                {
                    dealersTurn = true;

                    // Dealer's turn - keep hitting until their total is greater than player's total
                    while (dealerTotal < playerTotal)
                    {
                        Hit();
                    }

                    // Determine the winner and display the result
                    if (dealerTotal > 21 || playerTotal <= 21 && dealerTotal <= 21 && playerTotal > dealerTotal)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    }
                    else if (playerTotal < dealerTotal || playerTotal == dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                    }
                }

                /* END GAME LOOP */

                // Prompt user to play again
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            // Display player's cards and total, and dealer's visible card
            Console.WriteLine("You were dealt the cards : {0} of {1} and {2} of {3} ", playerCards[0].Name, playerCards[0].Suit, playerCards[1].Name, playerCards[1].Suit);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
            Console.WriteLine("Dealer's visible card: {0} of {1}", dealerCards[0].Name, dealerCards[0].Suit);
        }

        // Method to handle a hit
        static void Hit()
        {
            if (dealersTurn)
            {
                // If it's the dealer's turn, deal a card to the dealer
                dealerCardCount += 1;
                dealerCards.Add(DealCard());
                dealerTotal += dealerCards[dealerCards.Count - 1].Value;
                Console.WriteLine("The dealer's card is a(n) {0} of {1} and their new Total is {2}. ", dealerCards[dealerCards.Count - 1].Name, dealerCards[dealerCards.Count - 1].Suit, dealerTotal);

                // Check if the dealer busted
                if (dealerTotal > 21)
                {
                    Console.WriteLine("The dealer busted! Their total is {0}", dealerTotal);
                }
            }
            else
            {
                // If it's the player's turn, deal a card to the player
                playerCardCount += 1;
                playerCards.Add(DealCard());
                playerTotal += playerCards[playerCards.Count - 1].Value;
                Console.WriteLine("Your card is a(n) {0} of {1} and your new Total is {2}. ", playerCards[playerCards.Count - 1].Name, playerCards[playerCards.Count - 1].Suit, playerTotal);

                // Check if the player busted
                if (playerTotal > 21)
                {
                    Console.WriteLine("You busted! Sorry!");
                }
            }

        }

        // Method to handle user's choice to play again
        static void PlayAgain()
        {
            // Loop until the user makes a valid choice
            do
            {
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            // If user chooses to play again
            if (playAgain.Equals("Y"))
            {
                // Reset game state
                dealersTurn = false;
                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();
                dealerTotal = 0;
                playerCardCount = 1;
                playerTotal = 0;
            }
            // If user chooses not to play again
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

        // Method to deal the top card of the shuffled deck
        static Card DealCard()
        {
            return deck.cards.Pop();
        }
        
    }
}