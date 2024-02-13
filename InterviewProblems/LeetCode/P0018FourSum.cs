using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0018FourSum : ITestable
    {
        public void RunTest()
        {
            foreach (var (nums, target)  in new[]
            {
                (new[] {2,2,2,2,2}, 8)
            })
            {
                Console.WriteLine($"nums = [{string.Join(",", nums)}]\ntarget = {target}\n");

                var result = FourSum(nums, target);
                Console.WriteLine($"Output = [{string.Join("\n", result.Select(a => $"[{string.Join(", ", a)}]"))}]\n");
            }
        }

        private IList<IList<int>> FourSum(int[] nums, int target)
        {
            var result = new List<IList<int>>();
            if (nums.Length < 4) return result;

            Array.Sort(nums);

            for (var i = 0; i < nums.Length - 3; ++i)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;

                foreach (var triplet in ThreeSum(i + 1, target - nums[i]))
                {
                    result.Add(triplet.Prepend(nums[i]).ToList());
                }
            }

            return result;

            IEnumerable<IEnumerable<int>> ThreeSum(int sidx, int t)
            {
                for (var i = sidx; i < nums.Length - 2; i++)
                {
                    if (i > sidx && nums[i] == nums[i - 1])
                    {
                        continue;
                    }

                    Console.WriteLine($"sidx = {sidx}, i = {i}, nums[i] = {nums[i]}, nums[i-1] = {nums[i - 1]}");

                    for (var (start, end) = (i + 1, nums.Length - 1); start < end;)
                    {
                        if (start > i + 1 && nums[start] == nums[start - 1])
                        {
                            ++start;
                            continue;
                        }

                        if (end < nums.Length - 1 && nums[end] == nums[end + 1])
                        {
                            --end;
                            continue;
                        }

                        var (canSum, total) = TrySum(nums[i], nums[start], nums[end]);
                        Console.WriteLine(total);
                        if (!canSum)
                        {
                            if (total == int.MaxValue) --end;
                            else ++start;

                            continue;
                        }
                        Console.WriteLine(total);

                        if (total == t)
                        {
                            Console.WriteLine("huh");
                            yield return new[] { nums[i], nums[start], nums[end] }.AsEnumerable();
                            ++start;
                            --end;
                            continue;
                        }

                        if (total < t) ++start;
                        else --end;
                    }
                }
            }

            (bool, int) TrySum(int x, int y, int z)
            {
                if (y > 0 && x > int.MaxValue - y) return (false, int.MaxValue);
                if (y < 0 && x < int.MinValue - y) return (false, int.MinValue);

                var sum = x + y;
                if (z > 0 && sum > int.MaxValue - z) return (false, int.MaxValue);
                if (z < 0 && sum < int.MinValue - z) return (false, int.MinValue);

                return (true, sum + z);
            }
        }
    }
}
