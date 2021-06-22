using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class Render
    {

        public void RenderIntro()
        {
            RenderAscii(5);
            Console.WriteLine();
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Your goal is to guess the capital of a country.");
            Console.WriteLine("You can guess one letter at a time. Guessing the wrong letter costs 1 life.");
            Console.WriteLine("When you think you have the answer, guess the entire capital! Beware, getting the capital wrong costs 2 lives.");
            Console.WriteLine();
            Console.Write("There is a special hint at 1 life left. Good luck! Press enter to continue.");
            Console.ReadLine();
        }

        public void RenderCore(GameState gameState, Dictionary<string, string> capitalDict)
        {
            // Render hint at 1 life
            if (gameState.livesCurrent == 1 && gameState.hintGiven == false) RenderHint(gameState, capitalDict);

            Console.Clear();
            RenderAscii(gameState.livesCurrent);
            Console.WriteLine("Lives: " + gameState.livesCurrent);
            Console.WriteLine();
            foreach (char letter in gameState.wordCurrentList) Console.Write($"{letter}");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Wrong letters: ");
            foreach (char letter in gameState.wrongLettersList) Console.Write($"{letter} ");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: ");
        }

        public void RenderOutro(GameState gameState, Dictionary<string, string> capitalDict)
        {
            Console.Clear();
            RenderAscii(gameState.livesCurrent);
            Console.WriteLine();

            if (gameState.gameWon == true) RenderWin(gameState, capitalDict);
            else RenderLoss(gameState, capitalDict);

            if (gameState.gameWon == true && gameState.scoreSaved == false)
            {
                gameState.scoreSaved = true;
                RenderSaveScore(gameState);
            }
            else RenderScore();

            Console.WriteLine();
            Console.Write("Enter \"1\" to start over, or \"2\" to exit: ");
        }

        void RenderHint(GameState gameState, Dictionary<string, string> capitalDict)
        {
            gameState.hintGiven = true;
            Console.WriteLine();
            Console.Write("Hint: you are guessing the capital of " + capitalDict[gameState.wordTarget] + ".");
            Console.ReadLine();
        }

        void RenderWin(GameState gameState, Dictionary<string, string> capitalDict)
        {
            Console.WriteLine("You guessed the capital. You win!");
            Console.WriteLine();
            Console.WriteLine(gameState.wordTarget + " is the capital of " + capitalDict[gameState.wordTarget] + ".");
            Console.WriteLine();
            Console.WriteLine("You guessed the capital after " + gameState.inputLettersList.Count + " letter guesses and " + gameState.wordGuessCount + " word guesses." + " It took you " + gameState.Timer + ".");
        }

        void RenderLoss(GameState gameState, Dictionary<string, string> capitalDict)
        {
            Console.WriteLine("You lost all your lives. Game over!");
            Console.WriteLine();
            Console.WriteLine("The capital was " + gameState.wordTarget + ". It is the capital of " + capitalDict[gameState.wordTarget] + ".");
            Console.WriteLine();
            Console.WriteLine("You had " + gameState.inputLettersList.Count + " letter guesses and " + gameState.wordGuessCount + " word guesses." + " You played for " + gameState.Timer + ".");
        }

        void RenderSaveScore(GameState gameState)
        {
            Console.WriteLine();
            Console.Write("Enter your name (maximum 16 characters): ");

            string playerName = Console.ReadLine();
            if (playerName.Length > 16) playerName = playerName.Substring(0, 16);

            HiScore hiScore = new HiScore(playerName, gameState);
            hiScore.RenderScores(hiScore);
        }

        void RenderScore()
        {
            HiScore hiScore = new HiScore();
            if (hiScore.scoreTable.Rows.Count > 0) hiScore.RenderScores(hiScore);
        }

        void RenderAscii(int livesCur)
        {
            switch (livesCur)
            {
                case -1:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine(" / \\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 0:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine(" / \\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 1:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine(" /    |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 2:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 3:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 4:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 5:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
            }
        }

    }
}
