using System;

namespace InterviewProblems.LeetCode
{
    public class P1802MaximunValueAtAGivenIndexInABoundedArray : ITestable
    {
        public void RunTest()
        {
            foreach (var (n, index, maxSum) in new (int, int, int)[]
            {
                (4, 1, 1000000000),
                (4, 2, 6),
                (6, 1, 10)
            })
            {
                Console.WriteLine(MaxValue(n, index, maxSum));
            }
        }

        public int MaxValue(int n, int index, int maxSum)
        {
            var result = -1;

            for (var (start, end, mid) = (0, maxSum, maxSum / 2); start <= end; mid = start + (end - start) / 2)
            {
                long total = 0;
                int leftFirst;
                if (mid <= index + 1)
                {
                    leftFirst = 1;
                    total = index - mid + 1;
                }
                else
                {
                    leftFirst = mid - index;
                }

                int rightFirst;
                if (mid <= n - index)
                {
                    rightFirst = 1;
                    total += n - index - mid;
                }
                else
                {
                    rightFirst = mid - (n - 1 - index);
                }

                total += Sum(leftFirst, mid) + Sum(rightFirst, mid) - mid;
                if (total <= maxSum)
                {
                    result = Math.Max(result, mid);
                    start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }

            return result;
        }

        long Sum(long first, long last)
        {
            long n = last - first + 1;
            return (first + last) * n / 2;
        }
    }
}
