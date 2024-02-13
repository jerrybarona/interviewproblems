using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P2097ValidArrangementsOfPairs : ITestable
    {
        public void RunTest()
        {
            foreach (var pairs in new int[][][]
            {
                new[] { new[]{ 1,2}, new[]{1,3 }, new[] { 2,1, int.MinValue} },
                //new[] { new[]{ 5,1}, new[]{4,5 }, new[] { 11,9}, new[] { 9,4} },
                //new[] { new[]{ 1,3}, new[]{3,2 }, new[] { 2,1} },
            })
            {
                Console.WriteLine($"[{string.Join(",", ValidArrangement(pairs).Select(p => $"[{p[0]},{p[1]}]"))}]");
            }
        }

        public int[][] ValidArrangement(int[][] pairs)
        {
            var pairMap = Enumerable.Range(0, pairs.Length)
                .Aggregate(new Dictionary<int, List<int>>(), (dict, idx) =>
                {
                    var s = pairs[idx][0];
                    if (!dict.ContainsKey(s))
                    {
                        dict.Add(s, new List<int>());
                    }

                    dict[s].Add(idx);

                    return dict;
                });

            var (graph, indegree) = Enumerable.Range(0, pairs.Length)
                .Aggregate((new Queue<int>[pairs.Length], new int[pairs.Length]), (tup, idx) =>
                {
                    var (graph, indegree) = tup;
                    var e = pairs[idx][1];
                    if (pairMap.ContainsKey(e))
                    {
                        if (graph[idx] == null) graph[idx] = new Queue<int>();
                        foreach (var i in pairMap[e])
                        {
                            graph[idx].Enqueue(i);
                            ++indegree[i];
                        }
                    }

                    return (graph, indegree);
                });

            var begin = -1;

            for (var i = 0; i < pairs.Length; ++i)
            {
                var indeg = indegree[i];
                var outdeg = graph[i]?.Count ?? 0;

                if (outdeg - indeg == 1)
                {
                    begin = i;
                    break;
                }
            }
            begin = begin == -1 ? 0 : begin;

            var result = new Stack<int>();
            Dfs(begin);

            return result.Select(x => pairs[x]).ToArray();

            void Dfs(int node)
            {
                if (graph[node] != null)
                {
                    for (; graph[node].Count > 0;)
                    {
                        var i = graph[node].Dequeue();
                        Dfs(i);
                    }
                }
                
                result.Push(node);
            }
        }
    }
}
