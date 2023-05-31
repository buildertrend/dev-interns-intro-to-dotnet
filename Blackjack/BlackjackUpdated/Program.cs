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
        static int dealerCardCount = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";
        static Boolean gameOver = false;
        static string playAgain = "Y";

        static void Main(string[] args)
        {
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    dealerCards[0] = DealersCard();
                    dealerCards[1] = DealersCard();

                    playerCards[0] = DealCard();
                    playerCards[1] = DealCard();


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
                            gameOver = true;
                        }
                        else if (playerTotal < dealerTotal)
                        {
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                            gameOver = true;
                        }
                    }
                }
                while (!gameOver);

               

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
            Console.WriteLine("Your player total is {0} ", playerTotal);
            Console.WriteLine("The dealers first card is : {0} ", dealerCards[0].Name);
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = DealCard();
            
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            //Is this true? I don't think it is.
            CheckScore();
        }

        static void CheckScore()
        {
            if (playerTotal.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                gameOver = true;

            }
            else if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                gameOver = true;

            }
            
        }


        static Card DealCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            Card card = Card.GetCardValue(cardValue);
            playerTotal += card.Value;
            return card;
        }

        static Card DealersCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            Card card = Card.GetCardValue(cardValue);
            dealerTotal += card.Value;
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