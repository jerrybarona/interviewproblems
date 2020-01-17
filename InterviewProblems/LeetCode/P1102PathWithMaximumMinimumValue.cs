using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P1102PathWithMaximumMinimumValue
    {
        private static readonly (int r, int c)[] _steps = { (1, 0), (0, 1), (-1, 0), (0, -1) };
        public int MaximumMinimumPath(int[][] A)
        {

            var (m, n) = (A.Length, A[0].Length);
            var memo = new Dictionary<(int, int, int), int>();
            return MaxScore(0, 0, int.MaxValue);

            int MaxScore(int r, int c, int currMin)
            {
                Console.WriteLine($"Processing: {r},{c},{currMin}");
                if (r < 0 || c < 0 || r >= m || c >= n || A[r][c] < 0) return int.MinValue;
                if (memo.ContainsKey((r, c, currMin)))
                {
                    Console.WriteLine($"Cache result {memo[(r, c, currMin)]} from {r},{c},{currMin}");
                    return memo[(r, c, currMin)];
                }

                var min = Math.Min(currMin, A[r][c]);
                if (r == m - 1 && c == n - 1) return min;
                A[r][c] *= -1;

                var result = _steps.Select(step => MaxScore(r + step.r, c + step.c, min)).Max();
                A[r][c] *= -1;
                Console.WriteLine($"Result {result} from {r},{c},{currMin}");
                memo.Add((r, c, currMin), result);
                return result;
            }
        }
    }
}
