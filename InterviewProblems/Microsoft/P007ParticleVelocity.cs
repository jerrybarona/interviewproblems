using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P007ParticleVelocity
    {
        public int ParticleVelocity(int[] arr)
        {
            var result = 0;
            for (var (end,start,diff) = (2,0,arr[1] - arr[0]); end < arr.Length; ++end)
            {
                var currDiff = arr[end] - arr[end - 1];
                if (currDiff == diff) result += end - start - 1;
                else
                {
                    diff = currDiff;
                    start = end - 1;
                }
            }

            return result;
        }
    }
}
