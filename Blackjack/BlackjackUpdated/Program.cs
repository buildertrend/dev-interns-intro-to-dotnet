using Blackjack;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;


namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly List<Card> playerCards = new List<Card>();
        //Add functionality for more than one player
        static readonly List<Player> playerStats = new List<Player>();

        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static int playerNum = 0;


        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        private static readonly List<Card> deck = new List<Card>();

  
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
                    Console.WriteLine("hi");
                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {

                    try
                    {
                        Console.WriteLine("How many players are playing? (1-5)");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("hi");
                    }

                    playerNum = Convert.ToInt32(Console.ReadLine());

                    //Create Deck of Cards
                    CreateDeckOfCards();

                    //Deal all players out
                    for (int i = 0; i < playerNum; i++)
                    {
                        //Currently, just get a value between 16-21 for the dealer
                        //dealerTotal = cardRandomizer.Next(15, 22);
                        playerCards.Add(DealCard());
                        playerCards.Add(DealCard());


                        playerTotal += playerCards[0].Value;
                        playerTotal += playerCards[1].Value;

                        playerStats.Add(new Player() { Total = playerTotal, State = "Dealt" });

                        Console.WriteLine("Player {0} ", i + 1);
                        DisplayWelcomeMessage();

                        playerTotal = 0;
                        playerCards.Clear();

                    }


                    //The dealer is dealt one card face up, one card face down.
                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;


                    Console.WriteLine("The dealer's visible card is {0} ", dealerCards[1].Name);

                }
                else
                {
                    Environment.Exit(0);
                }

                for (int i = 0; i < playerNum; i++)
                {
                    //Check to see if the player has a black jack right away that ends the game
                    Boolean trueBlackJack = false;
                    if (playerStats[i].Total == 21)
                    {
                        trueBlackJack = true;

                    }


                    //if there is a true blackjack no need to ask whether the player wants to hit or stay 
                    if (!trueBlackJack)
                    {
                        /* START GAME LOOP */
                        do
                        {
                            Console.WriteLine("Player {0} Would you like to (H)it or (S)tay?", i + 1);
                            playerChoice = Console.ReadLine().ToUpper();
                        }

                        while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                        if (playerChoice.Equals("H"))
                        {
                            //hit will get them a card / check the total and ask for another hit
                            Hit();
                        }

                        if (playerChoice.Equals("S"))
                        {
                            playerStats[i].State = "Done";

                            //Deal the dealer out if their current total is less than the player total
                            DealDealerOut();
                            //Check if the dealer busted 
                            if (dealerTotal > 21)
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.WriteLine("Congrats! You won the game! The dealer busted with {0}", dealerTotal);
                                Console.ResetColor();

                            }
                            else if (playerTotal < dealerTotal)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                                Console.ResetColor();

                            }
                            else if (playerTotal == dealerTotal)
                            {
                                Console.WriteLine("Sorry, you lost! You tied with the dealer. The dealer's total was {0}", dealerTotal);
                            }
                        }
                    }

                    else
                    {
                        //Check to see if the dealer also got blackjack to determine winner
                        if (playerTotal == 21 && playerTotal != dealerTotal)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine("Congrats! You got Blackjack and won the game!");
                            Console.ResetColor();
                        }
                        else if (playerTotal == dealerTotal)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Sorry, you lost! You and the dealer tied");
                            Console.ResetColor();
                        }

                    }
                }

                /* END GAME LOOP */

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        private static void CreateDeckOfCards()
        {
            string[] Name = { "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "jack", "queen", "king", "ace" };
            string[] Suit = { "spade", "club", "diamond", "heart" };
            int[] Value = {2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            int j = 0;
            foreach (String name in Name)
            {
                foreach (String suit in Suit)
                {
                    deck.Add(new Card() { Name = name, Suit = suit, Value = Value[j] });
                }
                j++;
            }

        }

        private static void DealDealerOut()
        {
            int i = 2;
            if (dealerTotal < playerTotal && playerTotal <= 21 && i < 11)
            {
                while (dealerTotal < 21)
                {
                    dealerCards.Add(DealCard());
                    dealerTotal += dealerCards[i].Value;
                    i++;
                }

            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);

        }

        static void Hit()
        {
            
            playerCards.Add(DealCard());
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            //Is this true? I don't think it is.
            //if (playerTotal.Equals(21))
            //{
                //Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            //}
            if (playerTotal > 21)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("You busted! Sorry, you lost! The dealer's Total was {0}", dealerTotal);
                Console.ResetColor();
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
            Random rng = new Random();
            int num = rng.Next(0, deck.Count);

            Card card = new Card() {Name = deck[num].Name, Value = deck[num].Value };
            deck.RemoveAt(num);

            return card;
        
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