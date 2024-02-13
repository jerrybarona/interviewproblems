using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P1494ParallelCourses2 : ITestable
    {
        public void RunTest()
        {
            foreach (var (n, relations, k) in new (int, int[][], int)[]
            {
                (4, new[]{ new[] {2,1}, new[]{3,1}, new[]{1,4} }, 2),
                //(5, new[]{ new[] {2,1}, new[]{3,1}, new[]{1,4} }, 2),
            })
            {
                Console.WriteLine(MinNumberOfSemesters(n, relations, k));
            }
        }

        public int MinNumberOfSemesters(int n, int[][] relations, int k)
        {
            var (graph, indegree) = relations.Aggregate((new List<int>[n + 1], new int[n + 1]), (tup, rel) =>
            {
                var (p, n) = (rel[0], rel[1]);
                var (graph, indegree) = tup;

                if (graph[p] == null) graph[p] = new List<int>();
                graph[p].Add(n);
                ++indegree[n];

                return (graph, indegree);
            });

            var seenAll = (1 << (n + 1)) - 1;
            var memo = new Dictionary<int, int>();

            return Dp();

            int Dp(int mask = 1)
            {
                if (mask == seenAll) return 0;
                if (memo.ContainsKey(mask)) return memo[mask];

                var result = 20000;
                foreach (var cset in GetCandidates(mask))
                {
                    AdjustIndegrees(cset, true);

                    var nextMask = cset.Aggregate(mask, (res, c) => res | (1 << c));
                    result = Math.Min(result, 1 + Dp(nextMask));

                    AdjustIndegrees(cset, false);
                }

                memo.Add(mask, result);
                return result;
            }

            void AdjustIndegrees(List<int> list, bool deduct)
            {
                foreach (var item in list)
                {
                    if (graph[item] == null) continue;
                    foreach (var n in graph[item])
                    {
                        indegree[n] += deduct ? -1 : 1;
                    }
                }
            }

            List<List<int>> GetCandidates(int mask)
            {
                var xcourses = Enumerable.Range(1, n).Where(x => indegree[x] == 0 && ((1 << x) & mask) == 0).ToArray();
                var quant = Math.Min(k, xcourses.Length);

                var candidates = new List<List<int>>();
                var xset = new List<int>();

                Combine();

                return candidates;

                void Combine(int idx = 0, int q = 0)
                {
                    if (q == quant)
                    {
                        candidates.Add(new List<int>(xset));
                        return;
                    }

                    for (var i = idx; i < xcourses.Length; ++i)
                    {
                        xset.Add(xcourses[i]);
                        Combine(i + 1, q + 1);
                        xset.RemoveAt(xset.Count - 1);
                    }
                }
            }
        }
    }
}
