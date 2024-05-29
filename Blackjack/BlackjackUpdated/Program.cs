using Blackjack;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        private List<Card> cards = new List<Card>();
        //static readonly List<Card> playerCards = new List<Card>();
        static Dictionary<List<Card>, int> playersCardsDictionary = new Dictionary<List<Card>, int>();
        //static int playerTotal = 0;
        static int playerCardCount = 1;
        static Stack<Card> DeckOfCards = CreateDeckOfCards();
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
                    //Currently, just get a value between 16-21 for the dealer
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        List<Card> playerCards = new List<Card>();
                        playerCards.Add(DealCard());
                        playerCards.Add(DealCard());
                        int playerTotal = playerCards[0].Value;
                        playerTotal += playerCards[1].Value;
                        playersCardsDictionary.Add(playerCards, playerTotal);
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
                foreach (var playerKey in playersCardsDictionary.Keys)
                {
                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());
                    dealerCardCount = 2;

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;
                    Console.WriteLine("The dealer's first card is : {0}", dealerCards[0].Name);
                    var player = playersCardsDictionary.FirstOrDefault(player => player.Key == playerKey);
                    Console.WriteLine("You were dealt the cards : {0} and {1} ", player.Key[0].Name, player.Key[1].Name);
                    Console.WriteLine("Your player total is {0} ", player.Value);
                    Console.WriteLine("Player {0}", playerNumber);
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

        private static void AskForHitOrStay(KeyValuePair<List<Card>, int> player)
        {
            var playerTotal = player.Value;
            do
            {
                Console.WriteLine("Would you like to (H)it or (S)tay?");
                playerChoice = Console.ReadLine().ToUpper();
                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit(player.Key);
                }
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

            if (playerChoice.Equals("S") || playerTotal == 21)
            {
                while (dealerTotal < 17 && dealerTotal != playerTotal)
                {
                    dealerCards.Add(DealCard());
                    dealerTotal += dealerCards[dealerCardCount].Value;
                    dealerCardCount += 1;
                }
                if (playerTotal > dealerTotal && playerTotal <= 21 || dealerTotal > 21)
                {
                    Console.WriteLine("Congrats! You won the game! The dealer's total is {0}", dealerTotal);
                }
                else if (playerTotal <= dealerTotal)
                {
                    Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                }
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        /*private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your player total is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }*/

        static Stack<Card> CreateDeckOfCards() {
            Stack<Card> deckOfCards = new Stack<Card>();
            string[] suits = { "Spades", "Clubs", "Hearts", "Diamonds"};
            foreach (var suit in suits) {
                for (int i = 1; i <= 13; i++) {
                    /*deckOfCards.Push(item: new Card() {
                        name = GetCardName();
                        value = GetCardValue();
                    });*/
                }
            }
            return deckOfCards;
        }

        static void Hit(List<Card> playerKey)
        {
            var player = playersCardsDictionary.FirstOrDefault(player => player.Key == playerKey);
            var playerCardCount = player.Key.Count;
            player.Key.Add(DealCard());

            playersCardsDictionary[playerKey] += player.Key[playerCardCount].Value;
            Console.WriteLine("Your card is a(n) {0} and your new Total is {1}. ", player.Key[playerCardCount].Name, playersCardsDictionary[playerKey]);

            var playerTotal = playersCardsDictionary[playerKey];
            if (playerTotal > 21)
            {
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

        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            /*playerTotal += cardValue;*/
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
                playerCardCount = 1;
                playersCardsDictionary = new Dictionary<List<Card>, int>();
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