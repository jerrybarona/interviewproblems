using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0465OptimalAccountBalancing
    {
        public void MinTransferTest()
        {
            var t = new[] { new[] { 0, 3, 2 }, new[] { 1, 4, 3 }, new[] { 2, 3, 2 }, new[] { 2, 4, 2 } };

            Console.WriteLine(MinTransfers(t));
        }

        public int MinTransfers(int[][] transactions)
        {
            var map = transactions.Aggregate(new Dictionary<int, int>(), (dict, trans) =>
            {
                if (!dict.ContainsKey(trans[0])) dict.Add(trans[0], 0);
                if (!dict.ContainsKey(trans[1])) dict.Add(trans[1], 0);

                dict[trans[0]] -= trans[2];
                dict[trans[1]] += trans[2];

                return dict;
            }).Select(x => x.Value).Where(y => y != 0).ToArray();

            return Dfs(0);

            int Dfs(int idx)
            {
                if (idx == map.Length) return 0;
                if (map[idx] == 0) return Dfs(idx + 1);

                var val = map[idx];

                for (var i = idx + 1; i < map.Length; ++i)
                {
                    if (val + map[i] == 0)
                    {
                        map[i] += val;
                        var r = 1 + Dfs(idx + 1);
                        map[i] -= val;

                        return r;
                    }
                }

                var result = 1000000007;
                for (var i = idx + 1; i < map.Length; ++i)
                {
                    if (val * map[i] < 0)
                    {
                        map[i] += val;
                        result = Math.Min(result, 1 + Dfs(idx + 1));
                        map[i] -= val;
                    }
                }

                return result;
            }
        }
    }
}
