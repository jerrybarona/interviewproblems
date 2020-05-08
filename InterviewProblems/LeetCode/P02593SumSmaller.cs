using System;

namespace InterviewProblems.LeetCode
{
    public class P02593SumSmaller
    {
        public void ThreeSumSmallerTest()
        {
            var nums = new[] { -2, -2, 0, 1, 1, 3, 3 };
            Console.WriteLine($"[{string.Join(',', nums)}] => {ThreeSumSmaller(nums, 2)}");
        }

        public int ThreeSumSmaller(int[] nums, int target)
        {
            Array.Sort(nums);
            var result = 0;

            for (var i = 0; i < nums.Length - 2; ++i)
            {
                for (var l = i + 1; l < nums.Length - 1; ++l)
                {
                    var t = target - nums[i] - nums[l] - 1;
                    var idx = Find(l + 1, t);
                    if (idx != -1) result += idx - l;
                }
            }

            return result;

            int Find(int start, int t)
            {
                var res = -1;
                for (var (end, mid) = (nums.Length - 1, start + (nums.Length - 1 - start) / 2);
                     start <= end;
                     mid = start + (end - start) / 2)
                {
                    if (nums[mid] <= t)
                    {
                        res = mid;
                        start = mid + 1;
                    }
                    else end = mid - 1;
                }

                return res;
            }
        }

    }
}
