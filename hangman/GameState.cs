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


        public GameState(string aWordTgt, int aLivesMax)
        {
            livesMax = aLivesMax;
            livesCur = livesMax;

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
            Console.WriteLine();

            wordCurList.ForEach(z => Console.Write("{0}", z));
            wordTgtList.ForEach(z => Console.Write("{0}", z));


            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
            // testing

        }

        public static bool wordHasLetter(GameState gameState, char aSelectLetter)
        {
            List<char> wordTgtListLower = new List<char>();
            for (int i = 0; i < gameState.wordTgtList.Count; i++)
            {
                wordTgtListLower.Add(char.ToLower(gameState.wordTgtList[i]));
            }

            // Returns the index of each instance of the guessed letter
            int[] matchedLetters = wordTgtListLower.Select((c, i) => c == aSelectLetter ? i : -1).Where(i => i != -1).ToArray();

            if (matchedLetters.Length != 0)
            {
                // do something
                Console.WriteLine("True");

                for (int i = 0; i < matchedLetters.Length; i++)
                {
                    Console.Write(matchedLetters[i] + ", ");
                }

                Console.ReadLine();
                Console.Clear();
                return true;
            }
            else
            {
                Console.WriteLine("False");
                Console.ReadLine();
                Console.Clear();
                return false;
            }

        }
    }

}
