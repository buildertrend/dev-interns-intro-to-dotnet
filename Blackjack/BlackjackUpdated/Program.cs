using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;
using static BlackjackUpdated.Card;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        static Shoe shoe = new Shoe(2);
        static List<Player> playerList = new List<Player>();

        static bool playing = true;
        //static readonly List<Card> playerCards = new List<Card>();
        //static int playerTotal = 0;
        private static readonly List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;

        //users to store the player choice (hit or stay)
        static string currentPlayerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                playing = true;

                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not start game!");
                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    playerList = DefinePlayers();

                    dealerCards.Add(shoe.Draw());
                    dealerCards.Add(shoe.Draw());
                    dealerTotal = dealerCards.Sum(x => x.Value);

                    foreach (Player player in playerList)
                    {
                        player.Deal();
                        if (player.playerScore.Equals(21))
                        {
                            Console.WriteLine("Player {0} got Blackjack! The dealer's Total was {0}.", dealerTotal);
                            player.staying = true;
                        }
                    }

                    DisplayWelcomeMessage();
                }
                else
                {
                    Console.WriteLine("Exiting due to invaid keypress...");
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                while (playing)
                {
                    for (int playerNum = 0; playerNum < playerList.Count; playerNum++)
                    {
                        Player player = playerList[playerNum];
                        player.AnnounceTurn();
                        while (!player.staying && !player.busted)
                        {
                            do
                            {
                                Console.WriteLine("Would you like to (H)it or (S)tay?");
                                currentPlayerChoice = Console.ReadLine().ToUpper();
                            }
                            while (!currentPlayerChoice.Equals("H") && !currentPlayerChoice.Equals("S"));

                            if (currentPlayerChoice.Equals("H"))
                            {
                                player.Hit();
                            }

                            if (currentPlayerChoice.Equals("S"))
                            {
                                player.staying = true;
                                continue;
                            }
                        }
                    }

                    // Dealer continues to hit until > 16 points.
                    while (dealerTotal < 16) { DealerHit(); }
                    Console.WriteLine("Dealer has 16 or more points. Staying...");

                    for (int playerNum = 0; playerNum < playerList.Count; playerNum++)
                    {
                        Player player = playerList[playerNum];
                        player.AnnounceTurn();

                        if (player.playerScore > dealerTotal && player.playerScore <= 21)
                        {
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                        }
                        if (player.playerScore <= dealerTotal)
                        {
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        }
                    }
                    playing = false;       
                }
            /* END GAME LOOP */
            }
        Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
        PlayAgain();
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("All cards have been dealt to the {0} players.", playerList.Count);
            Console.WriteLine("The dealer shows their first card, a(n) {0}", dealerCards[0].Name);
            Console.WriteLine("Their current known total is {0}", dealerCards[0].Value);
            Console.WriteLine();
            //Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            //Console.WriteLine("Your playerTotal is {0} ", playerTotal);
        }


        static void DealerHit()
        {
            Console.WriteLine("Dealer's score is < 16 - Hitting...");
            //dealerCardCount++;
            dealerCards.Add(shoe.Draw());
            dealerTotal += dealerCards.Last().Value;
            Console.WriteLine("Dealer received a(n): {0}", dealerCards.Last().Name);
            // Remove the hidden card from the known value.
            Console.WriteLine("Dealer's current (known) score: {0}", dealerTotal - dealerCards[1].Value);
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
                dealerCards.Clear();
                playerList.Clear();
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

        static List<Player> DefinePlayers()
        {
            Console.WriteLine("Enter the number of players: ");
            int numPlayers = Int16.Parse(Console.ReadLine());
            var players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                players.Add(new Player(i, shoe));
            }
            return players;
        }
    }
}