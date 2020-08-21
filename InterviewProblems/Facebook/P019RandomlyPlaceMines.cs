using System;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P019RandomlyPlaceMines
    {
        // https://leetcode.com/discuss/interview-question/363952/Facebook-or-Phone-Screen-or-Randomly-Place-Mines

        private static readonly Random Random = new Random();

        public void PlaceMinesTest()
        {
            var grid1 = PlaceMines(100, 100, 2500);
            ArrayUtilities.PrintMatrix(grid1, string.Empty);

            var grid2 = PlaceMines2(100, 100, 2500);
            ArrayUtilities.PrintMatrix(grid2, string.Empty);
        }

        public int[][] PlaceMines(int m, int n, int k)
        {
            var grid = Enumerable.Repeat(0, m).Select(x => new int[n]).ToArray();
            var places = Enumerable.Range(0, k).ToArray();
            var gridSize = m * n;

            for (var i = k; i < gridSize; ++i)
            {
                var r = Random.Next(0, i + 1);
                if (r < k) places[r] = i;
            }

            foreach (var place in places)
            {
                var coord = NumToCoord(place);
                grid[coord.r][coord.c] = 1;
            }

            return grid;

            (int r, int c) NumToCoord(int x) => (x / n, x % n);
        }

        public int[][] PlaceMines2(int m, int n, int k)
        {
            var grid = Enumerable.Repeat(0, m).Select(x => new int[n]).ToArray();
            var gridSize = m * n;

            for (var i = 0; i < k; ++i)
            {
                var (r, c) = NumToCoord(i);
                grid[r][c] = 1;
            }

            for (var i = k; i < gridSize; ++i)
            {
                var rand = Random.Next(0, i + 1);
                if (rand < k)
                {
                    var (r1, c1) = NumToCoord(i);
                    var (r2, c2) = NumToCoord(rand);
                    (grid[r1][c1], grid[r2][c2]) = (grid[r2][c2], grid[r1][c1]);
                }
            }

            return grid;

            (int r, int c) NumToCoord(int x) => (x / n, x % n);
        }
    }
}
