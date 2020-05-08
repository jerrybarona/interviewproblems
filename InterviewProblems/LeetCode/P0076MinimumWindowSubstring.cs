using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0076MinimumWindowSubstring
    {
        public void MinWindowTest()
        {
            var s = "ADOBECODEBANC";
            var t = "ABC";

            Console.WriteLine(MinWindow(s, t));
        }

        public string MinWindow(string s, string t)
        {
            var map = t.Aggregate(new Dictionary<char, int>(), (arr, c) =>
            {
                if (!arr.ContainsKey(c)) arr.Add(c, 0);
                ++arr[c];
                return arr;
            });

            var minLen = int.MaxValue;
            var minStart = 0;
            for (var (start, end, count) = (0, 0, map.Count()); end < s.Length; ++end)
            {
                if (map.ContainsKey(s[end]) && --map[s[end]] == 0) --count;

                for (; start <= end && count == 0; ++start)
                {
                    if (end - start + 1 < minLen)
                    {
                        minLen = end - start + 1;
                        minStart = start;
                    }
                    if (map.ContainsKey(s[start]) && ++map[s[start]] == 0) ++count;
                }
            }

            return minLen == int.MaxValue ? string.Empty : s.Substring(minStart, minLen);
        }
    }
}
