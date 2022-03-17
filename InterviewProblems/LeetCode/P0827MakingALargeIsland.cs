using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0827MakingALargeIsland
    {
        public void LargestIslandTest()
        {
            foreach (var item in new int [][][] { new int[][] { new int [] {1,0},new int [] {0,1} } })
            {
                Console.WriteLine(LargestIsland(item));
            }
        }

        private static (int r, int c)[] steps = new (int, int)[] { (1, 0), (0, -1), (0, 1), (-1, 0) };

        public int LargestIsland(int[][] grid)
        {
            var n = grid.Length;
            var map = new Dictionary<int, int>();
            var id = 2;

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    if (grid[i][j] == 1)
                    {
                        map.Add(id, GetIslandSize(i, j, id));
                        ++id;
                    }
                }
            }

            var result = 0;
            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    if (grid[i][j] == 0)
                    {
                        var val = 1 + steps
                            .Select(s => (r: i + s.r, c: j + s.c))
                            .Where(t => t.r >= 0 && t.r < n && t.c >= 0 && t.c < n && grid[t.r][t.c] != 0)
                            .Select(u => grid[u.r][u.c])
                            .Distinct()
                            .DefaultIfEmpty(0)
                            .Sum(v => map[v]);

                        result = Math.Max(result, val);
                    }
                }
            }

            return result;

            int GetIslandSize(int r, int c, int id)
            {
                grid[r][c] = id;

                return 1 + steps
                    .Select(s => (r: r + s.r, c: c + s.c))
                    .Where(t => t.r >= 0 && t.r < n && t.c >= 0 && t.c < n && grid[t.r][t.c] == 1)
                    //.DefaultIfEmpty(0)
                    .Sum(u => GetIslandSize(u.r, u.c, id));
            }
        }
    }
}
