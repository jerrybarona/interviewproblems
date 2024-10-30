using System;
using System.Linq;

namespace InterviewProblems.Amazon
{
    internal class P019ArrayGeneratorService : ITestable
    {
        public void RunTest()
        {
            foreach (var (arr, state, m) in new(int[], string, int) []
            {
                (new[]{5,3,4,6}, "1100", 5), // [5, 5, 6, 6, 6]
            })
            {
                Console.WriteLine(string.Join(", ", Generate(arr, state, m)));
            }
        }

        private int[] Generate(int[] arr, string state, int m)
        {
            int[] availableTime = state.Select(c => c - '0').ToArray();
            int[] result = new int[m];

            for (var i = 0; i < availableTime.Length; ++i)
            {
                if (i > 0 && availableTime[i] == 0) availableTime[i] = availableTime[i - 1] + 1;

                if (availableTime[i] <= m)
                {
                    var idx = availableTime[i] - 1;
                    result[idx] = Math.Max(result[idx], arr[i]);
                }
            }

            for (var i = 1; i < result.Length; ++i)
            {
                result[i] = Math.Max(result[i], result[i - 1]);
            }

            return result;
        }
    }
}
