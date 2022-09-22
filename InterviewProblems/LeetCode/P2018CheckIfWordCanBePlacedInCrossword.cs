using System;

namespace InterviewProblems.LeetCode
{
    public class P2018CheckIfWordCanBePlacedInCrossword
    {
        public void PlaceWordInCrosswordTest()
        {
            foreach (var (board, word) in new (char[][], string)[]
            {
                (new char[][] { new[] { ' ', '#', 'a' }, new[] { ' ', '#', 'c' }, new[] { ' ', '#', 'a' }  }, "ac"),
                (new char[][] { new[] {' '}, new[] {'#'}, new[] {'o'}, new[] {' '}, new[] {'t'}, new[] {'m'}, new[] {'o'}, new[] {' '}, new[] {'#'}, new[] {' '} }, "octmor")
            })
            {
                Console.WriteLine(PlaceWordInCrossword(board, word));
            }
        }

        public bool PlaceWordInCrossword(char[][] board, string word)
        {
            var len = word.Length;
            var (m, n) = (board.Length, board[0].Length);

            return IsRowMatch() || IsColumnMatch();

            bool IsRowMatch()
            {
                if (len > n) return false;

                foreach (var row in board)
                {
                    for (var i = 0; i < n; ++i)
                    {
                        if (row[i] == '#') continue;
                        if (i + len > n) break;

                        if (i + len < n && row[i + len] != '#')
                        {
                            while (i < n && row[i] != '#') ++i;
                            continue;
                        }

                        var k = i;
                        var j = 0;
                        var foundBlock = false;
                        for (; j < len; ++j, ++k)
                        {
                            if (row[k] == word[j] || row[k] == ' ') continue;
                            if (row[k] == '#')
                            {
                                foundBlock = true;
                            }

                            break;
                        }

                        if (j == len) return true;
                        if (foundBlock)
                        {
                            i += len;
                            continue;
                        }

                        k = i;
                        j = len - 1;
                        for (; j >= 0; --j, ++k)
                        {
                            if (row[k] == word[j] || row[k] == ' ') continue;
                            break;
                        }

                        if (j == -1) return true;
                        i += len;
                    }
                }

                return false;
            }

            bool IsColumnMatch()
            {
                if (len > m) return false;

                for (var col = 0; col < n; ++col)
                {
                    for (var i = 0; i < m; ++i)
                    {
                        if (board[i][col] == '#') continue;
                        if (i + len > m) break;

                        if (i + len < m && board[i + len][col] != '#')
                        {
                            while (i < m && board[i][col] != '#') ++i;
                            continue;
                        }

                        var k = i;
                        var j = 0;
                        var foundBlock = false;
                        for (; j < len; ++j, ++k)
                        {
                            if (board[k][col] == word[j] || board[k][col] == ' ') continue;
                            if (board[k][col] == '#')
                            {
                                foundBlock = true;
                            }

                            break;
                        }

                        if (j == len) return true;
                        if (foundBlock)
                        {
                            i += len;
                            continue;
                        }

                        k = i;
                        j = len - 1;
                        for (; j >= 0; --j, ++k)
                        {
                            if (board[k][col] == word[j] || board[k][col] == ' ') continue;
                            break;
                        }

                        if (j == -1) return true;
                        i += len;
                    }
                }

                return false;
            }
        }
    }
}
