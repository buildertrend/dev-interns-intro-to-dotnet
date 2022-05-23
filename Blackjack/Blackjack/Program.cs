using System;
using System.Collections.Generic;

namespace Blackjack
{
    class  Program
    {
        static Random cardRandomizer = new Random();

        static readonly Card[] playerCards = new Card[11];
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;     
        static int playersNumber = 0;
        static List<Player> players = new List<Player>();   
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

                    do
                    {
                        Console.WriteLine("How Many Players? (1-6) ");
                        try
                        {
                            playersNumber = Int32.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Input");
                        }


                    } while (playersNumber < 1 || playersNumber > 6);
                    
                    for (int i = 0; i< playersNumber; i++)
                    {
                        players.Add(new Player());
                    }

                    foreach (Player player in players)
                    {
                        DealCard(player);
                        DealCard(player);
                    }




                    //TODO: The dealer is dealt one card face up, one card face down.
                    dealerCards[0] = DealDealerCard();
                    dealerCards[1] = DealDealerCard();

                    
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                foreach (Player player in players)
                {
                    DisplayWelcomeMessage(player);


                    do
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit(player);
                    }

                    if (playerChoice.Equals("S"))
                    {
                        if (player.getTotal() > dealerTotal && player.getTotal() <= 21)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        else if (player.getTotal() < dealerTotal)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;

                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
                            Console.ForegroundColor = ConsoleColor.White;

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
        private static void DisplayWelcomeMessage(Player player)
        {
            Console.WriteLine("You were dealt the cards : {0} and {1} ", player.cards[0].Name, player.cards[0].Name);
            Console.WriteLine("Your player Total is {0} ", player.getTotal());
            Console.WriteLine("The dealer was dealt the card : {0} and one face down card", dealerCards[0].Name);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(Player player)
        {
            DealCard(player);
            //playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("Your card is a(n) {0} and your new Total is {1}. ", playerCards[player.cards.Count-1].Name, player.getTotal());

            //Is this true? I don't think it is.
            if (player.getTotal().Equals(21))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                Console.ForegroundColor = ConsoleColor.White;


            }
            else if (player.getTotal() > 21)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You are busted! Sorry! The dealer's Total was {0}", dealerTotal);
                Console.ForegroundColor = ConsoleColor.White;

            }
            else if (player.getTotal() < 21)
            {
                do
                {
                    Console.WriteLine("Would you like to hit or stay? h for hit s for stay");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(player);
                }
            }
        }



        static Card DealCard(Player player)
        {
            int cardValue = cardRandomizer.Next(1, 14);
            Card card = GetCardValue(cardValue);
            player.cards.Add(card);

            return card;
        }

        static Card DealDealerCard()
        {
            int cardValue = cardRandomizer.Next(1, 14);
            dealerTotal += GetCardValue(cardValue).Value;
            dealerCardCount += 1;
            return GetCardValue(cardValue);
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