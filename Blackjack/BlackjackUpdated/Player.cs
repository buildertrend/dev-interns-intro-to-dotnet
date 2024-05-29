using BlackjackUpdated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackUpdated
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public int Total { get; set; }
        public int CardCount { get; set; }
        public bool IsBusted { get; set; }

        public Player(string name)
        {
            Name = name;
            Cards = new List<Card>();
            Total = 0;
            CardCount = 0;
            IsBusted = false;
        }

        public void DisplayCards()
        {
            foreach (var card in Cards)
            {
                card.Display();
            }
            Console.WriteLine("Total: {0}", Total);
        }
    }

}
