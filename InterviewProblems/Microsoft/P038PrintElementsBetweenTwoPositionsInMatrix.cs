using System;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Microsoft
{
    public class P038PrintElementsBetweenTwoPositionsInMatrix
    {
        // https://leetcode.com/discuss/interview-question/544790/Microsoft-or-Phone-or-Print-Elements-Between-2-Positions-in-Matrix

        public void ElementsTest()
        {
            var matrix = new[] { new[] { 5, 2, 3 }, new[] { 4, 41, 2 }, new[] { 11, 34, 98 } };
            var coord1 = new[] { 1, 2 };
            var coord2 = new[] { 2, 1 };
            ArrayUtilities.PrintMatrix(matrix);
            Console.WriteLine($"\nPoint: [{coord1[0]}, {coord1[1]}], [{coord2[0]}, {coord2[1]}]");
            Console.WriteLine($"Output: [{string.Join(", ", Elements(matrix, coord1, coord2))}]");
        }

        public int[] Elements(int[][] matrix, int[] coord1, int[] coord2)
        {
            var (m, n) = (matrix.Length, matrix[0].Length);
            var start = CoordToPos(coord1[0], coord1[1]);
            var end = CoordToPos(coord2[0], coord2[1]);

            return Enumerable.Range(start, end - start + 1).Select(p =>
            {
                var (r, c) = PosToCoord(p);
                return matrix[r][c];
            }).ToArray();

            int CoordToPos(int r, int c) => r * m + c % n;
            (int r, int c) PosToCoord(int pos) => (pos / m, pos % n);
        }
    }
}
