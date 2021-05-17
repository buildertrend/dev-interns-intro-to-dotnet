using System;

namespace Blackjack
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 1;
        static int dealerCardCount = 1;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

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
                    //dealerTotal = cardRandomizer.Next(15, 22);

                    dealerCards[0] = DealCard();
                    dealerCards[1] = DealCard();

                    dealerTotal = dealerCards[0].Value + dealerCards[1].Value;

 
             


                    playerCards[0] = DealCard();
                    //Hit();
                    playerCards[1] = DealCard();

                    //playerTotal += playerCards[0].Value;
                    //playerTotal += playerCards[1].Value;

                    calculateTotal();

                    


                    //TODO: The dealer is dealt one card face up, one card face down.
                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                if (playerTotal < 22)
                {
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

                    else if (playerChoice.Equals("S"))
                    {
                        dealer_hit();
                        if (playerTotal > dealerTotal && playerTotal <= 21)
                        {
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                            print_dealer();
                        }
                        else if (dealerTotal > 21)
                        {
                            Console.WriteLine("You won! Dealer got busted. The dealer's total was {0}", dealerTotal);
                            print_dealer();
                        }
                        else if (playerTotal < dealerTotal)
                        {
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                            print_dealer();
                        }
                        else
                        {
                            Console.WriteLine("Tie with the dealer! The dealer's total was {0}", dealerTotal);
                            print_dealer();
                        }
                    }
                }
                else if (playerTotal == 21)
                {
                    dealer_hit();
                    Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                    print_dealer();
                }
                else
                {
                    Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                    print_dealer();
                }
                

                /* END GAME LOOP */

                Console.WriteLine("Wuold you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        private static void dealer_hit()
        {
            while (dealerTotal < 17)
            {
                dealerCardCount++;
                dealerCards[dealerCardCount] = DealCard();
                dealerTotal += dealerCards[dealerCardCount].Value;

                //if total is greater than 21, we check for ace
                if (dealerTotal > 21)
                {
                    for(int i = 0; i <= dealerCardCount; i++)
                    {
                        if (dealerCards[i].Value == 11) dealerTotal -= 10;

                        if (dealerTotal <= 21) break;
                    }
                }

            }
        }

        private static void print_dealer()
        {
            Console.Write("Dealer has the following cards : ");
            for (int i = 0; i <= dealerCardCount; i++) { Console.Write("  {0}", dealerCards[i].Name); }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("One of dealer's card is {0}", dealerCards[0].Name);
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        private static int calculateTotal()
        {
            playerTotal = 0;
            int i = playerCardCount;
            for (int j = 0; j <= i; j++)
            {
                playerTotal += playerCards[j].Value;
            }
            //we check if there is an ace in case the total is greater than 21
            if (playerTotal > 21)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (playerCards[j].Value == 11)
                    { playerTotal -= 10; }

                    if (playerTotal <= 21) break;
                }


            }
            return 0;
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards[playerCardCount] = DealCard();
            //playerTotal += playerCards[playerCardCount].Value;
            calculateTotal();
            Console.WriteLine("You card is {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            //Is this true? I don't think it is.
            if (playerTotal.Equals(21))
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                print_dealer();

            }
            else if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);
                print_dealer();

            }
            else if (playerTotal < 21)
            {
                do
                {
                    Console.WriteLine("Would you like to hit or stay? H for hit S for stay");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit();
                }
                else 
                {
                    dealer_hit();

                    if (playerTotal > dealerTotal && playerTotal <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                        print_dealer();
                    }
                    else if (dealerTotal > 21)
                    {
                        Console.WriteLine("You won! Dealer got busted. The dealer's total was {0}", dealerTotal);
                        print_dealer();
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                        print_dealer();
                    }
                    else
                    {
                        Console.WriteLine("Tie with the dealer! The dealer's total was {0}", dealerTotal);
                        print_dealer();
                    }
                }
            }
        }

        

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
                1 => new Card() { Name = "Ace", Value = 11 },
                2 => new Card() { Name = "Two", Value = 2 },
                3 => new Card() { Name = "Three", Value = 3 },
                4 => new Card() { Name = "Four", Value = 4 },
                5 => new Card() { Name = "Five", Value = 5 },
                6 => new Card() { Name = "Six", Value = 6 },
                7 => new Card() { Name = "Seven", Value = 7 },
                8 => new Card() { Name = "Eight", Value = 8 },
                9 => new Card() { Name = "Nine", Value = 9 },
                10 => new Card() { Name = "Ten", Value = 10 },
                11 => new Card() { Name = "Jack", Value = 10 },
                12 => new Card() { Name = "Queen", Value = 10 },
                13 => new Card() { Name = "King", Value = 10 },
                //14 => new Card() { Name = "Ace", Value = 11 }
                _ => new Card() { Name = "unknown", Value = 0 }
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