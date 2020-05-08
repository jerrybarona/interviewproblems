using System;

namespace InterviewProblems.LeetCode
{
    public class P0912SortAnArray
    {
        public void SortArrayTest()
        {
            var nums = new[] { 5, 2, 3, 1 };
            Console.WriteLine($"[{string.Join(',', nums)}] => [{string.Join(',', SortArray(nums))}]");
        }

        public int[] SortArray(int[] nums)
        {
            QuickSort(0, nums.Length - 1);

            return nums;

            void QuickSort(int start, int end)
            {
                if (start >= end) return;
                var pivot = nums[start];
                var p = start + 1;
                for (var i = start + 1; i <= end; ++i)
                {
                    if (nums[i] <= pivot)
                    {
                        (nums[i], nums[p]) = (nums[p], nums[i]);
                        ++p;
                    }
                }
                --p;
                (nums[start], nums[p]) = (nums[p], nums[start]);

                QuickSort(start, p - 1);
                QuickSort(p + 1, end);
            }
        }
    }
}
