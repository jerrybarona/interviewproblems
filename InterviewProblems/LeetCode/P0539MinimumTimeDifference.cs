using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P0539MinimumTimeDifference
    {
        private const long One = 1;

        public void FindMinDifferenceTest()
        {
            foreach (var test in new[] { new[] { "23:59", "00:00" } } )
            {
                Console.WriteLine(FindMinDifference(test));
            }
        }

        public int FindMinDifference(IList<string> timePoints)
        {
            var arr = new long[24];
            var len = timePoints.Count;
            string timePoint;

            for (var i = 0; i < len; ++i)
            {
                timePoint = timePoints[i];
                var idx = (timePoint[0] - '0') * 10 + timePoint[1] - '0';
                var pos = (timePoint[3] - '0') * 10 + timePoint[4] - '0';

                if ((arr[idx] & (One << pos)) == 0)
                {
                    arr[idx] |= (One << pos);
                    continue;
                }

                return 0;
            }

            var first = -1;
            var last = -1;
            var result = 6000;
            var curr = 0;
            for (var i = 0; i < 24; ++i)
            {
                var val = arr[i];
                for (var j = 0; j < 60; ++j, ++curr)
                {
                    if ((val & (1 << j)) != 0)
                    {
                        if (first == -1)
                        {
                            first = curr;
                            last = curr;
                            continue;
                        }

                        result = Math.Min(result, curr - last);
                        last = curr;
                    }
                }
            }

            return Math.Min(result, 1440 + first - last);
        }
    }
}
