using System;
using System.Collections.Generic;

namespace InterviewProblems.Facebook
{
    public class P009WordBreak
    {
        // https://leetcode.com/discuss/interview-question/454548/Facebook-or-Phone-or-Teoplitz-Matrix-and-Word-Break

        public void WordBreakTest()
        {
            var s = "catsanddog";
            var wordDict = new[] { "cat", "cats", "and", "sand", "dog" };

            Console.WriteLine(WordBreak(s, wordDict));
        }

        public string WordBreak(string s, IList<string> wordDict)
        {
            var memo = new List<string>[s.Length];
            var wordSet = new HashSet<string>(wordDict);

            return string.Join(' ', Break());

            List<string> Break(int idx = 0)
            {
                if (idx == s.Length) return new List<string> { string.Empty };
                if (memo[idx] != null) return memo[idx];

                var result = new List<string>();
                for (var i = idx; i < s.Length; ++i)
                {
                    var word = s.Substring(idx, i - idx + 1);
                    if (wordSet.Contains(word))
                    {
                        var next = Break(i + 1);
                        if (next.Count > 0)
                        {
                            result.Add(word);
                            result.AddRange(next);
                            break;
                        }
                    }
                }

                memo[idx] = result;
                return result;
            }
        }
    }
}
