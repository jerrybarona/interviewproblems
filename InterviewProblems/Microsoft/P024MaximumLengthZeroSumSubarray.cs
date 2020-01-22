using System;
using System.Collections.Generic;

namespace InterviewProblems.Microsoft
{
    public class P024MaximumLengthZeroSumSubarray
    {
        // https://leetcode.com/discuss/interview-question/447202/Microsoft-or-Onsite

        public void MaximumLengthZeroSumSubarrayTest()
        {
            var input = new[] { 2, 3, -5, 9, -1, 2, -10, 7 };
            Console.WriteLine("input 1: " + string.Join(',', input));
            Console.WriteLine("Result:");
            Console.WriteLine(MaximumLengthZeroSumSubarray(input));

            input = new[] { 0 };
            Console.WriteLine("input 2: " + string.Join(',', input));
            Console.WriteLine("Result:");
            Console.WriteLine(MaximumLengthZeroSumSubarray(input));
        }

        public int MaximumLengthZeroSumSubarray(int[] arr)
        {
            var map = new Dictionary<int, int>(arr.Length) { { 0, -1} };
            var sum = 0;
            var max = 0;
            for (var i = 0; i < arr.Length; ++i)
            {
                sum += arr[i];
                if (map.ContainsKey(sum)) max = Math.Max(max, i - map[sum]);
                else map.Add(sum, i);
            }

            return max;
        }
    }
}
