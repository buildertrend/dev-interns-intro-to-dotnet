using System;

namespace BlackjackUpdated
{
    /// <summary>
    /// Class that contains all of the information and relevant methods for a card in a regular deck of playing cards
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Value of the card in a game of blackjack (Aces are 11)
        /// </summary>
        public int Value { get; }
        /// <summary>
        /// Name of the card as a string
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Suit of the card as a string
        /// </summary>
        public string Suit { get; }

        /// <summary>
        /// Constructor that builds the card
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="suit"></param>
        public Card(string name, int value, string suit)
        {
            Name = name;
            Value = value;
            Suit = suit;
        }

        /// <summary>
        /// Displays the card as a pixel art piece
        /// </summary>
        public void Display()
        {
            string pixelArt = GetPixelArt();
            if (Suit == "Hearts" || Suit == "Diamonds")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine(pixelArt);

            Console.ResetColor();
        }

        /// <summary>
        /// returns the string of the pixel art card
        /// </summary>
        /// <returns></returns>
        private string GetPixelArt()
        {
            string top = "┌─────────┐";
            string bottom = "└─────────┘";
            string valueLine = $"│ {GetSymbol(),-6}{GetIcon()} │";
            string suitLine = $"│ {GetIcon()}     {GetSymbol(),-2}│";
            string emptyLine = "│         │";

            return $"{top}\n{valueLine}\n{emptyLine}\n{emptyLine}\n{emptyLine}\n{suitLine}\n{bottom}";
        }

        /// <summary>
        /// Gets the icon of the card based on the suit
        /// </summary>
        /// <returns>Icon of the card</returns>
        private string GetIcon()
        {
            if (Suit.Equals("Spades"))
            {
                return "♠";
            }
            else if (Suit.Equals("Hearts"))
            {
                return "♥";
            }
            else if (Suit.Equals("Diamonds"))
            {
                return "♦";
            }
            else if (Suit.Equals("Clubs"))
            {
                return "♣";
            }
            else
            {
                return $"{Suit}";
            }
        }

        /// <summary>
        /// Returns the shortened symbol of the card that would appear on it
        /// </summary>
        /// <returns>Symbol of the card</returns>
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
}