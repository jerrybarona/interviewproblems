using System;

namespace InterviewProblems.LeetCode
{
    internal class P2217FindPalindromeWithFixedLength : ITestable
    {
        public void RunTest()
        {
            foreach (var (queries, intLength) in new (int[], int)[]
            {
                (new int[]{ 1,2,3,4,5,90 }, 3)
            })
            {
                Console.WriteLine(string.Join(", ", KthPalindrome(queries, intLength)));
            }
        }

        public long[] KthPalindrome(int[] queries, int intLength)
        {
            long root = (long)Math.Pow(10, (intLength - 1) / 2);

            var len = queries.Length;
            long[] answer = new long[len];

            for (var i = 0; i < len; ++i)
            {
                var q = queries[i];
                long r = root + q - 1;
                long num = r;

                for (var (count, nextRoot) = (0, r); nextRoot > 0; ++count, nextRoot /= 10)
                {
                    if (count == 0 && intLength % 2 != 0) continue;
                    num = 10 * num + (nextRoot % 10);
                }

                answer[i] = num;
            }

            return answer;
        }
    }
}
