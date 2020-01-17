using System;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P011MinAbsDifferenceOfServerLoads
    {
        public int MinAbsDifferenceOfServerLoads(int[] arr)
        {
            //Array.Sort(arr, (a,b) => b.CompareTo(a));
            var sum = arr.Sum();
            var min = int.MaxValue;
            var memo = Enumerable.Repeat(0, arr.Length).Select(x => Enumerable.Repeat(-1, sum+1).ToArray()).ToArray();
            //MinDiff(0, 0);
            //return min;

            return MinDiff2(0, 0);

            void MinDiff(int idx, int currSum)
            {
                var currMin = Math.Abs(currSum - (sum - currSum));
                min = Math.Min(min, currMin);
                if (min <= 1 || idx >= arr.Length) return;

                for (var i = idx; i < arr.Length; ++i)
                {
                    MinDiff(i + 1, currSum + arr[i]);
                }
            }

            int MinDiff2(int idx, int currSum)
            {
                if (idx >= arr.Length) return int.MaxValue;
                if (memo[idx][currSum] > -1) return memo[idx][currSum];

                var currMin = Math.Min(Math.Abs(currSum - (sum - currSum)),
                    Math.Min(MinDiff2(idx + 1, currSum), MinDiff2(idx + 1, currSum + arr[idx])));

                memo[idx][currSum] = currMin;
                return currMin;
            }
        }
    }
}
