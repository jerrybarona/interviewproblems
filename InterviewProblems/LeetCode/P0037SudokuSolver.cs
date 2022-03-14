using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P0037SudokuSolver
    {
        public void SolveSudokuTest()
        {
            var board = new[]
            {
                new[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };

            SolveSudoku(board);

            foreach (var row in board)
            {
                Console.WriteLine(string.Join(',', row));
            }
        }

        private static int subMask = 0x3FF;

        public void SolveSudoku(char[][] board)
        {
            var rowUsed = new int[9];
            var colUsed = new int[9];
            var boxUsed = new int[9];
            var squares = new LinkedList<(int, int)>();

            for (var r = 0; r < 9; ++r)
            {
                for (var c = 0; c < 9; ++c)
                {
                    if (board[r][c] == '.') squares.AddLast((r, c));
                    else SetCandidate(r, c, board[r][c] - '0');
                }
            }

            ResolveSingleCandidateSquares();

            var currSquare = squares.First;
            var isBoardFilled = false;
            
            FillBoard();

            void FillBoard()
            {
                if (currSquare == null)
                {
                    isBoardFilled = true;
                    return;
                }

                var (r, c) = currSquare.Value;
                currSquare = currSquare.Next;

                for (var val = 1; val <= 9; ++val)
                {
                    if (CanUseCandidate(r, c, val))
                    {
                        SetCandidate(r, c, val);
                        FillBoard();

                        if (isBoardFilled) return;
                        ResetCandidate(r, c, val);
                    }
                }

                currSquare = currSquare.Previous;
            }

            void SetCandidate(int r, int c, int val)
            {
                var b = BoxIdx(r, c);
                var mask = (1 << val);

                rowUsed[r] |= mask;
                colUsed[c] |= mask;
                boxUsed[b] |= mask;

                board[r][c] = (char)('0' + val);
            }

            void ResetCandidate(int r, int c, int val)
            {
                var b = BoxIdx(r, c);
                var mask = ~(1 << val);

                rowUsed[r] &= mask;
                colUsed[c] &= mask;
                boxUsed[b] &= mask;

                //board[r][c] = '.';
            }

            bool CanUseCandidate(int r, int c, int val)
            {
                var b = BoxIdx(r, c);
                var mask = (1 << val);

                return (rowUsed[r] & mask) == 0
                    && (colUsed[c] & mask) == 0
                    && (boxUsed[b] & mask) == 0;
            }

            void ResolveSingleCandidateSquares()
            {
                var isUpdated = false;
                for (var node = squares.First; node != null; )
                {
                    var (r, c) = node.Value;
                    var b = BoxIdx(r, c);

                    var val = 0;
                    if (HasSingleCandidate(rowUsed[r]))
                    {
                        val = GetValueFromBits(rowUsed[r] >> 1);
                    }
                    else if (HasSingleCandidate(colUsed[c]))
                    {
                        val = GetValueFromBits(colUsed[c] >> 1);
                    }
                    else if (HasSingleCandidate(boxUsed[b]))
                    {
                        val = GetValueFromBits(boxUsed[b] >> 1);
                    }

                    if (val > 0)
                    {
                        isUpdated = true;
                        SetCandidate(r, c, val);

                        var resolvedNode = node;
                        node = node.Next;
                        squares.Remove(resolvedNode);
                    }
                    else
                    {
                        node = node.Next;
                    }
                }

                if (isUpdated) ResolveSingleCandidateSquares();
            }

            int GetValueFromBits(int bits)
            {
                var result = 0;
                for (; bits > 0; bits = bits >> 1) ++result;

                return result;
            }

            bool HasSingleCandidate(int bits)
            {
                var val = (~bits) & subMask;
                return (val & (val - 1)) == 0;
            }

            static int BoxIdx(int r, int c) => (r / 3) * 3 + c / 3;            
        }
    }
}
