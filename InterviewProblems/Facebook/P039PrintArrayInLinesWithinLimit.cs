using System;
using System.Text;

namespace InterviewProblems.Facebook
{
    internal class P039PrintArrayInLinesWithinLimit : ITestable
    {
        public void RunTest()
        {
            foreach (var (arr, limit) in new (int[], int)[]
            {
                (new[]{1, 23, 5, 234, 5, 56, 123}, 5),
            })
            {
                PrintLines(arr, limit);
            }
        }

        private void PrintLines(int[] arr, int limit)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < arr.Length; ++i)
            {
                var str = arr[i].ToString();
                var currLen = str.Length + (i == arr.Length - 1 ? 0 : 1);
                if (sb.Length + currLen <= limit)
                {
                    sb.Append(str);
                    if (i != arr.Length - 1) sb.Append(",");
                }
                else
                {
                    Console.WriteLine(sb.ToString());
                    sb.Length = 0;
                    sb.Append(str);
                    if (i != arr.Length - 1) sb.Append(",");
                }
            }

            if (sb.Length > 0)
            {
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
