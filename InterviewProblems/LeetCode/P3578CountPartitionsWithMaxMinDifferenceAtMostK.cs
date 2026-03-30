using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    internal class P3578CountPartitionsWithMaxMinDifferenceAtMostK : ITestable
    {
        public void RunTest()
        {
            foreach (var (nums, k) in new (int[], int)[]
            {
                (new [] { 9,4,1,3,7 }, 4), // 6
                (new [] { 3,3,4 }, 0), // 2
            })
            {
                Console.WriteLine(CountPartitions(nums, k));
            }
        }

        public int CountPartitions(int[] nums, int k)
        {
            // https://leetcode.com/problems/count-partitions-with-max-min-difference-at-most-k/solutions/6821862/javacpython-mono-deque-dp-by-lee215-mszo

            var minWin = new LinkedList<int>();
            var maxWin = new LinkedList<int>();
            var memo = new int[nums.Length + 1];
            var prefixSum = 1;

            for (var (s, e) = (0, 0); e < nums.Length; ++e)
            {
                while (minWin.Count > 0 && nums[minWin.Last.Value] >= nums[e]) minWin.RemoveLast();
                minWin.AddLast(e);
                while (maxWin.Count > 0 && nums[maxWin.Last.Value] <= nums[e]) maxWin.RemoveLast();
                maxWin.AddLast(e);

                while (nums[maxWin.First.Value] - nums[minWin.First.Value] > k)
                {
                    var sidx = s - 1;
                    prefixSum -= sidx >= 0 ? memo[sidx] : 1;
                    if (minWin.First.Value == s) minWin.RemoveFirst();
                    if (maxWin.First.Value == s) maxWin.RemoveFirst();
                    ++s;
                }
                memo[e] = prefixSum;
                prefixSum += memo[e];
            }

            return memo[nums.Length - 1];
        }
    }
}
