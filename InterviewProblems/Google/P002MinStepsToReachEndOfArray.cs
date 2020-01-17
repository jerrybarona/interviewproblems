using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P002MinStepsToReachEndOfArray
    {
        // https://leetcode.com/discuss/interview-question/450018/Google-or-Phone-or-Min-step-to-reach-the-end-of-array

        private static readonly int[] Steps = { -1, 1 };
        public int MinSteps(int[] arr)
        {
            var map = arr.Select((val, idx) => (val, idx)).Aggregate(new Dictionary<int, List<int>>(), (dict, x) =>
               {
                   if (!dict.ContainsKey(x.val)) dict.Add(x.val, new List<int>());
                   dict[x.val].Add(x.idx);
                   return dict;
               });

            var queue = new Queue<int>(new[] { 0 });
            var seen = new bool[arr.Length];
            seen[0] = true;
            var result = 1;
            var finish = arr.Length - 1;

            while (queue.Count > 0)
            {
                for (var count = queue.Count; count > 0; --count)
                {
                    var idx = queue.Dequeue();
                    var nextIdxs = Steps.Select(step => idx + step).Where(x => x >= 0).Concat(map[arr[idx]].Where(y => y != idx));

                    foreach (var nextIdx in nextIdxs)
                    {
                        if (nextIdx == finish) return result;
                        if (seen[nextIdx]) continue;
                        seen[nextIdx] = true;
                        queue.Enqueue(nextIdx);
                    }
                }
                ++result;
            }

            return -1;
        }
    }
}
