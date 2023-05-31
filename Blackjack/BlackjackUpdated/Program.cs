using Blackjack;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        //Deck is a collection of cards 
        static Deck deck = new Deck();

        //This will be used for card counting
        static List<Card> pulledCards = new List<Card>();

        //ConsoleColors are used to style the console
        static ConsoleColor tableColor = ConsoleColor.DarkGreen;
        static ConsoleColor textColor = ConsoleColor.White;

        //Players are a list of players objects stored in an array
        static Player[] players;

        //Current player is an int used to determine, which player's turn it is 
        static int currentPLayer = 0;

        //Dealer is a player object that the players are trying to beat
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

                    //shuffle the deck (list of cards)
                    deck.Shuffle();

                    //Dealing a dealer two cards
                    for(int i = 0; i < 2; i++)
                    {
                        Card card = deck.TakeCard();
                        pulledCards.Add(card);
                        dealer.AddCardToHand(card);
                        resetDeckIfNeeded(deck);
                    }

                
                }
                else
                {
                    Environment.Exit(0);
                }
                
                for(currentPLayer = 0; currentPLayer < players.Length; currentPLayer++){

                    //Dealing a player two cards 
                    for (int i = 0; i < 2; i++)
                    {
                        Card card = deck.TakeCard();
                        pulledCards.Add(card);
                        players[currentPLayer].AddCardToHand(card);
                        resetDeckIfNeeded(deck);

                    }    

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

                //here see if dealer needs more cards and if the dealer wins because he already has higher cards 
                while(dealer.getPlayerTotal() < 17 )
                {
                    Card card = deck.TakeCard();
                    dealer.AddCardToHand(card);
                    pulledCards.Add(card);
                    resetDeckIfNeeded(deck);
                    if (dealer.getPlayerTotal() > 21)
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
            Console.WriteLine("The dealer's face up card is a {0}", dealer.playerCards[0].cardNumber);

            //Inform the user of their cards and total
            Console.WriteLine("Player {0}, you were dealt the cards: {1} and a {2} ", currentPLayer + 1, players[currentPLayer].playerCards[0].cardNumber, players[currentPLayer].playerCards[1].cardNumber);
            Console.WriteLine("Your player total is {0} ", players[currentPLayer].getPlayerTotal());
            
        }

        static void Hit()
        {
            Card card = deck.TakeCard();
            pulledCards.Add(card);
            players[currentPLayer].AddCardToHand(card);
            resetDeckIfNeeded(deck);
            Console.WriteLine("You card is an {0} and your new Total is {1}. ", card.cardNumber, players[currentPLayer].getPlayerTotal());

           
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
                deck.Reset();

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

        static void resetDeckIfNeeded(Deck deck)
        {
            if (deck.CardsLeft() == 0)
            {
                deck.Reset();
                deck.Shuffle();
                pulledCards = new List<Card>();
            }
        }

        static void informPLayer(int currentPlayer)
        {
            //TODO here add stuff to inform player on what to do based on cards played
        }
        
    }
}