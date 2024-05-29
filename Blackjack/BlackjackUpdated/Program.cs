using Blackjack;
using System.Reflection;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static readonly List<Card> deck = new List<Card>();
        static readonly List<Card> playerCards = new List<Card>();
        static int playerTotal = 0;
        private static List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        private static readonly List<string> suits = new List<string> { "Hearts", "Clubs", "Diamonds", "Spades" };
        private static List<Player> players = new List<Player> { new Player() };


        static string playAgain = "Y";

        static void Main(string[] args)
        {

            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            Game game = new Game();
            bool playingGame = true;
            while (playingGame)
            {
                //StartGame
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                    game.SetInitialGameState();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occured during runtime: ", ex);

                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    game.setInitialDealerCards();
                    game.askForPlayerCount();
                    game.dealInitialCardsToPlayers();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                foreach (Player player in game.players)
                {
                    game.PlayerTurn(player);
                }
                game.DetermineGameWinners();
                /* END GAME LOOP */
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                playingGame = game.PlayAgain();
            }
        }


    }
}