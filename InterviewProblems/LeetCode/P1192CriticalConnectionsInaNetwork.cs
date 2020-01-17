using System;
using System.Collections.Generic;
using System.Linq;


namespace InterviewProblems.LeetCode
{
    public class P1192CriticalConnectionsInaNetwork
    {
        private enum Status { Unvisited, Visiting, Visited };

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            var graph = BuildGraph();
            var state = new Status[n];
            var loopNumber = Enumerable.Range(0, n).ToArray();
            var ranks = new int[n];
            var currentLoop = 1;
            var parents = Enumerable.Range(0, n).ToArray();

            FindLoops(0, 0);
            for (var i = 0; i < loopNumber.Length; ++i)
            {
                Console.WriteLine($"{i} : {GetLoopNumber(i)}");
            }

            return connections
                .Aggregate(new List<IList<int>>(),
                           (res, connection) =>
                           {
                               if (loopNumber[connection[0]] != loopNumber[connection[1]]) res.Add(connection);
                               return res;
                           });

            // Utility functions

            void Union(int node1, int node2)
            {
                if (node1 == node2) return;
                if (ranks[node1] >= ranks[node2])
                {
                    if (ranks[node1] == ranks[node2]) ++ranks[node1];
                    loopNumber[node2] = node1;
                }
                else loopNumber[node1] = node2;
            }

            int GetLoopNumber(int node)
            {
                if (node == loopNumber[node]) return node;
                var number = GetLoopNumber(loopNumber[node]);
                loopNumber[node] = number;
                return number;
            }

            void UpdateCurrentLoop(int origin, int node, int loop)
            {
                if (node == origin) return;
                loopNumber[node] = loop;
                UpdateCurrentLoop(origin, parents[node], loop);
            }

            void FindLoops(int parent, int node)
            {
                if (state[node] == Status.Visiting)
                {
                    Union(GetLoopNumber(node), node);
                    loopNumber[node] = GetLoopNumber(node);
                    UpdateCurrentLoop(node, parent, node);
                    ++currentLoop;
                }
                else if (state[node] == Status.Unvisited)
                {
                    state[node] = Status.Visiting;
                    parents[node] = parent;
                    var children = graph[node];
                    foreach (var child in children)
                    {
                        if (child == parent) continue;
                        FindLoops(node, child);
                    }
                    state[node] = Status.Visited;
                }
            }

            List<int>[] BuildGraph() =>
                connections
                    .Aggregate(Enumerable.Repeat(0, n).Select(x => new List<int>()).ToArray(),
                               (arr, connection) =>
                               {
                                   var node1 = connection[0];
                                   var node2 = connection[1];
                                   arr[node1].Add(node2);
                                   arr[node2].Add(node1);
                                   return arr;
                               });
        }
    }     
}
