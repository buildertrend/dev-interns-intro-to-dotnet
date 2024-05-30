using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly List<Card> playerCards = new List<Card>();
        //static readonly Card[] playerCards = new Card[11];
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly List<Card> dealerCards = new List<Card>();
        //private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static bool firstBlackjack = false;

        static bool dealerWins = false;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";
        
        static string playAgain = "Y";

        static readonly List<Player> playerList = new List<Player>();
        static int playersDONE = 0;

        static int playerAmount = 1;
        static ConsoleColor[] colors = { ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Cyan };

        // deck stuff
        static string[] suit = {"clubs", "spades", "hearts", "diamonds"};
        static string[] name = {"two", "three", "four","five","six", "seven", "eight", "nine", "ten", "Jack", "Queen", "King", "Ace"};
        static int[] value = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

    static void Main(string[] args)
        {
            List<Card> deck = new List<Card>();         // maybe stack for simplicity?
            int w = 0;
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                try
                {

                    // create deck

                    foreach(string s in suit)
                    {
                        foreach(string n in name)
                        {
                            deck.Add(new Card { Name = n, Suit = s, Value = value[w] });
                            w++;
                        }
                        w = 0;
                    }
              

                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)esss (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("hi");
                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {

                    Console.WriteLine("Number of Players (1 to 5) : ");
                    playerAmount = int.Parse(Console.ReadLine());
                    
                    for(int j = 0; j < playerAmount; j++)
                    {
                        playerList.Add(new Player());
                    }

                    for(int j = 0; j < playerAmount; j++)
                    {

                        Card c1 = DealCard(deck);
                        playerList[j].playerCards.Add(c1);
                        playerList[j].total += c1.Value;
                        playerList[j].cardAmount++;
                        Card c2 = DealCard(deck);
                        playerList[j].playerCards.Add(c2);
                        playerList[j].total += c2.Value;
                        playerList[j].cardAmount++;

                        if (playerList[j].total == 21)
                        {
                            playerList[j].state = State.BLACKJACK;
                        }
                        else
                        {
                            playerList[j].state = State.DECISION;
                        }

                    }


                    dealerCards.Add(DealCard(deck));
                    dealerCards.Add(DealCard(deck));

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    if(dealerTotal == 21) { dealerWins = true; }

                    dealerCardCount = 2;

                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }


                /* START GAME LOOP */
                while (playersDONE != playerAmount)
                {


                    for (int j = 0; j < playerAmount; j++)      // players take turns when deciding to hit or stay
                    {
                        Console.ForegroundColor = colors[j];
                        Console.WriteLine("Player {0}: total is {1}", j + 1, playerList[j].total);

                        if (playerList[j].state == State.DECISION)
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
                                Hit(playerList[j], deck);
                            }
                            if (playerChoice.Equals("S"))
                            {
                                playerList[j].state = State.DONE;
                                playersDONE++;

                                if (playerList[j].total > 21)
                                {
                                    Console.WriteLine("You busted! Sorry!");
                                    playerList[j].state = State.BUST;
                                }

                            }
                        }
                        else if (playerList[j].state == State.DONE)
                        {
                            Console.WriteLine("Done");
                        }
                        else if (playerList[j].state == State.BLACKJACK)
                        {
                            Console.WriteLine("Has Blackjack");
                        }
                        else if (playerList[j].state == State.BUST)
                        {
                            Console.WriteLine("Bust");
                        }

                        Console.ResetColor();
                    }
                }

                // Dealer hit's when under 17
                while (dealerTotal < 17)
                {
                    Card c = DealCard(deck);
                    dealerCards.Add(c);
                    dealerTotal += c.Value;
                    dealerCardCount++;
                }


                /* END GAME LOOP */
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nRESULTS");
                Console.ResetColor();

                // evaluate dealerWins boolean
                if (dealerWins)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dealer Wins with Blackjack");
                    Console.ResetColor();
                }
                else if(dealerTotal == 21)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dealer Wins with total 21");
                    Console.ResetColor();
                }
                else
                {
                    int max = -300;
                    int winner = -1;
                    int blackjackAmount = 0;

                    for (int j = 0; j < playerAmount; j++)
                    {
                        if (playerList[j].state == State.DONE)
                        {
                            if(max < playerList[j].total)
                            {
                                max = playerList[j].total;
                                winner = j + 1;
                            }
                        }
                        else if (playerList[j].state == State.BLACKJACK)
                        {
                            blackjackAmount++;
                        }
                    }

                    if(blackjackAmount == 0 && playerList[winner-1].total == dealerTotal)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dealer Wins with total {0}", dealerTotal);
                        Console.ResetColor();
                    }


                    for (int j = 0; j < playerAmount; j++)
                    {
                        Console.ForegroundColor = colors[j];
                        Console.WriteLine("Player {0}: {1}", j + 1, playerList[j].total);
                        if(j + 1 == winner && blackjackAmount == 0)
                        {
                            Console.WriteLine("Congrats! You win. Dealer's total was: {0}", dealerTotal);
                        }
                        else if (playerList[j].state == State.BLACKJACK)
                        {
                            Console.WriteLine("Congrats! You win with a Blackjack. Dealer's total was: {0}", dealerTotal);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, you lost! Dealer's total was: {0}", dealerTotal);
                        }
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            
            for(int j = 0; j < playerAmount; j++)
            {
                Console.ForegroundColor = colors[j];
                Console.WriteLine("Player {0} was dealt cards : {1} and {2} ", j+1, playerList[j].playerCards[0].Name, playerList[j].playerCards[1].Name);
                Console.WriteLine("Total is {0} ", playerList[j].total);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Dealer's first card is {0} ", dealerCards[0].Name);
            Console.ResetColor();
        }

        static void Hit(Player player, List<Card> deck)
        {
            player.cardAmount += 1;
            player.playerCards.Add(DealCard(deck));
            player.total += player.playerCards[player.cardAmount-1].Value;
            Console.WriteLine("Your card is a(n) {0} and your new Total is {1}. ", player.playerCards[player.cardAmount-1].Name, player.total);

            if (player.total > 21)
            {
                Console.WriteLine("You busted! Sorry!");
                player.state = State.BUST;
                playersDONE++;
            }
            return;
        }


        static Card DealCard(List<Card> deck)
        {
            
            int cardNumber = cardRandomizer.Next(1, deck.Count);
            Card card = deck[cardNumber];
            deck.RemoveAt(cardNumber);
            return card;
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
                playerCardCount = 1;
                playerTotal = 0;
                playerCards.Clear();
                dealerCards.Clear();
                playersDONE = 0;
                playerList.Clear();
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