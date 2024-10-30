using System;
using System.Linq;

namespace InterviewProblems.Facebook
{
    internal class P035CaveAdvanture : ITestable
    {
        // https://www.fastprep.io/problems/mt-cave

        public void RunTest()
        {
            foreach (var A in new string[][]
            {
                new [] { "..0.", "...0", "....", "0..." }, // 9
                new [] { ".0...", ".0...", "...0." }, // -1
                new [] { "......", ".0000.", "...0..", "...0.0", "......" }, // 14
            })
            {
                Console.WriteLine(AllAboutCave(A));
            }
        }

        private int AllAboutCave(string[] A)
        {
            var steps = new (int r, int c)[] { (0, 1), (0, -1), (1, 0) };
            int[][] matrix = A.Select(str => str.Select(c => c == '.' ? 1 : 0).ToArray()).ToArray();
            var n = A.Length;
            var m = A[0].Length;

            return MaxRooms();

            int MaxRooms(int r = 0, int c = 0)
            {
                if (r == n - 1 && c == m - 1) return 1;
                matrix[r][c] = -1;
                var result = -1;

                foreach (var (nr, nc) in steps.Select(s => (r: r + s.r, c: c + s.c))
                    .Where(t => t.r >= 0 && t.c >= 0 && t.r < n && t.c < m && matrix[t.r][t.c] == 1))
                {
                    var next = MaxRooms(nr, nc);
                    if (next != -1)
                    {
                        result = Math.Max(result, 1 + next);
                    }
                }

                matrix[r][c] = 1;
                return result;
            }
        }
    }
}
