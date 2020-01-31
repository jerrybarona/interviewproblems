using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P028MinimumCostToMakeSticksTheSameLength
    {
        // https://leetcode.com/discuss/interview-question/380730/Microsoft-or-Onsite-or-Min-Cost-to-Make-All-Sticks-the-Same

        public void MinCostTest()
        {
            var l = new[] { 1, 2, 3, 4, 5 };
            var w = new[] { 0.15, 0.1, 0.2, 0.3, 0.25 };
            Console.WriteLine(MinCost(l,w));

            l = new[] { 1, 2, 3, 4 };
            w = new[] { 0.49, 0.01, 0.25, 0.25 };
            Console.WriteLine(MinCost(l, w));
        }

        //public double MinCost2(int[] lengths, double[] costs)
        //{


        //}

        public double MinCost(int[] lengths, double[] costs)
        {
            var map = Enumerable.Range(0, costs.Length).Aggregate(new Dictionary<int, double>(), (dict, idx) =>
            {
                if (!dict.ContainsKey(lengths[idx])) dict.Add(lengths[idx], 0);
                dict[lengths[idx]] += costs[idx];
                return dict;
            });

            var list = map.Select(item => new Node(item.Key, item.Value)).OrderBy(x => x.Len).ToArray();

            var result = 0.0;

            for (var (start, end) = (0, list.Length - 1); start < end;)
            {
                var incCost = Math.Abs(list[start + 1].Len - list[start].Len) * list[start].Cost;
                var decCost = Math.Abs(list[end - 1].Len - list[end].Len) * list[end].Cost;

                if (incCost <= decCost)
                {
                    result += incCost;
                    list[start + 1].Cost += list[start].Cost;
                    ++start;
                }
                else
                {
                    result += decCost;
                    list[end - 1].Cost += list[end].Cost;
                    --end;
                }
            }

            return result;
        }


        class Node
        {
            public int Len { get; set; }
            public double Cost { get; set; }

            public Node(int len, double cost)
            {
                Len = len;
                Cost = cost;
            }
        }

    }
}
