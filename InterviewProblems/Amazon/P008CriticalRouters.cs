using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Amazon
{
    public class P008CriticalRouters
    {
        public int[] CriticalRouters(int numNodes, int[][] edges)
        {
            var graph = edges.Aggregate(Enumerable.Repeat(0, numNodes).Select(x => new List<int>()).ToArray(), (arr, edge) =>
             {
                 arr[edge[0]].Add(edge[1]);
                 arr[edge[1]].Add(edge[0]);
                 return arr;
             });

            var visited = new HashSet<int>(numNodes);
            var visitedTime = new int[numNodes];
            var lowVisitedTime = new int[numNodes];
            var root = 0;
            var time = 1;
            var result = new List<int>();
            Dfs(0, -1);
            return result.ToArray();

            void Dfs(int node, int parent)
            {
                visited.Add(node);
                visitedTime[node] = time;
                lowVisitedTime[node] = time;
                ++time;
                var isCritical = false;
                var childCount = 0;
                var children = graph[node];
                foreach (var child in children)
                {
                    if (child == parent) continue;
                    if (!visited.Contains(child))
                    {
                        ++childCount;
                        Dfs(child, node);
                        if (visitedTime[node] <= lowVisitedTime[child]) isCritical = true;
                    }
                    lowVisitedTime[node] = Math.Min(lowVisitedTime[node], lowVisitedTime[child]);
                }
                if (node == root && childCount >= 2 || node != root && isCritical) result.Add(node);
            }            
        }
    }
}
