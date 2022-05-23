using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static Player[] allPlayers;
        static List<Player> winningPlayers = new List<Player>();

        private static readonly Card[] dealerCards = new Card[2];
        static int dealerTotal = 0;

        static ConsoleColor defaultTextColor = Console.ForegroundColor;

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            Console.WriteLine("Before we begin Blackjack, enter 1 to play just against the dealer or enter the number of friends you'd like to play with.");
            var numPlayers = int.Parse(Console.ReadLine());
            allPlayers = new Player[numPlayers];

            for (int i = 0; i < numPlayers; i++)
            {
                // i + 1 for player numbers
                allPlayers[i] = new Player(i + 1);
            }

            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    // Deal cards and add values for dealer and players
                    dealerCards[0] = DealCardDealer();
                    dealerCards[1] = DealCardDealer();

                    for (int i = 0; i < allPlayers.Length; i++)
                    {
                        // Deal two cards
                        allPlayers[i].AddCardToHand(DealCard());
                        allPlayers[i].AddCardToHand(DealCard());
                    }

                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Player currentPlayer = allPlayers[i];
                    Console.WriteLine("\nPlayer {0}: ", currentPlayer.GetPlayerNum());

                    var playerChoice = "";
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

                    if (playerChoice.Equals("S"))
                    {
                        int playerTotal = currentPlayer.GetTotalPoints();

                        if (playerTotal > dealerTotal && playerTotal <= 21)
                        {
                            winningPlayers.Add(currentPlayer);
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nThe Dealer's Total was: {0}.\n", dealerTotal);
                Console.ForegroundColor = defaultTextColor;

                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Player currentPlayer = allPlayers[i];
                    if (winningPlayers.Contains(currentPlayer))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Player {0} wins with a total of {1}!", currentPlayer.GetPlayerNum(), currentPlayer.GetTotalPoints());
                        Console.ForegroundColor = defaultTextColor;
                    } 
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Player {0} loses with a total of {1}. :(", currentPlayer.GetPlayerNum(), currentPlayer.GetTotalPoints());
                        Console.ForegroundColor = defaultTextColor;
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
            for (int i = 0; i < allPlayers.Length; i++)
            {
                Player currentPlayer = allPlayers[i];
                Console.WriteLine("Player {0} was dealt the cards: {1} and {2}.", currentPlayer.GetPlayerNum(), currentPlayer.GetCards()[0].Name, currentPlayer.GetCards()[1].Name);
                Console.WriteLine("Player {0}'s Total is {1}.\n", currentPlayer.GetPlayerNum(), currentPlayer.GetTotalPoints());
            }

            // Inform the player the value of the dealer's visible card.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("The dealer was dealt a(n) {0}.", dealerCards[0].Name);
            Console.ForegroundColor = defaultTextColor;
        }

        static void Hit(Player player)
        {
            player.AddCardToHand(DealCard());
            Console.WriteLine("\nPlayer {0}'s card is a(n) {1} and their new Total is {2}.", player.GetPlayerNum(), player.GetMostRecentCard().Name, player.GetTotalPoints());

            //Is this true? I don't think it is.
            int playerTotal = player.GetTotalPoints();
            if (playerTotal.Equals(21))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Player {0} got Blackjack!", player.GetPlayerNum());
                Console.ForegroundColor = defaultTextColor;
                winningPlayers.Add(player);
            }
            else if (playerTotal > 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Player {0} busted! Sorry!", player.GetPlayerNum());
                Console.ForegroundColor = defaultTextColor;
            }
            else if (playerTotal < 21)
            {
                Console.WriteLine("Player {0}: ", player.GetPlayerNum());
                var playerChoice = "";
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
        }

        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            return GetCardValue(cardValue);
        }

        static Card DealCardDealer()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            dealerTotal += (cardValue + 1);
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

                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Player currentPlayer = allPlayers[i];
                    currentPlayer.Reset();
                }
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