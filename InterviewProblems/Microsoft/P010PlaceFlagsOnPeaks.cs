using System;
using System.Collections.Generic;

namespace InterviewProblems.Microsoft
{
    public class P010PlaceFlagsOnPeaks
    {
        public int PlaceFlags(int[] arr)
        {
            var peaks = new List<int>();
            if (arr[0] > arr[1]) peaks.Add(0);
            for (var i = 1; i < arr.Length - 1; ++i)
            {
                if (arr[i] > arr[i - 1] && arr[i] > arr[i + 1]) peaks.Add(i);
            }

            if (arr[^1] > arr[^2]) peaks.Add(arr.Length - 1);

            return Place(0, -1, int.MaxValue, 0);

            int Place(int idx, int prev, int min, int k)
            {
                if (idx == peaks.Count) return 0;
                if (k > min) return int.MinValue;

                var result = Place(idx + 1, prev, min, k);
                if (prev == -1 || peaks[idx] - prev >= k+1)
                {
                    min = prev == -1 ? min : Math.Min(min, peaks[idx] - prev);
                    var nextResult = Place(idx + 1, peaks[idx], min, k + 1);
                    if (nextResult != int.MinValue) nextResult += 1;
                    result = Math.Max(result, nextResult);
                }
                return result;
            }
        }
    }
}
