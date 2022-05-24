using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly int MAX_PLAYERS = 4;
        static readonly ConsoleColor DEALER_COLOR = ConsoleColor.Red;
        static readonly ConsoleColor[] PLAYER_COLORS = { ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Magenta };

        static readonly Card[,] playerCards = new Card[MAX_PLAYERS, 11];
        static int[] playerTotals = new int[MAX_PLAYERS];
        static int[] playerCardCounts = new int[MAX_PLAYERS];
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 1;

        static int numPlayers = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static List<Card> deck = new List<Card>();

        static void Main(string[] args)
        {
            for (int i = 1; i < 14; i++)
            {
                foreach (Suit s in Enum.GetValues(typeof(Suit)))
                {
                    Card c = Card.GetCardValue(i);
                    c.suit = s;
                    deck.Add(c);
                }
            }
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    Console.WriteLine("How many players are there?");
                    playerChoice = Console.ReadLine();
                    while (!Int32.TryParse(playerChoice, out numPlayers) || numPlayers > MAX_PLAYERS)
                    {
                        if (numPlayers > MAX_PLAYERS) Console.WriteLine("Too many players!");
                        Console.WriteLine("How many players are there?");
                        playerChoice = Console.ReadLine();
                    }

                    //Currently, just get a value between 16-21 for the dealer
                    dealerCards[0] = DealCard(Player.DEALER, 0);
                    dealerCards[1] = DealCard(Player.DEALER, 0);

                    for (int i = 0; i < numPlayers; i++)
                    {
                        playerCards[i,0] = DealCard(Player.PLAYER, i);
                        playerCards[i,1] = DealCard(Player.PLAYER, i);
                    }


                    //TODO: The dealer is dealt one card face up, one card face down.
                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                for (int i = 0; i < numPlayers; i++) 
                {
                    Console.ForegroundColor = PLAYER_COLORS[i];
                    Console.WriteLine($"Player {i + 1} is up.");
                    Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[i, 0].CardName(), playerCards[i, 1].CardName());
                    Console.WriteLine("Your playerTotal is {0} ", playerTotals[i]);
                    do
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit(i);
                    }
                    else if (playerChoice.Equals("S"))
                    {
                        if (i == numPlayers - 1) {
                            EndGame();
                        }
                    }
                }


                /* END GAME LOOP */
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = DEALER_COLOR;
            Console.WriteLine("The dealer was dealt a {0} and an unknown card.", dealerCards[0].CardName());
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(int playerId)
        {
            playerCardCounts[playerId] += 1;
            playerCards[playerId, playerCardCounts[playerId]] = DealCard(Player.PLAYER, playerId);
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerId, playerCardCounts[playerId]].CardName(), playerTotals[playerId]);

            //Is this true? I don't think it is.
            if (playerTotals[playerId].Equals(21))
            {
                Console.WriteLine("You got Blackjack!");
                if (playerId == numPlayers - 1)
                {
                    EndGame();
                }
            }
            else if (playerTotals[playerId] > 21)
            {
                Console.WriteLine("You busted! Sorry!");
                if (playerId == numPlayers - 1)
                {
                    EndGame();
                }
            }
            else if (playerTotals[playerId] < 21)
            {
                do
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(playerId);
                }
                else if (playerChoice.Equals("S"))
                {
                    if (playerId == numPlayers - 1)
                    {
                        EndGame();
                    }
                }
            }
        }

        enum Player
        {
            DEALER, PLAYER
        }

        static Card DealCard(Player player, int playerId)
        {
            int cardValue = cardRandomizer.Next(0, deck.Count);
            Card card = deck[cardValue];
            deck.Remove(card);
            if (player == Player.PLAYER) playerTotals[playerId] += card.Value;
            else if (player == Player.DEALER) dealerTotal += card.Value;
            return card;
        }

        static void EndGame()
        {
            Console.ForegroundColor = DEALER_COLOR;
            Console.WriteLine($"The dealer's second card is a {dealerCards[1].CardName()}. The dealer's total is {dealerTotal}.");
            while (dealerTotal < 17)
            {
                dealerCardCount += 1;
                dealerCards[dealerCardCount] = DealCard(Player.DEALER, 0);
                Console.WriteLine($"The dealer drew a {dealerCards[dealerCardCount].CardName()}. The dealer's new total is {dealerTotal}.");
            }
            for (int p = 0; p < numPlayers; p++)
            {
                Console.ForegroundColor = PLAYER_COLORS[p];
                if (playerTotals[p].Equals(21))
                {
                    Console.WriteLine($"You got Blackjack, Player {p + 1}! The dealer's Total was {dealerTotal}. ");
                }
                else if (playerTotals[p] > 21)
                {
                    Console.WriteLine($"You busted, Player {p + 1}! Sorry! The dealer's Total was {dealerTotal}.");

                }
                else if (playerTotals[p] > dealerTotal || (playerTotals[p] < dealerTotal && dealerTotal > 21))
                {
                    Console.WriteLine($"Congrats, Player {p + 1}! You won the game! The dealer's total is {dealerTotal}. ");
                }
                else if (playerTotals[p] < dealerTotal)
                {
                    Console.WriteLine($"Sorry, Player {p + 1}, you lost! The dealer's total was {dealerTotal}.");
                }
                else if (playerTotals[p] == dealerTotal)
                {
                    Console.WriteLine($"Whew, Player {p + 1}. You drew the dealer. The dealer's total was {dealerTotal}.");
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
                dealerTotal = 0;
                playerCardCounts = new int[MAX_PLAYERS];
                playerTotals = new int[MAX_PLAYERS];
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