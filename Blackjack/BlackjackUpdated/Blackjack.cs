// Blackjack.cs
using System;
using System.Collections.Generic;

namespace BlackjackUpdated
{
    public class Blackjack
    {
        private List<Card> fullDeck;
        private List<int> cardsPulled;
        private List<Player> players;
        private List<Card> dealerCards;
        private int dealerTotal;
        private Random cardRandomizer;

        public Blackjack()
        {
            InitializeDeck();
            cardsPulled = new List<int>();
            players = new List<Player>();
            dealerCards = new List<Card>();
            dealerTotal = 0;
            cardRandomizer = new Random();
        }

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

        private void InitializePlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player($"Player {i + 1}"));
            }
        }

        private void DealInitialCards()
        {
            dealerCards.Add(DealCard());
            dealerCards.Add(DealCard());
            dealerTotal = AddPoints(dealerTotal, dealerCards[0].Value);
            dealerTotal = AddPoints(dealerTotal, dealerCards[1].Value);

            foreach (var player in players)
            {
                player.Cards.Add(DealCard());
                player.Cards.Add(DealCard());
                player.Total = AddPoints(player.Total, player.Cards[0].Value);
                player.Total = AddPoints(player.Total, player.Cards[1].Value);
            }
        }

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
                    Console.WriteLine($"{player.Name} wins with a total of {player.Total}.");
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

        private void DisplayPlayerCards(Player player)
        {
            Console.WriteLine($"{player.Name} was dealt:");
            foreach (var card in player.Cards)
            {
                card.Display();
            }
            Console.WriteLine($"{player.Name}'s total is {player.Total}");
        }

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
