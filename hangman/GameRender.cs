using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class GameRender
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

            return;
        }

        public void RenderCore(GameState gameState, Dictionary<string, string> capitalDict)
        {
            // Render hint at 1 life
            if (gameState.livesCur == 1 && gameState.hintGiven == false)
            {
                gameState.hintGiven = true;
                Console.WriteLine();
                Console.Write("Hint: you are guessing the capital of " + capitalDict[gameState.wordTgt] + ".");
                Console.ReadLine();
            }

            Console.Clear();
            RenderAscii(gameState.livesCur);
            Console.WriteLine("Lives: " + gameState.livesCur);
            Console.WriteLine();
            foreach (char element in gameState.wordCurList) { Console.Write($"{element}"); }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Wrong letters: ");
            foreach (char element in gameState.wrongLettersList) { Console.Write($"{element} "); }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter \"1\" to guess a letter, or \"2\" to guess the whole Capital: ");

            return;
        }

        public void RenderOutro(GameState gameState, Dictionary<string, string> capitalDict)
        {
            Console.Clear();
            RenderAscii(gameState.livesCur);
            Console.WriteLine();
            if (gameState.gameWon == true)
            {
                Console.WriteLine("You guessed the capital. You win!");
                Console.WriteLine();
                Console.WriteLine(gameState.wordTgt + " is the capital of " + capitalDict[gameState.wordTgt] + ".");
                Console.WriteLine();
                Console.WriteLine("You guessed the capital after " + gameState.inputLettersList.Count + " letter guesses and " + gameState.wordGuessCount + " word guesses." + " It took you " + gameState.Timer + ".");
            }
            else
            {
                Console.WriteLine("You lost all your lives. Game over!");
                Console.WriteLine();
                Console.WriteLine("The capital was " + gameState.wordTgt + ". It is the capital of " + capitalDict[gameState.wordTgt] + ".");
                Console.WriteLine();
                Console.WriteLine("You had " + gameState.inputLettersList.Count + " letter guesses and " + gameState.wordGuessCount + " word guesses." + " You played for " + gameState.Timer + ".");
            }

            if (gameState.gameWon == true && gameState.scoreSaved == false)
            {
                Console.WriteLine();

                Console.Write("Enter your name (maximum 16 characters): ");
                string playerName = Console.ReadLine();
                if (playerName.Length > 16) { playerName = playerName.Substring(0, 16); }

                Console.WriteLine();
                HiScore hiScore = new HiScore(playerName, gameState);
                hiScore.RenderScores(hiScore);
            }
            else
            {
                HiScore hiScore = new HiScore();

                // Only renders scores if there are any
                if (hiScore.scoreTable.Rows.Count > 0)
                {
                    Console.WriteLine();
                    hiScore.RenderScores(hiScore);
                }
            }

            Console.WriteLine();
            Console.Write("Enter \"1\" to start over, or \"2\" to exit: ");

            return;
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
