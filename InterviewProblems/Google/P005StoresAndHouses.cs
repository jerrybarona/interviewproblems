using System;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P005StoresAndHouses
    {
        // https://leetcode.com/discuss/interview-question/352460/Google-Online-Assessment-Questions

        public int[] StoreLocations(int[] houses, int[] stores)
        {
            Array.Sort(stores);
            return houses.Select(x => stores[Closest(x)]).ToArray();

            int Closest(int target)
            {
                var result = 0;
                var minDiff = int.MaxValue;

                for (var (start, end, mid) = (0, stores.Length - 1, (stores.Length - 1)/2);
                     start <= end;
                     mid = start + (end - start)/2)
                {
                    var diff = Math.Abs(target - stores[mid]);
                    if (diff < minDiff || diff == minDiff && mid < result)
                    {
                        minDiff = diff;
                        result = mid;
                    }

                    if (stores[mid] >= target) end = mid - 1;
                    else start = mid + 1;
                }

                return result;
            }
        }
    }
}
