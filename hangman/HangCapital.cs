using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class HangCapital
    {
        public string hangTarget;
        public int hangLen;
        public List<char> hangCurrentList = new List<char>();
        public List<char> hangTargetList = new List<char>();

        public HangCapital(string aHangTarget)
        {
            hangTarget = aHangTarget;
            hangLen = hangTarget.Length;
            
            for (int i = 0; i < hangLen; i++)
            {
                if (hangTarget[i] == ' ')
                {
                    hangCurrentList.Add(' ');
                }
                else
                {
                    hangCurrentList.Add('_');
                }
            }

            for (int i = 0; i < hangLen; i++)
            {
                hangTargetList.Add(hangTarget[i]);
            }

            // testing
            Console.WriteLine("The correct Capital is " + hangTarget);
            Console.WriteLine();

            hangCurrentList.ForEach(z => Console.Write("{0}", z));
            hangTargetList.ForEach(z => Console.Write("{0}", z));


            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
            // testing

        }

        public static bool hangHasLetter (HangCapital hangCapital, char aGuessedLetter)
        {
            List<char> hangTargetListLower = new List<char>();
            for (int i = 0; i < hangCapital.hangTargetList.Count; i++)
            {
                hangTargetListLower.Add(char.ToLower(hangCapital.hangTargetList[i]));
            }

            int[] matchedLetters = hangTargetListLower.Select((c, i) => c == aGuessedLetter ? i : -1).Where(i => i != -1).ToArray();

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
