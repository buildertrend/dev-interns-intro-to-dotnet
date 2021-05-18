using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Types
{
    public class GameState
    {
        public static Player player = new Player() { Name = "Player", Type = PlayerType.Human };
        public static Player dealer = new Player() { Name = "Dealer", Type = PlayerType.Dealer };
        public static Deck deck = new Deck();
    }

}
