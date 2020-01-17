using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0261GraphValidTree
    {
        public bool ValidTree(int n, int[][] edges)
        {
            var graph = edges
                .Aggregate(Enumerable.Range(0, n).Select(x => new HashSet<int>()).ToArray(), (children, edge) =>
                {
                    var node1 = edge[0];
                    var node2 = edge[1];
                    children[node1].Add(node2);
                    children[node2].Add(node1);
                    return children;
                });

            var indegree = edges
                .Aggregate(new int[n], (arr, x) =>
                {
                    ++arr[x[0]];
                    ++arr[x[1]];
                    return arr;
                });

            var queue = new Queue<int>(indegree.Select((val, idx) => (val, idx)).Where(x => x.val == 1).Select(y => y.idx));
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();                
                foreach (var child in graph[node].ToList())
                {
                    graph[node].Remove(child);
                    graph[child].Remove(node);
                    --indegree[node];
                    --indegree[child];
                    if (indegree[child] == 1) queue.Enqueue(child);
                }
            }

            return indegree.All(x => x == 0);
        }
    }
}
