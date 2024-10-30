using System;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    internal class P0689MaximumSumOfThreeNonOverlappingSubarrays : ITestable
    {
        public void RunTest()
        {
            foreach (var (nums, k) in new (int[], int)[]
            {
                (new [] {1,2,1,2,6,7,5,1}, 2)
            })
            {
                Console.WriteLine(string.Join(", ", MaxSumOfThreeSubarrays(nums, k)));
            }
        }

        private int[] MaxSumOfThreeSubarrays(int[] nums, int k)
        {
            var memo = Enumerable.Repeat(0, nums.Length).Select(x => Enumerable.Repeat((-1, -1, -1, -1), 4).ToArray()).ToArray();
            var sum3 = Enumerable.Range(0, nums.Length - k + 1).Aggregate(new int[nums.Length - k + 1], (total, idx) =>
            {
                if (idx == 0)
                {
                    total[idx] = nums.Take(k).Sum();
                }
                else
                {
                    total[idx] = total[idx - 1] - nums[idx - 1] + nums[idx + k - 1];
                }

                return total;
            });

            var (_, r1, r2, r3) = MaxSum(0, 3);

            return new int[] { r1, r2, r3 };

            (int, int, int, int) MaxSum(int idx, int subs)
            {
                if (subs == 0) return (0, -1, -1, -1);
                if (idx >= nums.Length - subs * k + 1) return (0, -1, -1, -1);

                if (memo[idx][subs] != (-1, -1, -1, -1)) return memo[idx][subs];

                var (sum, i1, i2, i3) = MaxSum(idx + 1, subs);

                var (nextSum, j1, j2, j3) = MaxSum(idx + k, subs - 1);
                nextSum += sum3[idx];

                if (nextSum >= sum)
                {
                    switch (subs)
                    {
                        case 3:
                            j1 = idx;
                            break;
                        case 2:
                            j2 = idx;
                            break;
                        case 1:
                            j3 = idx;
                            break;
                        default:
                            break;
                    }

                    memo[idx][subs] = (nextSum, j1, j2, j3);
                }
                else memo[idx][subs] = (sum, i1, i2, i3);

                return memo[idx][subs];
            }
        }
    }
}
