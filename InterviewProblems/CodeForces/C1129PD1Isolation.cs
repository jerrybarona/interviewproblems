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
         public int Solve(int[] nums, int k)
        {
            var memo = new int[nums.Length + 1];
            memo[0] = 1;

            for (var i = 1; i < memo.Length; ++i)
            {
                var uniques = new HashSet<int>();
                var repeats = new HashSet<int>();
                for (var j = i; j >= 1; --j)
                {
                    var num = nums[j - 1];
                    if (!repeats.Contains(num))
                    {
                        if (uniques.Contains(num))
                        {
                            uniques.Remove(num);
                            repeats.Add(num);
                        }
                        else uniques.Add(num);
                    }
                    if (uniques.Count <= k) memo[i] += memo[j - 1];
                }
            }

            return memo[nums.Length];
        }
    }
}
