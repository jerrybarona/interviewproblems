using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P0909SnakesAndLadders
    {
        public void SnakesAndLaddersTest()
        {
            var b = new[]
            {
                new[] { -1, -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1, -1 },
                new[] { -1, 35, -1, -1, 13, -1 }, new[] { -1, -1, -1, -1, -1, -1 }, new[] { -1, 15, -1, -1, -1, -1 }
            };

            Console.WriteLine(SnakesAndLadders(b));
        }

        public int SnakesAndLadders(int[][] board)
        {
            var n = board.Length;
            if (n == 1) return 0;
            var finish = n * n;

            var queue = new Queue<int>(new[] { 1 });
            var visited = new HashSet<int>(new[] { 1 });

            return Bfs();

            int Bfs()
            {
                for (var moves = 0; queue.Count > 0; ++moves)
                {
                    for (var count = queue.Count; count > 0; --count)
                    {
                        var node = queue.Dequeue();
                        for (var i = 1; i <= 6; ++i)
                        {
                            var pos = node + i;
                            var coord = NumToCoord(pos);
                            if (board[coord.r][coord.c] != -1) pos = board[coord.r][coord.c];

                            if (pos == finish) return moves + 1;
                            if (!visited.Contains(pos))
                            {
                                visited.Add(pos);
                                queue.Enqueue(pos);
                            }
                        }
                    }
                }

                return -1;
            }

            (int r, int c) NumToCoord(int s)
            {
                var q = (s - 1) / n;
                var r = (s - 1) % n;
                if (q % 2 == 1) r = n - r - 1;

                return (n - q - 1, r);
            }
        }
    }
}
