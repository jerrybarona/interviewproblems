using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P016FindAssignmentOfBreadSlice
    {
        // https://leetcode.com/discuss/interview-question/480256/Google-or-Onsite-or-Find-assignment-of-bread-slice

        public void FindAssignmentOfBreadSliceTest()
        {
            var edges = new[] { new[] { 0, 2 }, new[] { 2, 1 }, new[] { 0, 3 } };
            var result = FindAssignmentOfBreadSlice(edges, 2);
            Console.WriteLine(string.Join(',', result.Select((val,idx) => $"({(char)('A' + idx)}, {val})")));
        }

        public int[] FindAssignmentOfBreadSlice(int[][] edges, int k)
        {
            var n = 1 << k;
            var fanout = new int[n];
            var children = Enumerable.Repeat(0, n).Select(x => new List<int>()).ToArray();
            var parent = Enumerable.Repeat(-1, n).ToArray();

            foreach (var edge in edges)
            {
                children[edge[0]].Add(edge[1]);
                parent[edge[1]] = edge[0];
            }

            foreach (var person in Enumerable.Range(0, n)) UpdateFanout(person);

            var result = new int[n];
            DistributeBread(0, 0, n-1);

            return result;

            void UpdateFanout(int child)
            {
                if (child == -1) return;
                fanout[child]++;
                UpdateFanout(parent[child]);
            }

            void DistributeBread(int person, int start, int end)
            {
                if (start != end)
                {
                    foreach (var child in children[person].Select(x => (val: x, fanout: fanout[x]))
                        .OrderByDescending(y => y.fanout).Select(z => z.val))
                    {
                        var mid = start + (end - start) / 2;
                        DistributeBread(child, start,mid);
                        start = mid + 1;
                    }
                }
                result[person] = end;
            }
        }
    }
}
