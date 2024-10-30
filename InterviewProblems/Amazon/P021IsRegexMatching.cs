using System;
using System.Linq;

using Microsoft.VisualBasic;

namespace InterviewProblems.Amazon
{
    internal class P021IsRegexMatching : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5358811/amazon-oa

        public void RunTest()
        {
            foreach (var (regex, arr) in new(string, string[])[]
            {
                ("(ab)*d", new [] {"d", "ababd", "abd","abbd", "abab"}), // YES , YES, YES, NO, NO
                ("ab*d", new [] { "abbbd", "ad", "abd", "ababd" }), // YES, YES, YES, NO
                ("ab(e.r)*e", new []{"abbeere", "abefretre"}), // NO, YES
                ("..()*e*", new [] { "code", "abeee", "cd" }), // NO, YES, YES
            })
            {
                Console.WriteLine(string.Join(" ", IsRegexMatching(regex, arr)));
            }
        }

        private string[] IsRegexMatching(string regex, string[] arr)
        {
            var maxLen = arr.Select(x => x.Length).Max();
            var memo = Enumerable.Repeat(0, regex.Length).Select(x => new int[maxLen]).ToArray();
            string word = null;
            string[] result = new string[arr.Length];


            for (var i = 0; i < arr.Length; ++i)
            {
                word = arr[i];
                result[i] = IsMatch() ? "YES" : "NO";

                for (var idx = 0; idx < memo.Length; ++idx)
                {
                    Array.Clear(memo[idx]);
                }
            }

            return result;

            bool IsMatch(int ridx = 0, int widx = 0)
            {
                if (ridx == regex.Length)
                {
                    return widx == word.Length;
                }

                if (widx == word.Length)
                {
                    if ((ridx + 1 < regex.Length && regex[ridx + 1] == '*') || regex[ridx] == '(')
                    {
                        while (regex[ridx] != '*') ++ridx;
                        
                        return IsMatch(ridx + 1, widx);
                    }

                    return false;
                }

                if (memo[ridx][widx] != 0) return memo[ridx][widx] == 1;

                bool result = false;
                if (regex[ridx] == '(')
                {
                    var expCount = 0;
                    for (var i = ridx + 1; regex[i] != ')'; ++i) ++expCount;
                    bool noMatch = IsMatch(ridx + expCount + 3, widx);

                    if (expCount > 0 && IsSubMatch(ridx + 1, widx))
                    {
                        result = noMatch || IsMatch(ridx + expCount + 3, widx + expCount) || IsMatch(ridx, widx + expCount);
                    }
                    else
                    {
                        result = noMatch;
                    }
                }
                else if (ridx < regex.Length - 1 && regex[ridx + 1] == '*')
                {
                    bool noMatch = IsMatch(ridx + 2, widx);
                    if (regex[ridx] == word[widx] || regex[ridx] == '.')
                    {
                        result = noMatch || IsMatch(ridx + 2, widx + 1) || IsMatch(ridx, widx + 1);
                    }
                    else
                    {
                        result = noMatch;
                    }
                }
                else
                {
                    result = (regex[ridx] == word[widx] || regex[ridx] == '.') && IsMatch(ridx + 1, widx + 1); 
                }

                memo[ridx][widx] = result ? 1 : -1;

                return result;
            }

            bool IsSubMatch(int ridx, int widx)
            {
                if (widx == word.Length) return false;
                if (regex[ridx] == ')') return true;
                if (regex[ridx] == word[widx] || regex[ridx] == '.') return IsSubMatch(ridx + 1, widx + 1);

                return false;
            }
        }
    }
}
