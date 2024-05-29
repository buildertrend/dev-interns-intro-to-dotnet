using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static Random cardRandomizer = new Random();

        static readonly List<Card> playerCards = new List<Card>();
        static int playerTotal = 0;
        static int playerCardCount = 1;
        private static readonly List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        static int dealerCardCount = 0;

        //users to store the player choice (hit or stay)
        static string playerChoice = "";

        static string playAgain = "Y";

        static Stack<Card> DeckOfCards = CreateDeckOfCards();


        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (playAgain.ToUpper() == "Y")
            {
                //StartGame
                try
                {
                    foreach (var card in DeckOfCards!)
                    {
                        Console.WriteLine("Card: {0} of {1} with a value of {2}", card.Name, card.Suit, card.Value);
                    }

                    Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("hi");
                }

                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    //Currently, just get a value between 16-21 for the dealer
                    playerCards.Add(DealCard());
                    playerCards.Add(DealCard());


                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());

                    dealerTotal += dealerCards[0].Value;
                    dealerTotal += dealerCards[1].Value;

                    dealerCardCount = 2;

                    playerTotal += playerCards[0].Value;
                    playerTotal += playerCards[1].Value;

                    //TODO: The dealer is dealt one card face up, one card face down.
                    DisplayWelcomeMessage();
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
                    Hit();
                }

                if (playerChoice.Equals("S"))
                {
                    if (playerTotal > dealerTotal && playerTotal <= 21)
                    {
                        Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", dealerTotal);
                    }
                    else if (playerTotal < dealerTotal)
                    {
                        Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
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
            Console.WriteLine("You were dealt the cards : {0} and {1} ", playerCards[0].Name, playerCards[1].Name);
            Console.WriteLine("Your playerTotal is {0} ", playerTotal);
            Console.WriteLine("The dealer was dealt the cards : {0} and a hidden card", dealerCards[0].Name);
            //TODO: Inform the player the value of the dealer's visible card.
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards.Add(DealCard());
            playerTotal += playerCards[playerCardCount].Value;
            Console.WriteLine("You card is a(n) {0} and your new Total is {1}. ", playerCards[playerCardCount].Name, playerTotal);

            DealOutDealersCards();

            //Is this true? I don't think it is.
            if (playerTotal.Equals(21) && playerCardCount == 1)
            {
                Console.WriteLine("You got Blackjack! The dealer's Total was {0}. ", dealerTotal);

            }
            else if (playerTotal > 21)
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

        private static void DealOutDealersCards()
        {
            while (dealerTotal < 17)
            {
                dealerCardCount += 1;
                dealerCards[dealerCardCount] = DealCard();
                dealerTotal += dealerCards[dealerCardCount].Value;
            }
        }


        private class Card
        {
            public int Value;
            public string Name;
            public string Suit;
        }

        static Card DealCard()
        {
            return DeckOfCards.Pop();
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

        static string GetCardName(int cardValue)
        {
            return cardValue switch
            {
                1 => "Two",
                2 => "Three",
                3 => "Four",
                4 => "Five",
                5 => "Six",
                6 => "Seven",
                7 => "Eight",
                8 => "Nine",
                9 => "Ten",
                10 => "Jack",
                11 => "Queen",
                12 => "King",
                13 => "Ace",
                _ => "Two",
            };

        }

        //method that creatres a card for each card in the deck and each suit
        static Stack<Card> CreateDeckOfCards()
        {
            //create a list of cards 4 suits * 13 cards spades, hearts, diamonds, clubs
            Stack<Card> deckOfCards = new Stack<Card>();
            string[] suits = { "Spades", "Hearts", "Diamonds", "Clubs" };
            foreach (var suit in suits)
            {
                for (int i = 1; i <= 13; i++)
                {
                    deckOfCards.Push(item: new Card()
                    {
                        Name = GetCardName(i),
                        Value = GetCardValue(i).Value,
                        Suit = suit
                    });
                }
            }

            return deckOfCards;

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