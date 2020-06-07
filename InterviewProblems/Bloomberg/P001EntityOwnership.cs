using System;
using System.Collections.Generic;

namespace InterviewProblems.Bloomberg
{
    public class P001EntityOwnership
    {
        // https://leetcode.com/discuss/interview-question/625192/Bloomberg-or-Phone-or-Entity-Ownership

        public void EntityOwnershipTest()
        {
            var graph = new Dictionary<char, Dictionary<char, double>>
            {
                { 'A', new Dictionary<char, double> { { 'B', 0.5 }, { 'C', 0.33 } } },
                { 'B', new Dictionary<char, double> { { 'D', 0.5 }, { 'F', 0.3 } } },
                { 'D', new Dictionary<char, double> { { 'G', 0.1 } } },
                { 'F', new Dictionary<char, double>() },
                { 'G', new Dictionary<char, double>() },
                { 'C', new Dictionary<char, double> { { 'E', 0.1 } } },
                { 'E', new Dictionary<char, double>() }
            };

            var result = EntityOwnership(graph);
            Console.WriteLine($"Parent\t\tChild\tOwnership%");
            Console.WriteLine($"------\t\t-----\t----------");
            foreach (var (entity, dictionary) in result)
            {
                foreach (var (child, ownership) in dictionary)
                {
                    Console.WriteLine($"{entity}\t\t{child}\t{ownership:P}");
                }
            }
        }

        public Dictionary<char, Dictionary<char, double>> EntityOwnership(
            Dictionary<char, Dictionary<char, double>> graph)
        {
            var memo = new Dictionary<char,Dictionary<char,double>>();
            foreach (var key in graph.Keys)
            {
                if (!memo.ContainsKey(key)) Traverse(key);
            }

            return memo;

            Dictionary<char, double> Traverse(char entity)
            {
                if (memo.ContainsKey(entity)) return memo[entity];

                var result = new Dictionary<char, double>();
                foreach (var child in graph[entity].Keys)
                {
                    var ownership = graph[entity][child];
                    result.Add(child, ownership);

                    foreach (var (childKey, childOwnership) in Traverse(child))
                        result.Add(childKey, ownership * childOwnership);
                }

                memo.Add(entity, result);
                return result;
            }
        }
    }
}
