using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Oracle
{
    public class P004SaddlePoint
    {
        public void SaddlePointTest()
        {
            var input = new[] { new[] { 1, 2, 3 }, new[] { 7, 5, 6 }, new[] { 4, 8, 9 } };
            Console.WriteLine(Calculate(input));
        }

        public int Calculate(int[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;

            var rowMins = new (int idx, int val)[m];

            for (int i = 0; i < m; i++)
            {
                var minVal = int.MaxValue;
                var minIdx = -1;

                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] < minVal)
                    {
                        minVal = matrix[i][j];
                        minIdx = j;
                    }
                }

                rowMins[i] = (minIdx, minVal);
            }

            for (int i = 0; i < n; i++)
            {
                var maxVal = int.MinValue;
                var maxIdx = -1;

                for (int j = 0; j < m; j++)
                {
                    if (matrix[j][i] > maxVal)
                    {
                        maxVal = matrix[j][i];
                        maxIdx = j;
                    }
                }

                if (maxVal == rowMins[maxIdx].val && rowMins[maxIdx].idx == i)
                {
                    return maxVal;
                }
            }

            return int.MinValue;
        }

    }
}
