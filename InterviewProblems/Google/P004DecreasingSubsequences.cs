using System;

namespace InterviewProblems.Google
{
    public class P004DecreasingSubsequences
    {
        // https://leetcode.com/discuss/interview-question/352460/Google-Online-Assessment-Questions

        public int DecreasingSubsequences(int[] nums)
        {
            var memo = new int[nums.Length];
            return MinNum(0);

            int MinNum(int idx)
            {
                if (idx == nums.Length - 1) return 1;
                if (memo[idx] > 0) return memo[idx];

                var result = MinNum(idx+1);
                if (nums[idx] <= nums[idx + 1]) result += 1;
                for (var i = idx + 2; i < nums.Length; ++i)
                {
                    if (nums[idx] > nums[i]) result = Math.Min(result, 1 + MinNum(i));
                }

                memo[idx] = result;
                return result;
            }
        }
    }
}
