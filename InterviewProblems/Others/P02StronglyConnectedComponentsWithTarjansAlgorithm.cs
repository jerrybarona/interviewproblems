using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Others
{
    public class P02StronglyConnectedComponentsWithTarjansAlgorithm : ITestable
    {
        // https://youtu.be/ZeDNSeilf-Y?si=S12OfRExUvxuS0Zn&t=1238
        public void RunTest()
        {
            foreach (var (n, edges) in new (int, IList<IList<int>>)[]
            {
                (7, new [] { new [] { 0, 1 }, new [] { 1, 2 }, new [] { 1, 3 }, new [] { 3, 4 }, new [] { 4, 0 }, new [] { 4, 5 }, new [] { 4, 6 }, new [] { 5, 2 }, new [] { 5, 6 }, new [] { 6, 5 } })
            })
            {
                Console.WriteLine($"SCCs:\n{string.Join("\n", FindStronglyConnectedComponents(n, edges).Select(c => string.Join(", ", c)))}");
            }
        }

        public IList<IList<int>> FindStronglyConnectedComponents(int n, IList<IList<int>> edges)
        {
            var graph = edges.Aggregate(new IList<int>[n], (arr, edge) =>
            {
                var (u, v) = (edge[0], edge[1]);
                if (arr[u] == null) arr[u] = new List<int>();
                arr[u].Add(v);

                return arr;
            });

            var timer = 0;
            var discovery = Enumerable.Repeat(-1, n).ToArray();
            var lowest = Enumerable.Repeat(-1, n).ToArray();
            var state = new int[n]; // 0: unvisited, 1: visiting, 2: visited
            var result = new List<IList<int>>();
            var scc = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (discovery[i] == -1) FindScc(i);
            }

            return result;

            void FindScc(int idx)
            {
                discovery[idx] = timer;
                lowest[idx] = timer;
                state[idx] = 1;

                if (graph[idx] != null)
                {
                    foreach (var neighbor in graph[idx])
                    {
                        if (state[neighbor] == 1) // backedge
                        {
                            lowest[idx] = Math.Min(lowest[idx], discovery[neighbor]);
                            continue;
                        }

                        if (state[neighbor] == 2) continue; // crossedge

                        ++timer;
                        FindScc(neighbor);
                        lowest[idx] = Math.Min(lowest[idx], lowest[neighbor]);
                    }
                }

                scc.Add(idx);
                if (discovery[idx] == lowest[idx])
                {
                    result.Add(scc);
                    scc = new List<int>();
                }

                state[idx] = 2;
            }
        }
    }
}
