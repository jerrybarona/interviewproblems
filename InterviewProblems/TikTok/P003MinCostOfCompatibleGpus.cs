using System;
using System.Collections.Generic;

namespace InterviewProblems.TikTok
{
    public class P003MinCostOfCompatibleGpus : ITestable
    {
        public void RunTest()
        {
            foreach (var (cost, compatible1, compatible2, minCompatible) in new(int[], int[], int[], int)[]
            {
                (new[] { 2, 4, 6, 5 }, new[] { 1, 1, 1, 0 }, new[] { 0, 0, 1, 1 }, 2 ),
            })
            {
                Console.WriteLine(MinCostOfCompatibleGpus(cost, compatible1, compatible2, minCompatible));
            }
        }

        public static int MinCostOfCompatibleGpus(int[] cost, int[] compatible1, int[] compatible2, int minCompatible)
        {
            var len = cost.Length;
            var memo = new Dictionary<(int,int,int), int>();

            return MinCost();

            int MinCost(int idx = 0, int count1 = 0, int count2 = 0)
            {
                if (idx == len || (count1 >= minCompatible && count2 >= minCompatible))
                {
                    return count1 == minCompatible && count2 == minCompatible ? 0 : int.MaxValue;
                }

                if (memo.ContainsKey((idx, count1, count2)))
                {
                    return memo[(idx, count1, count2)];
                }

                var result = MinCost(idx + 1, count1, count2);
                if (compatible1[idx] == 1 || compatible2[idx] == 1)
                {
                    var w = MinCost(idx + 1, count1 + (compatible1[idx] == 1 ? 1 : 0), count2 + (compatible2[idx] == 1 ? 1 : 0));
                    if (w < int.MaxValue) w += cost[idx];

                    result = Math.Min(result, w);
                }

                memo[(idx, count1, count2)] = result;
                return result;
            }
        }
    }
}
