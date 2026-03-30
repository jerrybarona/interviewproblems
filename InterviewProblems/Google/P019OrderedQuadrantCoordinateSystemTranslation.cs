using System.Linq;

using InterviewProblems.Utilities;

namespace InterviewProblems.Google
{
    internal class P019OrderedQuadrantCoordinateSystemTranslation : ITestable
    {
        // https://leetcode.com/discuss/interview-question/6219667/Google-Senior-engineer
        public void RunTest()
        {
            foreach (var n in new int[] { 2, 3 })
                ArrayUtilities.PrintMatrix(Translate(n));
        }

        private int[][] Translate(int n)
        {
            if (n == 0) return new[] { new[] { 0 } };

            var d = 1 << n;
            var result = Enumerable.Repeat(0, d).Select(_ => new int[d]).ToArray();
            Dfs(d - 1, 0, 0, d);

            return result;

            void Dfs(int r, int c, int init, int dimension)
            {
                if (dimension == 2)
                {
                    result[r][c] = init;
                    result[r - 1][c] = init + 1;
                    result[r - 1][c + 1] = init + 2;
                    result[r][c + 1] = init + 3;
                    return;
                }

                var len = dimension;
                var halfLen = len / 2;
                var val = len * len / 4;
                Dfs(r, c, init, dimension / 2);
                Dfs(r - halfLen, c, init + val, dimension / 2);
                Dfs(r - halfLen, c + halfLen, init + 2 * val, dimension / 2);
                Dfs(r, c + halfLen, init + (3 * val), dimension / 2);
            }
        }
    }
}
