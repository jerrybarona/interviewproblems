using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Oracle
{
    public class P001NodeDependencies
    {
        // https://leetcode.com/discuss/interview-question/435160/Oracle-OCI-or-Phone-or-Node-Dependencies         

        public void NodeDependenciesTest()
        {
            var input = new[] { new[] { 7, 11 }, new[] { 5, 11 }, new[] { 3, 8 }, new[] { 11, 2 }, new[] { 11, 9 }, new[] { 8, 9 }, new[] { 3, 10 } };
            Console.WriteLine(string.Join(',', NodeDependencies(input, 2)));
        }

        public List<int> NodeDependencies(int[][] input, int start)
        {

            var parents = input.Aggregate(new Dictionary<int, List<int>>(), (dict, x) =>
             {
                 if (!dict.ContainsKey(x[1])) dict.Add(x[1], new List<int>());
                 dict[x[1]].Add(x[0]);
                 return dict;
             });
            var visited = new HashSet<int>();
            var result = new Queue<int>();
            Dfs(start);
            return result.ToList();

            void Dfs(int node)
            {
                visited.Add(node);
                if (parents.ContainsKey(node))
                {
                    foreach (var child in parents[node])
                    {
                        if (!visited.Contains(child)) Dfs(child);
                    }
                }
                result.Enqueue(node);
            }
        }
    }
}
