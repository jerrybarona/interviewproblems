using System;

using InterviewProblems.Utilities;

namespace InterviewProblems.LeetCode
{
    public class P0723CandyCrush : ITestable
    {
        public void RunTest()
        {
            foreach (var board in new int[][][]
            {
                new[]
                {
                    new[] {110,5,112,113,114},
                    new[] {210,211,5,213,214},
                    new[] {310,311,3,313,314},
                    new[] {410,411,412,5,414},
                    new[] {5,1,512,3,3},
                    new[] {610,4,1,613,614},
                    new[] {710,1,2,713,714},
                    new[] {810,1,2,1,1},
                    new[] {1,1,2,2,2},
                    new[] {4,1,4,4,1014}
                },
                new[]
                {
                    new[] {1,3,5,5,2},
                    new[] {3,4,3,3,1},
                    new[] {3,2,4,5,2},
                    new[] {2,4,4,5,5},
                    new[] {1,4,4,1,1}
                }
            })
            {
                var result = CandyCrush(board);
                ArrayUtilities.PrintMatrix(result);
            }
        }

        public int[][] CandyCrush(int[][] board)
        {
            var m = board.Length;
            var n = board[0].Length;

            while (CanCrush()) Drop();
            return board;

            void Drop()
            {
                for (var j = 0; j < n; ++j)
                {
                    for (var (s, e) = (m - 1, m - 1); e >= 0 && board[e][j] != 0; --e)
                    {
                        if (board[e][j] > 0)
                        {
                            (board[e][j], board[s][j]) = (board[s][j], board[e][j]);
                            --s;
                        }
                    }

                    for (var i = 0; i < m && board[i][j] <= 0; ++i)
                    {
                        board[i][j] = 0;
                    }
                }
            }

            bool CanCrush()
            {
                var result = false;
                for (var i = 0; i < m; ++i)
                {
                    var (s, e) = (0, 0);
                    for (; e <= n; ++e)
                    {
                        if ((e == n || Math.Abs(board[i][e]) != Math.Abs(board[i][s])))
                        {
                            if (e - s < 3)
                            {
                                s = e;
                                continue;
                            }

                            var val = -Math.Abs(board[i][s]);
                            if (val == 0) continue;
                            result = true;
                            while (s < e)
                            {
                                board[i][s] = val;
                                ++s;
                            }
                        }
                    }
                }

                for (var j = 0; j < n; ++j)
                {
                    var (s, e) = (0, 0);
                    for (; e <= m; ++e)
                    {
                        if ((e == m || Math.Abs(board[e][j]) != Math.Abs(board[s][j])))
                        {
                            if (e - s < 3)
                            {
                                s = e;
                                continue;
                            }

                            var val = -Math.Abs(board[s][j]);
                            if (val == 0) continue;
                            result = true;
                            while (s < e)
                            {
                                board[s][j] = val;
                                ++s;
                            }
                        }
                    }
                }
                return result;
            }
        }
    }
}
