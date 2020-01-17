using System;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P003ServerLoadBalance
    {
        // https://leetcode.com/discuss/interview-question/352460/Google-Online-Assessment-Questions

        public int ServerLoadBalance(int[] arr)
        {
            var min = int.MaxValue;
            var sum = arr.Sum();
            var half = sum / 2;

            Balance(0, 0);
            return min;

            void Balance(int idx, int currSum)
            {
                min = Math.Min(min, Math.Abs(currSum - (sum - currSum)));
                if (min <= 1 || currSum >= half) return;
                
                for (var i = idx; i < arr.Length; ++i)
                {
                    Balance(i + 1, currSum + arr[i]);
                }
            }
        }
    }
}
