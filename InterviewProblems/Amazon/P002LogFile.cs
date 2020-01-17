using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Amazon
{
    public class P002LogFile
    {
        public List<string> Reorder(int logFileSize, string[] logLines)
        {
            var splitLines = logLines.Select(logLine => logLine.Split(' ', 2));

            return splitLines.Where(line => char.IsLetter(line[1][0]))
                .OrderBy(x => x[1])
                .ThenBy(y => y[0])
                .Select(z => string.Join(" ", z))
                .Concat(splitLines.Where(line => char.IsDigit(line[1][0])).Select(x => string.Join(" ", x)))
                .ToList();
        }
    }
}
