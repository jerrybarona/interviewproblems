using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0763PartitionLabels
    {
        public void PartitionLabelsTest()
        {
            var s = "ababcbacadefegdehijhklij";
            Console.WriteLine($"[{string.Join(',', PartitionLabels(s))}]");
        }

        public IList<int> PartitionLabels(string S)
        {
            var map = S.Select((chr, idx) => (chr, idx)).Aggregate(new int[26], (arr, x) =>
            {
                arr[x.chr - 'a'] = x.idx;
                return arr;
            });

            var result = new List<int>();

            for (var i = 0; i < S.Length;)
            {
                var count = 0;
                for (var n = map[S[i] - 'a']; i <= n; ++count, ++i)
                {
                    n = Math.Max(n, map[S[i] - 'a']);
                }
                result.Add(count);
            }

            return result;
        }
    }
}
