using Blackjack;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();
        static readonly List<Card> deck = new List<Card>();
        static readonly List<Card> playerCards = new List<Card>();
        static int playerTotal = 0;
        private static List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        private static readonly List<string> suits = new List<string>();


        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";
        // variable keeping track if they got a black jack
        static bool blackJack = false;

        static void Main(string[] args)
        {
            suits.Add("Hearts");
            suits.Add("Clubs");
            suits.Add("Diamonds");
            suits.Add("Spades");
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                try
                {
                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                    PopulateDeck();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occured during runtime: ", ex);

                }
                
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());
                    playerCards.Add(DealCard());
                    playerCards.Add(DealCard());

                    playerTotal = playerCards.Sum(x => x.Value);
                    dealerTotal = dealerCards.Sum(x => x.Value);
                    DisplayWelcomeMessage();
                    if (playerTotal.Equals(21))
                    {
                        blackJack = true;
                    }
                }
                else
                {
                    Environment.Exit(0);
                }

                /* START GAME LOOP */
                if (blackJack)
                {
                    Console.WriteLine("Congrats you got a BlackJack! You won the game!");
                } else {
                    do
                    {
                        Console.WriteLine("Would you like to (H)it or (S)tay?");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S") && !blackJack);

                    if (playerChoice.Equals("H"))
                    {
                        //hit will get them a card / check the total and ask for another hit
                        Hit();
                    }

                    if (playerChoice.Equals("S"))
                    {
                        Console.WriteLine("The dealer's second card is {0} of {1}", dealerCards[1].Name, dealerCards[1].Suit);
                        if (playerTotal > dealerTotal && playerTotal <= 21)
                        {
                            while (dealerTotal < playerTotal && dealerTotal < 16)
                            {
                                var newCard = DealCard();
                                dealerCards.Add(newCard);
                                Console.WriteLine("The dealer hit with {0} of {1}", newCard.Name, newCard.Suit);
                                dealerTotal += newCard.Value;
                            }
                            if (dealerTotal.Equals(playerTotal))
                            {
                                Console.WriteLine("Sorry, you lost because the dealer tied with you! The dealer's total was {0}", dealerTotal);
                            }
                            else
                            {
                                Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                            }
                        }
                        else if (dealerTotal.Equals(playerTotal))
                        {
                            Console.WriteLine("Sorry, you lost because the dealer tied with you! The dealer's total was {0}", dealerTotal);
                        }
                        else if (playerTotal < dealerTotal)
                        {
                            Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
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
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards : {0} of {1} and {2} of {3}", playerCards[0].Name, playerCards[0].Suit, playerCards[1].Name, playerCards[1].Suit);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            Console.WriteLine("The dealer has the card : {0} of {1} ", dealerCards.FirstOrDefault().Name, dealerCards.FirstOrDefault().Suit);

        }

        static void Hit()
        {
            var newCard = DealCard();
            playerCards.Add(newCard);
            playerTotal += newCard.Value;
            Console.WriteLine("You card is a(n) {0} of {1} and your new Total is {2}. ", newCard.Name, newCard.Suit, playerTotal);
            if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's Total was {0}", dealerTotal);

            }
            else if (playerTotal <= 21)
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
            int cardValue = cardRandomizer.Next(deck.Count);
            Card dealtCard = deck[cardValue];
            deck.RemoveAt(cardValue);
            return dealtCard;
        }

        static void PopulateDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                deck.Add(CreateCardBasedOffValue(i));
            }
        }
        static Card CreateCardBasedOffValue(int cardValue)
        {
            var cardSuit = cardValue % 4;
            cardValue = cardValue % 14;
            return cardValue switch
            {
                0 => new Card() { Name = "Two", Value = 2, Suit = suits[cardSuit] },
                1 => new Card() { Name = "Three", Value = 3, Suit = suits[cardSuit] },
                2 => new Card() { Name = "Four", Value = 4, Suit = suits[cardSuit] },
                3 => new Card() { Name = "Five", Value = 5, Suit = suits[cardSuit] },
                4 => new Card() { Name = "Six", Value = 6, Suit = suits[cardSuit] },
                5 => new Card() { Name = "Seven", Value = 7, Suit = suits[cardSuit] },
                6 => new Card() { Name = "Eight", Value = 8, Suit = suits[cardSuit] },
                7 => new Card() { Name = "Nine", Value = 9, Suit = suits[cardSuit] },
                8 => new Card() { Name = "Ten", Value = 10, Suit = suits[cardSuit] },
                9 => new Card() { Name = "Jack", Value = 10, Suit = suits[cardSuit] },
                10 => new Card() { Name = "Queen", Value = 10, Suit = suits[cardSuit] },
                11 => new Card() { Name = "King", Value = 10, Suit = suits[cardSuit] },
                12 => new Card() { Name = "Ace", Value = 11, Suit = suits[cardSuit] },
                _ => new Card() { Name = "Two", Value = 2, Suit = suits[cardSuit] },
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
                playerCards.Clear();
                dealerCards.Clear();
                playerTotal = 0;
                deck.Clear();
                blackJack = false;
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