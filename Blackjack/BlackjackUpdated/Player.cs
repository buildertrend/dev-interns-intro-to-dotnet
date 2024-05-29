using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        private int id;
        private Card[] playerCards = new Card[11];
        int playerCardCount = 1;
        int playerTotal = 0;
        String choice = "P";


        public int Id
        {
            get { return id; }   // get method
            set { id = value; }  // set method
        }
        public Card[] PlayerCards
        {
            get { return playerCards; }   // get method
            set { playerCards = value; }  // set method
        }
        public int PlayerCardCount
        {
            get { return playerCardCount; }   // get method
            set { playerCardCount = value; }  // set method
        }
        public int PlayerTotal
        {
            get { return playerTotal; }   // get method
            set { playerTotal = value; }  // set method
        }

        public String Choice
        {
            get { return choice; }   // get method
            set { choice = value; }  // set method
        }
    }


}
