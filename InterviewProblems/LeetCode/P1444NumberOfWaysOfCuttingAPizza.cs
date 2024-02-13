using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P1444NumberOfWaysOfCuttingAPizza : ITestable
    {
        public void RunTest()
        {
            foreach (var (pizza, k) in new(string[], int)[]
            {
                (new[]{ "A..","AA.","..." }, 3)
            })
            {
                Console.WriteLine(Ways(pizza, k));
            }
        }

        public int Ways(string[] pizza, int k)
        {
            var m = pizza.Length;
            var n = pizza[0].Length;
            var memo = new Dictionary<(int, int, int), int>();

            return NumWays(0, 0, k - 1);

            int NumWays(int r, int c, int cuts)
            {
                if (r == m || c == n) return 0;

                if (memo.ContainsKey((r, c, cuts))) return memo[(r, c, cuts)];

                var foundApple = false;
                int nr = -1;
                int nc = -1;

                for (var i = r; i < m; ++i)
                {
                    for (var j = c; j < n; ++j)
                    {
                        if (pizza[i][j] == 'A')
                        {
                            nr = i;
                            nc = j;
                            foundApple = true;
                            break;
                        }
                    }

                    if (foundApple) break;
                }

                var result = 0;
                if (!foundApple)
                {
                    result = 0;
                }
                else
                {
                    if (cuts == 0) result = 1;
                    else result = NumWays(nr + 1, nc, cuts - 1) + NumWays(nr, nc + 1, cuts - 1);
                }

                memo.Add((r, c, cuts), result);
                return result;
            }
        }
    }
}
