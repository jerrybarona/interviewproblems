using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0723CandyCrush
    {
        public int[][] CandyCrush(int[][] board)
        {
            var m = board.Length;
            var n = board[0].Length;

            while (CrushedCandy()) DropCandy();

            return board;

            void DropCandy()
            {
                for (var i = 0; i < n; ++i)
                {
                    var stack = new Stack<int>(board.Select(row => row[i]).Where(x => x > 0));
                    for (var j = m - 1; j >= 0; --j)
                    {
                        if (stack.Count > 0) board[j][i] = stack.Pop();
                        else board[j][i] = 0;
                    }
                }
            }

            bool CrushedCandy()
            {
                var crushed = false;
                for (var i = 0; i < m; ++i)
                {
                    for (var (start, end) = (0, 1); end <= n; ++end)
                    {
                        if (end < n && board[i][end] == board[i][end - 1])
                        {
                            if (board[i][end] == 0) start = end;
                            continue;
                        }
                        if (end - start >= 3)
                        {
                            crushed = true;
                            while (start < end) board[i][start++] *= -1;
                        }
                        else start = end;
                    }
                }

                for (var i = 0; i < n; ++i)
                {
                    for (var (start, end) = (0, 1); end <= m; ++end)
                    {
                        if (end < m && Math.Abs(board[end][i]) == Math.Abs(board[end - 1][i]))
                        {
                            if (board[end][i] == 0) start = end;
                            continue;
                        }
                        if (end - start >= 3)
                        {
                            crushed = true;
                            while (start < end) board[start][i] = -1 * Math.Abs(board[start++][i]);
                        }
                        else start = end;
                    }
                }

                return crushed;
            }
        }
    }
}
