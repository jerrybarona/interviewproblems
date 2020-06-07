using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P029ScheduleOfTasks
    {
        // https://leetcode.com/discuss/interview-question/338948/Facebook-or-Onsite-or-Schedule-of-Tasks

        public void ScheduleOfTasksTest()
        {
            var input = new[] { new[] { 1, 10 }, new[] { 2, 6 }, new[] { 9, 12 }, new[] { 14, 16 }, new[] { 16, 17 } };
            Console.WriteLine($"Input: [{string.Join(',', input.Select(x => $"[{string.Join(',', x)}]"))}]");
            Console.WriteLine($"Output Union: [{string.Join(',', Union(input).Select(x => $"[{string.Join(',', x)}]"))}]");
            Console.WriteLine($"Output Intersection: [{string.Join(',', Intersection(input).Select(x => $"[{string.Join(',', x)}]"))}]");
        }

        public List<List<int>> Union(int[][] intervals)
        {
            var result = new List<List<int>>();
            var newInterval = new List<int>(intervals[0]);
            for (var i = 1; i < intervals.Length; ++i)
            {
                var interval = intervals[i];
                if (interval[0] <= newInterval[1]) newInterval[1] = Math.Max(newInterval[1], interval[1]);
                else
                {
                    result.Add(newInterval);
                    newInterval = new List<int>(interval);
                }
            }

            result.Add(newInterval);
            return result;
        }

        public List<List<int>> Intersection(int[][] intervals)
        {
            var result = new List<List<int>>();
            var newInterval = new int[] {-1,-1};
            var intA = intervals[0];
            for (var i = 1; i < intervals.Length; ++i)
            {
                var intB = intervals[i];
                if (intB[0] <= intA[1])
                {
                    if (newInterval[0] == -1)
                    {
                        newInterval[0] = Math.Max(intA[0], intB[0]);
                        newInterval[1] = Math.Min(intA[1], intB[1]);
                    }
                    else
                    {
                        if (intB[0] <= newInterval[1]) newInterval[1] = Math.Min(intA[1], intB[1]);
                        else
                        {
                            result.Add(new List<int>(newInterval));
                            newInterval[0] = Math.Max(intA[0], intB[0]);
                            newInterval[1] = Math.Min(intA[1], intB[1]);
                        }
                    }
                }
                else if (newInterval[0] != -1)
                {
                    result.Add(new List<int>(newInterval));
                    newInterval = new [] { -1, -1 };
                }

                intA = intB[1] >= intA[1] ? intB : intA;
            }
            if (newInterval[0] != -1) result.Add(new List<int>(newInterval));

            return result;
        }
    }
}
