using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        public int total { get; set; }
        public PlayerStatus status { get; set; }
        public List<Card> cards { get; set; }
        public int? playerId { get; set; }
        public Player()
        {
            total = 0;
            status = PlayerStatus.READY;
            playerId = null;
            cards = new List<Card>();
        }
    }
}
