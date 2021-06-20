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
            bool gameContinue = true;

            // Creating list of capitals from dictionary so a capital can be randomly selected via index
            List<string> capitalList = new List<string>(CapitalDict.capitalDict.Keys);

            while (gameContinue == true)
            {
                Random rand = new Random();
                GameState gameState = new GameState(capitalList[rand.Next(capitalList.Count)], 5);

                GameIntro();

                while (gameState.livesCur > 0)
                {
                    RenderCore(gameState);
                    int selectOption = ChooseOption(gameState);

                    if (selectOption == 1)
                    {
                        RenderCore(gameState);
                        Console.Write(selectOption);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Guess a letter: ");

                        char inputLetter = ChooseLetter(gameState, selectOption);
                        if (GameState.wordHasLetter(gameState, inputLetter))
                        {
                            // Checks if player has guessed all the letters
                            if (gameState.wordCurList.SequenceEqual(gameState.wordTgtList))
                            {
                                gameState.gameWon = true;
                                break;
                            }
                        }
                        else
                        {
                            gameState.livesCur--;
                            Console.WriteLine();
                            Console.Write("The capital doesn't contain this letter, you lose 1 life!");
                            Console.ReadLine();
                        }

                    }
                    else if (selectOption == 2)
                    {
                        RenderCore(gameState);
                        Console.Write(selectOption);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Guess the capital: ");

                        string inputWord = ChooseWord(gameState, selectOption, CapitalDict.capitalDict);

                        // Giving proper capitalization to inputWord for later clean output in case user didn't capitalize
                        inputWord = capitalList[capitalList.FindIndex(x => x.Equals(inputWord, StringComparison.OrdinalIgnoreCase))];

                        if (inputWord == gameState.wordTgt)
                        {
                            gameState.gameWon = true;
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

                gameContinue = GameOutro(gameState);
            }

        }

        static string ChooseWord(GameState gameState, int selectOption, Dictionary<string,string> capitalDict)
        {
            string inputWord;

            for (; ; )
            {
                inputWord = Console.ReadLine();

                if (capitalDict.ContainsKey(inputWord)) { break; }
                Console.WriteLine();
                Console.Write("That's not a valid capital, press enter to retry.");
                Console.ReadLine();
                RenderCore(gameState);
                Console.Write(selectOption);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Guess the capital: ");
            }

            return inputWord;
        }

        static char ChooseLetter(GameState gameState, int selectOption)
        {
            char inputLetter ='0';

            for (; ; )
            {
                try { inputLetter = Convert.ToChar(Console.ReadLine()); }
                catch (System.FormatException) { }

                if (Char.IsLetter(inputLetter))
                {
                    inputLetter = Char.ToLower(inputLetter);
                    if  (!gameState.inputLettersList.Contains(inputLetter))
                    {
                        gameState.inputLettersList.Add(inputLetter);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You already guessed this letter, press enter to retry.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, press enter to retry.");
                }

                Console.ReadLine();
                RenderCore(gameState);
                Console.Write(selectOption);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Guess a letter: ");
            }

            return inputLetter;
        }

        static int ChooseOption(GameState gameState)
        {
            int selectOption = 0;

            for (; ; )
            {
                try { selectOption = Convert.ToInt32(Console.ReadLine()); }
                catch (System.FormatException) { }
                if (selectOption == 1 || selectOption == 2) break;
                Console.WriteLine();
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();
                if (gameState.gameWon == false)
                {
                    RenderCore(gameState);
                }
                else
                {
                    RenderOutro(gameState);
                }
            }

            return selectOption;
        }

        static void GameIntro()
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

        static void RenderCore(GameState gameState)
        {
            Console.Clear();
            Console.WriteLine();
            foreach (char element in gameState.wordCurList) { Console.Write($"{element}"); }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Lives: " + gameState.livesCur);
            Console.Write("Wrong letters: ");
            foreach (char element in gameState.wrongLettersList) { Console.Write($"{element} "); }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: ");

            return;
        }

        static bool GameOutro(GameState gameState)
        {
            RenderOutro(gameState);

            int selectOption = ChooseOption(gameState);

            if (selectOption == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void RenderOutro(GameState gameState)
        {
            Console.Clear();

            if (gameState.gameWon == true)
            {
                Console.WriteLine("You guessed the capital. You win!");
                Console.WriteLine();
                Console.WriteLine(gameState.wordTgt + " is the capital of " + CapitalDict.capitalDict[gameState.wordTgt] + ".");
            }
            else
            {
                Console.WriteLine("You lost all your lives. Game over!");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("The capital was " + gameState.wordTgt + ". It is the capital of " + CapitalDict.capitalDict[gameState.wordTgt] + ".");
            }

            Console.WriteLine();
            Console.Write("Enter \"1\" to start over, or \"2\" to exit: ");

            return;
        }

    }
}
