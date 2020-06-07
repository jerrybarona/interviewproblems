using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P020RemoveRedundantEdges
    {
        // https://leetcode.com/discuss/interview-question/630806/Facebook-or-Phone-or-Remove-redudant-edges-and-number-of-zeros
        // https://www.wikiwand.com/en/Transitive_reduction
        public void RemoveRedundantEdgesTest()
        {
            var input = new[]
                { new[] { 1, 2 }, new[] { 1, 3 }, new[] { 1, 4 }, new[] { 2, 3 }, new[] { 2, 4 }, new[] { 3, 4 } };
            var n = 4;
            var result = RemoveRedundantEdges(input, n);
            Console.WriteLine($"Input: [{string.Join(", ", input.Select(p => $"[{string.Join(", ", p)}]"))}]\nOutput: [{string.Join(", ", result.Select(p => $"[{string.Join(", ", p)}]"))}]");

            input = new[]
                { new[] { 1, 2 }, new[] { 1, 3 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 2, 4 }, new[] { 3, 4 }, new[] { 3, 5 }, new[] { 4, 5 } };
            n = 5;
            result = RemoveRedundantEdges(input, n);
            Console.WriteLine($"Input: [{string.Join(", ", input.Select(p => $"[{string.Join(", ", p)}]"))}]\nOutput: [{string.Join(", ", result.Select(p => $"[{string.Join(", ", p)}]"))}]");
        }

        public List<int[]> RemoveRedundantEdges(int[][] edges, int n)
        {
            var graph = edges.Aggregate(new Dictionary<int, HashSet<int>>(), (dict, edge) =>
            {
                if (!dict.ContainsKey(edge[0])) dict.Add(edge[0], new HashSet<int>());
                dict[edge[0]].Add(edge[1]);
                return dict;
            });
            var memo = new Dictionary<int, HashSet<int>>();

            foreach (var node in Enumerable.Range(1, n))
            {
                if (!memo.ContainsKey(node)) Descendants(node);
            }

            return graph.SelectMany(item => item.Value.Select(neighbor => new[] { item.Key, neighbor })).ToList();

            HashSet<int> Descendants(int node)
            {
                if (memo.ContainsKey(node)) return memo[node];
                var descendants = new HashSet<int>();
                if (!graph.ContainsKey(node)) return descendants;

                foreach (var neighbor in graph[node].ToList())
                {
                    if (graph[node].Contains(neighbor))
                    {
                        var nextDescendants = Descendants(neighbor);
                        foreach (var d in nextDescendants) graph[node].Remove(d);
                        descendants.UnionWith(nextDescendants);
                        descendants.Add(neighbor);
                    }
                }

                memo.Add(node, descendants);
                return descendants;
            }
        }
    }
}
