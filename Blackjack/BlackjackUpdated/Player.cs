using BlackjackUpdated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackUpdated
{
    /// <summary>
    /// Player class that contains all of the relevant information for a player in a game of blackjack
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Name of the Player
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of cards the player has
        /// </summary>
        public List<Card> Cards { get; set; }
        /// <summary>
        /// Total points this player has
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// number of cards this player has
        /// </summary>
        public int CardCount { get; set; }
        /// <summary>
        /// Status of the player, true if busted, false if not
        /// </summary>
        public bool IsBusted { get; set; }

        /// <summary>
        /// Player constructor that takes a game and assigns everything else to 0 or empty
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            Name = name;
            Cards = new List<Card>();
            Total = 0;
            CardCount = 0;
            IsBusted = false;
        }

        /// <summary>
        /// Displays all of the cartds of this player's Cards list
        /// </summary>
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
