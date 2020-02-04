using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Google
{
    public class P016MinModifications
    {
        private static readonly (int r, int c)[] _steps = { (1, 0), (0, 1), (-1, 0), (0, -1) };

        // https://leetcode.com/discuss/interview-question/476340/Google-or-Onsite-or-Min-Modifications

        public void MinModificationTest()
        {
            var matrix1 = new[] { new[] { 'R', 'R', 'D' }, new[] { 'L', 'L', 'L' }, new[] { 'U', 'U', 'R' } };
            Console.WriteLine(MinModificationWith01Bfs(matrix1));

            var matrix2 = new[] { new[] { 'R', 'R', 'R', 'R', 'D' }, new[] { 'D', 'L', 'L', 'L', 'L' }, new[] { 'R', 'R', 'R', 'R', 'R' } };
            Console.WriteLine(MinModificationWith01Bfs(matrix2));
        }

        public int MinModificationWith01Bfs(char[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            var memo = Enumerable.Repeat(0, m).Select(x => Enumerable.Repeat(int.MaxValue, n).ToArray()).ToArray();
            memo[0][0] = 0;

            var queue = new LinkedList<(int r, int c)>(new[] { (0, 0) });
            while (queue.Count > 0)
            {
                var (r,c) = queue.First.Value;
                queue.RemoveFirst();
                var dir = matrix[r][c];

                foreach (var step in _steps.Where(x => r + x.r >= 0 && r + x.r < m && c + x.c >= 0 && c + x.c < n))
                {
                    var cost = memo[r][c];
                    switch (dir)
                    {
                        case 'R':
                            cost += step == (0, 1) ? 0 : 1;
                            break;
                        case 'D':
                            cost += step == (1, 0) ? 0 : 1;
                            break;
                        case 'L':
                            cost += step == (0, -1) ? 0 : 1;
                            break;
                        default:
                            cost += step == (-1, 0) ? 0 : 1;
                            break;
                    }

                    var nextr = r + step.r;
                    var nextc = c + step.c;
                    if (memo[nextr][nextc] > cost)
                    {
                        memo[nextr][nextc] = cost;
                        if (nextr == m - 1 && nextc == n - 1) continue;
                        if (cost == memo[r][c]) queue.AddFirst((nextr, nextc));
                        else queue.AddLast((nextr, nextc));
                    }
                }
            }

            return memo[m - 1][n - 1];
        }

        public int MinModification(char[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            var memo = Enumerable.Repeat(0, m).Select(x => Enumerable.Repeat(int.MaxValue, n).ToArray()).ToArray();
            memo[0][0] = 0;

            Process(0, 0);

            return memo[m - 1][n - 1];

            void Process(int r, int c)
            {
                var dir = matrix[r][c];
                
                foreach (var step in _steps.Where(x => r + x.r >= 0 && r + x.r < m && c + x.c >= 0 && c + x.c < n))
                {
                    var cost = memo[r][c];
                    switch (dir)
                    {
                        case 'R':
                            cost += step == (0, 1) ? 0 : 1;
                            break;
                        case 'D':
                            cost += step == (1, 0) ? 0 : 1;
                            break;
                        case 'L':
                            cost += step == (0, -1) ? 0 : 1;
                            break;
                        default:
                            cost += step == (-1, 0) ? 0 : 1;
                            break;
                    }

                    if (memo[r + step.r][c + step.c] > cost)
                    {
                        memo[r + step.r][c + step.c] = cost;
                        if (r + step.r == m - 1 && c + step.c == n - 1) continue;
                        Process(r + step.r, c + step.c);
                    }
                }
            }

        }
    }
}
