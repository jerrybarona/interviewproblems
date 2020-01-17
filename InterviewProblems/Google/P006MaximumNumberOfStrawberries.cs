using System;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P006MaximumNumberOfStrawberries
    {
        // https://leetcode.com/discuss/interview-question/352460/Google-Online-Assessment-Questions

        public int MaxStraberries(int[] fruits, int k)
        {
            var memo = Enumerable.Repeat(0, fruits.Length).Select(x => Enumerable.Repeat(-1, k + 1).ToArray())
                .ToArray();
            return MaxNum(0, k);

            int MaxNum(int idx, int rem)
            {
                if (idx >= fruits.Length || rem <= 0) return 0;
                if (memo[idx][rem] > -1) return memo[idx][rem];

                var result = fruits[idx] > rem ? 0 : fruits[idx] + MaxNum(idx + 2, rem - fruits[idx]);
                result = Math.Max(result, MaxNum(idx + 1, rem));

                memo[idx][rem] = result;
                return result;
            }
        }
    }
}
