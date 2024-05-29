using Blackjack;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        
        private List<Card> cards = new List<Card>();
        //static readonly List<Card> playerCards = new List<Card>();
        static Dictionary<int, Player> playersDictionary = new Dictionary<int, Player>();
        //static int playerTotal = 0;
        static int playerCardCount = 1;
        static CardDeck DeckOfCards = new CardDeck();
        private static List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static int numberOfPlayers = 1;

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
                catch (Exception ex)
                {
                    Console.WriteLine("Couldn't start game");
                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    
                    Console.WriteLine("How many players are at the table?");
                    var playersAsString = Console.ReadLine().ToUpper();
                    numberOfPlayers = int.Parse(playersAsString);
                    DeckOfCards.ShuffleCards();

                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        List<Card> playerCards = new List<Card>();
                        playerCards.Add(DeckOfCards.DealCard());
                        playerCards.Add(DeckOfCards.DealCard());
                        int playerTotal = playerCards[0].Value;
                        playerTotal += playerCards[1].Value;
                        var player = new Player(i + 1, playerCards, playerTotal);
                        playersDictionary.Add(i + 1, player);
                    }
               
                    //dealerTotal = cardRandomizer.Next(15, 22);

                    /*if (playerTotal.Equals(21))
                    {
                        Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

                    }*/


                    //TODO: The dealer is dealt one card face up, one card face down.
                    
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                var playerNumber = 1;
                foreach (var playerKey in playersDictionary.Keys)
                {
                    dealerCards.Add(DeckOfCards.DealCard());
                    dealerCards.Add(DeckOfCards.DealCard());
                    dealerCardCount = 2;

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;
                    Console.WriteLine("The dealer's first card is : {0}", dealerCards[0].Name);
                    var player = playersDictionary[playerKey];
                    Console.WriteLine("You were dealt the cards : {0} and {1} ", player.PlayerCards[0].Name, player.PlayerCards[1].Name);
                    Console.WriteLine("Your player total is {0} ", player.PlayerTotal);
                    Console.WriteLine("Player {0}", player.PlayerID);
                    AskForHitOrStay(player);
                    playerNumber += 1;
                    dealerCards = new List<Card>();
                    dealerTotal = 0;
                }

                /* END GAME LOOP */

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        private static void AskForHitOrStay(Player player)
        {
            var playerTotal = player.PlayerTotal;
            do
            {
                Console.WriteLine("Would you like to (H)it or (S)tay?");
                playerChoice = Console.ReadLine().ToUpper();
                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit(player.PlayerID);
                }
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

            if (playerChoice.Equals("S") || playerTotal == 21)
            {
                while (dealerTotal < 17 && dealerTotal != playerTotal)
                {
                    dealerCards.Add(DeckOfCards.DealCard());
                    dealerTotal += dealerCards[dealerCardCount].Value;
                    dealerCardCount += 1;
                }
                if (playerTotal > dealerTotal && playerTotal <= 21 || dealerTotal > 21)
                {
                    Console.WriteLine("Congrats! You won the game! The dealer's total is {0}", dealerTotal);
                }
                else if (playerTotal <= dealerTotal)
                {
                    Console.WriteLine(@"
                         __     __             _                       
                         \ \   / /            | |                      
                          \ \_/ /___   _   _  | |      ___   ___   ___ 
                           \   // _ \ | | | | | |     / _ \ / __| / _ \
                            | || (_) || |_| | | |____| (_) |\__ \|  __/
                            |_| \___/  \__,_| |______|\___/ |___/ \___|
                                               
                                               
                        ");
                    Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                }
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>

        static void Hit(int playerKey)
        {
            var player = playersDictionary[playerKey];
            var playerCardCount = player.PlayerCards.Count;
            var newCard = DeckOfCards.DealCard();
            player.PlayerCards.Add(newCard);

            playersDictionary[playerKey].PlayerTotal += newCard.Value;
            Console.WriteLine("Your card is a(n) {0} and your new Total is {1}. ", newCard.Name, playersDictionary[playerKey].PlayerTotal);

            var playerTotal = playersDictionary[playerKey].PlayerTotal;
            if (playerTotal > 21)
            {
                Console.WriteLine(@"
                         __     __             _                       
                         \ \   / /            | |                      
                          \ \_/ /___   _   _  | |      ___   ___   ___ 
                           \   // _ \ | | | | | |     / _ \ / __| / _ \
                            | || (_) || |_| | | |____| (_) |\__ \|  __/
                            |_| \___/  \__,_| |______|\___/ |___/ \___|
                                               
                                               
                        ");
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            else if (playerTotal < 21)
            {
                do
                {
                    Console.WriteLine("Would you like to hit or stay? h for hit s for stay");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(playerKey);
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
                playerCardCount = 1;
                playersDictionary = new Dictionary<int, Player>();
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