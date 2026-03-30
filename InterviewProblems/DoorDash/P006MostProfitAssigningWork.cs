using System;
using System.Linq;

namespace InterviewProblems.DoorDash
{
    internal class P006MostProfitAssigningWork : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5073171/DoorDash-E4-OnSite-Reject

        public void RunTest()
        {
            var nums = Enumerable.Range(0, 5)
                .Select(x =>
                {
                    return x + 10;
                }).ToList();
            foreach (var n in nums)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine(nums.Count());
            foreach (var n in nums)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine(nums.Count());

            foreach (var (difficulty, profit, worker) in new(int[], int[], int[])[]
            {
                (new []{2,4,6,8,10}, new[]{10,20,30,40,50}, new[]{4,5,6,7}), // 100
                (new []{85,47,57}, new[]{24,66,99}, new[]{40,25,25}), // 0
                (new []{85,47,57,65}, new[]{24,66,99,101}, new[]{40,25,25,50,60,70,90,200,23}), // 468
                (new []{24,44,26,62,73}, new[]{23,21,3,95,27}, new[]{43}), // 23
            })
            {
                Console.WriteLine($"{MaxProfitAssignment(difficulty, profit, worker)}, {MaxProfitAssignment2(difficulty, profit, worker)}");
            }
        }

        private int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
        {
            var n = difficulty.Length;
            var sortedDifficulty = Enumerable.Range(0, difficulty.Length).Select(i => (diffic: difficulty[i], prof: profit[i])).OrderBy(t => t.diffic).ToArray();
            var maxProfit = new int[n];
            maxProfit[0] = sortedDifficulty[0].prof;
            for (var i = 1; i < n; ++i)
            {
                maxProfit[i] = Math.Max(maxProfit[i-1], sortedDifficulty[i].prof);
            }

            return worker.Sum(GetWorkerMaxProfit);

            int GetWorkerMaxProfit(int workerDifficulty)
            {
                var result = -1;

                for (var (s,e) = (0,n-1); s <= e; )
                {
                    var m = s + (e - s) / 2;
                    if (sortedDifficulty[m].diffic <= workerDifficulty)
                    {
                        result = m;
                        s = m + 1;
                    }
                    else e = m - 1;
                }

                return result == -1 ? 0 : maxProfit[result];
            }
        }

        private int MaxProfitAssignment2(int[] difficulty, int[] profit, int[] worker)
        {
            var n = difficulty.Length;
            var maxProfit = new int[300];

            for (var i = 0; i < n; ++i)
            {
                maxProfit[difficulty[i]] = Math.Max(maxProfit[difficulty[i]], profit[i]);
            }

            for (var i = 1; i < 300; ++i)
            {
                maxProfit[i] = Math.Max(maxProfit[i], maxProfit[i - 1]);
            }

            return worker.Sum(w => maxProfit[w]);
        }
    }
}
