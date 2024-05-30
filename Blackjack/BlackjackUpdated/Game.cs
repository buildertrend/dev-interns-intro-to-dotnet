using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Game
    {
        private string playAgain { get; set; }
        private List<Card> _dealerCards = new List<Card>();
        private int _dealerTotal = 0;
        private string playerChoice = "";
        private List<Player> _players = new List<Player> { new Player() };
        public List<Player> players => _players;
        public List<Card> deck = new List<Card>();
        private readonly List<string> suits = new List<string> { "Spades", "Hearts", "Diamonds", "Clubs" };

        public void SetInitialGameState()
        {
            _dealerCards.Clear();
            _players.Clear();
            deck.Clear();
            playerChoice = "";
            _dealerTotal = 0;
            PopulateDeck();
        }

        public void setInitialDealerCards()
        {
            _dealerCards.Add(DealCard());
            _dealerCards.Add(DealCard());
            _dealerTotal = _dealerCards.Sum(x => x.Value);
        }
        public void askForPlayerCount()
        {
            var playerCountString = "";
            do
            {
                Console.WriteLine("Enter the number of additional players would you like (0-4).");
                playerCountString = Console.ReadLine();
            } while (!int.TryParse(playerCountString, out int number) || number < 0 || number > 4);
            var playerCount = int.Parse(playerCountString);
            for (int i = 0; i < playerCount+1; ++i)
            {
                var newPlayer = new Player();
                newPlayer.playerId = i+1;
                _players.Add(newPlayer);
            }
        }

        public void dealInitialCardsToPlayers()
        {
            for (int i = 0; i < _players.Count(); ++i)
            {
                var player = _players[i];
                player.cards.Add(DealCard());
                player.cards.Add(DealCard());
                player.total += player.cards.Sum(x => x.Value);
                if (player.total == 21)
                {
                    player.status = PlayerStatus.BLACKJACK;
                }
            }
        }

        Card DealCard()
        {
            Random cardRandomizer = new Random();
            int cardValue = cardRandomizer.Next(deck.Count);
            Card dealtCard = deck[cardValue];
            deck.RemoveAt(cardValue);
            return dealtCard;
        }

        void PopulateDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                deck.Add(CreateCardBasedOffValue(i));
            }
        }

        Card CreateCardBasedOffValue(int cardValue)
        {
            var cardSuit = cardValue % 4;
            cardValue = cardValue % 13;
            return cardValue switch
            {
                1 => new Card() { Name = "Two", Value = 2, Suit = suits[cardSuit] },
                2 => new Card() { Name = "Three", Value = 3, Suit = suits[cardSuit] },
                3 => new Card() { Name = "Four", Value = 4, Suit = suits[cardSuit] },
                4 => new Card() { Name = "Five", Value = 5, Suit = suits[cardSuit] },
                5 => new Card() { Name = "Six", Value = 6, Suit = suits[cardSuit] },
                6 => new Card() { Name = "Seven", Value = 7, Suit = suits[cardSuit] },
                7 => new Card() { Name = "Eight", Value = 8, Suit = suits[cardSuit] },
                8 => new Card() { Name = "Nine", Value = 9, Suit = suits[cardSuit] },
                9 => new Card() { Name = "Ten", Value = 10, Suit = suits[cardSuit] },
                10 => new Card() { Name = "Jack", Value = 10, Suit = suits[cardSuit] },
                11 => new Card() { Name = "Queen", Value = 10, Suit = suits[cardSuit] },
                12 => new Card() { Name = "King", Value = 10, Suit = suits[cardSuit] },
                0 => new Card() { Name = "Ace", Value = 11, Suit = suits[cardSuit] },
                _ => new Card() { Name = "Two", Value = 2, Suit = suits[cardSuit] },
            };
        }
        public void Hit(Player player)
        {
            var newCard = DealCard();
            var playerTotal = player.total;
            var playerCards = player.cards;
            playerCards.Add(newCard);
            playerTotal += newCard.Value;
            if (player.status == PlayerStatus.BLACKJACK)
            {
                Console.WriteLine("Congrats you got a blackjack! You won!");
            }
            else
            {
                Console.WriteLine("You card is a(n) {0} of {1} and your new total is {2}. ", newCard.Name, newCard.Suit, playerTotal);
                if (playerTotal > 21)
                {
                    Console.WriteLine("You busted! Sorry!");
                    player.status = PlayerStatus.BUSTED;

                }
                else if (playerTotal <= 21)
                {
                    do
                    {
                        Console.WriteLine("Would you like to hit or stay? H for hit S for stay");
                        playerChoice = Console.ReadLine().ToUpper();
                    }
                    while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));
                    if (playerChoice.ToUpper() == "H")
                    {
                        Hit(player);
                    }
                }
            }
        }
        public void DetermineGameWinners()
        {
            var highestPlayer = _players.Where(x => x.status != PlayerStatus.BUSTED).MaxBy(x => x.total);
            if (highestPlayer == null)
            {
                Console.WriteLine("Every player busted so the dealer won");
                return;
            }
            var highestPlayerTotal = highestPlayer.total;
            DealerHitsUntillWinOrBust(highestPlayer, highestPlayerTotal);
            foreach (Player player in _players)
            {
                if (_dealerTotal.Equals(player.total))
                {
                    Console.WriteLine("Player {0} lost because the dealer tied with you! The dealer's total was {1}", player.playerId, _dealerTotal);
                }
                else if (player.status == PlayerStatus.BUSTED)
                {
                    Console.WriteLine("Player {0} lost because the dealer tied with you! The dealer's total was {1}", player.playerId, _dealerTotal);
                }
                else if (player.total < _dealerTotal)
                {

                    Console.WriteLine("Player {0} won the game! The dealer's total was {1} ", player.playerId, _dealerTotal);
                }
                else
                {
                    Console.WriteLine("Player {0} won the game! The dealer's total was {1} ", player.playerId, _dealerTotal);
                }
            }
        }

        private void DealerHitsUntillWinOrBust(Player? highestPlayer, int highestPlayerTotal)
        {
            if (highestPlayerTotal > _dealerTotal && highestPlayer.status != PlayerStatus.BLACKJACK)
            {
                var initialDealerTotal = _dealerTotal;
                while (_dealerTotal < highestPlayerTotal && initialDealerTotal < 16)
                {
                    var newCard = DealCard();
                    _dealerCards.Add(newCard);
                    Console.WriteLine("The dealer hit with {0} of {1}", newCard.Name, newCard.Suit);
                    _dealerTotal += newCard.Value;
                }
            }
        }

        private void DisplayWelcomeMessage(Player player)
        {
            var playerCards = player.cards;
            var playerTotal = player.total;
            Console.WriteLine("You were dealt the cards : {0} of {1} and {2} of {3}", playerCards[0].Name, playerCards[0].Suit, playerCards[1].Name, playerCards[1].Suit);
            Console.WriteLine("Your total is {0} ", playerTotal);
            Console.WriteLine("The dealer has the card : {0} of {1} ", _dealerCards.FirstOrDefault().Name, _dealerCards.FirstOrDefault().Suit);

        }
        public bool PlayAgain()
        {
            //Loop until they make a valid choice
            do
            {
                playAgain = Console.ReadLine().ToUpper();
            }
            while (!playAgain.Equals("Y") && !playAgain.Equals("N"));

            if (playAgain.Equals("Y"))
            {
                Console.WriteLine("Press enter to restart the game!");
                Console.ReadLine();
                Console.Clear();
                SetInitialGameState();
                return true;
            }
            else if (playAgain.Equals("N"))
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Read();
                    Environment.Exit(0);
                }
            }
            return false;
        }
        public void PlayerTurn(Player player)
        {
            var playerTotal = player.total;
            player.cards = player.cards;
            Console.WriteLine("It is player {0}'s turn", player.playerId);
            DisplayWelcomeMessage(player);
            do
            {
                Console.WriteLine("Would you like to (H)it or (S)tay?");
                playerChoice = Console.ReadLine().ToUpper();
            }
            while (!playerChoice.Equals("H") && !playerChoice.Equals("S"));

            if (playerChoice.Equals("H"))
            {
                //hit will get them a card / check the total and ask for another hit
                Hit(player);
            }
            if (playerChoice.Equals("S"))
            {
                //do nothing the player's turn is over.
                return;
            }
        }
    }
}
