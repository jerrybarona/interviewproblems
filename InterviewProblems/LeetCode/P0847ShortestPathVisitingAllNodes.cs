using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0847ShortestPathVisitingAllNodes : ITestable
    {
        public void RunTest()
        {
            foreach (var graph in new int[][][] 
            {
                //new[] { new[]{ 1, 2, 3 }, new[]{ 0 }, new[]{ 0 }, new[]{ 0 } },
                new[] { new[] { 1 },new[] { 0, 2, 4 },new[] { 1, 3 },new[] { 2 },new[] { 1, 5 }, new[] { 4 } },
            })
            {
                var (pathLen, path) = ShortestPathLength(graph);
                Console.WriteLine($"Output: {pathLen}, Path: [{string.Join(" ,", path)}]");
            }
        }

        public (int, Queue<int>) ShortestPathLength(int[][] graph)
        {
            var len = graph.Length;
            var memo = new Dictionary<int,int>();
            var all = (1 << len) - 1;

            var pathLen = int.MaxValue;
            Queue<int> path = null;

            foreach (var node in Enumerable.Range(0, len))
            {
                var queue = new Queue<int>();
                var p = Shortest((node << len) | (1 << node), queue);

                if (p < pathLen)
                {
                    pathLen = p;
                    path = queue;
                }
            }

            return (pathLen, path);

            int Shortest(int key, Queue<int> q)
            {
                var currNode = key >> len;
                var visited = key & all;
                if (visited == all) return 0;

                if (memo.ContainsKey(key)) return memo[key];

                memo.Add(key, int.MaxValue);

                var result = int.MaxValue;
                foreach (var neighbor in graph[currNode])
                {
                    var nextKey = (neighbor << len) | visited | (1 << neighbor);
                    var nextShort = Shortest(nextKey, q);

                    if (nextShort == int.MaxValue) continue;

                    result = Math.Min(result, 1 + nextShort);
                }

                if (result != int.MaxValue) q.Enqueue(currNode);

                memo[key] = result;

                return result;
            }
        }
    }
}
