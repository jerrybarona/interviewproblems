using System;
using System.Collections.Generic;

namespace InterviewProblems.Netflix
{
    public class P003SubarraysWithKDifferentIntegers : ITestable
    {
        // https://leetcode.com/discuss/interview-question/1703502/Netflix-or-Internship-Online-Assessment-or-No-of-subarrays-with-at-least-K-distinct-integers
        // https://leetcode.com/problems/subarrays-with-k-different-integers/description/
        public void RunTest()
        {
            foreach (var (nums, k) in new(int[], int)[]
            {
                (new [] {1,2,1,2,3}, 2),
                (new [] {1,2,1,3,4}, 3),
            })
            {
                Console.WriteLine(SubarraysWithKDistinct(nums, k));
            }
        }

        public int SubarraysWithKDistinct(int[] nums, int k)
        {
            var len = nums.Length;
            var map = new Dictionary<int, int>();
            var result = 0;

            for (var (s,e,i) = (0,0,0); e < len; ++e)
            {
                if (!map.ContainsKey(nums[e]))
                {
                    map[nums[e]] = 0;
                }

                ++map[nums[e]];

                if (map.Count < k) continue;

                while (map.Count > k)
                {
                    --map[nums[s]];
                    if (map[nums[s]] == 0)
                    {
                        map.Remove(nums[s]);
                    }

                    i = ++s;
                }

                while (map[nums[s]] > 1)
                {
                    --map[nums[s++]];
                }

                result += s - i + 1;
            }

            return result;
        }
    }
}
