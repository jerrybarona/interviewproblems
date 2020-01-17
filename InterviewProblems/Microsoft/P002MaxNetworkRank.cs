using System;

namespace InterviewProblems.Microsoft
{
    public class P002MaxNetworkRank
    {
        public int MaxRank(int[] A, int[] B, int N)
        {
            var indeg = new int[N + 1];
            for (var i = 0; i < A.Length; ++i)
            {
                indeg[A[i]]++;
                indeg[B[i]]++;
            }

            var result = 0;
            for (var i = 0; i < A.Length; ++i)
            {
                result = Math.Max(result, indeg[A[i]] + indeg[B[i]] - 1);
            }

            return result;
        }
    }
}
