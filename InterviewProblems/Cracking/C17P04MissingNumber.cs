using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Cracking
{
    public class C17P04MissingNumber
    {
        private static int Fetch(int num, int pos) => (num >> pos) & 1;

        public int FindMissing(int[] nums)
        {
            var result = 0;
            Find(nums.ToList(), 0);
            return result;

            void Find(List<int> numbers , int idx)
            {
                if (idx == 32) return;
                var counts = numbers.Aggregate((zeros: 0, ones: 0), (res, x) =>
                {
                    var f = Fetch(x, idx);
                    if (f == 0) res.zeros++;
                    else res.ones++;
                    return res;
                });

                var bit = counts.zeros <= counts.ones ? 0 : 1;
                result = (bit << idx) | result;
                Find(numbers.Where(x => Fetch(x, idx) == bit).ToList(), idx + 1);
            }
        }
    }
}
