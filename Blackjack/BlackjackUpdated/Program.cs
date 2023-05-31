using Blackjack;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        //Colors used f
        static ConsoleColor tableColor = ConsoleColor.DarkGreen;
        static ConsoleColor textColor = ConsoleColor.White;
        static Player[] players;
        static int currentPLayer = 0;
        static Player dealer; 
     

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = tableColor;

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

                    Console.Write("How many players will be playing: ");

                    int amountOfPLayers = Convert.ToInt32(Console.ReadLine());

              
                    //this is the method that creates players
                    CreatePlayers(amountOfPLayers);


                    //Dealing a dealer two cards
                    dealer.AddCardToHand(DealCard());
                    dealer.AddCardToHand(DealCard());

                    
                }
                else
                {
                    Environment.Exit(0);
                }
                
                for(currentPLayer = 0; currentPLayer < players.Length; currentPLayer++){

                    //Dealing a player two cards 
                    players[currentPLayer].AddCardToHand(DealCard());
                    players[currentPLayer].AddCardToHand(DealCard());

                    DisplayWelcomeMessage();


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
                        if(currentPLayer != players.Length - 1)
                        {
                            Console.WriteLine("Moving to next player");
                        }
                       
                   
                }

                }

                //here see if dealer needs more cards
                while(dealer.getPlayerTotal() < 17 )
                {
                    dealer.AddCardToHand(DealCard());
                    if(dealer.getPlayerTotal() > 21)
                    {
                        Console.WriteLine("The dealer has busted!");
                    }
                    bool dealerWin = true;
                    for (int i = 0; i < players.Length; i++)
                    {
                        
                        if(dealer.getPlayerTotal() < players[i].getPlayerTotal())
                        {
                            dealerWin = false;
                        }
                    }

                    if (dealerWin)
                    {
                        break;
                    }
                }

                //here check which players win
                for(int i = 0; i < players.Length; i++){
                    if(dealer.getPlayerTotal() > 21 && players[i].getPlayerTotal() <= 21)
                    {
                        Console.WriteLine("Congrats Player {0}! You won the game! The dealer busted ", i + 1);
                    }
                    else if (players[i].getPlayerTotal() > dealer.getPlayerTotal() && players[i].getPlayerTotal() <= 21)
                    {
                        Console.WriteLine("Congrats Player {0}! You won the game! The dealer's total is {1} ", i + 1, dealer.getPlayerTotal());
                    }
                    else if ( players[i].getPlayerTotal() < dealer.getPlayerTotal())
                    {
                        Console.WriteLine("Sorry Player {0}, you lost! The dealer's total was {1}", i + 1, dealer.getPlayerTotal());
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
            //Inform the player of the dealer's cards
            Console.WriteLine("The dealer's face up card is a {0}", dealer.playerCards[0].Name);

            //Inform the user of their cards and total
            Console.WriteLine("Player {0}, you were dealt the cards: {1} and a {2} ", currentPLayer + 1, players[currentPLayer].playerCards[0].Name, players[currentPLayer].playerCards[1].Name);
            Console.WriteLine("Your player total is {0} ", players[currentPLayer].getPlayerTotal());
            
        }

        static void Hit()
        {
            Card drawCard = DealCard();
            players[currentPLayer].AddCardToHand(drawCard);
            Console.WriteLine("You card is an {0} and your new Total is {1}. ", drawCard.Name, players[currentPLayer].getPlayerTotal());

           
            if (players[currentPLayer].getPlayerTotal().Equals(21))
            {
                Console.WriteLine("Player {0} got Blackjack!", currentPLayer + 1);

            }
            else if (players[currentPLayer].getPlayerTotal() > 21)
            {
                Console.WriteLine("Player {0} busted! Sorry!", currentPLayer + 1);

            }
            else if (players[currentPLayer].getPlayerTotal() < 21)
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
            int cardValue = cardRandomizer.Next(1, 14);
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

        static void CreatePlayers(int numberOfPlayers)
        {
            dealer = new Player();
            players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = new Player();
            }       
            Console.WriteLine(numberOfPlayers + " players will be playing the game");
        }

        
    }
}