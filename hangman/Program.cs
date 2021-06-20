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
            // StringComparer.InvariantCultureIgnoreCase makes containsKey method of dictionary case insensitive, see ChooseWord method

            /* Testing dictionary
            foreach (KeyValuePair<string, string> pair in capitalDict)
            { Console.WriteLine(pair); }
            Console.ReadLine();
            */

            // Creating list of capitals from dictionary so a capital can be randomly selected
            List<string> capitalList = new List<string>(capitalDict.Keys);
            Random rand = new Random();

            GameState gameState = new GameState(capitalList[rand.Next(capitalList.Count)],5);

            RenderIntro();

            while (gameState.livesCur > 0)
            {
                RenderChoose(gameState);
                int selectGuess = ChooseGuess(gameState);

                if (selectGuess == 1)
                {
                    RenderGuessLetter(gameState, selectGuess);
                    char inputLetter = ChooseLetter(gameState, selectGuess);

                    // For testing
                    Console.WriteLine("The character is " + inputLetter);
                    Console.ReadLine();

                    GameState.wordHasLetter(gameState, inputLetter);

                }
                else if (selectGuess == 2)
                {
                    RenderGuessWord(gameState, selectGuess);
                    string inputWord = ChooseWord(gameState, selectGuess, capitalDict);

                    // Giving proper capitalization to inputWord for clean display output in case user didn't capitalize
                    inputWord = capitalList[capitalList.FindIndex(x => x.Equals(inputWord, StringComparison.OrdinalIgnoreCase))];

                    if (inputWord == gameState.wordTgt)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You win!");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        gameState.livesCur--;
                        gameState.livesCur--;
                        Console.WriteLine();
                        Console.Write("That's not the correct capital, you lose 2 lives!");
                        Console.ReadLine();
                    }
                }
            }

            Console.WriteLine();
            Console.Write("You lost all your lives, Game over!");
            Console.ReadLine();

        }

        static string ChooseWord(GameState gameState, int selectGuess, Dictionary<string,string> capitalDict)
        {
            string inputWord;

            for (; ; )
            {
                inputWord = Console.ReadLine();

                if (capitalDict.ContainsKey(inputWord)) { break; }
                Console.WriteLine();
                Console.Write("That's not a valid capital, press enter to retry.");
                Console.ReadLine();
                RenderGuessWord(gameState, selectGuess);
            }

            return inputWord;
        }

        static char ChooseLetter(GameState gameState, int selectGuess)
        {
            char inputLetter ='0';

            for (; ; )
            {
                try { inputLetter = Convert.ToChar(Console.ReadLine()); }
                catch (System.FormatException) { }
                if (Char.IsLetter(inputLetter)) break;
                Console.WriteLine();
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                RenderGuessLetter(gameState, selectGuess);
            }

            inputLetter = Char.ToLower(inputLetter);
            return inputLetter;
        }

        static int ChooseGuess(GameState gameState)
        {
            int selectGuess = 0;

            for (; ; )
            {
                try { selectGuess = Convert.ToInt32(Console.ReadLine()); }
                catch (System.FormatException) { }
                if (selectGuess == 1 || selectGuess == 2) break;
                Console.WriteLine();
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                RenderChoose(gameState);
            }

            return selectGuess;
        }

        static void RenderIntro()
        {
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Your goal is to guess the capital of a country.");
            Console.WriteLine("You can guess one letter at a time. Guessing the wrong letter costs 1 life.");
            Console.WriteLine("When you think you have the answer, guess the entire Capital! Beware, getting the capital wrong costs 2 gameState.");
            Console.WriteLine();
            Console.Write("Good luck! Press enter to continue.");
            Console.ReadLine();

            return;
        }

        static void RenderChoose(GameState gameState)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + gameState.livesCur);
            Console.WriteLine();
            Console.Write("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: ");

            return;
        }

        static void RenderGuessLetter(GameState gameState, int selectGuess)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + gameState.livesCur);
            Console.WriteLine();
            Console.WriteLine("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: " + selectGuess);
            Console.WriteLine();
            Console.Write("Guess a letter: ");

            return;
        }

        static void RenderGuessWord(GameState gameState, int selectGuess)
        {
            Console.Clear();
            Console.WriteLine("Lives: " + gameState.livesCur);
            Console.WriteLine();
            Console.WriteLine("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: " + selectGuess);
            Console.WriteLine();
            Console.Write("Guess a Capital: ");

            return;
        }

    }
}
