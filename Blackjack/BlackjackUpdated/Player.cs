using BlackjackUpdated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Player
    {
        public int playerNumber;
        public int playerScore;
        public bool staying;
        public bool busted;

        public List<Card> playerCards;
        public Shoe GameShoe;

        public Player(int playerNum, Shoe gameShoe)
        {
            playerNumber = playerNum;
            GameShoe = gameShoe;

            busted      = false;
            staying     = false;
            playerCards = new List<Card>();
        }

        public void Deal()
        {
            this.playerCards.Add(GameShoe.Draw());
            this.playerCards.Add(GameShoe.Draw());

            this.playerScore = this.playerCards.Sum(x => x.Value);
        }

        public void AnnounceTurn()
        {
            Console.WriteLine("############################");
            Console.WriteLine("### PLAYER {0} - NEXT UP ###", playerNumber);
            Console.WriteLine("### Score: {0}\t\t###", playerScore);
            Console.WriteLine("############################");
        }

        public void Hit()
        {
            this.playerCards.Add(GameShoe.Draw());
            playerScore += this.playerCards.Last().Value;
            Console.WriteLine("Your card is a(n) {0} and your new Total is {1}. ", playerCards.Last().ToString(), playerScore);

            if (!SoftAces()) 
            {
                Console.WriteLine("You busted - Sorry! Wait for all players to finish to see the dealer's score...");
                busted = true;
            }
        }

        bool SoftAces()
        {
            for (int i = 0; i < playerCards.Count; i++)
            {
                if (playerCards[i].Value == 11)
                {
                    playerScore -= 10;
                    playerCards[i].Value = 1;
                    Console.WriteLine("Using the ace from card {0} as a soft-ace.", i);

                    playerScore = 0;
                    for (int j = 0; j <= playerCards.Count; j++)
                    {
                        playerScore += playerCards[j].Value;
                    }
                    Console.WriteLine("New player total: {0}", playerScore);
                    break;
                }
            }
            if (playerScore < 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
