using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0140WordBreakII
    {
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            var maxLen = wordDict.Select(x => x.Length).Max();
            var map = wordDict
                .Select((val, idx) => (val, idx))
                .ToDictionary(x => x.val, x => x.idx);

            var result = new List<List<int>>();
            
            return CanBreak(0, new List<int>()) ? result
                .Select(list => String.Join(" ", list.Select(y => wordDict[y]))).ToList() : new List<string>();

            bool CanBreak(int idx, List<int> comb)
            {
                if (idx == s.Length)
                {
                    result.Add(new List<int>(comb));
                    return true;
                }

                var canBreak = false;
                for (var i = idx; i < s.Length && i < idx + maxLen; ++i)
                {
                    var str = s.Substring(idx, i - idx + 1);
                    if (map.ContainsKey(str))
                    {
                        comb.Add(map[str]);
                        if (CanBreak(i + 1, comb)) canBreak = true;
                        comb.RemoveAt(comb.Count - 1);
                    }
                }
                return canBreak;
            }
        }
    }
}
