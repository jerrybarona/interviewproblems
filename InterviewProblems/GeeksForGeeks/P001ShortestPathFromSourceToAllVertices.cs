using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.GeeksForGeeks
{
    public class P001ShortestPathFromSourceToAllVertices : ITestable
    {
        // https://www.geeksforgeeks.org/dijkstras-shortest-path-algorithm-greedy-algo-7/?ref=lbp
        
        public void RunTest()
        {
            foreach (var (n, edges) in new (int, IList<(int, int, int)>)[]
            {
                (9, new (int,int,int)[] { (0,1,4), (0,7,8), (1,2,8), (1,7,11), (2,3,7), (2,8,2), (2,5,4), (7,8,7), (7,6,1), (8,6,6), (6,5,2), (3,5,14), (3,4,9), (5,4,10) })
            })
            {
                var result = ShortestPath(n, edges);
                Console.WriteLine($"Output: {string.Join(" ", result.Select(t => t.w))}\nExplanation: ");
                for (var i = 0; i < n; ++i)
                {
                    Console.WriteLine($"The minimum distance from 0 to {i} = {result[i].w}. {string.Join("->", result[i].l)}");
                }
            }
        }


        public (int w , IList<int> l)[] ShortestPath(int n, IList<(int node1, int node2, int weight)> edges)
        {
            var graph = edges.Aggregate(new List<(int, int)>[n], (arr, edge) =>
            {
                var (node1, node2, weight) = edge;

                if (arr[node1] == null) arr[node1] = new List<(int, int)>();
                if (arr[node2] == null) arr[node2] = new List<(int, int)>();

                arr[node1].Add((node2, weight));
                arr[node2].Add((node1, weight));

                return arr;
            });

            var parent = Enumerable.Repeat(-1, n).ToArray();
            var weight = Enumerable.Repeat(-1, n).ToArray();
            var pq = new PriorityQueue<(int nId,int pId),int>();

            pq.Enqueue((0, 0), 0);

            while (pq.Count > 0)
            {
                pq.TryDequeue(out (int, int) t, out int nodeWeight);
                var (nodeId, parentId) = t;
                if (parent[nodeId] != -1) continue;
                parent[nodeId] = parentId;
                weight[nodeId] = nodeWeight;

                foreach (var (neighborId, neighborWeight) in graph[nodeId])
                {
                    if (neighborId == nodeId || neighborId == parentId) continue;
                    pq.Enqueue((neighborId, nodeId), neighborWeight + nodeWeight);
                }
            }

            return Enumerable.Range(0, n).Aggregate(new (int, IList<int>)[n], (arr, idx) =>
            {
                var stack = new Stack<int>();
                stack.Push(idx);
                for (var i = idx; stack.Count > 0 && stack.Peek() != 0; )
                {
                    stack.Push(parent[i]);
                    i = parent[i];
                }

                arr[idx] = (weight[idx], stack.ToList());

                return arr;
            });

        }
    }
}
