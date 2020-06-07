using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P023CandyCrushFollowUp
    {
        // https://leetcode.com/discuss/interview-question/380650/

        public void CandyCrushTest()
        {
            var input = "aaabbbacd";
            Console.WriteLine($"\nInput: \"{input}\"\nOutput: \"{CandyCrush(input)}\"");

            input = "baaabbbabbccccd";
            Console.WriteLine($"\nInput: \"{input}\"\nOutput: \"{CandyCrush(input)}\"");
        }

        public string CandyCrush(string s)
        {
            var memo = new Dictionary<long, long>();
            var k = MinLenKey();
            return GetString(k);

            long MinLenKey(long key = 0)
            {
                if (memo.ContainsKey(key)) return memo[key];
                var currStr = s.Select((chr, idx) => (chr, idx)).Where(x => (key & (1 << x.idx)) == 0).ToList();
                var count = 1;

                var result = key;
                var minLen = currStr.Count;
                for (var i = 1; i < currStr.Count; ++i)
                {
                    var (chr, _) = currStr[i];
                    if (chr == currStr[i-1].chr) ++count;
                    else
                    {
                        if (count >= 3)
                        {
                            var mask = GetMask(currStr.Skip(i - count).Take(count).Select(x => x.idx));
                            var next = MinLenKey(key | mask);
                            var nextLen = CountZeros(next);
                            if (nextLen < minLen)
                            {
                                minLen = nextLen;
                                result = next;
                            }
                        }
                        count = 1;
                    }
                }

                if (count >= 3)
                {
                    var mask = GetMask(currStr.Skip(currStr.Count - count).Select(x => x.idx));
                    var next = MinLenKey(key | mask);
                    var nextLen = CountZeros(next);
                    if (nextLen < minLen) result = next;
                }

                memo.Add(key, result);
                return result;
            }

            long GetMask(IEnumerable<int> idxs) => idxs.Aggregate(0, (result, idx) => result | 1 << idx);

            int CountZeros(long n)
            {
                var ans = 0;
                while (n > 0)
                {
                    n &= n - 1;
                    ++ans;
                }

                return s.Length - ans;
            }

            string GetString(long key) => s.Select((chr, idx) => (chr, idx)).Aggregate(new StringBuilder(), (sb, x) =>
            {
                if ((key & (1 << x.idx)) == 0) sb.Append(x.chr);
                return sb;
            }).ToString();
        }
    }
}
