using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Lives lives = new Lives(5);
            lives.curLives = lives.maxLives;

            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Your goal is to guess the capital of a country.");
            Console.WriteLine("You can guess one letter at a time. Guessing the wrong letter costs 1 life.");
            Console.WriteLine("When you think you have the answer, guess the entire word! Beware, getting that wrong costs 2 lives.");
            Console.WriteLine();
            Console.Write("Good luck! Press enter to continue.");
            Console.ReadLine();

            Render(lives);
            int selectGuess = ChooseGuess(lives);

            Console.WriteLine();
            Console.WriteLine("Successfully selected " + selectGuess);
            Console.ReadLine();
        }


        static void Render(Lives lives)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + lives.curLives);
            Console.WriteLine("");
            Console.Write("Enter \"1\" to guess a letter, or \"2\" to guess the whole word: ");

            return;

        }

        static int ChooseGuess(Lives lives)
        {
            int selectGuess = 0;

            try
            {
                selectGuess = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("");
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                Render(lives);
                selectGuess = ChooseGuess(lives);
                return selectGuess;
            }

            if (selectGuess == 1)
            {
                return selectGuess;
            }
            else if (selectGuess == 2)
            {
                return selectGuess;
            }
            else
            {
                Console.WriteLine("");
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                Render(lives);
                selectGuess = ChooseGuess(lives);
                return selectGuess;
            }

        }
    }
}
