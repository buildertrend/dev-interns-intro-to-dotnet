using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Blackjack
{
    public class Player
    {
        public int total;
        public List<Card> cards = new List<Card>();
        public bool done = false;
    }

}
