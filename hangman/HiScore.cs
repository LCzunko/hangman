using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace hangman
{
    class HiScore
    {
        public DataTable scoreTable = new DataTable();
        DateTime currDate;

        public HiScore(string playerName, GameState gameState)
        {
            // Populate scoreTable with existing scores
            scoreTable.ReadXml("hiscores.xml");

            // Get current date/time
            currDate = DateTime.Now;
            string playDate = currDate.ToString();

            // Add new row with current score from args
            DataRow row = scoreTable.NewRow();
            row[0] = playerName;
            row[1] = playDate;
            row[2] = gameState.Timer;
            row[3] = Convert.ToString(gameState.inputLettersList.Count + gameState.wordGuessCount);
            row[4] = gameState.wordTarget;
            scoreTable.Rows.Add(row);

            // Sort DataTable - least total tries on top
            scoreTable.DefaultView.Sort = "Tries asc";
            scoreTable = scoreTable.DefaultView.ToTable();

            // Keep only 10 best scores
            scoreTable = scoreTable.AsEnumerable().Take(10).CopyToDataTable();
            scoreTable.TableName = "hangman";

            // Write updated DataTable to file
            scoreTable.WriteXml("hiscores.xml", XmlWriteMode.WriteSchema);
        }

        public HiScore()
        {
            // Populate scoreTable with existing scores
            scoreTable.ReadXml("hiscores.xml");
        }

        public void RenderScores(HiScore hiScore)
        {
            // Write header
            Console.WriteLine();
            Console.Write("Name                 Date                    Time      Tries     Capital");
            Console.WriteLine();

            // Write rows
            foreach (DataRow row in hiScore.scoreTable.Rows)
            {
                string playerName;
                try { playerName = Convert.ToString(row[0]); }
                catch { playerName = " "; }
                string date = Convert.ToString(row[1]);
                string timer = Convert.ToString(row[2]);
                string guessCount = Convert.ToString(row[3]);
                string wordTarget = Convert.ToString(row[4]);

                Console.Write(playerName);
                for (int i = (21 - playerName.Length); i > 0; i--) Console.Write(" ");
                Console.Write(date);
                for (int i = (24 - date.Length); i > 0; i--) Console.Write(" ");
                Console.Write(timer);
                for (int i = (10 - timer.Length); i > 0; i--) Console.Write(" ");
                Console.Write(guessCount);
                for (int i = (10 - guessCount.Length); i > 0; i--) Console.Write(" ");
                Console.Write(wordTarget);
                Console.WriteLine();
            }
        }
    }
}
