using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P0694NumberOfDistinctIslands : ITestable
    {
        private static readonly (int x, int y)[] _steps = new[] { (0, 1), (1, 0), (-1, 0), (0, -1) };

        public void RunTest()
        {
            throw new NotImplementedException();
        }

        public int NumDistinctIslands(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;

            int topMost = m;
            int leftMost = n;
            var island = new List<long>();
            var sb = new StringBuilder();
            var seen = new HashSet<string>();

            for (var i = 0; i < m; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    if (grid[i][j] == 1)
                    {
                        island.Clear();
                        topMost = i;
                        leftMost = n;
                        Traverse(i, j);
                        sb.Clear();
                        foreach (var row in island)
                        {
                            sb.Append((row >> leftMost));
                        }

                        seen.Add(sb.ToString());
                    }
                }
            }

            return seen.Count;

            void Traverse(int r, int c)
            {
                //if (grid[r][c] < 1) return;
                grid[r][c] = -1;

                leftMost = Math.Min(leftMost, c);
                var idx = r - topMost;
                if (idx == island.Count) island.Add(0);
                island[idx] |= (1 << c);

                foreach (var (nr, nc) in _steps.Select(s => (x: r + s.x, y: c + s.y)).Where(t => t.x >= 0 && t.x < m && t.y >= 0 && t.y < n && grid[t.x][t.y] > 0))
                {
                    Traverse(nr, nc);
                }
            }
        }
    }
}
