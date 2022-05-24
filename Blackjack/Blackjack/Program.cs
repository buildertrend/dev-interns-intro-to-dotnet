using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static Random cardRandomizer = new Random();
        static List<Card> deck = new List<Card>();

        static Player[] allPlayers;
        static List<Player> winningPlayers = new List<Player>();

        private static readonly Card[] dealerCards = new Card[2];
        static int dealerTotal = 0;

        static ConsoleColor defaultTextColor = Console.ForegroundColor;

        static string playAgain = "Y";

        static void Main(string[] args)
        {
            // Setup card deck
            deck = MakeCardDeck();

            // Decide number of players
            Console.WriteLine("Before we begin Blackjack, enter 1 to play just against the dealer or enter the number of friends you'd like to play with.");
            var numPlayers = int.Parse(Console.ReadLine());
            allPlayers = new Player[numPlayers];

            for (int i = 0; i < numPlayers; i++)
            {
                // i + 1 for player numbers
                allPlayers[i] = new Player(i + 1);
            }

            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    // Deal cards and add values for dealer and players
                    dealerCards[0] = DealCardDealer();
                    dealerCards[1] = DealCardDealer();

                    for (int i = 0; i < allPlayers.Length; i++)
                    {
                        // Deal two cards
                        allPlayers[i].AddCardToHand(DealCard());
                        allPlayers[i].AddCardToHand(DealCard());
                    }

                    DisplayWelcomeMessage();
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Player currentPlayer = allPlayers[i];
                    Console.WriteLine("\nPlayer {0}: ", currentPlayer.GetPlayerNum());

                    SuggestStrategy(currentPlayer.GetTotalPoints());

                    var playerChoice = "";
                    do
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit(currentPlayer);
                    }
                    else
                    {
                        int playerTotal = currentPlayer.GetTotalPoints();

                        if (playerTotal > dealerTotal && playerTotal <= 21)
                        {
                            winningPlayers.Add(currentPlayer);
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nThe Dealer's Total was: {0}.\n", dealerTotal);
                Console.ForegroundColor = defaultTextColor;

                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Player currentPlayer = allPlayers[i];
                    if (winningPlayers.Contains(currentPlayer))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Player {0} wins with a total of {1}!", currentPlayer.GetPlayerNum(), currentPlayer.GetTotalPoints());
                        Console.ForegroundColor = defaultTextColor;
                    } 
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Player {0} loses with a total of {1}. :(", currentPlayer.GetPlayerNum(), currentPlayer.GetTotalPoints());
                        Console.ForegroundColor = defaultTextColor;
                    }
                }

                /* END GAME LOOP */

                Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
                PlayAgain();
            }
        }

        private static void WriteStrategy(bool shouldHit)
        {
            if (shouldHit)
            {
                Console.WriteLine("I think you should Hit.");
            }
            else
            {
                Console.WriteLine("I think you should Stay.");
            }
        }

        private static void SuggestStrategy(int playerPoints)
        {
            // Strategies based on: https://blog.grosvenorcasinos.com/blackjack-strategy-hit-or-stand
            Card dealerCard = dealerCards[0];
            Console.ForegroundColor = ConsoleColor.Yellow;

            switch(dealerCard.Value)
            {
                case 2:
                case 3:
                    WriteStrategy(playerPoints < 13 || playerPoints == dealerCard.Value);
                    break;
                case 4:
                case 5:
                case 6:
                    WriteStrategy(playerPoints < 12 || playerPoints == dealerCard.Value);
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    WriteStrategy(playerPoints < 16);
                    break;
                case 11:
                    WriteStrategy(playerPoints < 17);
                    break;
                default:
                    Console.WriteLine("I'm not sure what you should do.");
                    break;
            }

            Console.ForegroundColor = defaultTextColor;
        }

        private static List<Card> MakeCardDeck()
        {
            List<Card> newDeck = new List<Card>();

            for (int i = 0; i < 52; i++)
            {
                Card newCard = Card.GetCardValue(i % 13);

                if (i / 13 < 1)
                {
                    newCard.Suit = "Hearts";
                }
                else if (i / 13 < 2)
                {
                    newCard.Suit = "Diamonds";
                }
                else if (i / 13 < 3)
                {
                    newCard.Suit = "Spades";
                }
                else
                {
                    newCard.Suit = "Clubs";
                }

                newDeck.Add(newCard);
            }

            return newDeck;
        }

        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            for (int i = 0; i < allPlayers.Length; i++)
            {
                Player currentPlayer = allPlayers[i];
                Console.WriteLine("Player {0} was dealt the cards: {1} and {2}.", currentPlayer.GetPlayerNum(), currentPlayer.GetCards()[0], currentPlayer.GetCards()[1]);
                Console.WriteLine("Player {0}'s Total is {1}.\n", currentPlayer.GetPlayerNum(), currentPlayer.GetTotalPoints());
            }

            // Inform the player the value of the dealer's visible card.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("The dealer was dealt a(n) {0}.", dealerCards[0]);
            Console.ForegroundColor = defaultTextColor;
        }

        static void Hit(Player player)
        {
            player.AddCardToHand(DealCard());
            Console.WriteLine("\nPlayer {0}'s card is a(n) {1} and their new Total is {2}.", player.GetPlayerNum(), player.GetMostRecentCard(), player.GetTotalPoints());

            //Is this true? I don't think it is.
            int playerTotal = player.GetTotalPoints();
            if (playerTotal.Equals(21))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Player {0} got Blackjack!", player.GetPlayerNum());
                Console.ForegroundColor = defaultTextColor;
                winningPlayers.Add(player);
            }
            else if (playerTotal > 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Player {0} busted! Sorry!", player.GetPlayerNum());
                Console.ForegroundColor = defaultTextColor;
            }
            else if (playerTotal < 21)
            {
                Console.WriteLine("Player {0}: ", player.GetPlayerNum());

                SuggestStrategy(player.GetTotalPoints());

                var playerChoice = "";
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
                else
                {
                    if (playerTotal > dealerTotal && playerTotal <= 21)
                    {
                        winningPlayers.Add(player);
                    }
                }
            }
        }

        static Card DealCard()
        {
            if (deck.Count <= 0)
            {
                // Act like adding a second deck
                deck = MakeCardDeck();
            }

            int randIndex = cardRandomizer.Next(0, deck.Count);
            Card newCard = deck[randIndex];
            deck.RemoveAt(randIndex);
            return newCard;
        }

        static Card DealCardDealer()
        {
            Card newCard = DealCard();
            dealerTotal += newCard.Value;
            return newCard;
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

                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Player currentPlayer = allPlayers[i];
                    currentPlayer.Reset();
                }
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