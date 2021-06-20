using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class GameState
    {
        public int livesMax;
        public int livesCur;
        public string wordTgt;
        public int wordLen;
        public List<char> wordCurList = new List<char>();
        public List<char> wordTgtList = new List<char>();
        public List<char> inputLettersList = new List<char>();
        public List<char> wrongLettersList = new List<char>();
        public bool gameWon;

        public GameState(string aWordTgt, int aLivesMax)
        {
            livesMax = aLivesMax;
            livesCur = livesMax;
            gameWon = false;

            wordTgt = aWordTgt;
            wordLen = wordTgt.Length;

            for (int i = 0; i < wordLen; i++)
            {
                if (wordTgt[i] == ' ')
                {
                    wordCurList.Add(' ');
                }
                else
                {
                    wordCurList.Add('_');
                }
            }

            for (int i = 0; i < wordLen; i++)
            {
                wordTgtList.Add(wordTgt[i]);
            }

            // testing
            Console.WriteLine("The correct Capital is " + wordTgt);
            Console.ReadLine();
            Console.Clear();
            // testing

        }

        public static bool wordHasLetter(GameState gameState, char aInputLetter)
        {
            List<char> wordTgtListLower = new List<char>();
            for (int i = 0; i < gameState.wordTgtList.Count; i++)
            {
                wordTgtListLower.Add(char.ToLower(gameState.wordTgtList[i]));
            }

            // Returns the index of each instance of the guessed letter
            int[] matchedLetters = wordTgtListLower.Select((c, i) => c == aInputLetter ? i : -1).Where(i => i != -1).ToArray();

            if (matchedLetters.Length != 0)
            {
                foreach (int element in matchedLetters)
                {
                    gameState.wordCurList[element] = gameState.wordTgtList[element];
                }
                return true;
            }
            else
            {
                gameState.wrongLettersList.Add(Char.ToUpper(aInputLetter));
                return false;
            }
        }
    }

}
