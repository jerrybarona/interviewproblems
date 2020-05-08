using System;
using System.Collections.Generic;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.LeetCode
{
    public class P0317ShortestDistanceFromAllBuildings
    {
        private static readonly (int r, int c)[] Steps = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        public void ShortestDistanceTest()
        {
            var grid = new[]
            {
                new[] { 1, 1, 1, 1, 1, 0 }, new[] { 0, 0, 0, 0, 0, 1 }, new[] { 0, 1, 1, 0, 0, 1 },
                new[] { 1, 0, 0, 1, 0, 1 }, new[] { 1, 0, 1, 0, 0, 1 }, new[] { 1, 0, 0, 0, 0, 1 },
                new[] { 0, 1, 1, 1, 1, 0 }
            };

            Console.WriteLine(ShortestDistance(grid));
        }

        public int ShortestDistance(int[][] grid)
        {
            var u = new ArrayUtilities();
            u.PrintMatrix(grid);
            var (m, n) = (grid.Length, grid[0].Length);
            var buildingCount = grid.SelectMany(x => x.Where(y => y == 1)).Count();
            var reached = Enumerable.Repeat(0, m).Select(x => new int[n]).ToArray();

            foreach (var building in grid
                .Select((row, rowIdx) => (row, rowIdx))
                .SelectMany(x => x.row.Select((val, colIdx) => (val, colIdx))
                    .Where(y => y.val == 1).Select(z => (x.rowIdx, z.colIdx))))
            {
                var queue = new Queue<(int r, int c)>(new[] { building });
                var visited = new HashSet<(int r, int c)>();

                for (var dist = 1; queue.Count > 0; ++dist)
                {
                    for (var count = queue.Count; count > 0; --count)
                    {
                        var node = queue.Dequeue();
                        foreach (var s in Steps
                            .Select(x => (r: node.r + x.r, c: node.c + x.c))
                            .Where(y => y.r >= 0 && y.c >= 0 && y.r < m && y.c < n &&
                                grid[y.r][y.c] != 1 && grid[y.r][y.c] != 2 && visited.Add(y)))
                        {
                            ++reached[s.r][s.c];
                            grid[s.r][s.c] -= dist;
                            queue.Enqueue(s);
                        }
                    }
                }
            }

            return reached
                .SelectMany((row, rowId) => row.Select((val, colId) => (val, colId)).Where(z => z.val == buildingCount)
                    .Select(a => (r: rowId, c: a.colId)))
                .Select(x => grid[x.r][x.c])
                .Where(y => y <= -1)
                .DefaultIfEmpty(1).Max();
        }
    }
}