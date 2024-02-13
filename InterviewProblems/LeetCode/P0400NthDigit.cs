using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P0400NthDigit : ITestable
    {
        public void RunTest()
        {
            foreach (var n in new[] { 1000000000 })
            {
                Console.WriteLine(FindNthDigit(n));
            }
        }

        public int FindNthDigit(int n)
        {
            if (n <= 9) return n;

            var c = 1;
            for (long i = 9; ; i *= 10)
            {
                long range = i * c;
                if (n <= range) break;
                n -= (int)range;
                ++c;
            }
            var rangeStart = (int)Math.Pow(10, c - 1);
            var rangeOffset = (n - 1) / c;
            var num = rangeStart + rangeOffset;
            var numPos = (n - 1) % c;

            var stack = new Stack<int>();
            for (var x = num; x > 0; x /= 10)
            {
                stack.Push(x % 10);
            }

            while (numPos > 0)
            {
                stack.Pop();
                --numPos;
            }

            return stack.Peek();
        }
    }
}
