using System;

namespace BlackjackUpdated
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Enter the number of players:");
                int numberOfPlayers = int.Parse(Console.ReadLine());

                Blackjack game = new Blackjack();
                game.PlayGame(numberOfPlayers);

                Console.WriteLine("Game over. Would you like to play again? (Y)es or (N)o:");
            }
            while (Console.ReadLine().ToUpper() == "Y");

            Console.WriteLine("Thanks for playing! Press any key to exit.");
            Console.ReadKey();
        }
    }
}
