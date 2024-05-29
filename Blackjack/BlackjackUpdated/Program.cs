using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();


        static List<Card> deck;

        static readonly List<Card> playerCards = new List<Card>();
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        
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
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)esss (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("hi");
                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    deck = buildDeck();
                    //dealer picks 2 cards
                    dealerCards[0] = DealCard();
                    dealerCards[1] = DealCard();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    dealerCardCount += 1;
                    dealerCardCount += 1;

                    //player picks 2 cards
                    playerCards.Add(DealCard());
                    playerTotal += playerCards[playerCards.Count - 1].Value;
                    playerCards.Add(DealCard());
                    playerTotal += playerCards[playerCards.Count - 1].Value;



                    //TODO: The dealer is dealt one card face up, one card face down.
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
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                if (playerChoice.Equals("H"))
                {
                    //hit will get them a card / check the total and ask for another hit
                    Hit();
                }

                if (playerChoice.Equals("S"))
                {
                    if (playerTotal > dealerTotal && playerTotal <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
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
            //TODO: Inform the player the value of the dealer's visible card.
            Console.WriteLine("Dealer was dealt: {0} ", dealerCards[0].Name);

        }

        static void addDealerCard()
        {
            dealerCards[dealerCardCount] = DealCard();
            dealerTotal += dealerCards[dealerCardCount].Value;
            dealerCardCount += 1;


        }

        static void Hit()
        {
            playerCards.Add(DealCard());
            playerTotal += playerCards[playerCards.Count - 1].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCards.Count - 1].Name, playerTotal);

            if(dealerTotal < 16)
            {
                addDealerCard();
            }

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
                    Hit();
                }
            }
        }



        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(0,deck.Count);
            Card c = deck[cardValue];
            deck.RemoveAt(cardValue);
            return c;
        }

        static List<Card> buildDeck()
        {
            List<Card> d = new List<Card>();
            d.Add(new Card() { Name = "Two", Value = 2, Suit = "Club" });
            d.Add(new Card() { Name = "Three", Value = 3, Suit = "Club"});
            d.Add(new Card() { Name = "Four", Value = 4, Suit = "Club" });
            d.Add(new Card() { Name = "Five", Value = 5, Suit = "Club" });
            d.Add(new Card() { Name = "Six", Value = 6,  Suit = "Club" });
            d.Add(new Card() { Name = "Seven", Value = 7, Suit = "Club" });
            d.Add(new Card() { Name = "Eight", Value = 8, Suit = "Club" });
            d.Add(new Card() { Name = "Nine", Value = 9, Suit = "Club" });
            d.Add(new Card() { Name = "Ten", Value = 10, Suit = "Club" });
            d.Add(new Card() { Name = "Jack", Value = 10, Suit = "Club" });
            d.Add(new Card() { Name = "Queen", Value = 10,  Suit = "Club" });
            d.Add(new Card() { Name = "King", Value = 10, Suit = "Club" });
            d.Add(new Card() { Name = "Ace", Value = 11, Suit = "Club" });

            d.Add(new Card() { Name = "Two", Value = 2, Suit = "Diamond" });
            d.Add(new Card() { Name = "Three", Value = 3, Suit = "Diamond" });
            d.Add(new Card() { Name = "Four", Value = 4, Suit = "Diamond" });
            d.Add(new Card() { Name = "Five", Value = 5, Suit = "Diamond" });
            d.Add(new Card() { Name = "Six", Value = 6, Suit = "Diamond" });
            d.Add(new Card() { Name = "Seven", Value = 7, Suit = "Diamond" });
            d.Add(new Card() { Name = "Eight", Value = 8, Suit = "Diamond" });
            d.Add(new Card() { Name = "Nine", Value = 9, Suit = "Diamond" });
            d.Add(new Card() { Name = "Ten", Value = 10, Suit = "Diamond" });
            d.Add(new Card() { Name = "Jack", Value = 10, Suit = "Diamond" });
            d.Add(new Card() { Name = "Queen", Value = 10, Suit = "Diamond" });
            d.Add(new Card() { Name = "King", Value = 10, Suit = "Diamond" });
            d.Add(new Card() { Name = "Ace", Value = 11, Suit = "Diamond" });

            d.Add(new Card() { Name = "Two", Value = 2, Suit = "Heart" });
            d.Add(new Card() { Name = "Three", Value = 3, Suit = "Heart" });
            d.Add(new Card() { Name = "Four", Value = 4, Suit = "Heart" });
            d.Add(new Card() { Name = "Five", Value = 5, Suit = "Heart" });
            d.Add(new Card() { Name = "Six", Value = 6, Suit = "Heart" });
            d.Add(new Card() { Name = "Seven", Value = 7, Suit = "Heart" });
            d.Add(new Card() { Name = "Eight", Value = 8, Suit = "Heart" });
            d.Add(new Card() { Name = "Nine", Value = 9, Suit = "Heart" });
            d.Add(new Card() { Name = "Ten", Value = 10, Suit = "Heart" });
            d.Add(new Card() { Name = "Jack", Value = 10, Suit = "Heart" });
            d.Add(new Card() { Name = "Queen", Value = 10, Suit = "Heart" });
            d.Add(new Card() { Name = "King", Value = 10, Suit = "Heart" });
            d.Add(new Card() { Name = "Ace", Value = 11, Suit = "Heart" });

            d.Add(new Card() { Name = "Two", Value = 2, Suit = "Spade" });
            d.Add(new Card() { Name = "Three", Value = 3, Suit = "Spade" });
            d.Add(new Card() { Name = "Four", Value = 4, Suit = "Spade" });
            d.Add(new Card() { Name = "Five", Value = 5, Suit = "Spade" });
            d.Add(new Card() { Name = "Six", Value = 6, Suit = "Spade" });
            d.Add(new Card() { Name = "Seven", Value = 7, Suit = "Spade" });
            d.Add(new Card() { Name = "Eight", Value = 8, Suit = "Spade" });
            d.Add(new Card() { Name = "Nine", Value = 9, Suit = "Spade" });
            d.Add(new Card() { Name = "Ten", Value = 10, Suit = "Spade" });
            d.Add(new Card() { Name = "Jack", Value = 10, Suit = "Spade" });
            d.Add(new Card() { Name = "Queen", Value = 10, Suit = "Spade" });
            d.Add(new Card() { Name = "King", Value = 10, Suit = "Spade" });
            d.Add(new Card() { Name = "Ace", Value = 11, Suit = "Spade" });


            return d;
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