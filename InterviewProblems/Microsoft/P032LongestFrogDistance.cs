using System;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P032LongestFrogDistance
    {
        // https://leetcode.com/discuss/interview-question/651142/Microsoft-Online-Assesment-Question

        public void MaxFrogDistanceTest()
        {
            var blocks = new[] { 2, 6, 8, 5 };
            Console.WriteLine($"\nInput: [{string.Join(", ", blocks)}]\nOutput: {MaxFrogDistance(blocks)}");

            blocks = new[] { 1, 5, 5, 2, 6 };
            Console.WriteLine($"\nInput: [{string.Join(", ", blocks)}]\nOutput: {MaxFrogDistance(blocks)}");
        }

        /*Optimal approach O(n) time, O(1) space:
        loop through array
        keep track of the length of the decreasing sequence (dec) ending at the current index idx
        the max at each point will be dec or the previous max (inc) or the length of the longest increasing sequence ending at idx
         */
        public int MaxFrogDistance(int[] blocks)
        {
            var result = 1;
            for (var (idx, dec, inc) = (1, 1, 1); idx < blocks.Length; ++idx)
            {
                dec = blocks[idx] <= blocks[idx - 1] ? dec + 1 : 1;
                inc = Math.Max(dec, blocks[idx] >= blocks[idx - 1] ? inc + 1 : 1);

                result = Math.Max(result, inc);
            }

            return result;
        }

        public int MaxFrogDistance2(int[] blocks)
        {
            var f = new int[blocks.Length];
            var b = new int[blocks.Length];

            var start = 0;
            for (var end = 1; end < blocks.Length; ++end)
            {
                if (blocks[end] < blocks[end - 1])
                {
                    while (start < end) f[start++] = end - 1;
                }
            }

            while (start < blocks.Length) f[start++] = blocks.Length - 1;

            start = blocks.Length - 1;
            for (var end = blocks.Length-2; end >= 0; --end)
            {
                if (blocks[end] < blocks[end + 1])
                {
                    while (start > end) b[start--] = end + 1;
                }
            }

            while (start >= 0) b[start--] = 0;

            return Enumerable.Range(0, blocks.Length).Select(i => Math.Abs(f[i] - b[i]) + 1).Max();
        }
    }
}
