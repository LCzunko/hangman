﻿using System;
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
            Dictionary<string, string> capitalDict = new CapitalDictProvider().capitalDict;

            bool gameContinue = true;
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
