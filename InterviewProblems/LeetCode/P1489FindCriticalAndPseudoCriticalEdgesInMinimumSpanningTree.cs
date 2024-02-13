using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P1489FindCriticalAndPseudoCriticalEdgesInMinimumSpanningTree : ITestable
    {
        public void RunTest()
        {
            foreach (var (n, edges) in new (int n, int[][] edges)[]
            {
                (5, new[] { new[] { 0, 1, 1 }, new[] { 1, 2, 1 }, new[] { 2, 3, 2 }, new[] { 0, 3, 2 }, new[] { 0, 4, 3 }, new[] { 3, 4, 3 }, new[] { 1, 4, 6 } }),
                (4, new[] { new[] { 0, 1, 1 }, new[] { 1, 2, 1 }, new[] { 2, 3, 1 }, new[] { 0, 3, 1 } }),
                (6, new[] { new[] { 0, 1, 1 }, new[] { 1, 2, 1 }, new[] { 0, 2, 1 }, new[] { 2, 3, 4 }, new[] { 3, 4, 2 }, new[] { 3, 5, 2 }, new[] { 4, 5, 2 } }),
            })
            {
                Console.WriteLine($"Test:\nn = {n}");
                Console.WriteLine($"edges = [{string.Join(", ", edges.Select(x => $"[{string.Join(", ", x)}]"))}]");
                Console.WriteLine($"output: [{string.Join(", ", FindCriticalAndPseudoCriticalEdges(n, edges).Select(x => $"[{string.Join(", ", x)}]"))}]");
                Console.WriteLine("");
            }
        }

        public IList<IList<int>> FindCriticalAndPseudoCriticalEdges(int n, int[][] edges)
        {
            var x = new PriorityQueue<int,int>();
            var y = x.UnorderedItems.Select(x => x.Element);
            (int idx, int node1, int node2, int weight)[] sortedEdges = edges.Select((e, i) => (e, i)).OrderBy(t => t.e[2]).Select(x => (x.i, x.e[0], x.e[1], x.e[2])).ToArray();
            var criticals = new List<int>();
            var psCriticals = new List<int>();

            var (_, mstWeight) = MstWeight();

            for (int i = 0; i < edges.Length; ++i)
            {
                var excluding = MstWeight(exclude: i);
                if (!excluding.isConnected || excluding.weight > mstWeight)
                {
                    criticals.Add(i);
                    continue;
                }

                var forcingIn = MstWeight(forceIn: i);
                if (forcingIn.weight == mstWeight)
                {
                    psCriticals.Add(i);
                }
            }

            return new List<IList<int>> { criticals, psCriticals };

            (bool isConnected, int weight) MstWeight(int exclude = -1, int forceIn = -1)
            {
                var graphEdges = sortedEdges.AsEnumerable();
                if (exclude != -1)
                {
                    graphEdges = graphEdges.Where(x => x.idx != exclude);
                }

                var ds = new DisjointSet(n);
                var result = 0;
                if (forceIn != -1)
                {
                    var n1 = edges[forceIn][0];
                    var n2 = edges[forceIn][1];
                    var w = edges[forceIn][2];

                    ds.Union(n1, n2);
                    result += w;
                    graphEdges = graphEdges.Where(x => x.idx != forceIn);
                }

                result += graphEdges.Where(x => ds.Union(x.node1, x.node2)).Sum(y => y.weight);

                return (ds.IsSetConnected(), result);
            }
        }

        class DisjointSet
        {
            private readonly int[] _ranks;
            private readonly int[] _parents;
            private readonly int _n;

            internal DisjointSet(int n)
            {
                _n = n;
                _ranks = new int[n];
                _parents = Enumerable.Range(0, n).ToArray();
            }

            private int Find(int node)
            {
                if (_parents[node] == node) return node;

                int parent = Find(_parents[node]);
                _parents[node] = parent;

                return parent;
            }

            internal bool Union(int node1, int node2)
            {
                int p1 = Find(node1);
                int p2 = Find(node2);

                if (p1 == p2) return false;

                if (_ranks[p1] >= _ranks[p2])
                {
                    if (_ranks[p1] == _ranks[p2]) ++_ranks[p1];
                    _parents[p2] = p1;
                }
                else _parents[p1] = p2;

                return true;
            }

            internal bool IsSetConnected()
            {
                var first = Find(0);
                return Enumerable.Range(0, _n).All(x => Find(x) == first);
            }
        }
    }
}
