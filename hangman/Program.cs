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
            // Creating dictionary of key=Capital,value=Country from "countries_and_capitals.txt"
            CapitalDictProvider dictProvider = new CapitalDictProvider();
            Dictionary<string, string> capitalDict = dictProvider.capitalDict;
            Render gameRender = new Render();

            // Game loops if user decides to start over
            bool gameContinue = true;
            while (gameContinue == true)
            {
                GameState gameState = new GameState(dictProvider.RandomCapital, 5);
                Game newGame = new Game(gameRender, gameState, capitalDict);
                newGame.GameLoop();
                gameContinue = gameState.startOver;
            }
        }

    }
}
