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
            // Create DataTable columns
            // aPlayerName|playDate|aPlayTime|aTotalTries|aWordTgt
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "Date";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Tries";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Capital";
            scoreTable.Columns.Add(column);

            // Populate DataTable with existing hiscores.txt data
            string[] txtLines = File.ReadAllLines("hiscores.txt");
            foreach (string txtLine in txtLines)
            {
                var txtCols = txtLine.Split('|');
                DataRow txtRow = scoreTable.NewRow();
                for (int cIndex = 0; cIndex < 5; cIndex++)
                {
                    txtRow[cIndex] = txtCols[cIndex];
                }
                scoreTable.Rows.Add(txtRow);
            }

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

            // Write updated DataTable to file
            //todo
        }

        public HiScore()
        {
            // Create DataTable columns
            // aPlayerName|playDate|aPlayTime|aTotalTries|aWordTgt
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "Date";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Tries";
            scoreTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Capital";
            scoreTable.Columns.Add(column);

            // Populate DataTable with existing hiscores.txt data
            string[] txtLines = File.ReadAllLines("hiscores.txt");
            foreach (string txtLine in txtLines)
            {
                var txtCols = txtLine.Split('|');
                DataRow txtRow = scoreTable.NewRow();
                for (int cIndex = 0; cIndex < 5; cIndex++)
                {
                    txtRow[cIndex] = txtCols[cIndex];
                }
                scoreTable.Rows.Add(txtRow);
            }
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
