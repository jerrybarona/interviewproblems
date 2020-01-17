using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P1004MaxConsecutiveOnesIII
    {
        public int LongestOnes(int[] A, int K)
        {
            var result = 0;
            for (var (start, end, swaps) = (0, 0, 0); end < A.Length; ++end)
            {
                if (A[end] == 0)
                {
                    if (swaps < K) swaps++;
                    else
                    {
                        for (; swaps == K; ++start)
                        {
                            if (A[start] == 0) --swaps;
                        }
                        ++start;
                    }
                }

                result = Math.Max(result, end - start + 1);
            }
            return result;
        }
    }
}
