using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class BlackjackConsoleColor
    {
        public static ConsoleColor[] colors = (ConsoleColor[]) ConsoleColor.GetValues(typeof(ConsoleColor));

        public static ConsoleColor background = Console.BackgroundColor;
        public static ConsoleColor foreground = Console.ForegroundColor;

        public static void display()
        {
            // Display all foreground colors except the one that matches the background.
            Console.WriteLine("All the foreground colors except {0}, the background color:",
                              background);
            foreach (var color in colors)
            {
                if (color == background) continue;

                Console.ForegroundColor = color;
                Console.WriteLine("   The foreground color is {0}.", color);
            }
            Console.WriteLine();
            // Restore the foreground color.

            // Restore the original console colors.
            Console.ResetColor();
            Console.WriteLine("\nOriginal colors restored...");
        }

        public static string ReadLine()
        {
            return ReadLine(ConsoleColor.Yellow);
        }

        public static string ReadLine(ConsoleColor color )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string value = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return value;
        } 

        public static void WriteLineColor(string[] textList, string[] valueList, ConsoleColor color)
        {
            for (int i = 0; i < textList.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(textList[i]);
                if (i < valueList.Length && valueList[i] != null)
                {
                    // Replace the values with the better color
                    Console.ForegroundColor = color;
                    Console.Write(valueList[i]);
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteLineValue(string[] textList, string[] valueList)
        {
            WriteLineColor(textList, valueList, ConsoleColor.Yellow);
        }

        public static void WriteLineOption(string[] textList, string[] valueList)
        {
            WriteLineColor(textList, valueList, ConsoleColor.Cyan);
        }
    }
}
