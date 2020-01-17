using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace InterviewProblems.Utilities
{
    public class TextFileUtilities
    {
        public IEnumerable<IList<int>> ReadListOfArrays(string filePath, int arrLen)
        {
            string readText = File.ReadAllText(filePath);
            var arr = Regex.Split(readText, @"\D+").Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();
            return Enumerable.Range(0, arr.Length)
                .Where(x => x % arrLen == 0)
                .Select(y => arr.Select((v, i) => (v, i)).Where(z => z.i >= y && z.i < y + arrLen).Select(a => a.v).ToArray());
        }
    }
}
