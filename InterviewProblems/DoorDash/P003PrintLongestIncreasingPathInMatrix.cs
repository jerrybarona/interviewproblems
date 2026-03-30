using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.DoorDash
{
    internal class P003PrintLongestIncreasingPathInMatrix : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5024675/Doordash-or-Onsite-or-2024

        public void RunTest()
        {
            foreach (var matrix in new int[][][]
            {
                new [] { new [] { 9,9,4 }, new[] {6,6,8}, new[] {2,1,1}},
                new [] { new [] { 3, 4, 5 }, new[] { 3, 2, 6 }, new[] { 2, 2, 1 } },
                new [] { new [] { 17,18,19,20,21,3 },
                    new [] { 16, 9, 8, 7,22,2 },
                    new [] { 15,10, 5, 6,23,1 },
                    new [] { 14,11, 4, 3,24,27 },
                    new [] { 13,12, 1, 2,25,26 }}
            })
            {
                Console.WriteLine(string.Join(", ", LongestIncreasingPathInMatrix(matrix)));
            }
        }

        public IEnumerable<int> LongestIncreasingPathInMatrix(int[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;

            var steps = new (int r, int c)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var memo = Enumerable.Range(0, m).Select(x => new int[n]).ToArray();

            var maxLen = 0;
            var maxR = -1;
            var maxC = -1;

            for (var i = 0; i < m; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    var currLen = GetLongestPath(i, j);
                    if (currLen > maxLen)
                    {
                        maxLen = currLen;
                        maxR = i;
                        maxC = j;
                    }
                }
            }

            var stack = new Stack<int>();
            stack.Push(matrix[maxR][maxC]);
            --maxLen;

            while (maxLen > 0)
            {
                (maxR, maxC) = steps.Select(s => (r: maxR + s.r, c: maxC + s.c)).First(t => t.r >= 0 && t.c >= 0 && t.r < m && t.c < n && memo[t.r][t.c] == maxLen);
                stack.Push(matrix[maxR][maxC]);
                --maxLen;
            }

            return stack.Reverse();

            int GetLongestPath(int r, int c)
            {
                if (memo[r][c] > 0) return memo[r][c];

                var result = 0;
                foreach (var (nr,nc) in steps.Select(s => (r: r+s.r, c: c+s.c)).Where(t => t.r >= 0 && t.c >= 0 && t.r < m && t.c < n && matrix[t.r][t.c] > matrix[r][c]))
                {
                    result = Math.Max(result, GetLongestPath(nr, nc));
                }

                memo[r][c] = 1 + result;
                return memo[r][c];
            }
        }
    }
}
