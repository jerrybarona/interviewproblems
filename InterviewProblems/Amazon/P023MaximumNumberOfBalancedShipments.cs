using System;
using System.Linq;

namespace InterviewProblems.Amazon
{
    internal class P023MaximumNumberOfBalancedShipments : ITestable
    {
        // https://leetcode.com/discuss/interview-question/4846016/Amazon-OA
        // https://leetcode.com/discuss/interview-question/4846016/Amazon-OA/2294682

        public void RunTest()
        {
            foreach (var weight in new int[][]
            {
                new [] {1,2,3,2,6,3}, // 2
                new [] {8,5,4,7,2}, // 2
                new [] {4,3,6,5,3,4,7,1}, // 3
                new [] { 10, 5, 6, 4, 7, 6, 4, 2, 7, 1, 4, 6, 3, 4, 5, 1, 7, 5, 4, 6, 7, 8, 4, 6, 1, 9, 9 }, // 1
                new [] { 3,2,6,8,7}, // 2
            })
            {
                Console.WriteLine(FindMaximumBalancedShipments(weight));
            }    
        }

        private int FindMaximumBalancedShipments(int[] weight)
        {
            var len = weight.Length;
            var max = weight.Max();
            if (weight[len - 1] == max) return 0;

            var memo = Enumerable.Repeat(-1, len).ToArray();

            var maxShipments = MaxShipments();

            return maxShipments == int.MinValue ? 0 : maxShipments;

            int MaxShipments(int idx = 0)
            {
                if (idx == len) return 0;
                if (memo[idx] != -1) return memo[idx];
                var currMax = weight[idx];
                var result = int.MinValue;

                for (var i = idx + 1; i < len; ++i)
                {
                    if (weight[i] < currMax)
                    {
                        var nextResult = MaxShipments(i + 1);
                        if (nextResult != int.MinValue) result = Math.Max(result, 1 + nextResult);
                    }

                    currMax = Math.Max(currMax, weight[i]);
                }

                memo[idx] = result;
                return result;
            }
        }
    }
}
