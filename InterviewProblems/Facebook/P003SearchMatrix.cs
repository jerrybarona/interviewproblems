using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P003SearchMatrix
    {
        // https://leetcode.com/discuss/interview-question/542908/Facebook-or-Phone-or-Search-in-a-sorted-matrix-with-duplicates

        public void SearchMatrixTest()
        {
            var matrix = new[]
            {
                new[] { 1, 2, 3, 4, 10 }, new[] { 2, 3, 5, 5, 11 }, new[] { 3, 5, 5, 5, 12 }, new[] { 4, 5, 5, 6, 12 }
            };

            Console.WriteLine(string.Join('\n', SearchMatrix(matrix, 5)));
        }


        public List<(int r, int c)> SearchMatrix(int[][] matrix, int target)
        {
            var (m, n) = (matrix.Length, matrix[0].Length);
            var result = new HashSet<(int r, int c)>();
            Traverse(0, n-1);

            return result.ToList();

            void Traverse(int r, int c)
            {
                if (r >= m || c < 0 || result.Contains((r, c))) return;
                if (matrix[r][c] > target) Traverse(r, c-1);
                else if (matrix[r][c] < target) Traverse(r+1, c);
                else
                {
                    result.Add((r, c));
                    Traverse(r, c-1);
                    Traverse(r+1, c);
                }
            }
        }
    }
}
