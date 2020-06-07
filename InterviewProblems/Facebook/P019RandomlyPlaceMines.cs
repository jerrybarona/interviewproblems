using System;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P019RandomlyPlaceMines
    {
        // https://leetcode.com/discuss/interview-question/363952/Facebook-or-Phone-Screen-or-Randomly-Place-Mines

        public void PlaceMinesTest()
        {
            var grid = PlaceMines(5, 5, 25);
            ArrayUtilities.PrintMatrix(grid, string.Empty);
        }

        public int[][] PlaceMines(int m, int n, int k)
        {
            var random = new Random();
            var grid = Enumerable.Repeat(0, m).Select(x => new int[n]).ToArray();
            var places = Enumerable.Range(0, k).ToArray();
            var gridSize = m * n;

            for (var i = k; i < gridSize; ++i)
            {
                var r = random.Next(0, i + 1);
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
    }
}
