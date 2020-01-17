using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P006MinDeletionsForUniqueLetterFrequencies
    {
        public int MinDeletions(string str)
        {
            var counts = new int[str.Length + 1];
            var map = new int[26];

            for (var i = 0; i < str.Length; ++i)
            {
                var chr = str[i];
                --counts[map[chr - 'a']];
                ++map[chr - 'a'];
                ++counts[map[chr - 'a']];
            }

            var result = 0;

            for (var i = counts.Length - 1; i >= 1; --i)
            {
                if (counts[i] <= 1) continue;
                result += counts[i] - 1;
                counts[i-1] += counts[i] - 1;
            }

            return result;
        }
    }
}
