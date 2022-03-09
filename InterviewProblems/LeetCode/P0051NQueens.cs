using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
	public class P0051NQueens
	{
        public void SolveNQueensTest()
		{
            foreach (var testCase in new [] { 4 })
			{
                var result = SolveNQueens(testCase);
                foreach (var row in result) Console.WriteLine(row);
			}
		}

        public IList<IList<string>> SolveNQueens(int n)
        {
            var board = new int[n];
            var attackedCols = 0;
            long attackedDiags = 0;
            long attackedAntiDiags = 0;

            var result = new List<IList<string>>();

            PlaceQueen();

            return result;

            void PlaceQueen(int row = 0)
            {
                if (row == n)
                {
                    AddBoardToResult();
                    return;
                }

                for (var col = 0; col < n; ++col)
                {
                    var (diag, antiDiag) = (row + col, row - col);
                    if (!IsColumnAttacked(col) && !IsDiagonalAttacked(diag) && !IsAntiDiagonalAttacked(antiDiag))
                    {
                        CheckColumn(col);
                        CheckDiag(diag);
                        CheckAntiDiag(antiDiag);

                        board[row] = col;
                        PlaceQueen(row + 1);

                        UncheckColumn(col);
                        UncheckDiag(diag);
                        UncheckAntiDiag(antiDiag);
                    }
                }
            }

            bool IsColumnAttacked(int col) => (attackedCols & (1 << col)) != 0;
            bool IsDiagonalAttacked(int diag) => (attackedDiags & (1 << (19 + diag))) != 0;
            bool IsAntiDiagonalAttacked(int antiDiag) => (attackedAntiDiags & (1 << (19 + antiDiag))) != 0;

            void CheckColumn(int col) => attackedCols |= (1 << col);
            void CheckDiag(int diag) => attackedDiags |= ((long)1 << (19 + diag));
            void CheckAntiDiag(int antiDiag) => attackedAntiDiags |= ((long)1 << (19 + antiDiag));

            void UncheckColumn(int col) => attackedCols &= ~(1 << col);
            void UncheckDiag(int diag) => attackedDiags &= ~((long)1 << (19 + diag));
            void UncheckAntiDiag(int antiDiag) => attackedAntiDiags &= ~((long)1 << (19 + antiDiag));

            void AddBoardToResult()
            {
                result.Add(board.Select(r => $"{new string('.', r)}Q{new string('.', n - r - 1)}").ToList());
            }
        }
    }
}
