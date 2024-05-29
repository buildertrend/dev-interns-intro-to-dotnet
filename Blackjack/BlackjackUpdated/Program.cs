using Blackjack;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {

        private static List<Card> dealerCards = new();
        static int dealerTotal = 0;

        private static List<Card> cards = new();

        static int numberOfPlayers = 0;
        private static readonly Dictionary<int, List<int>> playerDict = new();

        static string playerChoice = "";
        static string playAgain = "Y";

        static int cardCount = 0;
        static bool showCardCount = false;

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //Build deck of cards here and shuffle
                
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error");
                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    Console.WriteLine("How many players are playing? (1-5)");
                    numberOfPlayers = int.Parse(Console.ReadLine());
                    Console.WriteLine("Do you want to count cards? (Y)es or (N)o?");
                    string CountCardsInput = Console.ReadLine().ToUpper();
                    if (CountCardsInput == "Y")
                    {
                        showCardCount = true;
                    }

                    cards = GenerateDeck();

                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());
                    dealerTotal = dealerCards[0].Value + dealerCards[1].Value;
                    Console.WriteLine("The dealer's first card is " + dealerCards[0].Value);

                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        List<int> cardsDelt = new()
                        {
                            DealCard().Value,
                            DealCard().Value
                        };

                        playerDict[i] = cardsDelt;
                    }

                    DisplayWelcomeMessage(numberOfPlayers, playerDict);
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    do
                    {
                        Console.WriteLine("Would Player " + i + " like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                    if (playerChoice.Equals("H"))
                    {
                        Hit(i);
                    }  
                }
                Console.WriteLine("The dealer's second card is " + dealerCards[1].Value);

                Console.WriteLine("---------------------------------------------------------------------");

                while (dealerTotal < 17)
                {
                    Card card = DealCard();
                    dealerTotal = dealerTotal + card.Value;
                    Console.WriteLine("The dealer's next card is " + card.Value);

                    if (dealerTotal > 21)
                    {
                        Console.WriteLine("Every Player Wins. The dealer busted and the total was " + dealerTotal);
                    }
                }

                if (dealerTotal < 21)
                {
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        if (playerDict[i].Sum() > dealerTotal && playerDict[i].Sum() <= 21)
                        {
                            Console.WriteLine("Congrats! Player " + i + " won the game! The dealer's total is " + dealerTotal);
                        }
                        else if (playerDict[i].Sum() < dealerTotal)
                        {
                            Console.WriteLine("Sorry, Player " + i + " lost! The dealer's total was " + dealerTotal);
                        }
                        else if (playerDict[i].Sum() > 21)
                        {
                            Console.WriteLine("Sorry, Player " + i + " busted! The dealer's total was " + dealerTotal);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, Player " + i + ". It was a push! The dealer's total was {0}", dealerTotal);
                        }
                    }
                }

                Console.WriteLine("---------------------------------------------------------------------");


                /* END GAME LOOP */

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage(int numberOfPlayers, Dictionary<int, List<int>> playerDict)
        {
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.WriteLine("Player " + i + " were dealt the cards: " + string.Join(", ", playerDict[i]));
                Console.WriteLine("Player " + i + " has " + playerDict[i].Sum());
            }
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(int player)
        {
            Card card = DealCard();
            playerDict[player].Add(card.Value);
            Console.WriteLine("Player " + player + "'s card is a(n) {0} and your new Total is {1}. ", card.Name, playerDict[player].Sum());

            if (playerDict[player].Sum() > 21)
            {
                Console.WriteLine("You busted! Sorry!");

            }
            else if (playerDict[player].Sum() < 21)
            {
                do
                {
                    Console.WriteLine("Would player " + player + " like to hit or stay? h for hit s for stay");
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
            Random rnd = new();
            int index = rnd.Next(cards.Count);
            Card card = cards[index];
            cards.RemoveAt(index);
            if (showCardCount)
            {
                CalculateCardCount(card.Value);
            }
            
            return card;
        }

        static void CalculateCardCount(int cardValue)
        {
            if (cardValue >= 2 && cardValue <= 6)
            {
                cardCount++;
            } else if (cardValue >= 10)
            {
                cardCount--;
            }
            Console.WriteLine("The count is " + cardCount);

        }

        static List<Card> GenerateDeck()
        {

            return new List<Card>() {
                new() { Name = "Two", Value = 2, Suit = "Hearts" },
                new() { Name = "Two", Value = 2, Suit = "Diamonds" },
                new() { Name = "Two", Value = 2, Suit = "Clubs" },
                new() { Name = "Two", Value = 2, Suit = "Spades" },
                new() { Name = "Three", Value = 3, Suit = "Hearts" },
                new() { Name = "Three", Value = 3, Suit = "Diamonds" },
                new() { Name = "Three", Value = 3, Suit = "Clubs" },
                new() { Name = "Three", Value = 3, Suit = "Spades" },
                new() { Name = "Four", Value = 4, Suit = "Hearts" },
                new() { Name = "Four", Value = 4, Suit = "Diamonds" },
                new() { Name = "Four", Value = 4, Suit = "Clubs" },
                new() { Name = "Four", Value = 4, Suit = "Spades" },
                new() { Name = "Five", Value = 5, Suit = "Hearts" },
                new() { Name = "Five", Value = 5, Suit = "Diamonds" },
                new() { Name = "Five", Value = 5, Suit = "Clubs" },
                new() { Name = "Five", Value = 5, Suit = "Spades" },
                new() { Name = "Six", Value = 6, Suit = "Hearts" },
                new() { Name = "Six", Value = 6, Suit = "Diamonds" },
                new() { Name = "Six", Value = 6, Suit = "Clubs" },
                new() { Name = "Six", Value = 6, Suit = "Spades" },
                new() { Name = "Seven", Value = 7, Suit = "Hearts" },
                new() { Name = "Seven", Value = 7, Suit = "Diamonds" },
                new() { Name = "Seven", Value = 7, Suit = "Clubs" },
                new() { Name = "Seven", Value = 7, Suit = "Spades" },
                new() { Name = "Eight", Value = 8, Suit = "Hearts" },
                new() { Name = "Eight", Value = 8, Suit = "Diamonds" },
                new() { Name = "Eight", Value = 8, Suit = "Clubs" },
                new() { Name = "Eight", Value = 8, Suit = "Spades" },
                new() { Name = "Nine", Value = 9, Suit = "Hearts" },
                new() { Name = "Nine", Value = 9, Suit = "Diamonds" },
                new() { Name = "Nine", Value = 9, Suit = "Clubs" },
                new() { Name = "Nine", Value = 9, Suit = "Spades" },
                new() { Name = "Ten", Value = 10, Suit = "Hearts" },
                new() { Name = "Ten", Value = 10, Suit = "Diamonds" },
                new() { Name = "Ten", Value = 10, Suit = "Clubs" },
                new() { Name = "Ten", Value = 10, Suit = "Spades" },
                new() { Name = "Jack", Value = 10, Suit = "Hearts" },
                new() { Name = "Jack", Value = 10, Suit = "Diamonds" },
                new() { Name = "Jack", Value = 10, Suit = "Clubs" },
                new() { Name = "Jack", Value = 10, Suit = "Spades" },
                new() { Name = "Queen", Value = 10, Suit = "Hearts" },
                new() { Name = "Queen", Value = 10, Suit = "Diamonds" },
                new() { Name = "Queen", Value = 10, Suit = "Clubs" },
                new() { Name = "Queen", Value = 10, Suit = "Spades" },
                new() { Name = "King", Value = 10, Suit = "Hearts" },
                new() { Name = "King", Value = 10, Suit = "Diamonds" },
                new() { Name = "King", Value = 10, Suit = "Clubs" },
                new() { Name = "King", Value = 10, Suit = "Spades" },
                new() { Name = "Ace", Value = 11, Suit = "Hearts" },
                new() { Name = "Ace", Value = 11, Suit = "Diamonds" },
                new() { Name = "Ace", Value = 11, Suit = "Clubs" },
                new() { Name = "Ace", Value = 11, Suit = "Spades" },
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