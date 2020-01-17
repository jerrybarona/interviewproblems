using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P009LongestSubstringWithVowelsInEvenCount
    {
        private static readonly Dictionary<char, int> _vowels =
            new[] { 'a', 'e', 'i', 'o', 'u' }
            .Select((v, idx) => (v, idx))
            .ToDictionary(x => x.v, x => x.idx);

        public string LongestSubstring(string str)
        {
            var maxIdx = 0;
            var maxLen = 0;

            for (var (i, state, map) = (0, 0, new Dictionary<int, int> { { 0, -1 } }); i < str.Length; ++i)
            {
                if (_vowels.ContainsKey(str[i])) state ^= 1 << _vowels[str[i]];
                if (map.ContainsKey(state))
                {
                    var currLen = i - map[state];
                    if (currLen > maxLen)
                    {
                        maxLen = currLen;
                        maxIdx = map[state] + 1;
                    }
                }
                else map.Add(state, i);
            }

            return str.Substring(maxIdx, maxLen);
        }
    }
}
