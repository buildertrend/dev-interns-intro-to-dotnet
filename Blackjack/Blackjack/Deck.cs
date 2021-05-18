using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{

    public enum names
    {
        Ace,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen,
        king
    }

    public enum suits
    {
        heart,
        diamond,
        spades,
        clubs
    }

    class Deck
    {
        Card[] cardSet;
        int dealt;

        Deck()
        {
            cardSet = new Card[52];
            dealt = 0;
        }

        private static void shuffle(Card[] set)
        {
            int[] mirror = new int[set.Length];
            
            for (int i = 0; i < set.Length; i++)
            {
                mirror[i] = i;
            }

            Random seed = new Random();

            for (int i = 0; i < 500; i++)
            {
                int one = seed.Next() % 52;
                int two = seed.Next() % 52;
                int temp;

                temp = mirror[one];
                mirror[one] = mirror[two];
                mirror[two] = temp;
                Console.WriteLine("Shuffling.....");


            }

        }
    }
}
