using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Google
{
    public class P013NumberOfSubmatricesWithZeroSum
    {
        // https://leetcode.com/discuss/interview-question/494524/Google-or-Onsite-or-Number-of-Submatrices-With-Sum-Zero


        public void NumberOfSubmatricesWithZeroSumTest()
        {
            var input1 = new[] { new[] { 0, 0, 1, 0, 0 } };
            Console.WriteLine(NumberOfSubmatricesWithZeroSum(input1));

            var input2 = new[] { new[] { 0, 0, 1, 0, 0 }, new[] { 0, 1, 0, 0, 0 } };
            Console.WriteLine(NumberOfSubmatricesWithZeroSum(input2));
        }

        public int NumberOfSubmatricesWithZeroSum(int[][] matrix)
        {
            if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
            var m = matrix.Length;
            var n = matrix[0].Length;

            var prefix = Enumerable.Repeat(0, m+1).Select(x => new int[n]).ToArray();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    prefix[j + 1][i] = prefix[j][i] + matrix[j][i];
                }
            }

            var map = new Dictionary<int,int>();
            var result = 0;

            for (int rowStart = 0; rowStart < m; rowStart++)
            {
                for (var row = rowStart; row < m; ++row)
                {
                    map.Clear();
                    var sum = 0;

                    for (var col = 0; col < n; ++col)
                    {
                        sum += prefix[row + 1][col] - prefix[rowStart][col];
                        if (sum == 0) ++result;
                        if (map.ContainsKey(sum)) result += map[sum];
                        if (!map.ContainsKey(sum)) map.Add(sum, 0);
                        ++map[sum];
                    }
                }
            }

            return result;
        }
    }
}
