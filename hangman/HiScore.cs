﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace hangman
{
    public class HiScore
    {
        public DataTable scoreTable = new DataTable();
        DateTime currDate;

        public HiScore(string playerName, GameState gameState)
        {
            gameState.scoreSaved = true;
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
            row[4] = gameState.wordTgt;
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

        public HiScore(GameState gameState)
        {
            // Populate scoreTable with existing scores
            scoreTable.ReadXml("hiscores.xml");
        }

        public void RenderScores(HiScore hiScore)
        {
            // Write header
            Console.Write("Name                 Date                    Time      Tries     Capital");
            Console.WriteLine();

            // Write rows
            foreach (DataRow row in hiScore.scoreTable.Rows)
            {
                string col0;
                try { col0 = Convert.ToString(row[0]); }
                catch { col0 = " "; }
                string col1 = Convert.ToString(row[1]);
                string col2 = Convert.ToString(row[2]);
                string col3 = Convert.ToString(row[3]);
                string col4 = Convert.ToString(row[4]);

                Console.Write(col0);
                for (int i = (21 - col0.Length); i > 0; i--) { Console.Write(" "); }
                Console.Write(col1);
                for (int i = (24 - col1.Length); i > 0; i--) { Console.Write(" "); }
                Console.Write(col2);
                for (int i = (10 - col2.Length); i > 0; i--) { Console.Write(" "); }
                Console.Write(col3);
                for (int i = (10 - col3.Length); i > 0; i--) { Console.Write(" "); }
                Console.Write(col4);
                Console.WriteLine();
            }
        }
    }
}
