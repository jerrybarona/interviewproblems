using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P0493ReversePairs
    {
        public void ReversePairsTest()
        {
            foreach (var testCase in new [] { new [] { -185, -154, -338, 290, 27, 408, 155, 111, -230, -266, 84, -117, 252, -31, -241, -4, -315 } })
            {
                Console.WriteLine(ReversePairs(testCase));
            }
        }

        public int ReversePairs(int[] nums)
        {
            var temp = new int[nums.Length];
            var queue = new Queue<int>();

            return Count(0, nums.Length - 1);

            int Count(int start, int end)
            {
                if (start >= end) return 0;
                var mid = (start + end) / 2;
                var ans = Count(start, mid) + Count(mid + 1, end);

                for (var (i, j) = (start, mid + 1); i <= mid; ++i)
                {
                    for (; j <= end; ++j)
                    {
                        if ((long)nums[i] > 2 * (long)nums[j])
                        {
                            ans += (end - j + 1);
                            break;
                        }
                    }
                }

                Merge(start, mid, end);
                return ans;
            }

            void Merge(int start, int mid, int end)
            {
                for (var (x, i, j) = (start, start, mid + 1); x <= end; ++x)
                {
                    if (i <= mid) queue.Enqueue(nums[i++]);

                    if (queue.Count > 0 && j <= end)
                    {
                        nums[x] = queue.Peek() >= nums[j] ? queue.Dequeue() : nums[j++];
                    }
                    else if (queue.Count > 0) nums[x] = queue.Dequeue();
                    else if (j <= end) nums[x] = nums[j++];
                }
            }
        }
    }
}
