using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P009SubstringOfSizekWithkDistinctChars
    {
        public string[] UniqueSubstrings(string s, int k)
        {
            var result = new HashSet<string>();
            var map = new int[26];

            for (var (start, end) = (0, 0); end < s.Length; ++end)
            {
                ++map[s[end] - 'a'];
                while (map[s[end] - 'a'] > 1) --map[s[start++] - 'a'];

                if (end - start + 1 == k)
                {
                    result.Add(s.Substring(start, k));
                    --map[s[start++] - 'a'];
                }
            }

            return result.ToArray();
        }
    }
}
