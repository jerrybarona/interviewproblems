using System;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P032FindMostFrequentElementInAllIntervals
    {
        // https://leetcode.com/discuss/interview-question/668027/Facebook-or-Onsite-or-Find-the-most-frequent-element-in-all-intervals

        public void MostFrequentElementInIntervalTest()
        {
            var intervals = new [] { new[] { 1, 4 }, new[] { 3, 5 }, new[] { 4, 6 } };
            Console.WriteLine($"\nInput: [{string.Join(", ", intervals.Select(i => $"[{string.Join(',', i)}]"))}]");
            Console.WriteLine($"Output: {MostFrequentElementInInterval(intervals)}");

            intervals = new[] { new[] { 0,2 }, new[] { 3, 5 }};
            Console.WriteLine($"\nInput: [{string.Join(", ", intervals.Select(i => $"[{string.Join(',', i)}]"))}]");
            Console.WriteLine($"Output: {MostFrequentElementInInterval(intervals)}");

            intervals = new[] { new[] { 1, 2 }, new[] { 4, 5 }, new[] { -2, 0 } };
            Console.WriteLine($"\nInput: [{string.Join(", ", intervals.Select(i => $"[{string.Join(',', i)}]"))}]");
            Console.WriteLine($"Output: {MostFrequentElementInInterval(intervals)}");

            intervals = new[] { new[] { 1, 6 }, new[] { 2, 3 }, new[] { 2, 5 }, new []{3,8} };
            Console.WriteLine($"\nInput: [{string.Join(", ", intervals.Select(i => $"[{string.Join(',', i)}]"))}]");
            Console.WriteLine($"Output: {MostFrequentElementInInterval(intervals)}");

            intervals = new[] { new[] { 1, 5 }, new[] { 3, 5 } };
            Console.WriteLine($"\nInput: [{string.Join(", ", intervals.Select(i => $"[{string.Join(',', i)}]"))}]");
            Console.WriteLine($"Output: {MostFrequentElementInInterval(intervals)}");
        }

        /// <summary>
        /// 1. Create tuples for every interval start and end,
        ///     Like [[1,4],[3,5],[4,6]] => Tuples: [1, "start"], [4, "end"], [3, "start"], [5, "end"], etc...
        /// 2. Sort the tuples by time, then by "start" before "end"
        /// 3. Sum and subtract depending on "start" or "end" status
        /// 4. Keep track of the total and the number at which the maximum total occurs
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public int MostFrequentElementInInterval(int[][] intervals)
        {
            var tuples = intervals
                .SelectMany(interval => new[] { (time: interval[0], stat: "begin"), (time: interval[1], stat: "end") })
                .OrderBy(i => i.time).ThenBy(j => j.stat);

            var maxTotal = 0;
            var maxNum = 0;
            var total = 0;
            foreach (var tuple in tuples)
            {
                total += tuple.stat == "begin" ? 1 : -1;
                if (total > maxTotal)
                {
                    maxTotal = total;
                    maxNum = tuple.time;
                }
            }

            return maxNum;
        }
    }
}
