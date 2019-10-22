using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.CodeForces
{
    public class C1129PD1Isolation
    {
        /*
         * https://codeforces.com/contest/1129/problem/D
         */

        public int Solve(int[] a, int k)
        {
            var memo = Enumerable.Repeat(1, a.Length).Select(x => new int[a.Length]).ToArray();

            for (var i = 0; i < a.Length; ++i)
            {
                var seen = new HashSet<int>();
                for (var j = i; j >= 0; --j)
                {
                    seen.Add(a[j]);
                    if (seen.Count <= k)
                    {
                    }

                    if (seen.Count > k) break;
                }
            }

            return 0;
        }
    }
}
