using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 0;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";
        // variable keeping track if they got a black jack
        static bool blackJack = false;

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
                    Console.WriteLine("An exception occured during runtime: ", ex);

                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    dealerCards[dealerCardCount] = DealCard();
                    dealerCardCount++;
                    dealerCards[dealerCardCount] = DealCard();
                    dealerCardCount++;
                    playerCards[playerCardCount] = DealCard();
                    playerCardCount++;
                    playerCards[playerCardCount] = DealCard();
                    playerCardCount++;

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;
                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;
                    DisplayWelcomeMessage();
                    if (playerTotal.Equals(21))
                    {
                        blackJack = true;
                    }
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                if (blackJack)
                {
                    Console.WriteLine("Congrats you got a BlackJack! You won the game!");
                } else {
                    do
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S") && !blackJack);

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit();
                    }

                    if (playerChoice.Equals("S"))
                    {
                        if (playerTotal > dealerTotal && playerTotal <= 21)
                        {
                            do
                            {
                                dealerCards[dealerCardCount] = DealCard();
                                dealerTotal += dealerCards[dealerCardCount].Value;
                                dealerCardCount++;
                            }
                            while (dealerTotal < playerTotal);
                            if (dealerTotal.Equals(playerTotal))
                            {
                                Console.WriteLine("Sorry, you lost because the dealer tied with you! The dealer's total was {0}", dealerTotal);
                            }
                            else
                            {
                                Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                            }
                        }
                        else if (playerTotal < dealerTotal)
                        {
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        }
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
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            Console.WriteLine("The dealer has the card : {0} ", dealerCards[0].Name);

        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = DealCard();
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);
            if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            else if (playerTotal <= 21)
            {
                do
                {
                    Console.WriteLine("Would you like to hit or stay? h for hit s for stay");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit();
                }
            }
        }

        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
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
                playerCardCount = 0;
                playerTotal = 0;
                dealerCardCount = 0;
                blackJack = false;
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