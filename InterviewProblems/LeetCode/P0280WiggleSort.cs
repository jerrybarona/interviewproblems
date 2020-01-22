using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0280WiggleSort
    {
        public void WiggleSortTest()
        {
            var input = new[] { 3, 5, 2, 1, 6, 4 };
            Console.WriteLine(string.Join(',', input));
            WiggleSort(input);
            Console.WriteLine(string.Join(',', input));
        }

        public void WiggleSort(int[] nums)
        {
            Sort(0, nums.Length - 1);

            void Sort(int start, int end)
            {
                if (start >= end) return;
                if (start + 1 == end)
                {
                    if (nums[start] > nums[end]) (nums[start], nums[end]) = (nums[end], nums[start]);
                }
                else
                {
                    var mid = start + (end - start) / 2;
                    Sort(start, mid);
                    Sort(mid + 1, end);
                    Merge(mid, mid + 1);
                }
            }

            void Merge(int e1, int s2)
            {
                if (nums[e1] < nums[s2]) (nums[e1], nums[s2]) = (nums[s2], nums[e1]);
            }
        }
    }
}
