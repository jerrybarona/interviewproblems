using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P012FillMatrix
    {
        public void FillMatrix(int n)
        {
            //var numbers = Enumerable.Range(1, n*n).ToHashSet();
            var vals = GetMidTuple(n);
            var set = Enumerable.Range(1, n * n).Where(x => !vals.midTuple.Contains(x)).ToList();
            foreach (var val in vals.midTuple)
            {
                var combinations = Combinations(set, n-1, vals.sum - val);
                Console.WriteLine($"Value: {val}");
                Console.WriteLine("\tCombinations:");
                foreach (var combination in combinations)
                {
                    Console.WriteLine(string.Join(',', combination));
                }
                Console.Write("\n");
            }

            
            //foreach (var item in x)
            //{
            //    Console.WriteLine(string.Join(',', item));
            //}
            //Console.WriteLine(x.sum);
            //Console.WriteLine(string.Join(',', x.midTuple));
        }

        (int sum, int[] midTuple) GetMidTuple(int n)
        {
            var nsq = n * n;
            var nSqSum = (1 + nsq) * nsq / 2;
            var sum = nSqSum / n;
            var idx = (1 + nsq) / 2;
            var s = idx;
            var i = 0;
            for ( ; s + 2 * (i + 1) * idx <= sum; ++i)
            {
            }

            return (sum, Enumerable.Range(idx - i, n).ToArray());
        }

        public List<List<int>> Combinations(List<int> set, int k, int s)
        {
            var result = new List<List<int>>();
            var comb = new List<int>();
            Combine(0, 0);
            return result;

            void Combine(int idx, int curr)
            {
                if (comb.Count == k)
                {
                    if (curr == s) result.Add(new List<int>(comb));
                    return;
                }

                for (var i = idx; i < set.Count; ++i)
                {
                    comb.Add(set[i]);
                    Combine(i+1, curr + set[i]);
                    comb.RemoveAt(comb.Count - 1);
                }
            }

        }
    }
}
