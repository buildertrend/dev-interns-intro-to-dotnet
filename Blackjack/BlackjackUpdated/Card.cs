using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;

namespace BlackjackUpdated
{
    public class Card
    { 
        public int Value;
        public string Name;
        public string Suit;

        public Card(int suit, string name)
        {
            Name = name;
            switch(suit)
            {
                case 1:
                    Suit = "Heart";
                    break;
                case 2:
                    Suit = "Club";
                    break;
                case 3:
                    Suit = "Spade";
                    break;
                case 4:
                    Suit = "Diamond";
                    break;
            }
            switch (name)
            {
                case "Two":
                    Value = 2;
                    break;
                case "Three":
                    Value = 3;
                    break;
                case "Four":
                    Value = 4;
                    break;
                case "Five":
                    Value = 5;
                    break;
                case "Six":
                    Value = 6;
                    break;
                case "Seven":
                    Value = 7;
                    break;
                case "Eight":
                    Value = 8;
                    break;
                case "Nine":
                    Value = 9;
                    break;
                case "Ten":
                    Value = 10;
                    break;
                case "Jack":
                    Value = 10;
                    break;
                case "Queen":
                    Value = 10;
                    break;
                case "King":
                    Value = 10;
                    break;
                case "Ace":
                    Value = 11;
                    break;
            }
        }
    }
    
}