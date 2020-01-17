using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0354RussianDollEnvelopes
    {
        public int MaxEnvelopes(int[][] envelopes)
        {
            Array.Sort(envelopes, (a, b) =>
            {
                var diff = b[0].CompareTo(a[0]);
                return diff != 0 ? diff : b[1].CompareTo(a[1]);
            });

            var memo = new Dictionary<(int, int, int), int>();
            return Count(0, int.MaxValue, int.MaxValue);

            int Count(int idx, int w, int h)
            {
                if (idx == envelopes.Length) return 0;
                if (memo.ContainsKey((idx, w, h))) return memo[(idx, w, h)];

                var result = Count(idx + 1, w, h);
                if (envelopes[idx][0] < w && envelopes[idx][1] < h) result = Math.Max(result, 1 + Count(idx + 1, envelopes[idx][0], envelopes[idx][1]));

                memo.Add((idx, w, h), result);
                return result;
            }
        }
    }
}
