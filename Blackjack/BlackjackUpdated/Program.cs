using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    partial class Program
    {
        static int numPlayers = 0;
        static Player[]? players = null; 
        static Deck deck = new Deck();

        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";
        static string playAgain = "Y";

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception)
                {
                    Console.WriteLine("hi");
                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    // Identify the number of players in the game
                    bool converted = false;
                    while (!converted)
                    {
                        Console.WriteLine("Enter the number of players to include in the game");
                        string input = Console.ReadLine();
                        converted = int.TryParse(input, out numPlayers);
                    }
                    players = new Player[numPlayers];

                    // Deal to the dealer first
                    dealerCards[0] = deck.draw();
                    dealerCards[1] = deck.draw();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    // Deal to each one of the players
                    for (int i = 0; i < numPlayers; i++)
                    {
                        Player player = new Player();
                        player.cards[0] = deck.draw();
                        player.cards[1] = deck.draw();
                        player.total += player.cards[0].Value;
                        player.total += player.cards[1].Value;
                        players[i] = player;
                    }
                    
                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                for (int playerNum = 0; playerNum < numPlayers; playerNum++)
                {
                    Player currentPlayer = players[playerNum];
                    if (currentPlayer.busted) { continue; }

                    Console.WriteLine();
                    Console.WriteLine("Current turn: Player {0}", playerNum + 1);
                    DisplayPlayerCards(currentPlayer);

                    do
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit(currentPlayer);
                    }

                    // TODO: Only display this once all players have either stayed or busted.
                    if (playerChoice.Equals("S"))
                    {
                        if (currentPlayer.total > dealerTotal && currentPlayer.total <= 21)
                        {
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                        }
                        else if (currentPlayer.total < dealerTotal)
                        {
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        }
                    }
                }
                /* END GAME LOOP */

                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("The dealer's first card is: {0}.", dealerCards[0].Name);
        }

        private static void DisplayPlayerCards(Player player)
        {
            Console.WriteLine("You were dealt the cards : {0} and {1} ", player.cards[0].Name, player.cards[1].Name);
            Console.WriteLine("Your player total is {0} ", player.total);
        }

        /// <summary>
        /// Checks if there are any aces in the hand. These are automatically set to a value of 11 (hard ace),
        /// but can be set to a 1 to prevent the player from busting.
        /// </summary>
        private static void AlterAces(Player player)
        {
            bool alteredFlag = false;
            int currPlayerTotal = 0;
            
            for (int i = 0; i < player.cards.Length; i++)
            {
                if (player.cards[i] == null) { continue; }
                if (player.cards[i].Equals(new Card() { Name = "Ace", Value = 11 }))
                {
                    // Switch the ace to soft.
                    player.cards[i] = new Card() { Name = "Ace", Value = 1 };
                    currPlayerTotal += 1;
                    Console.WriteLine("Your card #{0} has been changed from a hard ace to a soft ace.", i);
                    alteredFlag = true;
                }
                else
                {
                    currPlayerTotal += player.cards[i].Value;
                }
            }
            if (alteredFlag)
            {
                Console.WriteLine("Your new hand total is: {0}", player.total);
            }
        }

        static void Hit(Player player)
        {
            player.cardCount += 1;
            player.cards[player.cardCount] = deck.draw();
            player.total += player.cards[player.cardCount].Value;
            Console.WriteLine("Your card is a(n) {0} and your new total is {1}. ", player.cards[player.cardCount].Name, player.total);

            //Is this true? I don't think it is.
            if (player.total.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's total was {0}. ", dealerTotal);

            }
            else if (player.total > 21)
            {
                AlterAces(player);
                // Still check to see if we're over 21.
                if (player.total > 21)
                {
                    Console.WriteLine("You busted! Sorry!");
                    return;
                }
            }

            // We're safe. Ask to hit again if desired.
            do
            {
                Console.WriteLine("Would you like to hit or stay? h for hit s for stay");
                playerChoice = Console.ReadLine().ToUpper();
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
            if (playerChoice.ToUpper() == "H")
            {
                Hit(player);
            }
        }

        static void PlayAgain()
        {
            //Loop until they make a valid choice
            do
            {
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            if (playAgain.Equals("Y"))
            {
                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();
                dealerTotal = 0;
                // playerCardCount = 1;
                // playerTotal = 0;
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