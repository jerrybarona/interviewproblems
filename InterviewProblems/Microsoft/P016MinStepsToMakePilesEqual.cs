using System;

namespace InterviewProblems.Microsoft
{
    public class P016MinStepsToMakePilesEqual
    {
        public int MinSteps(int[] piles)
        {
            Array.Sort(piles, (a,b) => b.CompareTo(a));

            var result = 0;
            for (var (i, count) = (1,1); i < piles.Length; ++i)
            {
                if (piles[i] != piles[i - 1]) result += count;
                ++count;
            }

            return result;
        }
    }
}
