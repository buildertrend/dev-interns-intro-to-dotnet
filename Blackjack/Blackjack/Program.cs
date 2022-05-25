using System;

namespace Blackjack
{
    class Program
    {
        static readonly Card[,] playerCards = new Card[4, 11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        static Player[] player;

        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        //static bool player = true;
        static bool dealer = false;
        static int dealerFaceUp = 0;
        //users to store the player choice (hit or stay)
        static string playerChoice = "";
        static string playAgain = "Y";
        static CardDeck deck;
        static string decision;

        static void Main(string[] args)
        {
            while (playAgain.ToUpper() == "Y")
            {
                deck = new CardDeck();

                //StartGame
                do
                {
                    Console.Write("Welcome to Blackjack - are you ready to play?");
                    cPrint(" (Y)es", "green");
                    cPrint(" (N)o\n", "red");

                    decision = Console.ReadLine().ToUpper();
                }
                while (!decision.Equals("Y") && !decision.Equals("N"));

                if (decision == "Y")
                {
                    DealDealer();
                    DealDealer();

                    Console.WriteLine("How many players? (as many as a deck of 52 can support)");
                    var pCount = Console.ReadLine();
                    int playerCount = Int32.Parse(pCount);
                    player = new Player[playerCount];

                    Console.WriteLine("The dealer was dealt the card {0}", dealerCards[0].getName());

                    for (int i = 0; i < playerCount; i++)
                    {
                        player[i] = new Player();
                        DealPlayer(i);
                        DealPlayer(i);
                        cPrint(String.Format("Player{0} ", i + 1), "green");
                        Console.WriteLine("was dealt the cards: {0} and {1} ", player[i].getCard(0).getName(), player[i].getCard(1).getName());
                    }

                    for (int i = 0; i < playerCount; i++)
                    {
                        Console.WriteLine("");
                        playerTotal = player[i].getTotal();

                        if (playerTotal == 21)
                        {
                            cPrint(String.Format("Player{0} ", i + 1), "green");
                            Console.WriteLine("got Blackjack!", i);
                        }
                        else
                        {
                            do
                            {
                                cPrint(String.Format("Player{0}", i + 1), "green");
                                Console.Write(": Would you like to");
                                cPrint(" (H)it", "green");
                                Console.Write(" or");
                                cPrint(" (S)tay", "red");
                                Console.WriteLine("? Your current total is {1}", i, playerTotal);

                                playerChoice = Console.ReadLine().ToUpper();
                            }
                            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                        }

                        if (playerChoice.Equals("H"))
                        {
                            //hit will get them a card / check the total and ask for another hit
                            Hit(i);
                        }

                        if (playerChoice.Equals("S"))
                        {
                        }
                    }

                    Console.WriteLine("\nThe dealer's second card is {0}, dealer's total is {1}", dealerCards[1].getName(), dealerTotal);

                    while (dealerTotal < 17)
                    {
                        Console.WriteLine("The dealer's total is {0}, less than 17", dealerTotal);
                        String card = DealDealer().getName();
                        if (dealerTotal > 21)
                        {
                            Console.WriteLine("The dealer draws a {0} and busted, total is {1}", card, dealerTotal);
                        }
                        else
                        {
                            Console.WriteLine("The dealer draws a {0}, total is {1}", card, dealerTotal);
                        }
                    }

                    bool dealerBust = dealerTotal > 21 ? true : false;

                    Console.WriteLine("\n");
                    for (int j = 0; j < playerCount; j++)
                    {
                        playerTotal = player[j].getTotal();
                        cPrint(String.Format("Player{0} ", j + 1), "green");

                        if (playerTotal > 21)
                        {
                            Console.WriteLine("has a total of {0} and busted", playerTotal);
                        }

                        else if (playerTotal < dealerTotal && !dealerBust)
                        {
                            Console.WriteLine("has a total of {0} and lost to the dealer", playerTotal);
                        }

                        else if (playerTotal > dealerTotal)
                        {
                            Console.WriteLine("has a total of {0} and won the dealer", playerTotal);
                        }

                        else if (playerTotal == dealerTotal)
                        {
                            Console.WriteLine("has a total of {0} and tied the dealer", playerTotal);
                        }

                        else
                        {
                            Console.WriteLine("has a total of {0} and won", playerTotal);
                        }
                    }

                    //DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* END GAME LOOP */


                Console.WriteLine("{0} cards were used in the game", deck.cardsUsed());
                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards: {0} and {1} ", player[0].getCard(0).getName(), player[0].getCard(1).getName());
            Console.WriteLine("Your player total is {0}, dealercard 1 is {0}", player[0].getTotal(), dealerCards[0].getName());
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(int index)
        {
            playerCardCount = player[index].getCardCount();
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            //DealCard(player);
            DealPlayer(index);

            playerTotal = player[index].getTotal();

            Console.WriteLine("Your card is a(n) {0} and your new total is {1}. ", player[index].getCard(player[index].getCardCount() - 1).getName(), playerTotal);

            //Is this true? I don't think it is.
            if (playerTotal == 21)
            {
                cPrint(String.Format("Player{0} ", index + 1), "green");
                Console.WriteLine("got Blackjack!");

            }
            else if (playerTotal > 21)
            {
                cPrint(String.Format("Player{0} ", index + 1), "green");
                Console.WriteLine("is busted! Sorry!", index);

            }
            else if (playerTotal < 21)
            {
                do
                {
                    Console.Write("\nWould you like to hit or stay?");
                    cPrint(" (h) for hit", "green");
                    cPrint(" (s) for stay", "red");
                    Console.WriteLine(", dealer's face up card is {0}", dealerCards[0].getName());

                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit(index);
                }
            }
        }

        public static void cPrint(String s, String color)
        {
            int colorInt = 15;
            switch (color)
            {
                case "green":
                    colorInt = 10;
                    break;
                case "red":
                    colorInt = 12;
                    break;
                case "blue":
                    colorInt = 9;
                    break;
            }

            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Console.ForegroundColor = colors[colorInt];
            Console.Write(s);
            Console.ResetColor();
        }

        static Card DealDealer()
        {
            dealerCards[dealerCardCount] = deck.drawCard();
            dealerTotal += dealerCards[dealerCardCount].Value;
            dealerCardCount++;
            dealerFaceUp = dealerCards[0].Value;

            return dealerCards[dealerCardCount - 1];
        }

        static Card DealPlayer(int index)
        {
            player[index].addCard(deck.drawCard());
            return player[index].getCard(playerCardCount);
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