using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class Game
    {
        private GameState gameState;
        private Render gameRender;
        private readonly Dictionary<string, string> capitalDict;

        public Game(Dictionary<string, string> capitalDictData)
        {
            capitalDict = capitalDictData;
            gameState = new GameState(RandomizeCapital(), 5);
            gameRender = new Render();
        }

        public GameState GameLoop()
        {
            gameRender.RenderIntro();

            // Game loops while lives are above 0, winning exits loop with break
            while (gameState.livesCurrent > 0)
            {
                gameRender.RenderCore(gameState, capitalDict);
                
                // Option 1 to guess letter, 2 to guess whole word
                int selectOption = ChooseOption();
                if (selectOption == 1) GuessLetter(selectOption);
                else if (selectOption == 2) GuessWord(selectOption);
                if (gameState.gameWon == true) break;
            }

            GameOutro();
            return gameState;
        }

        void GameOutro()
        {
            gameRender.RenderOutro(gameState, capitalDict);

            int selectOption = ChooseOption();
            if (selectOption == 1) gameState.startOver = true;
            else gameState.startOver = false;
        }

        void GuessLetter(int selectOption)
        {
            
            gameRender.RenderCore(gameState, capitalDict);
            Console.Write(selectOption);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Guess a letter: ");

            char inputLetter = ChooseLetter(selectOption);
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
        }

        void GuessWord(int selectOption)
        {
            
            gameRender.RenderCore(gameState, capitalDict);
            Console.Write(selectOption);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Guess the capital: ");

            string inputWord = ChooseWord(selectOption);

            if (inputWord.Equals(gameState.wordTarget, StringComparison.CurrentCultureIgnoreCase)) gameState.gameWon = true;
            else
            {
                gameState.livesCurrent--;
                gameState.livesCurrent--;
                Console.WriteLine();
                Console.Write("That's not the correct capital, you lose 2 lives!");
                Console.ReadLine();
            }
        }

        int ChooseOption()
        {
            
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

        char ChooseLetter(int selectOption)
        {
            
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

        string ChooseWord(int selectOption)
        {
            
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

        string RandomizeCapital()
        {
            Random rand = new Random();
            List<string> capitalList = new List<string>(capitalDict.Keys);
            return capitalList[rand.Next(capitalList.Count)];
        }

    }
}
