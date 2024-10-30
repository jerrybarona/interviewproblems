using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P2791CountPathsThatCanFormAPalindromeInATree : ITestable
    {
        public void RunTest()
        {
            foreach (var (parent, s) in new (IList<int>, string)[]
            {
                (new[]{-1,0,0,1,1,2}, "acaabc"),
                //(new[]{-1,0,0,0,0}, "aaaaa")
            })
            {
                Console.WriteLine(CountPalindromePaths(parent, s));
            }
        }

        public long CountPalindromePaths(IList<int> parent, string s)
        {
            var n = parent.Count;
            var graph = new List<(int, char)>[n];

            for (var child = 0; child < n; ++child)
            {
                var par = parent[child];
                if (par == -1) continue;

                if (graph[par] == null)
                {
                    graph[par] = new List<(int, char)>();
                }

                graph[par].Add((child, s[child]));
            }

            var childPaths = new List<(int, int)>[n];
            var parentPaths = new List<(int, int)>[n];
            long result = 0;
            CountChildPaths();
            CountParentPaths();

            return result;

            void CountParentPaths(int node = 0)
            {
                int nodeParent = parent[node];
                char parentNodeLetter = s[node];
                if (nodeParent != -1)
                {
                    if (parentPaths[node] == null) parentPaths[node] = new List<(int, int)>();
                    foreach (var (sibling, letter) in graph[nodeParent])
                    {
                        if (sibling == node) continue;

                        var prefix = (1 << (parentNodeLetter - 'a')) ^ (1 << (letter - 'a'));
                        parentPaths[node].Add((prefix, sibling));
                        if (node < sibling && (prefix == 0 || (prefix & (prefix - 1)) == 0)) ++result;

                        if ((childPaths[sibling]?.Count ?? 0) == 0) continue;

                        foreach (var (childPath, lastNodeInPath) in childPaths[sibling])
                        {
                            var path = prefix ^ childPath;
                            parentPaths[node].Add((path, lastNodeInPath));
                            if (node < lastNodeInPath && (path == 0 || (path & (path - 1)) == 0)) ++result;
                        }
                    }
                }

                if (nodeParent != -1 && (parentPaths[nodeParent]?.Count ?? 0) > 0)
                {
                    foreach (var (parentPath, lastNodeInPath) in parentPaths[nodeParent])
                    {
                        var path = parentPath ^ (1 << (parentNodeLetter - 'a'));
                        parentPaths[node].Add((path, lastNodeInPath));
                        if (node < lastNodeInPath && (path == 0 || (path & (path - 1)) == 0)) ++result;
                    }
                }

                if (graph[node] == null) return;

                foreach (var (child, _) in graph[node]) CountParentPaths(child);
            }

            void CountChildPaths(int node = 0)
            {
                if ((graph[node]?.Count ?? 0) == 0) return;

                if (childPaths[node] == null) childPaths[node] = new List<(int, int)>();

                foreach (var (child, letter) in graph[node])
                {
                    CountChildPaths(child);
                    if (childPaths[child]?.Count > 0)
                    {
                        foreach (var (childPath, lastNodeInPath) in childPaths[child])
                        {
                            var path = childPath ^ (1 << (letter - 'a'));
                            childPaths[node].Add((path, lastNodeInPath));

                            if (node < lastNodeInPath && (path == 0 || (path & (path - 1)) == 0))
                            {
                                ++result;
                            }
                        }
                    }
                    childPaths[node].Add((1 << (letter - 'a'), child));
                    ++result;
                }
            }
        }
    }
}
