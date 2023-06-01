using Blackjack;
using Microsoft.VisualBasic;
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
        static int playerCardCount = 1;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 1;
        private static Card[] deck;
        private static Card[] shuffledDeck;
        static int dealDeck = 0;

        enum CardValues
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

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
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    deck = generateDeck();

                    shuffledDeck = deck.OrderBy(e => cardRandomizer.NextDouble()).ToArray();

                    dealerCards[0] = DealCard();
                    dealerCards[1] = DealCard();

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    while(dealerTotal <= 16)
                    {
                        dealerCardCount += 1;
                        dealerCards[dealerCardCount] = DealCard();
                        dealerTotal += dealerCards[dealerCardCount].Value;
                        if (dealerTotal > 21)
                        {
                            Console.WriteLine("Congrats! You won the game! The dealer has busted with a total of {0} ", dealerTotal);
                            Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                            PlayAgain();
                        }
                    }

                    playerCards[0] = DealCard();
                    playerCards[1] = DealCard();

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;


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
            Console.WriteLine("The dealers visable card is a : {0} ", dealerCards[0].Name);
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your Total is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = DealCard();
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            //Is this true? I don't think it is.
            if (playerTotal.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
            {
                var temp = 0;
                while (temp <= playerCardCount)
                {
                    if (playerCards[temp].Name == "Ace")
                    {
                        playerCards[temp].Value = 1;
                        playerTotal -= 10;
                    }
                    temp += 1;
                }
                if(playerTotal > 21)
                {
                    Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                }
                else
                {
                    Console.WriteLine("Your ace was changed to a 1. Your new total is {0}. ", playerTotal);
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
            var card = shuffledDeck[dealDeck];
            ++dealDeck;
            if(dealDeck > 51)
            {
                shuffledDeck = deck.OrderBy(e => cardRandomizer.NextDouble()).ToArray();
                dealDeck = 0;
            }
            return card;
        }

        static Card[] generateDeck()
        {
            Card[] deck = new Card[52];
            int i = 0;
            for(int suit = 1; suit < 5; suit++)
            {
                for (int name = 1; name < 14; name++)
                {
                    string nameString = Enum.GetName(typeof(CardValues), name);
                    deck[i] = new Card(suit, nameString);
                    i++;
                }
            }
            return deck;
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
                dealerCardCount = 1;
                playerCardCount = 1;
                playerTotal = 0;
                dealDeck = 0;
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