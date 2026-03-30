using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    internal class P3651MinimumCostPathWithTeleportation : ITestable
    {
        public void RunTest()
        {
            foreach (var (grid, k) in new (int[][], int)[]
            {
                (new [] { new [] { 1,3,3 }, new [] { 2,5,4 }, new [] { 4,3,5 } }, 2), // 7
                (new [] { new [] { 1,2 }, new [] { 2,3 }, new [] { 3,4 } }, 1), // 9
            })
            {
                Console.WriteLine(MinCost(grid, k));
            }
        }

        public int MinCost(int[][] grid, int k)
        {
            var m = grid.Length;
            var n = grid[0].Length;

            var list = grid.SelectMany((row, i) => row.Select((val, j) => (i: i, j: j, val: val))).OrderBy(x => x.val).ToArray();
            var memo = Enumerable.Repeat(0, m).Select(_ => Enumerable.Repeat(0, n).Select(_ => Enumerable.Repeat(-1, k + 1).ToArray()).ToArray()).ToArray();

            return GetCost(0, 0, k) - grid[0][0];

            int GetCost(int i, int j, int count)
            {
                var val = grid[i][j];
                if (i == m - 1 && j == n - 1) return val;
                if (memo[i][j][count] != -1) return memo[i][j][count];


                var result = int.MaxValue;
                if (i < m - 1) result = Math.Min(result, GetCost(i + 1, j, count));
                if (j < n - 1) result = Math.Min(result, GetCost(i, j + 1, count));

                if (count > 0)
                {
                    var idx = -1;
                    for (var (start, end, mid) = (0, list.Length - 1, (list.Length - 1) / 2); start <= end; mid = start + (end - start) / 2)
                    {
                        var listVal = list[mid].val >= 0 ? list[mid].val : Math.Abs(list[mid].val + 1);
                        if (listVal <= val)
                        {
                            idx = mid;
                            start = mid + 1;
                        }
                        else
                        {
                            end = mid - 1;
                        }
                    }

                    if (idx > -1)
                    {
                        for (; idx >= 0; --idx)
                        {
                            if (list[idx].val < 0 || (list[idx].i == i && list[idx].j == j)) continue;
                            var nexti = list[idx].i;
                            var nextj = list[idx].j;
                            list[idx] = (nexti, nextj, -list[idx].val - 1);
                            result = Math.Min(result, GetCost(nexti, nextj, count - 1));
                            list[idx] = (nexti, nextj, Math.Abs(list[idx].val + 1));
                        }
                    }
                }

                memo[i][j][count] = result + val;
                return memo[i][j][count];
            }
        }
    }
}
