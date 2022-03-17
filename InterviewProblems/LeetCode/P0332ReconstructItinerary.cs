using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0332ReconstructItinerary
    {
        public void FindItineraryTest()
        {
            foreach (var testCase in new[] { new[] { new[] { "JFK", "KUL" }, new[] { "JFK", "NRT" }, new[] { "NRT", "JFK" } } })
            {
                Console.WriteLine($"['{string.Join("', '", FindItinerary(testCase))}']");
            }
        }

        public IList<string> FindItinerary(IList<IList<string>> tickets)
        {
            var graph = tickets.Aggregate(new Dictionary<string, LinkedList<string>>(), (dict, ticket) =>
            {
                if (!dict.ContainsKey(ticket[0])) dict.Add(ticket[0], new LinkedList<string>());
                dict[ticket[0]].AddLast(ticket[1]);

                return dict;
            });

            var count = tickets.Count + 1;

            foreach (var key in graph.Keys)
            {
                graph[key] = new LinkedList<string>(graph[key].OrderBy(x => x));
            }

            var result = new List<string>(new[] { "JFK" });

            Dfs();

            return result;

            void Dfs(string key = "JFK")
            {
                if (result.Count == count || !graph.ContainsKey(key) || graph[key].Count == 0) return;

                for (var node = graph[key].First; node != null;)
                {
                    var next = node.Next;

                    result.Add(node.Value);
                    graph[key].Remove(node);

                    Dfs(node.Value);
                    if (result.Count == count) return;

                    if (next == null) graph[key].AddLast(node);
                    else graph[key].AddBefore(next, node);

                    result.RemoveAt(result.Count - 1);

                    node = next;
                }
            }
        }

    }
}
