using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hangman
{
    public static class CapitalDict
    {
        public static Dictionary<string, string> capitalDict = 
            File.ReadLines("countries_and_capitals.txt")
                .Select(line => line.Replace(" | ", "|"))
                .Select(line => line.Split('|'))
                .Select(line => line.Reverse())
                .ToDictionary(split => split.First(), split => split.Last(), StringComparer.InvariantCultureIgnoreCase); // StringComparer.InvariantCultureIgnoreCase makes containsKey method of dictionary case insensitive, see ChooseWord method
    }
}
