using System;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P010MaxOfMinAltitutes
    {
        // https://leetcode.com/discuss/interview-question/344650/Amazon-Online-Assessment-Questions

        public int MaxScore(int[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            var min = int.MaxValue;
            var memo = Enumerable.Repeat(0, m).Select(x => new int[n]).ToArray();

            for (var i = n - 2; i >= 0; --i)
            {
                min = Math.Min(min, matrix[m - 1][i]);
                memo[m - 1][i] = min;
            }
            min = int.MaxValue;
            for (var i = m - 2; i >= 0; --i)
            {
                min = Math.Min(min, matrix[i][n-1]);
                memo[i][n - 1] = min;
            }

            for (var i = m - 2; i >= 0; --i)
            {
                for (var j = n - 2; j >= 0; --j)
                {
                    memo[i][j] = Math.Min(matrix[i][j], Math.Max(memo[i + 1][j], memo[i][j + 1]));
                }
            }

            return Math.Max(memo[1][0], memo[0][1]);
        }
    }
}
