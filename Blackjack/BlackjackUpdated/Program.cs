using System.Drawing;

namespace BlackjackUpdated
{
    class Program
    {
        static CardDeck cardDeck = new CardDeck();
        static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        // Save the current background and foreground colors.

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            
            while (playAgain.ToUpper() == "Y")
            {
                Console.BackgroundColor = colors[0];
                //StartGame
                cardDeck = new CardDeck();
                cardDeck.shuffle();
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    //dealerTotal = cardRandomizer.Next(16, 22);
                    dealerCards[0] = DealCardDealer();
                    dealerCards[1] = DealCardDealer(); // "face down" card

                    playerCards[0] = DealCardUser();
                    playerCards[1] = DealCardUser();
                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                if (playerTotal == 21) {
                    Console.WriteLine("You got Blackjack on your first deal! You win!");
                    //TODO: Add condition where the dealer also has a natural
                }
                else {
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
                            Console.BackgroundColor = colors[2];
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                        }
                        else if (playerTotal < dealerTotal)
                        {
                            Console.BackgroundColor = colors[4];
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        }
                        else if (playerTotal == dealerTotal)
                        {
                            Console.WriteLine("Its a tie!");
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
            Console.WriteLine("You were dealt the cards : {0} of {1} and {2} of {3} ", playerCards[0].Face, playerCards[0].Suit, playerCards[1].Face, playerCards[1].Suit);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);

            Console.WriteLine("The dealer has the card {0} of {1} face up ", dealerCards[0].Face, dealerCards[0].Suit);
        }

        static void Hit()
        {
            playerCards[playerCardCount] = DealCardUser();
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount-1].Face, playerTotal);
            if (playerTotal == 21)
            {
                Console.BackgroundColor = colors[6];
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
            {
                Console.BackgroundColor = colors[4];
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            else if (playerTotal < 21)
            {
                do
                {
                    Console.WriteLine("Would you like to hit or stay? H for hit S for stay");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice == "H")
                {
                    Hit();
                }
            }
        }

        //TODO: Move this class to it's own file.
        

        static Card DealCardUser()
        {
            playerCardCount++;
            Card card = cardDeck.drawCard();
            playerTotal += card.getValue(playerTotal);
            return card;
        }

        static Card DealCardDealer()
        {
            dealerCardCount++;
            Card card = cardDeck.drawCard();
            dealerTotal += card.getValue(dealerTotal);
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
            Console.BackgroundColor = colors[0];
            if (playAgain.Equals("Y"))
            {

                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();
                dealerTotal = 0;
                dealerCardCount = 1;
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