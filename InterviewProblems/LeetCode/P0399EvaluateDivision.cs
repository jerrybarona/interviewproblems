using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0399EvaluateDivision
    {
        public void CalcEquationTest()
        {
            IList<IList<string>> equations = new List<IList<string>>
            {
                new[] { "a", "b" }, new[] { "c", "d" }, new[]
                    { "e", "f" },
                new[] { "b", "f" }, new[] { "d", "f" }
            };
            var values = new double[] { 2.0, 3.0, 5.0, 7.0, 11.0 };
            IList<IList<string>> queries = new List<IList<string>>
            {
                new[] { "a", "c" }, new[] { "b", "a" }, new[]
                    { "a", "e" },
                new[] { "a", "a" }, new[] { "x", "x" }, new[] { "f", "a" }, new[] { "c", "b" }
            };

            Console.WriteLine(string.Join(',', CalcEquation(equations, values, queries)));
        }

        public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            var parentMap = new Dictionary<string, string>();
            var ratioMap = new Dictionary<string, double>();
            var rankMap = new Dictionary<string, int>();

            foreach (var equation in equations)
            {
                if (!parentMap.ContainsKey(equation[0]))
                {
                    parentMap.Add(equation[0], equation[0]);
                    ratioMap.Add(equation[0], 1);
                    rankMap.Add(equation[0], 0);
                }
                if (!parentMap.ContainsKey(equation[1]))
                {
                    parentMap.Add(equation[1], equation[1]);
                    ratioMap.Add(equation[1], 1);
                    rankMap.Add(equation[1], 0);
                }
            }

            for (var i = 0; i < equations.Count; ++i)
            {
                Union(equations[i][0], equations[i][1], values[i]);
            }

            foreach (var kvp in ratioMap) Console.WriteLine($"{kvp.Key}: {kvp.Value}");

            var result = new double[queries.Count];
            for (var i = 0; i < queries.Count; ++i)
            {
                var query = queries[i];
                if (!new[] { query[0], query[1] }.All(q => parentMap.ContainsKey(q)))
                {
                    result[i] = -1.0;
                    continue;
                }
                var (parent1, _) = GetParentRank(query[0]);
                var (parent2, _) = GetParentRank(query[1]);

                if (parent1 != parent2) result[i] = -1.0;
                else result[i] = ratioMap[query[1]] / ratioMap[query[0]];
            }

            return result;

            void Union(string node1, string node2, double ratio)
            {
                if (node1 == node2) return;
                var (parent1, rank1) = GetParentRank(node1);
                var (parent2, rank2) = GetParentRank(node2);

                if (rank1 >= rank2)
                {
                    if (rank1 == rank2) rankMap[parent1]++;
                    UpdateParent(node2, parent1, GetRatio(node1) * ratio);
                }
                else UpdateParent(node1, parent2, GetRatio(node2) / ratio);
            }

            (string, int) GetParentRank(string node)
            {
                if (parentMap[node] == node) return (node, rankMap[node]);
                return GetParentRank(parentMap[node]);
            }

            double GetRatio(string node)
            {
                if (parentMap[node] == node) return ratioMap[node];
                return ratioMap[node] * GetRatio(parentMap[node]);
            }

            void UpdateParent(string node, string parent, double ratio)
            {
                if (parentMap[node] == node) ratioMap[node] *= ratio;
                else
                {
                    var nextRatio = ratioMap[node] / ratio;
                    ratioMap[node] = ratio;
                    UpdateParent(parentMap[node], parent, nextRatio);
                }
                parentMap[node] = parent;
            }
        }
    }
}
