using Blackjack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Blackjack.ConsoleControlHandler;

namespace BlackjackUpdated
{
    class Program
    {
        static readonly List<Card> playerCards = new List<Card>();
        static int playerTotal = 0;
        static int playerCardCount = 1;

        private static readonly List<Card> dealerCards = new List<Card>();
        static int dealerTotal = 0;
        static int dealerCardCount = 1;

        static List<Card> fullDeck = new List<Card>
        {
            new Card("Two", 2, "Spades"),
            new Card("Two", 2, "Diamonds"),
            new Card("Two", 2, "Hearts"),
            new Card("Two", 2, "Clubs"),
            new Card("Three", 3, "Spades"),
            new Card("Three", 3, "Diamonds"),
            new Card("Three", 3, "Hearts"),
            new Card("Three", 3, "Clubs"),
            new Card("Four", 4, "Spades"),
            new Card("Four", 4, "Diamonds"),
            new Card("Four", 4, "Hearts"),
            new Card("Four", 4, "Clubs"),
            new Card("Five", 5, "Spades"),
            new Card("Five", 5, "Diamonds"),
            new Card("Five", 5, "Hearts"),
            new Card("Five", 5, "Clubs"),
            new Card("Six", 6, "Spades"),
            new Card("Six", 6, "Diamonds"),
            new Card("Six", 6, "Hearts"),
            new Card("Six", 6, "Clubs"),
            new Card("Seven", 7, "Spades"),
            new Card("Seven", 7, "Diamonds"),
            new Card("Seven", 7, "Hearts"),
            new Card("Seven", 7, "Clubs"),
            new Card("Eight", 8, "Spades"),
            new Card("Eight", 8, "Diamonds"),
            new Card("Eight", 8, "Hearts"),
            new Card("Eight", 8, "Clubs"),
            new Card("Nine", 9, "Spades"),
            new Card("Nine", 9, "Diamonds"),
            new Card("Nine", 9, "Hearts"),
            new Card("Nine", 9, "Clubs"),
            new Card("Ten", 10, "Spades"),
            new Card("Ten", 10, "Diamonds"),
            new Card("Ten", 10, "Hearts"),
            new Card("Ten", 10, "Clubs"),
            new Card("Jack", 10, "Spades"),
            new Card("Jack", 10, "Diamonds"),
            new Card("Jack", 10, "Hearts"),
            new Card("Jack", 10, "Clubs"),
            new Card("Queen", 10, "Spades"),
            new Card("Queen", 10, "Diamonds"),
            new Card("Queen", 10, "Hearts"),
            new Card("Queen", 10, "Clubs"),
            new Card("King", 10, "Spades"),
            new Card("King", 10, "Diamonds"),
            new Card("King", 10, "Hearts"),
            new Card("King", 10, "Clubs"),
            new Card("Ace", 11, "Spades"),
            new Card("Ace", 11, "Diamonds"),
            new Card("Ace", 11, "Hearts"),
            new Card("Ace", 11, "Clubs")
        };

        static List<int> cardsPulled = new List<int>();

        static string playerChoice = "";
        static string playAgain = "Y";

        static void Main(string[] args)
        {
            while (playAgain.ToUpper() == "Y")
            {
                Console.WriteLine("Welcome to Blackjack - are you ready to play? (Y)es (N)o");
                var decision = Console.ReadLine().ToUpper();

                if (decision == "Y")
                {
                    // Deals the dealer two cards
                    dealerCards.Add(DealCard());
                    dealerCards.Add(DealCard());

                    dealerTotal = AddPoints(dealerTotal, dealerCards[0].Value);
                    dealerTotal = AddPoints(dealerTotal, dealerCards[1].Value);

                    // Deals the player two cards
                    playerCards.Add(DealCard());
                    playerCards.Add(DealCard());

                    playerTotal = AddPoints(playerTotal, playerCards[0].Value);
                    playerTotal = AddPoints(playerTotal, playerCards[1].Value);

                    DisplayWelcomeMessage();

                    // If the player gets a blackjack
                    if (playerTotal.Equals(21) && playerCardCount.Equals(1))
                    {
                        if (dealerTotal.Equals(21) && dealerCardCount.Equals(1))
                        {
                            Console.WriteLine("Sorry! You got a blackjack, but the dealer also got a blackjack and the house always wins!!");
                        }
                        else
                        {
                            Console.WriteLine("Congrats! You got a blackjack and won the game!! The dealer's total is {0}", dealerTotal);
                        }
                        PlayAgain();
                    }
                }
                else
                {
                    Environment.Exit(0);
                }

                // Start game loop
                do
                {
                    Console.WriteLine("Would you like to (H)it or (S)tay?");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

                if (playerChoice.Equals("H"))
                {
                    Hit();
                }

                if (playerTotal <= 21)
                {
                    DealerPlay();
                }

                if (playerChoice.Equals("S") || playerTotal > 21)
                {
                    GameResult();
                }

                PlayAgain();
            }
        }

        private static int AddPoints(int total, int points)
        {
            if (points != 11 || total + points <= 21)
            {
                total += points;
            }
            else
            {
                total += 1;
            }
            return total;
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("You were dealt the cards:");
            playerCards[0].Display();
            playerCards[1].Display();
            Console.WriteLine("Your total is {0}", playerTotal);
            Console.WriteLine("The Dealer's first card is:");
            dealerCards[0].Display();
        }

        static void Hit()
        {
            playerCardCount += 1;
            playerCards.Add(DealCard());
            playerTotal = AddPoints(playerTotal, playerCards[playerCardCount - 1].Value);

            playerCards[playerCardCount - 1].Display();
            Console.WriteLine("Your new total is {0}", playerTotal);

            if (playerTotal <= 21)
            {
                do
                {
                    Console.WriteLine("Would you like to hit or stay? (H)it or (S)tay");
                    playerChoice = Console.ReadLine().ToUpper();
                }
                while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                if (playerChoice.ToUpper() == "H")
                {
                    Hit();
                }
            }
        }

        private class Card
        {
            public int Value { get; }
            public string Name { get; }
            public string Suit { get; }

            public Card(string name, int value, string suit)
            {
                Name = name;
                Value = value;
                Suit = suit;
            }

            public void Display()
            {
                string pixelArt = GetPixelArt();
                Console.WriteLine(pixelArt);
            }

            private string GetPixelArt()
            {
                string top = "┌─────────┐";
                string bottom = "└─────────┘";
                string valueLine = $"│ {GetSymbol(),-6}{GetIcon()} │";
                string suitLine = $"│ {GetIcon()}     {GetSymbol(),-2}│";
                string emptyLine = "│         │";

                return $"{top}\n{valueLine}\n{emptyLine}\n{emptyLine}\n{suitLine}\n{bottom}";
            }

            private string GetIcon()
            {
                if (Suit.Equals("Spades"))
                {
                    return "♠";
                } else if (Suit.Equals("Hearts"))
                {
                    return "♥";
                } else if (Suit.Equals("Diamonds"))
                {
                    return "♦";
                } else if (Suit.Equals("Clubs"))
                {
                    return "♣";
                } else
                {
                    return $"{Suit}";
                }
            }

            private string GetSymbol()
            {
                switch (Name)
                {
                    case "Ace":
                        return "A";
                    case "Two":
                        return "2";
                    case "Three":
                        return "3";
                    case "Four":
                        return "4";
                    case "Five":
                        return "5";
                    case "Six":
                        return "6";
                    case "Seven":
                        return "7";
                    case "Eight":
                        return "8";
                    case "Nine":
                        return "9";
                    case "Ten":
                        return "10";
                    case "Jack":
                        return "J";
                    case "Queen":
                        return "Q";
                    case "King":
                        return "K";
                    default:
                        return "NAN";
                }
            }
        }

        static Card DealCard()
        {
            Random cardRandomizer = new Random();
            int cardValue = cardRandomizer.Next(0, fullDeck.Count);

            while (cardsPulled.Contains(cardValue))
            {
                cardValue = cardRandomizer.Next(0, fullDeck.Count);
            }

            cardsPulled.Add(cardValue);
            return fullDeck[cardValue];
        }

        private static void DealerPlay()
        {
            Console.WriteLine("The Dealer's second card was:");
            dealerCards[1].Display();
            while (dealerTotal < 17 && dealerTotal < playerTotal)
            {
                dealerCardCount += 1;
                dealerCards.Add(DealCard());
                dealerTotal = AddPoints(dealerTotal, dealerCards[dealerCardCount - 1].Value);

                dealerCards[dealerCardCount - 1].Display();
                Console.WriteLine("The Dealer hit and his new score is {0}", dealerTotal);
            }
        }

        private static void GameResult()
        {
            if (playerTotal > 21)
            {
                Console.WriteLine("You busted! Sorry! The dealer's total was {0}", dealerTotal);
            }
            else if ((playerTotal > dealerTotal && playerTotal <= 21) || dealerTotal > 21)
            {
                Console.WriteLine("Congrats! You won the game! The dealer's total is {0}", dealerTotal);
            }
            else if (playerTotal <= dealerTotal && dealerTotal <= 21)
            {
                Console.WriteLine("Sorry, you lost! The dealer's total was {0}", dealerTotal);
            }
        }

        static void PlayAgain()
        {
            Console.WriteLine("Would you like to play again? (Y)es or (N)o?");
            do
            {
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            if (playAgain.Equals("Y"))
            {
                Console.Clear();
                dealerTotal = 0;
                dealerCardCount = 1;
                playerCards.Clear();
                dealerCards.Clear();
                playerCardCount = 1;
                playerTotal = 0;
                cardsPulled.Clear();
            }
            else if (playAgain.Equals("N"))
            {
                Environment.Exit(0);
            }
        }
    }
}