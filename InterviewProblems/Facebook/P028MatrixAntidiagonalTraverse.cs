using System;
using System.Collections.Generic;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P028MatrixAntidiagonalTraverse
    {
        // https://leetcode.com/discuss/interview-question/346342/Facebook-or-Onsite-or-Matrix-Antidiagonal-Traverse

        public void MatrixAntidiagonalTraverseTest()
        {
            var matrix = new[]
            {
                new[] { 12, 7, 21, 31, 11 },
                new[] { 45, -2, 14, 27, 19 },
                new[] { -3, 15, 36, 71, 26 },
                new[] { 4, -13, 55, 34, 15 }
            };

            ArrayUtilities.PrintMatrix(matrix);
            Console.WriteLine($"\n{string.Join('\n', MatrixAntidiagonalTraverse(matrix).Select(r => $"[{string.Join(',', r)}]"))}");
        }

        public List<List<int>> MatrixAntidiagonalTraverse(int[][] matrix)
        {
            var result = new List<List<int>>();
            var (m, n) = (matrix.Length, matrix[0].Length);
            for (var i = 0; i < m; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    var pos = i + j;
                    if (pos == result.Count) result.Add(new List<int>());
                    result[pos].Add(matrix[i][j]);
                }
            }

            return result;
        }
    }
}
