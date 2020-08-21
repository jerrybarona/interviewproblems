using System;
using System.Collections.Generic;

namespace InterviewProblems.Bloomberg
{
    public class P004NumbersOfWaysToPurchaseOil
    {
        // https://leetcode.com/discuss/interview-question/550259/Bloomberg-or-Phone-or-Number-of-ways-to-purchase-oil

        public void NumbersOfWaysToPurchaseOilTest()
        {
            var companies = new[] { 10, 15, 50 };
            var target = 60;
            Console.WriteLine($"Companies: [{string.Join(',', companies)}]; Target: {target}");
            foreach (var combo in NumbersOfWaysToPurchaseOil(companies, target))
            {
                Console.WriteLine($"[{string.Join(',', combo)}]");
            }
        }

        public List<List<int>> NumbersOfWaysToPurchaseOil(int[] companies, int target)
        {
            var memo = new Dictionary<(int,int),List<List<int>>>();

            return NumWays(0, 0);

            List<List<int>> NumWays(int idx, int sum)
            {
                if (idx == companies.Length || sum >= target) return sum == target ? new List<List<int>> { new List<int>() } : null;
                if (memo.ContainsKey((idx, sum)))
                {
                    Console.Write("y");
                    return memo[(idx, sum)];
                }

                var result = new List<List<int>>();
                for (var i = idx; i < companies.Length; ++i)
                {
                    var next = NumWays(i, sum + companies[i]);
                    if (next == null) continue;
                    foreach (var l in next)
                    {
                        var newCombo = new List<int> { companies[i] };
                        newCombo.AddRange(l);
                        result.Add(newCombo);
                    }
                }

                memo.Add((idx,sum), result.Count == 0 ? null : result);
                return result;
            }
        }
    }
}
