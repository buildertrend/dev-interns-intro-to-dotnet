using System;
using System.Collections.Generic;

namespace BlackjackUpdated
{
    /// <summary>
    /// Game of blackjack that can be played with multiple players
    /// </summary>
    public class Blackjack
    {
        private List<Card> fullDeck;
        private List<int> cardsPulled;
        private List<Player> players;
        private List<Card> dealerCards;
        private int dealerTotal;
        private Random cardRandomizer;

        /// <summary>
        /// Constructor for the game of blackjacik
        /// </summary>
        public Blackjack()
        {
            InitializeDeck();
            cardsPulled = new List<int>();
            players = new List<Player>();
            dealerCards = new List<Card>();
            dealerTotal = 0;
            cardRandomizer = new Random();
        }

        /// <summary>
        /// Plays the game of blackjack with the selected number of players
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        public void PlayGame(int numberOfPlayers)
        {
            InitializePlayers(numberOfPlayers);
            DealInitialCards();

            foreach (var player in players)
            {
                PlayerTurn(player);
            }

            DealerTurn();
            DetermineWinners();
        }

        /// <summary>
        /// Resets the fullDeck class variable to be a full deck of cards
        /// </summary>
        private void InitializeDeck()
        {
            fullDeck = new List<Card>
            {
                new Card("Two", 2, "Spades"), new Card("Two", 2, "Diamonds"), new Card("Two", 2, "Hearts"), new Card("Two", 2, "Clubs"),
                new Card("Three", 3, "Spades"), new Card("Three", 3, "Diamonds"), new Card("Three", 3, "Hearts"), new Card("Three", 3, "Clubs"),
                new Card("Four", 4, "Spades"), new Card("Four", 4, "Diamonds"), new Card("Four", 4, "Hearts"), new Card("Four", 4, "Clubs"),
                new Card("Five", 5, "Spades"), new Card("Five", 5, "Diamonds"), new Card("Five", 5, "Hearts"), new Card("Five", 5, "Clubs"),
                new Card("Six", 6, "Spades"), new Card("Six", 6, "Diamonds"), new Card("Six", 6, "Hearts"), new Card("Six", 6, "Clubs"),
                new Card("Seven", 7, "Spades"), new Card("Seven", 7, "Diamonds"), new Card("Seven", 7, "Hearts"), new Card("Seven", 7, "Clubs"),
                new Card("Eight", 8, "Spades"), new Card("Eight", 8, "Diamonds"), new Card("Eight", 8, "Hearts"), new Card("Eight", 8, "Clubs"),
                new Card("Nine", 9, "Spades"), new Card("Nine", 9, "Diamonds"), new Card("Nine", 9, "Hearts"), new Card("Nine", 9, "Clubs"),
                new Card("Ten", 10, "Spades"), new Card("Ten", 10, "Diamonds"), new Card("Ten", 10, "Hearts"), new Card("Ten", 10, "Clubs"),
                new Card("Jack", 10, "Spades"), new Card("Jack", 10, "Diamonds"), new Card("Jack", 10, "Hearts"), new Card("Jack", 10, "Clubs"),
                new Card("Queen", 10, "Spades"), new Card("Queen", 10, "Diamonds"), new Card("Queen", 10, "Hearts"), new Card("Queen", 10, "Clubs"),
                new Card("King", 10, "Spades"), new Card("King", 10, "Diamonds"), new Card("King", 10, "Hearts"), new Card("King", 10, "Clubs"),
                new Card("Ace", 11, "Spades"), new Card("Ace", 11, "Diamonds"), new Card("Ace", 11, "Hearts"), new Card("Ace", 11, "Clubs")
            };
        }

        /// <summary>
        /// Initializes the players class list with the number of players in the params. Asks and assigns the name of each player as they are added
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void InitializePlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"What is Player {i + 1}'s name?");
                players.Add(new Player(Console.ReadLine()));
            }
        }

        /// <summary>
        /// Deals the inital two cards to each player and the dealer, as well as display the dealers first card
        /// </summary>
        private void DealInitialCards()
        {
            dealerCards.Add(DealCard());
            dealerCards.Add(DealCard());
            dealerTotal = AddPoints(dealerTotal, dealerCards[0].Value);
            dealerTotal = AddPoints(dealerTotal, dealerCards[1].Value);

            Console.WriteLine("The Dealer's First Card is:");
            dealerCards[0].Display();

            foreach (var player in players)
            {
                player.Cards.Add(DealCard());
                player.Cards.Add(DealCard());
                player.Total = AddPoints(player.Total, player.Cards[0].Value);
                player.Total = AddPoints(player.Total, player.Cards[1].Value);
            }
        }

        /// <summary>
        /// Method where the param player takes their specific turn. They can hit up untill they bust or stay.
        /// </summary>
        /// <param name="player"></param>
        private void PlayerTurn(Player player)
        {
            Console.WriteLine($"{player.Name}'s turn:");
            DisplayPlayerCards(player);

            while (true)
            {
                Console.WriteLine("Would you like to (H)it or (S)tay?");
                var playerChoice = Console.ReadLine().ToUpper();

                if (playerChoice == "H")
                {
                    player.Cards.Add(DealCard());
                    player.Total = AddPoints(player.Total, player.Cards[player.Cards.Count - 1].Value);
                    DisplayPlayerCards(player);

                    if (player.Total > 21)
                    {
                        Console.WriteLine($"{player.Name} busted!");
                        break;
                    }
                }
                else if (playerChoice == "S")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter H or S.");
                }
            }
        }

        /// <summary>
        /// Method for the dealers turn, who hits untill he has 17 or more points.
        /// </summary>
        private void DealerTurn()
        {
            Console.WriteLine("Dealer's turn:");
            DisplayDealerCards();

            while (dealerTotal < 17)
            {
                dealerCards.Add(DealCard());
                dealerTotal = AddPoints(dealerTotal, dealerCards[dealerCards.Count - 1].Value);
                DisplayDealerCards();
            }
        }

        /// <summary>
        /// Prints out in the consol which players beat, busted, or lost to the dealer
        /// </summary>
        private void DetermineWinners()
        {
            foreach (var player in players)
            {
                if (player.Total > 21)
                {
                    Console.WriteLine($"{player.Name} busted with a total of {player.Total}.");
                } 
                else if (dealerTotal > 21 || player.Total > dealerTotal)
                {
                    if (player.CardCount.Equals(1))
                    {
                        Console.WriteLine($"{player.Name} wins with a Blackjack.");

                    } 
                    else
                    {
                        Console.WriteLine($"{player.Name} wins with a total of {player.Total}.");

                    }
                }
                else if (player.Total < dealerTotal)
                {
                    Console.WriteLine($"{player.Name} loses with a total of {player.Total}.");
                }
                else
                {
                    Console.WriteLine($"{player.Name} ties with the dealer with a total of {player.Total}.");
                }
            }
        }

        /// <summary>
        /// Gets a card from the deck that hasn't been gotten before this game
        /// </summary>
        /// <returns>New card from the deck</returns>
        private Card DealCard()
        {
            int cardValue = cardRandomizer.Next(0, fullDeck.Count);

            while (cardsPulled.Contains(cardValue))
            {
                cardValue = cardRandomizer.Next(0, fullDeck.Count);
            }

            cardsPulled.Add(cardValue);
            return fullDeck[cardValue];
        }

        /// <summary>
        /// Summation method that adds the points param to the total param and returns. This is for the Ace calculation as they can be 11 or 1 point depending if they bust the player/dealer
        /// </summary>
        /// <param name="total"></param>
        /// <param name="points"></param>
        /// <returns>New Total points</returns>
        private int AddPoints(int total, int points)
        {
            if (points != 11 || total + points <= 21)
            {
                total += points;
            }
            else
            {
                total += 1;
            }
            return total;
        }

        /// <summary>
        /// Displays all of the cards of the selected player
        /// </summary>
        /// <param name="player"></param>
        private void DisplayPlayerCards(Player player)
        {
            Console.WriteLine($"{player.Name} was dealt:");
            foreach (var card in player.Cards)
            {
                card.Display();
            }
            Console.WriteLine($"{player.Name}'s total is {player.Total}");
        }

        /// <summary>
        /// Displays the dealer's cards
        /// </summary>
        private void DisplayDealerCards()
        {
            Console.WriteLine("Dealer's cards:");
            foreach (var card in dealerCards)
            {
                card.Display();
            }
            Console.WriteLine("Dealer's total is {0}", dealerTotal);
        }
    }
}
