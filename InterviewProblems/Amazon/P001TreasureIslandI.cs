using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Amazon
{
    public class P001TreasureIslandI
    {
        //https://aonecode.com/amazon-online-assessment-questions

        public int NumSteps(char[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            Console.WriteLine($"C1: {Count1(0, 0)}, C2: {Count2(0, 0, 0)}");
            return 0;

            int Count1(int r, int c)
            {
                if (r < 0 || c < 0 || r >= m || c >= n || matrix[r][c] == '-' || matrix[r][c] == 'D') return int.MaxValue;
                if (matrix[r][c] == 'X') return 1;
                matrix[r][c] = '-';
                var results = new[] { Count1(r + 1, c), Count1(r - 1, c), Count1(r, c + 1), Count1(r, c - 1) };
                var min = results.Min();
                matrix[r][c] = 'O';
                return min == int.MaxValue ? int.MaxValue : 1 + min;
            }

            int Count2(int r, int c, int count)
            {
                if (r < 0 || c < 0 || r >= m || c >= n || matrix[r][c] == '-' || matrix[r][c] == 'D') return int.MaxValue;
                if (matrix[r][c] == 'X') return count;
                matrix[r][c] = '-';
                var results = new[] { Count2(r + 1, c, count+1), Count2(r - 1, c, count + 1), Count2(r, c + 1, count + 1), Count2(r, c - 1, count + 1) };
                matrix[r][c] = 'O';
                return results.Min();
            }
        }
    }
}
