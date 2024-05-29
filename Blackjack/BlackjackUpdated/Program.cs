using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;
using static Blackjack.Card;
using static Blackjack.Player;
using System;
namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        static Player[] players = new Player[7];
        private static readonly Card[] dealerCards = new Card[11];
        static int dealerTotal = 0;
        static int dealerCardCount = 0;
        static string playAgain = "Y";

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static int playersCount = 0;

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            while (playAgain.Equals("Y"))
            {
                //StartGame
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error");
                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    Console.WriteLine("How many players will be playing?");
                    try
                    {
                        playersCount = Convert.ToInt32(Console.ReadLine());

                        for (int p = 0; p < playersCount; p++)
                        {
                            players[p] = new Player();
                            players[p].Id = p;
                            players[p].PlayerCards[0] = DealCard(p);
                            players[p].PlayerCards[1] = DealCard(p);

                        }
                        //TODO: The dealer is dealt one card face up, one card face down.
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error");
                    }
                    //Currently, just get a value between 16-21 for the dealer
                    dealerTotal = cardRandomizer.Next(15, 22);
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */

                for (int p = 0; p < playersCount; p++)
                {
                    Console.WriteLine("");
                    //if player is playing
                    DisplayWelcomeMessage(p);

                    do
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
                            Hit(p);
                        }
                    }
                    while (playerChoice.Equals("H") && !players[p].Choice.Equals("L"));

                    if (playerChoice.Equals("S"))
                    {
                        if (players[p].PlayerTotal == dealerTotal)
                        {
                            Console.WriteLine("The dealer's total was also {0}", dealerTotal);
                            players[p].Choice = "L";
                            WriteResponse(p);
                        }
                        else if (players[p].PlayerTotal > dealerTotal && players[p].PlayerTotal <= 21)
                        {
                            Console.WriteLine("The dealer's total is {0} ", dealerTotal);
                            players[p].Choice = "W";
                            WriteResponse(p);
                        }
                        else if (players[p].PlayerTotal < dealerTotal)
                        {
                            Console.WriteLine("The dealer's total was {0}", dealerTotal);
                            players[p].Choice = "L";
                            WriteResponse(p);
                        }
                    }

                    /* END GAME LOOP */
                }

                PlayAgain();
            }
        }

        static void WriteResponse(int p)
        {
            Console.WriteLine("");

            string[] funnyWinStrings = {
    "You actually won? Miracles do happen, apparently.",
    "Congratulations on your victory, I guess even a broken clock is right twice a day.",
    "You won? Well, even a blind squirrel finds a nut sometimes.",
    "Look who won! Don't get too cocky, luck can turn on you in an instant.",
    "Winning this time? Must be a glitch in the matrix.",
    "A victory for you? The odds must have been in your favor for once.",
    "Oh look, another win for you. Even a broken clock is right twice a day.",
    "You won? Don't let it get to your head, even a blind squirrel finds a nut sometimes."
};

            string[] funnyLoseStrings = {
    "You lost? Shocking, said no one ever.",
    "Oops, you lost! If only failure burned calories, you'd be a marathon runner by now.",
    "Another loss? You're really making failure your signature move.",
    "Losing again? Maybe consider a new hobby, like knitting or watching paint dry.",
    "Did you lose again? You're as consistent as gravity.",
    "Another loss? You're like a magnet for disappointment."
};


            // Choose a random funny string based on the player's choice
            Random random = new Random();

            Console.WriteLine("Game Stats:");
            
                string result = "";
                string playerName = "Players " + (players[p].Id + 1);
                string scoreText = ", your score was " + players[p].PlayerTotal + ", ";

                if (players[p].Choice == "W")
                {
                    result = funnyWinStrings[random.Next(funnyWinStrings.Length)];
                }
                else if (players[p].Choice == "L")
                {
                    result = funnyLoseStrings[random.Next(funnyLoseStrings.Length)];
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{playerName} ");
                Console.Write(scoreText);
                if (players[p].Choice == "W")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\r\n   .,--.\r\n .' __  \\\r\n | .._  |\r\n |{)(} .'\r\n / /|  |.\r\n(_/ /____)\r\n  |_||\r\n    /'\r\n    //\r\n  .'''\\\r\n /\\:::/\\\r\n( /|::|\\\\\r\n_\\:|;;|{/_\r\n'.;|**|\\;,/\r\n   \\_ /\r\n   | ||\r\n   | ||\r\n   | ||\r\n   | ||\r\n ._| ||_.\r\n;,_.-._,;");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\r\n__      __    ____     __    __    _____   __    __     ____   __   ___  \r\n) \\    / (   / __ \\    ) )  ( (   / ____\\  ) )  ( (    / ___) () ) / __) \r\n \\ \\  / /   / /  \\ \\  ( (    ) ) ( (___   ( (    ) )  / /     ( (_/ /    \r\n  \\ \\/ /   ( ()  () )  ) )  ( (   \\___ \\   ) )  ( (  ( (      ()   (     \r\n   \\  /    ( ()  () ) ( (    ) )      ) ) ( (    ) ) ( (      () /\\ \\    \r\n    )(      \\ \\__/ /   ) \\__/ (   ___/ /   ) \\__/ (   \\ \\___  ( (  \\ \\   \r\n   /__\\      \\____/    \\______/  /____/    \\______/    \\____) ()_)  \\_\\  \r\n                                                                         \r\n");
                }
                Console.WriteLine(result);
                Console.ResetColor();         
                Thread.Sleep(5000);
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        static void DisplayWelcomeMessage(int p)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Its players {0} turn!", p + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("You were dealt the cards : {0} and {1} ", players[p].PlayerCards[0].Name, players[p].PlayerCards[1].Name);
            Console.WriteLine("Your total is {0} ", players[p].PlayerTotal);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit(int p)
        {
            players[p].PlayerCardCount += 1;
            players[p].PlayerCards[players[p].PlayerCardCount] = DealCard(p);
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", players[p].PlayerCards[players[p].PlayerCardCount].Name, players[p].PlayerTotal);

            //Is this true? I don't think it is.
            if (players[p].PlayerTotal.Equals(21) && players[p].PlayerCardCount == 1)
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);
                players[p].Choice = "W";
            }
            else if (players[p].PlayerTotal > 21)
            {
                Console.WriteLine("The dealer's Total was {0}", dealerTotal);
                players[p].Choice = "L";
                WriteResponse(p);
            }
        }

        static Card DealCard(int p)
        {
            int cardValue = cardRandomizer.Next(1, 14);
            players[p].PlayerTotal = players[p].PlayerTotal + GetCardValue(cardValue).Value;
            if (cardValue == 13 && players[p].PlayerTotal > 21)
            {
                players[p].PlayerTotal = players[p].PlayerTotal - GetCardValue(cardValue).Value + 1;
            }
            return GetCardValue(cardValue);
        }


        static Card GetCardValue(int cardValue)
        {
            return cardValue switch
            {
                1 => new Card() { Name = "One", Value = 1 },
                2 => new Card() { Name = "Two", Value = 2 },
                3 => new Card() { Name = "Three", Value = 3 },
                4 => new Card() { Name = "Four", Value = 4 },
                5 => new Card() { Name = "Five", Value = 5 },
                6 => new Card() { Name = "Six", Value = 6 },
                7 => new Card() { Name = "Seven", Value = 7 },
                8 => new Card() { Name = "Eight", Value = 8 },
                9 => new Card() { Name = "Nine", Value = 9 },
                10 => new Card() { Name = "Ten", Value = 10 },
                11 => new Card() { Name = "Queen", Value = 10 },
                12 => new Card() { Name = "King", Value = 10 },
                13 => new Card() { Name = "Ace", Value = 11 },
               
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
                playersCount = 0;
                Array.Clear(players);
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