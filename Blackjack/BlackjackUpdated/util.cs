using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class util
    {
        public static bool checkBlackjack(Player player)
        {
            if (player.cards.Count == 2 && player.total == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkBust(Player player)
        {
            if (player.cards.Count == 2 && player.total > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Player> initPlayers(int numPlayers)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                players.Add(new Player());
            }
            return players;
        }

        public static void playerTurn(Player player, Game game)
        {
            String playerChoice;
            do
            {
                Console.WriteLine("Would you like to (H)it or (S)tay?\n");
                playerChoice = Console.ReadLine().ToUpper();

                if (playerChoice.Equals("H"))
                {
                    // Perform a hit
                    playerHit(game, player);
                }
            }
            while (!playerChoice.Equals("S") && player.total <= 21);

            // If player chooses to stay, it's the dealer's turn
            if (playerChoice.Equals("S"))
            {

                // Dealer's turn - keep hitting until their total is greater than player's total
                while (game.Dealer.total < player.total)
                {
                    dealerHit(game);
                }

                // Determine the winner and display the result
                if (game.Dealer.total > 21 || player.total <= 21 && game.Dealer.total <= 21 && player.total > game.Dealer.total)
                {
                    Console.WriteLine("Congrats! You won the game! The dealer's total is {0} ", game.Dealer.total);
                    game.Dealer.total = 0;
                }
                else if (player.total < game.Dealer.total || player.total == game.Dealer.total)
                {
                    Console.WriteLine("Sorry, you lost! The dealer's total was {0}", game.Dealer.total);
                    game.Dealer.total = 0;
                }
            }
        }


        public static void dealerHit(Game game)
        {
            // If it's the dealer's turn, deal a card to the dealer
            game.Dealer.cards.Add(game.Deck.DealCard());
            game.Dealer.total += game.Dealer.cards[game.Dealer.cards.Count - 1].Value;
            Console.WriteLine("The dealer's card is a(n) {0} of {1} and their new Total is {2}. ", game.Dealer.cards[game.Dealer.cards.Count - 1].Name, game.Dealer.cards[game.Dealer.cards.Count - 1].Suit, game.Dealer.total);

            // Check if the dealer busted
            if (game.Dealer.total > 21)
            {
                Console.WriteLine("The dealer busted! Their total is {0}", game.Dealer.total);
            }

        }

        static void playerHit(Game game, Player player)
        {
            // If it's the player's turn, deal a card to the player
            player.cards.Add(game.Deck.DealCard());
            player.total += player.cards[player.cards.Count - 1].Value;
            Console.WriteLine("Your card is a(n) {0} of {1} and your new Total is {2}. ", player.cards[player.cards.Count - 1].Name, player.cards[player.cards.Count - 1].Suit, player.total);

            // Check if the player busted
            if (player.total > 21)
            {
                Console.WriteLine("You busted! Sorry!");
            }

        }


        /// <summary>
        /// Displays a friendly message to the user and shows their current hand.
        /// </summary>
        public static void DisplayWelcomeMessage(Player player, Game game)
        {
            // Deal initial cards to dealer and player
            game.Dealer.cards.Add(game.Deck.DealCard());
            game.Dealer.cards.Add(game.Deck.DealCard());
            player.cards.Add(game.Deck.DealCard());
            player.cards.Add(game.Deck.DealCard());

            // Calculate initial totals for dealer and player
            game.Dealer.total += game.Dealer.cards[0].Value + game.Dealer.cards[1].Value;
            player.total += player.cards[0].Value + player.cards[1].Value;

            // Display player's cards and total, and dealer's visible card
            Console.WriteLine("You were dealt the cards : {0} of {1} and {2} of {3} ", player.cards[0].Name, player.cards[0].Suit, player.cards[1].Name, player.cards[1].Suit);
            Console.WriteLine("Your playerTotal is {0} ", player.total);
            //TODO: Inform the player the value of the dealer's visible card.
            Console.WriteLine("Dealer's visible card: {0} of {1}", game.Dealer.cards[0].Name, game.Dealer.cards[0].Suit);
        }

        // Method to handle user's choice to play again
        public static bool PlayAgain(Game game)
        {
            String playAgain;
            // Loop until the user makes a valid choice
            do
            {
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            // If user chooses to play again
            if (playAgain.Equals("Y"))
            {

                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();
                game.Dealer.total = 0;
                game.Dealer.cards.Clear();
                game.Players.Clear();
                game.Deck.cards.Clear();

                foreach(Player player in game.Players)
                {
                    player.total = 0;
                    player.cards.Clear();
                }
                return true;
            }
            else
            {
                return false;
            }
            

        }
    }
}
