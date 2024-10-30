using System;
using System.Collections.Generic;

namespace InterviewProblems.UiPath
{
    internal class P002ArrayBreak : ITestable
    {
        // https://leetcode.com/discuss/interview-question/2644246/UI-Path-Online-Assessment

        public void RunTest()
        {
            foreach (var arr in new int[][]
            {
                new [] { 2, 3, 2 }, // 
            })
            {
                Console.WriteLine(PerfectBreak(arr));
            }
        }

        private int PerfectBreak(int[] arr)
        {
            var memo = new Dictionary<(int,int,int), int>();

            var t = arr[0];
            var b = 0;

            var ans = 0;
            for (; t >= 0;)
            {
                ans += NumWays(1, t, b);
                --t;
                ++b;
            }

            return ans;

            int NumWays(int idx, int prevTop, int prevBottom)
            {
                if (idx == arr.Length) return 1;
                if (memo.ContainsKey((idx,prevTop,prevBottom))) return memo[(idx,prevTop,prevBottom)];

                var top = Math.Min(prevTop, arr[idx]);
                var bottom = arr[idx] - top;                

                var result = 0;
                for (; top >= 0;)
                {
                    if (bottom >= prevBottom)
                    {
                        result += NumWays(idx + 1, top, bottom);
                    }

                    --top;
                    ++bottom;
                }

                memo.Add((idx,prevTop,prevBottom), result);

                return result;
            }
        }
    }
}
