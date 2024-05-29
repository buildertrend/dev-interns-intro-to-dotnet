using System;

namespace BlackjackUpdated
{
    public class Card
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

            return $"{top}\n{valueLine}\n{emptyLine}\n{emptyLine}\n{emptyLine}\n{suitLine}\n{bottom}";
        }

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