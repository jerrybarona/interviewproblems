using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
	public class P0630CourseSchedule3
	{
        public void CourseSchedule3Test()
		{
			foreach (var testCase in
				new[]
                {
                    new []
                    {
                        new [] { 5,15 }, new [] { 3,19 }, new [] { 5,16 }, new [] { 2,19}
                    },
     //               new[]
					//{
     //                   new[] { 7, 17 }, new[] { 3, 12 }, new[] { 9, 10 }, new[] { 5, 20 }, new[] { 4, 18 }
     //               },
      //              new []
      //              {
						//new [] { 100,200},new [] { 200,1300},new [] { 1000,1250},new [] { 2000,3200}
      //              }
                })
			{
                Console.WriteLine(ScheduleCourse(testCase));
			}
		}

        public int ScheduleCourse(int[][] courses)
        {
			Array.Sort(courses, (a, b) =>
					   {
						   return a[1] == b[1] ? a[0] - b[0] : a[1] - b[1];
					   });
			var memo = new Dictionary<(int, int), int>();
            var taken = new bool[courses.Length];

            return Enumerable.Range(0, courses.Length)
                .Where(c => CanCourseBeTaken(c, 0))
                .DefaultIfEmpty(0)
                .Max(idx => MaxNum(idx, 0));

            int MaxNum(int idx, int totalDuration)
            {
                //if (idx >= courses.Length || taken[idx] || !CanCourseBeTaken(idx, totalDuration)) return 0;
                if (memo.ContainsKey((idx, totalDuration))) return memo[(idx, totalDuration)];

                var nextTotalDuration = totalDuration + courses[idx][0];
                taken[idx] = true;
                var maxNum = 0;

                for (var i = 0; i < courses.Length; ++i)
                {
                    if (i == idx || taken[i] || !CanCourseBeTaken(i, nextTotalDuration)) continue;
                    maxNum = Math.Max(maxNum, MaxNum(i, nextTotalDuration));
                }

                taken[idx] = false;
                ++maxNum;
                memo.Add((idx, totalDuration), maxNum);

                return maxNum;
            }

            bool CanCourseBeTaken(int idx, int totalDuration)
            {
                var (duration, lastDay) = (courses[idx][0], courses[idx][1]);
                return totalDuration + duration <= lastDay;
            }
        }
    }
}
