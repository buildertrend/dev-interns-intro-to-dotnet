using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        

        //static readonly Card[] playerCards = new Card[11];
        static readonly List<Card> playerCards = new List<Card>(); 
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        static int dealerCardCount = 1;
        static int dealerMinTotal = 0;
        

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {   Random cardPicker = new Random();

            List<Card> deck = new List<Card>();
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
                    deck = Card.BuildDeck();
                    deck = shuffleDeck(deck);
                    deck = shuffleDeck(deck);
                    
                    //Currently, just get a value between 16-21 for the dealer
                    //dealerTotal = cardRandomizer.Next(15, 22);
                    playerCards.Add(DealCard(deck));
                    playerCards.Add(DealCard(deck));

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;


                    //TODO: The dealer is dealt one card face up, one card face down.
                    DisplayWelcomeMessage();
                    dealerCards.Add(DealCard(deck));
                    dealerCards.Add(DealCard(deck));

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;
                    dealerMinTotal += dealerCards[0].Value;
                    Console.WriteLine($"Dealer has at least {dealerMinTotal}");
                }
                else
                {
                    Environment.Exit(0);
                }

                if(playerTotal == 21 && dealerTotal != 21)
                {
                    Console.WriteLine("BLACKJACK!!! congrats you won");
                }
                else
                {
                    do
                    {

                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();

                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                }
                /* START GAME LOOP */
                

                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit(deck);

                }

                if (playerChoice.Equals("S"))
                {
                    while(dealerTotal < playerTotal && dealerTotal < 21)
                    {
                        dealerCardCount += 1;
                        dealerCards.Add(DealCard(deck));
                        dealerTotal += dealerCards[dealerCardCount].Value;
                        dealerMinTotal += dealerCards[dealerCardCount].Value;
                        Console.WriteLine($"dealer minimum total is {dealerMinTotal}.");
                        

                    }

                    if (dealerTotal > 21 && playerTotal <=21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                    }
                    else if (dealerTotal == playerTotal)
                    {
                        Console.WriteLine($"Dealer wins with {dealerTotal}");
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
            Console.WriteLine("You were dealt the cards : {0} {2} and {1} {3} ", playerCards[0].Name, playerCards[1].Name, playerCards[0].Suit, playerCards[1].Suit);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(List<Card> deck)
        {
            playerCardCount += 1;
            playerCards.Add(DealCard(deck));
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
                    Hit(deck);
                }
            }
        }

        

        public static Card DealCard(List<Card> deck)
        {
            Card temp = new Card();
            if(deck.Count() > 0)
            {
                temp = deck[0];
                deck.Remove(deck[0]);
            }
            return temp;
            //int cardValue = cardRandomizer.Next(1, 14);
            //return GetCardValue(cardValue);
        }
        public static List<Card> shuffleDeck(List<Card> original)
        {
            List<Card> newDeck = new List<Card>();
            Random cardPicker = new Random();

            while (original.Count() > 0)
            {
                int card = cardPicker.Next(0, original.Count());
                newDeck.Add(original[card]);
                original.Remove(original[card]);

            }

            return newDeck;
        }


        //static Card GetCardValue(int cardValue)
        //{
        //    return cardValue switch
        //    {
        //        1 => new Card() { Name = "Two", Value = 2 },
        //        2 => new Card() { Name = "Three", Value = 3 },
        //        3 => new Card() { Name = "Four", Value = 4 },
        //        4 => new Card() { Name = "Five", Value = 5 },
        //        5 => new Card() { Name = "Six", Value = 6 },
        //        6 => new Card() { Name = "Seven", Value = 7 },
        //        7 => new Card() { Name = "Eight", Value = 8 },
        //        8 => new Card() { Name = "Nine", Value = 9 },
        //        9 => new Card() { Name = "Ten", Value = 10 },
        //        10 => new Card() { Name = "Jack", Value = 10 },
        //        11 => new Card() { Name = "Queen", Value = 10 },
        //        12 => new Card() { Name = "King", Value = 10 },
        //        13 => new Card() { Name = "Ace", Value = 11 },
        //        _ => new Card() { Name = "Two", Value = 2 },
        //    };
        //}

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
                dealerCardCount = 1;
                dealerMinTotal = 0;
                
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