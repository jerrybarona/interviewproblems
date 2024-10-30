using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P2781LengthOfTheLongestValidSubstring : ITestable
    {
        public void RunTest()
        {
            foreach (var (word, forbidden) in new (string, IList<string>)[]
            {
                ("baaaca", new[]{"aaa","baaac"})
            })
            {
                Console.WriteLine(LongestValidSubstring(word, forbidden));
            }
        }

        public int LongestValidSubstring(string word, IList<string> forbidden)
        {
            var maxLen = forbidden.Select(x => x.Length).Max();
            var minLen = forbidden.Select(x => x.Length).Min();
            var wordSet = new HashSet<string>(forbidden);
            var wordLen = word.Length;

            var result = 0;

            for (var (s, e) = (0, 0); e < wordLen; ++e)
            {
                for (var i = e - minLen + 1; i >= s && e - i + 1 <= maxLen; --i)
                {
                    var w = word[i..(e + 1)];
                    if (wordSet.Contains(w))
                    {
                        s = i + 1;
                        break;
                    }
                }

                result = Math.Max(result, e - s + 1);
            }

            return result;
        }
    }
}
