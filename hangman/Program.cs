using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Dictionary<string, string> capitalDict =
                      File.ReadLines("countries_and_capitals.txt")
                          .Select(line => line.Replace(" | ", "|"))
                          .Select(line => line.Split('|'))
                          .Select(line => line.Reverse())
                          .ToDictionary(split => split.First(), split => split.Last(), StringComparer.InvariantCultureIgnoreCase);
            
            /*

            Testing dictionary

            foreach (KeyValuePair<string, string> pair in capitalDict)
            {
                Console.WriteLine(pair);
            }

            Console.ReadLine();

            */

            Lives lives = new Lives(5);
            lives.curLives = lives.maxLives;

            RenderIntro();

            RenderChoose(lives);
            int selectGuess = ChooseGuess(lives);

            if (selectGuess == 1)
            {
                RenderGuessLetter(lives, selectGuess);
                char selectLetter = ChooseLetter(lives, selectGuess);
                Console.WriteLine("The character is " + selectLetter);
                Console.ReadLine();
                
            }
            else if (selectGuess == 2)
            {
                RenderGuessCapital(lives, selectGuess);
                string selectCapital = ChooseCapital(lives, selectGuess, capitalDict);
                Console.WriteLine("The Capital is " + selectCapital);
                Console.ReadLine();
            }


            Console.WriteLine();
            Console.WriteLine("Successfully selected " + selectGuess);


            Console.ReadLine();
        }

        static string ChooseCapital(Lives lives, int selectGuess, Dictionary<string,string> capitalDict)
        {
            string selectCapital;

            for (; ; )
            {
                selectCapital = Console.ReadLine();

                if (capitalDict.ContainsKey(selectCapital)) {
                    selectCapital = char.ToUpper(selectCapital[0]) + selectCapital.Substring(1).ToLower();
                    break; }
                Console.WriteLine("");
                Console.Write("That's not a valid capital, press enter to retry.");
                Console.ReadLine();
                RenderGuessCapital(lives, selectGuess);
            }

            return selectCapital;
        }

        static char ChooseLetter(Lives lives, int selectGuess)
        {
            char selectLetter ='0';

            for (; ; )
            {
                try { selectLetter = Convert.ToChar(Console.ReadLine()); }
                catch (System.FormatException) { }
                if (Char.IsLetter(selectLetter)) break;
                Console.WriteLine("");
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                RenderGuessLetter(lives, selectGuess);
            }

            selectLetter = Char.ToLower(selectLetter);
            return selectLetter;
        }

        static int ChooseGuess(Lives lives)
        {
            int selectGuess = 0;

            for (; ; )
            {
                try { selectGuess = Convert.ToInt32(Console.ReadLine()); }
                catch (System.FormatException) { }
                if (selectGuess == 1 || selectGuess == 2) break;
                Console.WriteLine("");
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                RenderChoose(lives);
            }

            return selectGuess;
        }

        static void RenderIntro()
        {
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Your goal is to guess the capital of a country.");
            Console.WriteLine("You can guess one letter at a time. Guessing the wrong letter costs 1 life.");
            Console.WriteLine("When you think you have the answer, guess the entire Capital! Beware, getting the capital wrong costs 2 lives.");
            Console.WriteLine();
            Console.Write("Good luck! Press enter to continue.");
            Console.ReadLine();

            return;
        }

        static void RenderChoose(Lives lives)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + lives.curLives);
            Console.WriteLine("");
            Console.Write("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: ");

            return;
        }

        static void RenderGuessLetter(Lives lives, int selectGuess)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + lives.curLives);
            Console.WriteLine("");
            Console.WriteLine("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: " + selectGuess);
            Console.WriteLine();
            Console.Write("Guess a letter: ");

            return;
        }

        static void RenderGuessCapital(Lives lives, int selectGuess)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + lives.curLives);
            Console.WriteLine("");
            Console.WriteLine("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: " + selectGuess);
            Console.WriteLine();
            Console.Write("Guess a Capital: ");

            return;
        }

    }
}
