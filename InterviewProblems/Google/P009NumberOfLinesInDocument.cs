using System;
using System.Text;

namespace InterviewProblems.Google
{
    public class P009NumberOfLinesInDocument
    {
        // https://leetcode.com/discuss/interview-experience/440123/Google-or-L3-or-Nov-2019-Offer

        public int NumberOfLines(string str, int width)
        {
            var result = new StringBuilder();
            var count = 0;
            var line = new StringBuilder();
            var word = new StringBuilder();

            for (var i = 0; i <= str.Length; ++i)
            {
                if (i < str.Length && !char.IsWhiteSpace(str[i])) word.Append(str[i]);
                else
                {
                    if (i < str.Length && word.Length == 0)
                    {
                        if (line.Length > 0 && line.Length < width) line.Append(' ');
                        continue;
                    }
                    var currLen = word.Length;
                    if (currLen + line.Length > width)
                    {
                        if (result.Length > 0) result.Append("\n");
                        result.Append(line);
                        ++count;
                        line.Length = 0;
                    }

                    if (line.Length > 0) line.Append(' ');
                    line.Append(word);
                    word.Length = 0;
                }
            }

            Console.Write(result);
            return count;
        }
    }
}
