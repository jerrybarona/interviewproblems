using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P004LongestSubstringWihtoutTwoContiguousOccurrencesOfLetter
    {
        public string LongestSubstring(string str)
        {
            var idx = 0;
            var len = 1;
            for (var (count, start, end) = (1, 0, 1); end < str.Length; ++end)
            {
                if (str[end] == str[end - 1]) ++count;
                else count = 1;

                if (count > 2)
                {
                    start = end - 1;
                    count = 2;
                }

                if (end - start + 1 > len)
                {
                    idx = start;
                    len = end - start + 1;
                }
            }

            return str.Substring(idx, len);
        }
    }
}
