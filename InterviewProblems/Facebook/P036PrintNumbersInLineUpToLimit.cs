using System;
using System.Text;

namespace InterviewProblems.Facebook
{
    internal class P036PrintNumbersInLineUpToLimit : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5541169/Meta-Screening-Jul-2024

        public void RunTest()
        {
            foreach (var (arr, limit) in new (int[], int)[]
            {
                (new [] {1, 23, 5, 234, 5, 56, 123}, 5),
                /*
                    1,23,
                    5,
                    234,
                    5,56,
                    123
                 */
            })
            {
                PrintLines(arr, limit);
            }
        }

        private void PrintLines(int[] arr, int limit)
        {
            var count = 0;
            for (var i = 0; i < arr.Length; ++i)
            {
                var next = arr[i].ToString();
                if (i < arr.Length - 1) next += ",";

                if (count + next.Length > limit)
                {
                    Console.Write('\n');
                    count = 0;
                }

                Console.Write(next);
                count += next.Length;
            }
        }
    }
}
