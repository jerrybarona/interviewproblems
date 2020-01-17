using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P003MinCostToConnectAllNodesMinimumSpanningTreeI
    {
        // https://aonecode.com/amazon-online-assessment-questions
        public int Compute(int n, int[][] edges, int[][] newEdges)
        {
            var parents = Enumerable.Range(0, n + 1).ToArray();
            var ranks = new int[n + 1];

            foreach (var edge in edges)
            {
                var (node1, node2) = (edge[0], edge[1]);
                Union(GetParent(node1), GetParent(node2));
            }

            var dict = new Dictionary<(int, int), int>();

            foreach (var newEdge in newEdges)
            {
                var (node1, node2, cost) = (GetParent(newEdge[0]), GetParent(newEdge[1]), newEdge[2]);
                var x = Math.Min(node1, node2);
                var y = x == node1 ? node2 : node1;
                if (!dict.ContainsKey((x, y))) dict.Add((x, y), cost);
                else dict[(x, y)] = Math.Min(dict[(x, y)], cost);
            }

            var sum = dict.Values.Sum();
            var max = dict.Values.Max();

            return parents.Distinct().Count() - 1 == dict.Count ? sum - max : sum;

            int GetParent(int node)
            {
                if (parents[node] == node) return node;
                var parent = GetParent(parents[node]);
                parents[node] = parent;
                return parent;
            }

            void Union(int node1, int node2)
            {
                if (ranks[node1] >= ranks[node2])
                {
                    if (ranks[node1] == ranks[node2]) ++ranks[node1];
                    parents[node2] = node1;
                }
                else parents[node1] = node2;
            }
        }
    }
}
