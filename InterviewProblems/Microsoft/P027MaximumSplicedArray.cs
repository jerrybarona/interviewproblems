using System;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P027MaximumSplicedArray
    {
        // https://leetcode.com/discuss/interview-question/387124/Microsoft-or-Onsite-or-Maximum-Spliced-Array

        public void MaxSplicedArrayTest()
        {
            var a1 = new[] { 60, 60, 60 };
            var b1 = new[] { 10, 90, 10 };
            Console.WriteLine($"A     : {string.Join(',', a1)}");
            Console.WriteLine($"B     : {string.Join(',', b1)}");
            Console.WriteLine($"Result: {string.Join(',', MaxSplicedArray(a1,b1))}");

            var a2 = new[] { 20, 40, 20, 70, 30 };
            var b2 = new[] { 50, 20, 50, 40, 20 };
            Console.WriteLine($"A     : {string.Join(',', a2)}");
            Console.WriteLine($"B     : {string.Join(',', b2)}");
            Console.WriteLine($"Result: {string.Join(',', MaxSplicedArray(a2, b2))}");
        }

        public int[] MaxSplicedArray(int[] a, int[] b)
        {
            var aIsLarger = a.Sum() >= b.Sum();
            var larger = aIsLarger ? a : b;
            var smaller = larger == a ? b : a;

            var maxStart = -1;
            var maxLen = 0;
            var maxSum = 0;

            for (var (start, end, curr) = (0, 0, 0); end < a.Length; ++end)
            {
                curr += smaller[end] - larger[end];
                while (curr < 0 && start <= end)
                {
                    curr -= smaller[start] - larger[start];
                    ++start;
                }

                if (curr > 0)
                {
                    if (curr > maxSum)
                    {
                        maxSum = curr;
                        maxStart = start;
                        maxLen = end - start + 1;
                    }
                }
            }

            return maxStart == -1
                ? larger
                : Enumerable.Range(0, a.Length)
                    .Select(idx => idx >= maxStart && idx < maxStart + maxLen ? smaller[idx] : larger[idx]).ToArray();
        }
    }
}
