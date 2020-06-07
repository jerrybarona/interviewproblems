using System;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P004LargestPerimeterIsland
    {
        // https://leetcode.com/discuss/interview-question/535473/Facebook-or-Phone-or-Largest-Perimeter-Island

        private static readonly (int r, int c)[] Steps = { (1, 0), (0, 1), (-1, 0), (0, -1) };

        public void LargestPerimeterIslandTest()
        {
            var grid1 = new[]
            {
                new[] { 1, 0, 1, 1, 1 },
                new[] { 1, 0, 1, 1, 1 },
                new[] { 0, 1, 0, 1, 1 }
            };

            Console.WriteLine(LargestPerimeterIsland(grid1));

            var grid2 = new[]
            {
                new[] { 0, 0, 0, 0, 0, 0, 0 },
                new[] { 0, 1, 0, 1, 1, 1, 0 },
                new[] { 0, 1, 0, 1, 1, 1, 0 },
                new[] { 0, 0, 1, 1, 1, 1, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 0 }
            };

            Console.WriteLine(LargestPerimeterIsland(grid2));
        }

        public int LargestPerimeterIsland(int[][] grid)
        {
            var (m, n) = (grid.Length, grid[0].Length);
            var largest = 0;
            for (var i = 0; i < m; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    if (grid[i][j] == 1) largest = Math.Max(largest, Perimeter(i, j));
                }
            }

            return largest;

            int Perimeter(int r, int c)
            {
                if (r < 0 || c < 0 || r >= m || c >= n || grid[r][c] == 0 || grid[r][c] == -1) return 0;
                grid[r][c] = -1;

                var result = Steps.Select(x => (r: r + x.r, c: c + x.c)).Any(y =>
                    y.r < 0 || y.c < 0 || y.r >= m || y.c >= n || grid[y.r][y.c] == 0)
                    ? 1
                    : 0;

                return result + Steps.Sum(x => Perimeter(r + x.r, c + x.c));
            }
        }
    }
}
