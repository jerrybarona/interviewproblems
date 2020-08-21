using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0042TrappingRainWater
    {
        public void TrapTest()
        {
            var height = new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            Console.WriteLine(Trap(height));
        }

        public int Trap(int[] height)
        {
            if (height.Length == 0) return 0;
            var (l, r) = (0, height.Length - 1);
            var (lmax, rmax) = (height[l], height[r]);
            var result = 0;
            while (l < r)
            {
                if (height[l] < height[r])
                {
                    result += Math.Max(0, Math.Min(lmax, rmax) - height[l]);
                    lmax = Math.Max(lmax, height[++l]);
                }
                else
                {
                    result += Math.Max(0, Math.Min(lmax, rmax) - height[r]);
                    rmax = Math.Max(rmax, height[--r]);
                }
            }

            return result;
        }
    }
}
