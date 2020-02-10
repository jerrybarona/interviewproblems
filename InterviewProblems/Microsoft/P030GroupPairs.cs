using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P030GroupPairs
    {
        // https://leetcode.com/discuss/interview-question/354150/Microsoft-or-Onsite-or-Group-Pairs

        public void GroupPairsTest()
        {
            var pairs = new[] { new[] { 1, 3 }, new[] { 7, 10 }, new[] { 5, 7 }, new[] { 4, 1 } };
            foreach (var l in GroupPairs(pairs))
            {
                Console.WriteLine($"[{string.Join(',', l)}]");
            }
            Console.WriteLine("\n");
            var pairs2 = new[] { new[] { 1, 2 }, new[] { 3,4 }, new[] { 4,5 }, new[] { 5,6 }, new[] { 6,7 }, new[] { 2,5 } };
            foreach (var l in GroupPairs(pairs2))
            {
                Console.WriteLine($"[{string.Join(',', l)}]");
            }
        }

        public List<List<int>> GroupPairs(int[][] pairs)
        {
            var parents = new Dictionary<int,int>();
            var ranks = new Dictionary<int, int>();

            foreach (var pair in pairs)
            {
                if (!parents.ContainsKey(pair[0]))
                {
                    parents.Add(pair[0], pair[0]);
                    ranks.Add(pair[0], 0);
                }

                if (!parents.ContainsKey(pair[1]))
                {
                    parents.Add(pair[1], pair[1]);
                    ranks.Add(pair[1], 0);
                }
                Union(GetParent(pair[0]), GetParent(pair[1]));
            }

            foreach (var node in parents.Keys.ToList())
            {
                GetParent(node);
            }

            return parents.GroupBy(item => item.Value, (parent, items) => items.Select(x => x.Key).ToList()).ToList();

            void Union(int n1, int n2)
            {
                if (n1 != n2)
                {
                    if (ranks[n1] >= ranks[n2])
                    {
                        if (ranks[n1] == ranks[n2]) ++ranks[n1];
                        parents[n2] = n1;
                    }
                    else parents[n1] = n2;
                }
            }

            int GetParent(int node)
            {
                if (parents[node] == node) return node;
                parents[node] = GetParent(parents[node]);
                return parents[node];
            }
        }
    }
}
