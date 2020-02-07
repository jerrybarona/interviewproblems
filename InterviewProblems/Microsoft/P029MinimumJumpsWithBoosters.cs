using System;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P029MinimumJumpsWithBoosters
    {
        // https://leetcode.com/discuss/interview-question/446026/Microsoft-or-Onsite-or-Minimum-jumps-with-boosters

        public void MinimumJumpsWithBoostersTest()
        {
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var boosters = new[] { new[] { 3, 8 }, new[] { 5, 10 } };

            Console.WriteLine(MinimumJumpsWithBoosters(arr, boosters));
        }

        public int MinimumJumpsWithBoosters(int[] arr, int[][] boosters)
        {
            var boosts = boosters.ToDictionary(x => x[0], x => x[1]);
            var memo = Enumerable.Repeat(-1, arr.Length).ToArray();

            return NumJumps(0);

            int NumJumps(int idx)
            {
                if (idx >= arr.Length - 1) return 0;
                if (memo[idx] != -1) return memo[idx];
                memo[idx] = int.MaxValue;
                if (boosts.ContainsKey(idx + 1)) memo[idx] = 1 + NumJumps(boosts[idx + 1] - 1);
                memo[idx] = Math.Min(memo[idx],
                    1 + Enumerable.Range(1, 4).Reverse().Select(x => NumJumps(idx + x)).Min());

                return memo[idx];
            }
        }

    }
}
