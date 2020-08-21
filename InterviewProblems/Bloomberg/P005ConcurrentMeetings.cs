using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Bloomberg
{
    public class P005ConcurrentMeetings
    {
        // https://leetcode.com/discuss/interview-question/679396/Bloomberg-or-Onsite-or-Concurrent-Meetings

        public void BusiestTimeTest()
        {
            var meetings = new[]
            {
                new[] { 100, 300 }, new[] { 145, 215 }, new[] { 200, 230 }, new[] { 215, 300 }, new[] { 215, 400 },
                new[] { 500, 600 }, new[] { 600, 700 }
            };
            Console.WriteLine($"[{string.Join(", ", BusiestTime(meetings))}]");
        }

        public List<(int,int)> BusiestTime(int[][] meetings)
        {
            var currTime = -1;
            var currVolume = 0;
            var maxVolume = 0;

            var result = new List<(int,int)>();

            foreach (var t in meetings
                .SelectMany(m => new[] { (time: m[0], isStart: true), (time: m[1], isStart: false) })
                .OrderBy(x => x.time).ThenBy(y => !y.isStart))
            {
                if (currTime != -1 && t.time != currTime && currVolume >= maxVolume && !t.isStart)
                {
                    if (currVolume > maxVolume)
                    {
                        maxVolume = currVolume;
                        result.Clear();
                    }
                    
                    result.Add((currTime, t.time));
                }

                currTime = t.time;
                currVolume += t.isStart ? 1 : -1;
            }

            return result;
        }
    }
}
