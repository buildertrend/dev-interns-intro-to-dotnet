using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;
using static Blackjack.Card;
using System.Linq;
using System.Text;

namespace BlackjackUpdated
{
    class Program
    {
        static readonly List<Card> gameDeck = BuildDeck();
        static readonly List<Card> playerCards = new List<Card>();
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        static int dealerCardCount = 1;

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
                    Console.WriteLine("hi");
                }
//                for(int i = 0; i < gameDeck.Count; i++)
//                {
//                    Console.WriteLine("Name : {0}", gameDeck.ElementAt(i).Name);
//                    Console.WriteLine("Suit : {0}", gameDeck.ElementAt(i).suit);
//                    Console.WriteLine("Value : {0}", gameDeck.ElementAt(i).Value);
//                }
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());

                    //dealerTotal = cardRandomizer.Next(15, 22);
                    playerCards.Add(DealCard());
                    playerCards.Add(DealCard());

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;
                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    if(playerTotal == 21)
                    {
                        Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
                        Console.WriteLine("BLACKJACKKKKKKK!!!!");
                        break;
                    }
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
                    Console.WriteLine("The dealer's card is {0} ", dealerCards[0].Name);
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
                    do
                    {
                        dealerCardCount += 1;
                        //dealerCards[dealerCardCount] = DealCard();
                        dealerCards.Add(DealCard());
                        dealerTotal += dealerCards[dealerCardCount].Value;
                        //Console.WriteLine("Dealer drew a(n) {0} ", dealerCards[dealerCardCount].Name);
                    } while (dealerTotal <= 17);

                    //Console.WriteLine("The dealer's total is {0} ", dealerTotal);
                    if (((playerTotal > dealerTotal) || (dealerTotal > 21)) && playerTotal <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                    }
                    else
                    {
                        Console.WriteLine("You tied!");
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
        }

        static void Hit()
        {
            playerCardCount += 1;
            //playerCards[playerCardCount] = DealCard();
            playerCards.Add(DealCard());
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            //Is this true? I don't think it is.
            if (playerTotal.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
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

        //TODO: Move this class to it's own file.
        

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
        public static List<Card> BuildDeck()
        {
            List<Card> Deck = new List<Card>();
            List<Card> Clubs = generateClubs();
            List<Card> Spades = generateSpades();
            List<Card> Hearts = generateHearts();
            List<Card> Diamonds = generateDiamonds();
            for (int i = 0; i < Clubs.Count; i++)
            {
                Deck.Add(Clubs.ElementAt(i));
            }
            for (int i = 0; i < Spades.Count; i++)
            {
                Deck.Add(Spades.ElementAt(i));
            }
            for (int i = 0; i < Hearts.Count; i++)
            {
                Deck.Add(Hearts.ElementAt(i));
            }
            for (int i = 0; i < Diamonds.Count; i++)
            {
                Deck.Add(Diamonds.ElementAt(i));
            }
            return Deck;
        }
        public static List<Card> generateHearts()
        {
            List<Card> hearts = new List<Card>();
            for (int i = 1; i < 13; i++)
            {
                var newName = new StringBuilder(i.ToString(), 20);
                newName.Append(" of Hearts");
                Card newCard = new Card();
                newCard.Name = newName.ToString();
                newCard.suit = "Hearts";
                newCard.Value = Math.Min(i, 10);
                hearts.Add(newCard);
            }
            return hearts;
        }
        public static List<Card> generateDiamonds()
        {
            List<Card> diamonds = new List<Card>();
            for (int i = 1; i < 13; i++)
            {
                var newName = new StringBuilder(i.ToString(), 20);
                newName.Append(" of Diamonds");
                Card newCard = new Card();
                newCard.Name = newName.ToString();
                newCard.suit = "Diamonds";
                newCard.Value = Math.Min(i, 10);
                diamonds.Add(newCard);
            }
            return diamonds;
        }
        public static List<Card> generateClubs()
        {
            List<Card> clubs = new List<Card>();
            for (int i = 1; i < 13; i++)
            {
                var newName = new StringBuilder(i.ToString(), 20);
                newName.Append(" of Clubs");
                Card newCard = new Card();
                newCard.Name = newName.ToString();
                newCard.suit = "Clubs";
                newCard.Value = Math.Min(i, 10);
                clubs.Add(newCard);
            }
            return clubs;
        }
        public static List<Card> generateSpades()
        {
            List<Card> spades = new List<Card>();
            for (int i = 1; i < 13; i++)
            {
                var newName = new StringBuilder(i.ToString(), 20);
                newName.Append(" of Spades");
                Card newCard = new Card();
                newCard.Name = newName.ToString();
                newCard.suit = "Spades";
                newCard.Value = Math.Min(i, 10);
                spades.Add(newCard);
            }
            return spades;
        }
        public static Card DealCard()
        {
            Random cardRandomizer = new Random();
            int cardValue = cardRandomizer.Next(1, gameDeck.Count);
            Card newCard = gameDeck.ElementAt(cardValue);
            gameDeck.RemoveAt(cardValue);
            //playerTotal += cardValue;
            return newCard;
        }


    }
}