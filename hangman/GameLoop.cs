using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class GameLoop
    {
        public GameState gameLoop(Dictionary<string, string> capitalDict)
        {
            // Select random capital from list by index
            Random rand = new Random();
            List<string> capitalList = new List<string>(capitalDict.Keys);

            // Create gameState with selected random capital and 5 lives
            GameState gameState = new GameState(capitalList[rand.Next(capitalList.Count)], 5);
            
            GameRender gameRender = new GameRender();
            gameRender.RenderIntro();

            // Game loops while lives are above 0, winning exits loop with break
            while (gameState.livesCur > 0)
            {
                gameRender.RenderCore(gameState, capitalDict);
                
                // Option 1 to guess letter, 2 to guess whole word
                int selectOption = ChooseOption(gameState, capitalDict);
                if (selectOption == 1)
                {
                    gameRender.RenderCore(gameState, capitalDict);
                    Console.Write(selectOption);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("Guess a letter: ");

                    char inputLetter = ChooseLetter(gameState, selectOption, capitalDict);
                    if (gameState.wordHasLetter(gameState, inputLetter))
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
                    gameRender.RenderCore(gameState, capitalDict);
                    Console.Write(selectOption);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("Guess the capital: ");

                    string inputWord = ChooseWord(gameState, selectOption, capitalDict);

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

            return gameState;
        }

        public int ChooseOption(GameState gameState, Dictionary<string, string> capitalDict)
        {
            GameRender gameRender = new GameRender();
            int selectOption = 0;

            for (; ; )
            {
                try { selectOption = Convert.ToInt32(Console.ReadLine()); }
                catch (System.FormatException) { }
                if (selectOption == 1 || selectOption == 2) break; // Only returns 1 or 2
                Console.WriteLine();
                Console.Write("Invalid selection, press enter to retry.");
                Console.ReadLine();

                // Different renders for ChooseOption depending on whether it's being used to select letter/word guess or restart/exit
                if (gameState.gameWon == false && gameState.livesCur > 0)
                {
                    gameRender.RenderCore(gameState, capitalDict);
                }
                else
                {
                    gameRender.RenderOutro(gameState, capitalDict);
                }
            }

            return selectOption;
        }

        public char ChooseLetter(GameState gameState, int selectOption, Dictionary<string, string> capitalDict)
        {
            GameRender gameRender = new GameRender();
            char inputLetter = '0';

            for (; ; )
            {
                try { inputLetter = Convert.ToChar(Console.ReadLine()); }
                catch (System.FormatException) { }

                if (Char.IsLetter(inputLetter))
                {
                    inputLetter = Char.ToLower(inputLetter);
                    if (!gameState.inputLettersList.Contains(inputLetter)) // QOL, can only guess a given letter once
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
                gameRender.RenderCore(gameState, capitalDict);
                Console.Write(selectOption);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Guess a letter: ");
            }

            return inputLetter;
        }

        public string ChooseWord(GameState gameState, int selectOption, Dictionary<string, string> capitalDict)
        {
            GameRender gameRender = new GameRender();
            string inputWord;
            
            for (; ; )
            {
                inputWord = Console.ReadLine();

                if (capitalDict.ContainsKey(inputWord)) // QOL, can only guess an actual capital
                {
                    gameState.wordGuessCount++;
                    break;
                }
                Console.WriteLine();
                Console.Write("That's not a valid capital, press enter to retry.");
                Console.ReadLine();
                gameRender.RenderCore(gameState, capitalDict);
                Console.Write(selectOption);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Guess the capital: ");
            }

            return inputWord;
        }

        public bool ChooseOutro(GameState gameState, Dictionary<string, string> capitalDict)
        {
            GameRender gameRender = new GameRender();
            gameRender.RenderOutro(gameState, capitalDict);

            // Start over or exit
            int selectOption = ChooseOption(gameState, capitalDict);
            if (selectOption == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
