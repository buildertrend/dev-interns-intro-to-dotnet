// Program.cs
using System;

namespace BlackjackUpdated
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of players:");
            int numberOfPlayers = int.Parse(Console.ReadLine());

            Blackjack game = new Blackjack();
            game.PlayGame(numberOfPlayers);

            Console.WriteLine("Game over. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
