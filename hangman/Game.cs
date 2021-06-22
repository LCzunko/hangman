using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class Game
    {
        public GameState GameLoop(Dictionary<string, string> capitalDict)
        {
            GameState gameState = new GameState(RandomizeCapital(capitalDict), 5);
            Render gameRender = new Render();
            gameRender.RenderIntro();

            // Game loops while lives are above 0, winning exits loop with break
            while (gameState.livesCurrent > 0)
            {
                gameRender.RenderCore(gameState, capitalDict);
                
                // Option 1 to guess letter, 2 to guess whole word
                int selectOption = ChooseOption(gameState, capitalDict);
                if (selectOption == 1) GuessLetter(gameState, capitalDict, selectOption);
                else if (selectOption == 2) GuessWord(gameState, capitalDict, selectOption);
                if (gameState.gameWon == true) break;
            }

            return gameState;
        }

        public bool ChooseOutro(GameState gameState, Dictionary<string, string> capitalDict)
        {
            Render gameRender = new Render();
            gameRender.RenderOutro(gameState, capitalDict);

            // Start over or exit
            int selectOption = ChooseOption(gameState, capitalDict);
            if (selectOption == 1) return true;
            else return false;
        }

        string RandomizeCapital(Dictionary<string, string> capitalDict)
        {
            Random rand = new Random();
            List<string> capitalList = new List<string>(capitalDict.Keys);
            return capitalList[rand.Next(capitalList.Count)];
        }

        GameState GuessLetter(GameState gameState, Dictionary<string, string> capitalDict, int selectOption)
        {
            Render gameRender = new Render();
            gameRender.RenderCore(gameState, capitalDict);
            Console.Write(selectOption);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Guess a letter: ");

            char inputLetter = ChooseLetter(gameState, selectOption, capitalDict);
            if (gameState.WordHasLetter(gameState, inputLetter))
            {
                // Checks if player has guessed all the letters
                if (gameState.wordCurrentList.SequenceEqual(gameState.wordTargetList)) gameState.gameWon = true;
            }
            else
            {
                gameState.livesCurrent--;
                Console.WriteLine();
                Console.Write("The capital doesn't contain this letter, you lose 1 life!");
                Console.ReadLine();
            }
            return gameState;
        }

        GameState GuessWord(GameState gameState, Dictionary<string, string> capitalDict, int selectOption)
        {
            Render gameRender = new Render();
            gameRender.RenderCore(gameState, capitalDict);
            Console.Write(selectOption);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Guess the capital: ");

            string inputWord = ChooseWord(gameState, selectOption, capitalDict);

            if (inputWord.Equals(gameState.wordTarget, StringComparison.CurrentCultureIgnoreCase)) gameState.gameWon = true;
            else
            {
                gameState.livesCurrent--;
                gameState.livesCurrent--;
                Console.WriteLine();
                Console.Write("That's not the correct capital, you lose 2 lives!");
                Console.ReadLine();
            }
            return gameState;
        }

        int ChooseOption(GameState gameState, Dictionary<string, string> capitalDict)
        {
            Render gameRender = new Render();
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
                if (gameState.gameWon == false && gameState.livesCurrent > 0) gameRender.RenderCore(gameState, capitalDict);
                else gameRender.RenderOutro(gameState, capitalDict);
            }

            return selectOption;
        }

        char ChooseLetter(GameState gameState, int selectOption, Dictionary<string, string> capitalDict)
        {
            Render gameRender = new Render();
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
                    else Console.WriteLine("You already guessed this letter, press enter to retry.");
                }
                else Console.WriteLine("Invalid input, press enter to retry.");

                Console.ReadLine();
                gameRender.RenderCore(gameState, capitalDict);
                Console.Write(selectOption);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Guess a letter: ");
            }

            return inputLetter;
        }

        string ChooseWord(GameState gameState, int selectOption, Dictionary<string, string> capitalDict)
        {
            Render gameRender = new Render();
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

    }
}
