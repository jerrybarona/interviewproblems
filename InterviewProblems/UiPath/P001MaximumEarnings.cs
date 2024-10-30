using System;
using System.Linq;

namespace InterviewProblems.UiPath
{
    internal class P001MaximumEarnings : ITestable
    {
        // https://leetcode.com/discuss/interview-question/2653888/Ui-Path-online-assessment

        public void RunTest()
        {
            foreach (var (pickup, drop, tip) in new(int[], int[], int[])[]
            {
                (new[]{ 0, 2, 9, 10, 11, 12 }, new[]{ 5, 9, 11, 11, 14, 17 }, new[]{1,2,3,2,2,1}), // 20
                (new[]{1,4}, new[]{5,6}, new[]{2,5}), // 7
            })
            {
                Console.WriteLine(TaxiDriver(pickup, drop, tip));
            }
        }

        private int TaxiDriver(int[] pickup, int[] drop, int[] tip)
        {
            var len = pickup.Length;
            var memo = Enumerable.Repeat(-1, len).ToArray();
            var arr = Enumerable.Range(0, len)
                .Select(i => (p: pickup[i], d: drop[i], t: tip[i]))
                .OrderBy(j => j.p).ToArray();

            return MaxAmount(0);

            int MaxAmount(int idx)
            {
                if (idx == len) return 0;

                if (memo[idx] != -1) return memo[idx];

                var result = 0;
                for (var i = idx; i < len; ++i)
                {
                    var next = arr[i].d - arr[i].p + arr[i].t;
                    var nextIdx = Bin(i + 1, len - 1, arr[i].d);
                    if (nextIdx != -1) next += MaxAmount(nextIdx);
                    result = Math.Max(result, next);
                }

                memo[idx] = result;

                return result;
            }

            int Bin(int s, int e, int x)
            {
                var result = -1;
                for (; s <= e; )
                {
                    var m = s + (e - s) / 2;
                    var val = arr[m].p;

                    if (val >= x)
                    {
                        result = m;
                        e = m - 1;
                    }
                    else s = m + 1;
                }

                return result;
            }
        }
    }
}
