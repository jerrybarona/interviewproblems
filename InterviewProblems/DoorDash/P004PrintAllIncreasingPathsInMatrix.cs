using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.DoorDash
{
    internal class P004PrintAllIncreasingPathsInMatrix : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5024675/Doordash-or-Onsite-or-2024

        public void RunTest()
        {
            foreach (var matrix in new int[][][]
            {
                new [] { new [] { 9,9,4 }, new[] {6,6,8}, new[] {2,1,1}},
                new [] { new [] { 3, 4, 5 }, new[] { 3, 2, 6 }, new[] { 2, 2, 1 } },
                new [] {
                    new [] { 17,18,19,20,21,3 },
                    new [] { 16, 9, 8, 7,22,2 },
                    new [] { 15,10, 5, 6,23,1 },
                    new [] { 14,11, 4, 3,24,27 },
                    new [] { 13,12, 1, 2,25,26 }}
            })
            {
                Console.WriteLine("Paths:");
                var paths = IncreasingPathsInMatrix(matrix);
                foreach (var path in paths) { Console.WriteLine(string.Join(", ", path)); }                
            }
        }

        private IList<List<int>> IncreasingPathsInMatrix(int[][] matrix)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;

            var steps = new (int r, int c)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var result = new List<List<int>>();
            var path = new LinkedList<int>();

            for (var i = 0; i < m; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    TraversePaths(i, j);
                }
            }

            return result;

            void TraversePaths(int r, int c)
            {
                path.AddLast(matrix[r][c]);
                foreach (var (nr, nc) in steps.Select(s => (r: r + s.r, c: c + s.c)).Where(t => t.r >= 0 && t.c >= 0 && t.r < m && t.c < n && matrix[t.r][t.c] > matrix[r][c]))
                {
                    TraversePaths(nr, nc);
                }

                result.Add(new List<int>(path));
                path.RemoveLast();
            }
        }
    }
}
