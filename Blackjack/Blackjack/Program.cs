using System;

namespace Blackjack
{
    class Program
    {
        static Random cardRandomizer = new Random();

        //static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 0;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";
        static int maxPlayerTotal = 0;
        //static Card[] playerCard;
        static string playAgain = "Y";
        static Card[,] playerCards;
        static int playerIndex;

        static void Main(string[] args)
        {
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.WriteLine("Please enter the number of players :");
                var totalPlayers = Int32.Parse(Console.ReadLine());
                playerCards = new Card[totalPlayers, 11];
                dealerCards[0] = DealDealerCard();
                dealerCards[1] = DealDealerCard();
                dealerCardCount = 2;
                for (int i = 0; i < totalPlayers; i++)
                {
                   // Console.BackgroundColor = ConsoleColor.DarkRed;
                    playerTotal = 0;
                    playerCardCount = 0;
                    Console.WriteLine("Welcome to Blackjack - are you ready to play Player {0} ? (Y)es (N)o",i+1);
                    var decision = Console.ReadLine().ToUpper();

                    if (decision == "Y")
                    {
                        //Currently, just get a value between 16-21 for the dealer
                        //dealerTotal = cardRandomizer.Next(15, 22);
                        
                        // playerCards[0] = DealCard();
                        // playerCards[1] = DealCard();
                        playerCards[i,playerCardCount] = DealCard();
                        playerCardCount++;
                        playerCards[i, playerCardCount] = DealCard();
                        playerCardCount++;
                        //playerTotal += playerCards[0].Value;
                        //playerTotal += playerCards[1].Value;
                        if (playerTotal.Equals(21))
                        {
                            Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                            PlayAgain();

                        }

                        //TODO: The dealer is dealt one card face up, one card face down.
                        DisplayWelcomeMessage(i);
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
                        Hit(i);
                    }
                    if (playerChoice.Equals("S"))
                    {
                        if (playerTotal > maxPlayerTotal)
                        {
                            maxPlayerTotal = Math.Max(maxPlayerTotal, playerTotal);
                            playerIndex = i;
                            //playerCard = playerCards[i];
                            //Card[] test = playerCards.Get;
                        }
                        Console.WriteLine("Player {0} has a score of {1}", i+1, playerTotal);
                        continue;
                    }
                }
                

                if (!DealDealer())
                {
                    // Console.WriteLine("dealers Second card was : {0}", dealerCards[1].Name);
                    //if (playerTotal >= dealerTotal && playerTotal <= 21)
                    //{
                    //    Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    //}
                    //else if (playerTotal < dealerTotal)
                    //{
                    //    Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);

                    Console.WriteLine("Player {0} won the game with a score of {1}", playerIndex+1, maxPlayerTotal);
                    Console.WriteLine("Dealer total score is {0}", dealerTotal);
                }
                else
                {
                    Console.WriteLine("Dealer won the game with a score of {0}", dealerTotal);
                }

                /* END GAME LOOP */

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        /// 
        public static Boolean DealDealer()
        {
            while (dealerTotal < 17)
            {
                dealerCards[dealerCardCount] = DealDealerCard();
                dealerCardCount++;
            }
            if(dealerTotal<=21 && dealerTotal > maxPlayerTotal)
            {
                return true;
            }
            return false;
        }

        private static void DisplayWelcomeMessage(int index)
        {
            //Console.BackgroundColor = ConsoleColor.Green;

            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[index,0].Name, playerCards[index,1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
           // Console.WriteLine("Dealer's first card : {0}", dealerCards[0].Name);
           // Console.BackgroundColor = ConsoleColor.Red;

            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(int playerIndex)
        {
            playerCards[playerIndex,playerCardCount] = DealCard();
            playerCardCount += 1;
            //playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerIndex,playerCardCount-1].Name, playerTotal);

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
                    Hit(playerIndex);
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
            Card playerC = GetCardValue(cardValue);
            playerTotal += playerC.Value;
            return playerC;
        }

        static Card DealDealerCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            Card dealerC = GetCardValue(cardValue);
            dealerTotal += dealerC.Value;
            return dealerC;
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
                playerIndex = -1;
                maxPlayerTotal = 0;
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