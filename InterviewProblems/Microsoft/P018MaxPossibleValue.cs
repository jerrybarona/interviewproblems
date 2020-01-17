using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P018MaxPossibleValue
    {
        public int MaxVal(int N, int k)
        {
            var nums = new List<int>();
            var sign = N >= 0 ? 1 : -1;
            N = Math.Abs(N);
            do
            {
                nums.Add(N % 10);
                N /= 10;
            } while (N > 0);

            nums.Reverse();
            var result = new List<int>();
            var placed = false;
            foreach (var num in nums)
            {
                if (!placed)
                {
                    if (sign == 1 && k >= num || sign == -1 && k < num)
                    {
                        result.Add(k);
                        placed = true;
                    }
                }

                result.Add(num);
            }

            if (!placed) result.Add(N);

            return sign * result.Aggregate(0, (current, num) => 10 * current + num);
        }
    }
}
