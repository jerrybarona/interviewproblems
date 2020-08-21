using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P018PalindromicSubsequences
    {
        // https://leetcode.com/discuss/interview-question/372459/Facebook-or-Phone-Screen-or-Palindromic-Subsequences

        public void PalindromicSubsequencesTest()
        {
            var input = "abacab";
            Console.WriteLine($"Input: \"{input}\"\nOuput: [{string.Join(", ", PalindromicSubsequences(input).Select(x => $"\"{x}\""))}]");

            input = "aa";
            Console.WriteLine($"Input: \"{input}\"\nOuput: [{string.Join(", ", PalindromicSubsequences(input).Select(x => $"\"{x}\""))}]");
        }

        public List<string> PalindromicSubsequences(string s)
        {
            var memo = new Dictionary<(int,int), HashSet<string>>();
            return Subsequences(0, s.Length - 1).ToList();

            HashSet<string> Subsequences(int start, int end)
            {
                if (start == end) return new HashSet<string> { $"{s[start]}", string.Empty };
                if (memo.ContainsKey((start, end))) return memo[(start, end)];

                var result = new HashSet<string>();
                if (s[start] == s[end])
                {
                    result.UnionWith(Subsequences(start + 1, end - 1).SelectMany(x => new[] { x, $"{s[start]}{x}{s[end]}" }));
                }
                
                result.UnionWith(Subsequences(start+1, end));
                result.UnionWith(Subsequences(start, end-1));

                memo.Add((start,end), result);
                return result;
            }
        }
    }
}
