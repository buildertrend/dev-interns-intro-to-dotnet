using Blackjack;
using System.ComponentModel.Design;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        static Deck cardDeck;

        static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 0;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;

        static readonly int dealerID = 0;
        static readonly int playerID = 1;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
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
                    cardDeck = new Deck(); //Create and shuffle a new deck

                    //Deal two cards to both the dealer and the player
                    playerCards[0] = DealCard(playerID);
                    dealerCards[0] = DealCard(dealerID);

                    playerCards[1] = DealCard(playerID);
                    dealerCards[1] = DealCard(dealerID);

                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                do
                {
                    if (playerTotal.Equals(21))
                    {
                        Console.WriteLine("You are at 21!");
                        GameOver(playerID);
                    }
                    else
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                if (playerChoice.Equals("H"))
                {
                    //If Player chooses to hit, Dealer will hit after them if they are not at 17
                    Hit(playerID);
                    if(dealerTotal < 17 && playerTotal < 21)
                        Hit(dealerID);
                }

                if (playerChoice.Equals("S"))
                {
                    //If player decides to stay, dealer will have to hit until >= 17
                    while (dealerTotal < 17)
                    {
                        Hit(dealerID);
                    }
                        
                    if (playerTotal > dealerTotal && playerTotal <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                        GameOver(playerID);
                    } else if (dealerTotal > 21) {
                            Console.WriteLine("Dealer has busted with a total of {0}!", dealerTotal);
                            GameOver(playerID);
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        GameOver(dealerID);
                    }
                }
            }
        }

        private static void GameOver(int ID)
        {
            if (ID.Equals(dealerID))
                Console.WriteLine("Game Over! Dealer Wins!");
            else
                Console.WriteLine("Game Over! You Win!");
            Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
            PlayAgain();
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand, along with the dealers first card
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            Console.WriteLine("The first visible card of the dealer is : {0} ", dealerCards[0].Name);
            PrintCards(dealerCards[0]);
        }

        //TODO: Create switch case for multiple players
        static void Hit(int playerNum)
        {
            if(playerNum == 1) //Player
            {
                playerCards[playerCardCount] = DealCard(playerID);
                Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount - 1].Name, playerTotal);
                //Is this true? I don't think it is.
                if (playerTotal.Equals(21))
                {
                    Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                    GameOver(playerID);
                }
                else if (playerTotal > 21)
                {
                    Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                    GameOver(dealerID);
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
                        Hit(playerNum);
                    }
                }
            } else //Dealer
            {
                 dealerCards[dealerCardCount] = DealCard(dealerID);
                 dealerCardCount += 1;
            }
        }

        //Player num of dealer is 0
        static Card DealCard(int playerNum)
        {
            //int cardValue = cardRandomizer.Next(1, 14);
            Card drawnCard = cardDeck.TakeCard();
            int cardValue = drawnCard.Value;
            if (playerNum == dealerID)
            {
                dealerTotal += GetPointTotal(cardValue, dealerID);
                dealerCardCount++;
            } 
            else
            {
                playerTotal += GetPointTotal(cardValue, playerID);
                playerCardCount++;
            }
            return Card.GetCardValue(cardValue);
        }

        static int GetPointTotal(int cardValue, int playerNum)
        {
            if(playerNum == dealerID)
            {
                if (cardValue == 13)
                {
                    if (11 + dealerTotal > 21)
                        return 1;
                    else
                        return 11;
                }
            } else
            {
                if (cardValue == 13)
                {
                    if (11 + playerTotal > 21)
                        return 1;
                    else
                        return 11;
                }
            }
            
            return Card.GetCardValue(cardValue).Value;
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
                dealerCardCount = 1;
                playerTotal = 0;
            }
            else if (playAgain.Equals("N"))
            {
                    Environment.Exit(0);
            }
        }

        static void PrintCards(Card c)
        {
            int val = c.Value;
            string name = c.Name;
            string suit = c.Suit;

            if (suit.Equals("Diamond"))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (c.Suit.Equals("Hearts"))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (c.Suit.Equals("Clubs"))
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine("__________");
            string line = String.Format("[{0, 10}]", $"|{name.Substring(0)}|");
            Console.WriteLine(line);
        }
    }
}