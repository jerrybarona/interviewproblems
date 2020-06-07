using System;

namespace InterviewProblems.Microsoft
{
    public class P036MinCostOfCharactetDeletion
    {
        // https://leetcode.com/discuss/interview-question/558379/Microsoft-or-OA-2020-or-Min-Cost-to-Get-String-Without-2-Identical-Consecutive-Letters

        public void MinCostTest()
        {
            var s = "abccbd";
            var c = new[] { 0, 1, 2, 3, 4, 5 };
            Console.WriteLine($"\nS = \"{s}\"; C = [{string.Join(", ", c)}]");
            Console.WriteLine($"Output: {MinCost(s, c)}");

            s = "aabbcc";
            c = new[] { 1, 2, 1, 2, 1, 2 };
            Console.WriteLine($"\nS = \"{s}\"; C = [{string.Join(", ", c)}]");
            Console.WriteLine($"Output: {MinCost(s, c)}");

            s = "aaaa";
            c = new[] { 3, 4, 5, 6 };
            Console.WriteLine($"\nS = \"{s}\"; C = [{string.Join(", ", c)}]");
            Console.WriteLine($"Output: {MinCost(s, c)}");

            s = "ababa";
            c = new[] { 10, 5, 10, 5, 10 };
            Console.WriteLine($"\nS = \"{s}\"; C = [{string.Join(", ", c)}]");
            Console.WriteLine($"Output: {MinCost(s, c)}");
        }

        public int MinCost(string s, int[] c)
        {
            var result = 0;
            for (var (i, max, sum, count) = (1, c[0], c[0], 1); i <= s.Length; ++i)
            {
                if (i < s.Length && s[i] == s[i - 1])
                {
                    max = Math.Max(max, c[i]);
                    count++;
                    sum += c[i];
                }
                else
                {
                    if (count > 1) result += sum - max;
                    count = 1;
                    if (i < s.Length) sum = c[i];
                }
            }

            return result;
        }
    }
}
