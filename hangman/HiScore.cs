using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace hangman
{
    public class HiScore
    {
        public DataTable scoreTable = new DataTable();
        DateTime currDate;

        public HiScore(string aPlayerName, string aPlayTime, string aTotalTries, string aWordTgt)
        {
            scoreTable.ReadXml("hiscores.xml");

            // Get current date/time
            currDate = DateTime.Now;
            string playDate = currDate.ToString();

            // Add new row with current score from args
            DataRow aRow = scoreTable.NewRow();
            aRow[0] = aPlayerName;
            aRow[1] = playDate;
            aRow[2] = aPlayTime;
            aRow[3] = aTotalTries;
            aRow[4] = aWordTgt;
            scoreTable.Rows.Add(aRow);

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
            scoreTable.ReadXml("hiscores.xml");
        }

        public static void RenderScores(HiScore hiScore)
        {
            // TEST code from stackoverflow
            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in hiScore.scoreTable.Columns)
            {
                Console.Write(col.ColumnName);
                var maxLabelSize = hiScore.scoreTable.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 10; i++) Console.Write(" ");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in hiScore.scoreTable.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    Console.Write(dataRow.ItemArray[j]);
                    for (int i = 0; i < colWidths[hiScore.scoreTable.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 10; i++) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

    }
}
