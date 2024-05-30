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
        // Initialize Deck
        private static Deck deck = new Deck();

        // Initialize Dealer
        private static Dealer dealer = new Dealer();

        // List storing the Players
        private static List<Player> players = new List<Player>();

        // Variable to hold number of players (1 by default)
        private static int numPlayers = 1;

        private static Game game;

        // Variable to store player's choice (hit or stay)
        private static string playerChoice = "";

        // Variable to prompt user to play again
        private static string playAgain = "Y";
        

        static void Main(string[] args)
        {
            // Set up console control handler
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
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
                do
                {
                    do
                    {
                        Console.WriteLine("How many players are there? - (1 - 4)");
                        numPlayers = int.Parse(Console.ReadLine());
                    }
                    while (numPlayers < 1 || numPlayers > 4);

                    players = util.initPlayers(numPlayers);

                    game = new Game() { Deck = deck, Players = players, Dealer = dealer };

                    // Populate and shuffle the deck
                    game.Deck.Populate();
                    game.Deck.Shuffle();

                    int playerIndex = 1;
                    foreach (Player player in game.Players)
                    {
                        Console.WriteLine("\nPlayer {0}'s Turn:\n", playerIndex);
                        util.DisplayWelcomeMessage(player, game);

                        util.checkBlackjack(player);
                        util.checkBust(player);

                        util.playerTurn(player, game);
                        playerIndex++;
                    }

                    // Prompt user to play again
                    Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                } while (util.PlayAgain(game));

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
            else
            {
                // Exit the game if the player chooses not to play
                Environment.Exit(0);
            }
        }
    }
}