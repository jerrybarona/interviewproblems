using System;

namespace InterviewProblems.TikTok
{
    public class P008LongestConcaveSubsequence : ITestable
    {
        // https://leetcode.com/discuss/interview-question/4801929/TikTok-OA-Very-Hard-SDE-Longest-Concave-Subsequence

        public void RunTest()
        {
            foreach (var arr in new int[][]
            {
                new[]{ 4, 2, 6, 5, 3, 1 }, // 3
                new [] { 3, 1, 5, 2, 4 }, // 4
                new[]{ 1, 2, 3, 4, 5 }, // 2
                new [] { 1,5,8,3,9,6,4,7,2}, // 5
            })
            {
                Console.WriteLine(LongestConcaveSubsequence(arr));
            }
        }

        public int LongestConcaveSubsequence(int[] arr)
        {
            var result = 2;
            var exclude = 0;

            for (var (s,e) = (0,arr.Length - 1); s < e; )
            {
                var min = Math.Min(arr[s], arr[e]);
                var curr = min - 1 - exclude + 2;

                result = Math.Max(result, curr);
                if (arr[s] == min)
                {
                    while (arr[s] <= min)
                    {
                        ++exclude;
                        ++s;
                    }
                }
                else
                {
                    while (arr[e] <= min)
                    {
                        ++exclude;
                        --e;
                    }
                }
            }

            return result;
        }
    }
}
