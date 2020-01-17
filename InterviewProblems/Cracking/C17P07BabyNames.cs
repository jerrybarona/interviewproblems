using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Cracking
{
    public class C17P07BabyNames
    {
        public Dictionary<string, int> NameCountSample { get; } = new Dictionary<string, int> { { "John", 10 }, { "Jon", 3 }, { "Davis", 2 }, { "Kari", 3 }, { "Johnny", 11 }, { "Carlton", 8 }, { "Carleton", 2 }, { "Jonathan", 9 }, { "Carrie", 5 } };
        public string[][] SynonymSample { get; } = new string[][] { new [] { "Jonathan", "John" }, new[] { "Jon", "Johnny" }, new[] { "Johnny", "John" }, new[] { "Kari", "Carrie" }, new[] { "Carleton", "Carlton" } };

        public Dictionary<string,int> GraphSln(Dictionary<string,int> nameCounts, string[][] synonyms)
        {
            var graph = synonyms.Aggregate(new Dictionary<string, HashSet<string>>(nameCounts.Count), (res, x) =>
             {
                 if (!res.ContainsKey(x[0])) res.Add(x[0], new HashSet<string>());
                 if (!res.ContainsKey(x[1])) res.Add(x[1], new HashSet<string>());
                 res[x[0]].Add(x[1]);
                 res[x[1]].Add(x[0]);
                 return res;
             });

            foreach (var key in nameCounts.Keys.Where(x => !graph.ContainsKey(x)))
            {
                graph.Add(key, new HashSet<string>());
            }


            var visited = new HashSet<string>();
            var result = new Dictionary<string, int>();
            foreach (var key in graph.Keys)
            {
                if (!visited.Contains(key)) result.Add(key, Count(key));
            }

            return result;

            int Count(string node)
            {
                visited.Add(node);
                var count = nameCounts.ContainsKey(node) ? nameCounts[node] : 0;
                foreach (var child in graph[node])
                {
                    if (!visited.Contains(child)) count += Count(child);
                }

                return count;
            }
        }

        public Dictionary<string, int> UnionFindSln(Dictionary<string, int> nameCounts, string[][] synonyms)
        {
            var parents = nameCounts.ToDictionary(x => x.Key, x => x.Key);
            var ranks = nameCounts.ToDictionary(x => x.Key, x => 0);

            foreach (var pair in synonyms)
            {
                Union(GetParent(pair[0]), GetParent(pair[1]));
            }

            return parents.Keys.ToList().Aggregate(new Dictionary<string, int>(), (res, x) =>
             {
                 var parent = GetParent(x);
                 if (!res.ContainsKey(parent)) res.Add(parent, 0);
                 res[parent] += nameCounts[x];
                 return res;
             });

            string GetParent(string node)
            {
                if (parents[node] == node) return node;
                var parent = GetParent(parents[node]);
                parents[node] = parent;
                return parent;
            }

            void Union(string node1, string node2)
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
