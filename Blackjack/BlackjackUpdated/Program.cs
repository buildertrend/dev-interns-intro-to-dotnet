using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly List<Card> playerCards = new List<Card>();
        //static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly List<Card> dealerCards = new List<Card>();
        //private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static bool firstBlackjack = false;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        // deck

        

        static string[] suit = {"clubs", "spades", "hearts", "diamonds"};
        static string[] name = {"two", "three", "four","five","six", "seven", "eight", "nine", "ten", "Jack", "Queen", "King", "Ace"};
        static int[] value = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

    static void Main(string[] args)
        {
            List<Card> deck = new List<Card>();         // maybe stack for simplicity?
            int w = 0;
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                try
                {

                    // create deck

                    foreach(string s in suit)
                    {
                        foreach(string n in name)
                        {
                            deck.Add(new Card { Name = n, Suit = s, Value = value[w] });
                            w++;
                        }
                        w = 0;
                    }

                    // shuffle deck
                  
                    
                    //var shuffledDeck = deck.OrderBy(_ => _rand.Next()).ToList();
                    
                    


                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)esss (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("hi");
                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {

                    Console.WriteLine("Number of Players: ");
                    var playerAmount = Console.ReadLine().ToUpper();
                    



                    //Currently, just get a value between 16-21 for the dealer
                    //dealerTotal = cardRandomizer.Next(15, 22);

                    //playerCards[0] = DealCard();
                    //playerCards[1] = DealCard();

                    playerCards.Add(DealCard());
                    playerCards.Add(DealCard());

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;

                    if(playerTotal == 21) { firstBlackjack = true; }


                    //TODO: The dealer is dealt one card face up, one card face down.
                    //dealerCards[0] = DealCard();
                    //dealerCards[1] = DealCard();

                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    dealerCardCount = 2;
                    

                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                do
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S") && !firstBlackjack);

                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit();
                }

                if (playerChoice.Equals("S") && !firstBlackjack)
                {

                    if (playerTotal > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                        Console.ResetColor();
                    }
                    else
                    {

                        int i = 2;
                        //while (playerTotal > dealerTotal && playerTotal <= 21 && i < 11)
                        while (dealerTotal < 17 && i < 11)
                        {
                            dealerCards[i-1] = DealCard();
                            dealerTotal += dealerCards[i-1].Value;
                            dealerCardCount++;
                            //Console.WriteLine(dealerTotal);
                        }

                        if (playerTotal > dealerTotal && playerTotal <= 21)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                            Console.ResetColor();

                        }
                        else if (playerTotal < dealerTotal && dealerTotal <= 21)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                            Console.ResetColor();
                        }
                        else if (dealerTotal == playerTotal)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                            Console.ResetColor();
                        }

                    }
                    
                }

                if (firstBlackjack)
                {
                    if(dealerTotal == 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0}", dealerTotal);
                        Console.ResetColor();
                    }
                    firstBlackjack = false;
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


            //TODO: Inform the player the value of the dealer's visible card.

            Console.WriteLine("Dealer's first card is {0} ", dealerCards[0].Name);
           
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards.Add(DealCard());
            playerTotal += playerCards[playerCardCount-1].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount-1].Name, playerTotal);

            if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                // show dealer's cards?

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

        //TODO: Move this class to it's own file.
        //private class Card
        //{
        //    public int Value;
        //    public string Name;
        //}

        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            //playerTotal += cardValue;
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
                playerTotal = 0;
                playerCards.Clear();
                dealerCards.Clear();
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