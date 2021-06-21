using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace hangman
{
    public class GameState
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
        public bool hintGiven;
        public string timer;
        public int wordGuessCount;
        public Stopwatch stopwatch = new Stopwatch();
        public bool scoreSaved;

        public GameState(string word, int max)
        {
            livesMax = max;
            wordTgt = word;
            livesCur = livesMax;
            gameWon = false;
            hintGiven = false;
            wordGuessCount = 0;
            stopwatch.Start();
            scoreSaved = false;
            wordLen = wordTgt.Length;

            // Create list of characters that represent current state of guessed word
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

            // Create list of characters that represent target word to guess
            for (int i = 0; i < wordLen; i++)
            {
                wordTgtList.Add(wordTgt[i]);
            }

            // testing
            Console.Clear();
            Console.WriteLine("DEBUG");
            Console.WriteLine("The correct Capital is " + wordTgt);
            Console.ReadLine();
            Console.Clear();
            // testing
        }

        public string Timer
        {
            get 
            {
                timer = stopwatch.Elapsed.ToString(@"m\:ss");
                stopwatch.Stop();
                return timer;
            }
        }

        public bool wordHasLetter(GameState gameState, char letter)
        {
            // Create lowercase-only list of target characters for comparison
            List<char> wordTgtListLower = new List<char>();
            for (int i = 0; i < gameState.wordTgtList.Count; i++)
            {
                wordTgtListLower.Add(char.ToLower(gameState.wordTgtList[i]));
            }

            // Returns the index of each instance of the guessed letter
            int[] matchedLetters = wordTgtListLower.Select((c, i) => c == letter ? i : -1).Where(i => i != -1).ToArray();

            if (matchedLetters.Length != 0)
            {
                // If any letters were matched, update list that tracks current state of guessed word
                foreach (int element in matchedLetters)
                {
                    gameState.wordCurList[element] = gameState.wordTgtList[element];
                }
                return true;
            }
            else
            {
                // If no letters were matched add them to the wrong letters list (in uppercase for clean display)
                gameState.wrongLettersList.Add(Char.ToUpper(letter));
                return false;
            }
        }
    }

}
