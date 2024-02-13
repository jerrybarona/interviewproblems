using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.TikTok
{
    public class P001MinimumCommonIntegerInAllSubarrays : ITestable
    {
        // https://leetcode.com/discuss/interview-question/4157292/TikTok-OA-or-Hard-question

        public void RunTest()
        {
            foreach (var data in new int[][]
            {
                new[] { 4, 3, 3, 4, 2 },
                new[] { 3, 2, 3, 1 },
                new[] { 2, 4, 2, 3, 5 },
            })
            {
                Console.WriteLine($"I: [{string.Join(", ", data)}]");
                Console.WriteLine($"O: [{string.Join(", ", MinimumInt(data))}]");
            }
        }

        public int[] MinimumInt(int[] data)
        {
            var dict = new Dictionary<int, (int, int)>();

            for (var i = 0; i < data.Length; ++i)
            {
                if (!dict.ContainsKey(data[i]))
                {
                    dict.Add(data[i], (i, i + 1));
                    continue;
                }

                var (idx, gap) = dict[data[i]];
                gap = Math.Max(gap, i - idx);
                dict[data[i]] = (i, gap);
            }

            var result = Enumerable.Repeat(-1, data.Length).ToArray();

            foreach (var item in dict)
            {
                var num = item.Key;
                var (idx, gap) = item.Value;
                gap = Math.Max(gap, data.Length - idx);

                var k = gap - 1;
                if (result[k] == -1 || num < result[k])
                {
                    result[k] = num;
                }
            }

            var min = int.MaxValue;
            var j = 0;
            for (; j < result.Length && result[j] == -1; ++j) ;

            for (; j < result.Length; ++j)
            {
                if (result[j] != -1) min = Math.Min(min, result[j]);
                result[j] = min;
            }

            return result;
        }
    }
}
