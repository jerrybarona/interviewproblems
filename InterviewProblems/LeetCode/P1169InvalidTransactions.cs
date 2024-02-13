using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P1169InvalidTransactions
    {
        public void InvalidTransactionsTest()
        {
            var transactions = new string[] { "alice,20,800,mtv", "alice,50,100,beijing" };
            Console.WriteLine($"\nInput: [\"{string.Join("\",\"", transactions)}\"\nOutput: [\"{string.Join("\",\"", InvalidTransactions(transactions))}\"]");
        }

        public IList<string> InvalidTransactions(string[] transactions)
        {
            var x = new PriorityQueue<int, int>(new[] { (5,0)},Comparer<int>.Create( (a, b) => a - b));
            var result = new List<string>();
            HashSet<string>[] cities;
            Dictionary<(int time, string city), List<int>> amounts;
            foreach (var t in GetTransactions().GroupBy(u => u.name, (name, transacts) => new { name = name, transacts = transacts }))
            {
                cities = new HashSet<string>[1001];
                amounts = new Dictionary<(int time, string city), List<int>>();
                foreach (var (_, time, amount, city) in t.transacts)
                {
                    if (cities[time] == null) cities[time] = new HashSet<string>();
                    cities[time].Add(city);

                    if (!amounts.ContainsKey((time, city))) amounts.Add((time, city), new List<int>());
                    amounts[(time, city)].Add(amount);
                }

                for (var i = 0; i <= 1000; ++i)
                {
                    if (cities[i] == null || cities[i].Count == 0) continue;
                    if (cities[i].Count > 1)
                    {
                        foreach (var c in cities[i]) result.AddRange(amounts[(i, c)].Select(amount => $"{t.name},{i},{amount},{c}"));
                        continue;
                    }
                    var city = cities[i].Single();
                    var allInvalid = false;
                    for (var j = 1; j <= 60; ++j)
                    {
                        if ((i + j <= 1000 && cities[i + j] != null && (cities[i + j].Count > 1 || !cities[i + j].Contains(city))) ||
                            (i - j >= 0 && cities[i - j] != null && (cities[i - j].Count > 1 || !cities[i - j].Contains(city))))
                        {
                            allInvalid = true;
                            break;
                        }
                    }

                    result.AddRange(allInvalid
                        ? amounts[(i, city)].Select(amount => $"{t.name},{i},{amount},{city}")
                        : amounts[(i, city)].Where(a => a > 1000).Select(amount => $"{t.name},{i},{amount},{city}"));
                }
            }

            return result;

            IEnumerable<(string name, int time, int amount, string city)> GetTransactions()
            {
                foreach (var t in transactions)
                {
                    var arr = t.Split(',');
                    yield return (arr[0], GetNumber(arr[1]), GetNumber(arr[2]), arr[3]);
                }
            }

            int GetNumber(string str)
            {
                var result = 0;
                for (var i = 0; i < str.Length; ++i)
                {
                    result = 10 * result + str[i] - '0';
                }
                return result;
            }
        }
    }
}
