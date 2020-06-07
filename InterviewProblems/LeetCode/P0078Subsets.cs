using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0078Subsets
    {
        public void SubsetsTest()
        {
            var nums = new[] { 1, 2, 3 };
            Console.WriteLine($"{string.Join('\n', Subsets(nums).Select(s => $"[{string.Join(',', s)}]"))}");
        }

        public IList<IList<int>> Subsets(int[] nums)
        {
            var result = new List<IList<int>>();
            var subset = new List<int>();
            Combine();

            return result;

            void Combine(int idx = 0)
            {
                result.Add(new List<int>(subset));
                for (var i = idx; i < nums.Length; ++i)
                {
                    subset.Add(nums[i]);
                    Combine(i + 1);
                    subset.RemoveAt(subset.Count - 1);
                }
            }
        }
    }
}
