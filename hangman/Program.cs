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

            // Creating dictionary of key=Capital,value=Country from "countries_and_capitals.txt"
            Dictionary<string, string> capitalDict =
               File.ReadLines("countries_and_capitals.txt")
                   .Select(line => line.Replace(" | ", "|"))
                   .Select(line => line.Split('|'))
                   .Select(line => line.Reverse())
                   .ToDictionary(split => split.First(), split => split.Last(), StringComparer.InvariantCultureIgnoreCase); // StringComparer.InvariantCultureIgnoreCase makes containsKey method of dictionary case insensitive, see ChooseWord method

            // Game loops if user decides to start over
            while (gameContinue == true)
            {
                GameLoop gameLoop = new GameLoop();
                GameState gameState = gameLoop.gameLoop(capitalDict);
                gameContinue = gameLoop.ChooseOutro(gameState, capitalDict);
            }
        }

    }
}
