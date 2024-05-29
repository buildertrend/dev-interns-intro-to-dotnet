using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Deck
    {
        static Random rng = new Random();
        public Stack<Card> cards = new Stack<Card>();

        public void Populate()
        {
            // Hearts
            this.cards.Push(new Card() { Name = "Two", Value = 2, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Three", Value = 3, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Four", Value = 4, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Five", Value = 5, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Six", Value = 6, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Seven", Value = 7, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Eight", Value = 8, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Nine", Value = 9, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Ten", Value = 10, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Jack", Value = 10, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Queen", Value = 10, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "King", Value = 10, Suit = "Hearts" });
            this.cards.Push(new Card() { Name = "Ace", Value = 11, Suit = "Hearts" });

            // Diamonds
            this.cards.Push(new Card() { Name = "Three", Value = 3, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Four", Value = 4, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Five", Value = 5, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Two", Value = 2, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Six", Value = 6, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Seven", Value = 7, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Eight", Value = 8, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Nine", Value = 9, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Ten", Value = 10, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Jack", Value = 10, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Queen", Value = 10, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "King", Value = 10, Suit = "Diamonds" });
            this.cards.Push(new Card() { Name = "Ace", Value = 11, Suit = "Diamonds" });

                // Clubs
            this.cards.Push(new Card() { Name = "Two", Value = 2, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Three", Value = 3, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Four", Value = 4, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Five", Value = 5, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Six", Value = 6, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Seven", Value = 7, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Eight", Value = 8, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Nine", Value = 9, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Ten", Value = 10, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Jack", Value = 10, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Queen", Value = 10, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "King", Value = 10, Suit = "Clubs" });
            this.cards.Push(new Card() { Name = "Ace", Value = 11, Suit = "Clubs" });

            // Spades
            this.cards.Push(new Card() { Name = "Two", Value = 2, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Three", Value = 3, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Four", Value = 4, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Five", Value = 5, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Six", Value = 6, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Seven", Value = 7, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Eight", Value = 8, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Nine", Value = 9, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Ten", Value = 10, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Jack", Value = 10, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Queen", Value = 10, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "King", Value = 10, Suit = "Spades" });
            this.cards.Push(new Card() { Name = "Ace", Value = 11, Suit = "Spades" });

        }


        public void Shuffle<T>(Stack<T> stack)
        {
            // Convert the stack to a list
            List<T> list = stack.ToList();

            // Shuffle the list
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            // Clear the stack and push the shuffled items back into it
            stack.Clear();
            foreach (var item in list)
            {
                stack.Push(item);
            }
        }

    }
    
}
