using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.TikTok
{
    internal class P010ValidMeetings : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5630998/Tiktok-or-Round-1-(Lark)-or-Valid-Meetings

        public void RunTest()
        {
            foreach (var meetings in new (int, int)[][]
            {
                new[]{ (1, 2), (2, 3), (5, 10), (2, 4) }, // [ True, True, True, False].
                new[]{ (1, 10), (2, 5), (6, 7) }, //  [True, False, False]
                new[]{ (1, 2), (2, 4), (5, 10), (4, 5), (1, 4) }, //  [True, True, True, True, False]
                new[]{ (10, 20), (5, 15)}, // True, True
            })
            {
                Console.WriteLine(string.Join(", ", ValidMeetings(meetings)));
            }
        }

        private bool[] ValidMeetings((int start, int end)[] meetings)
        {
            var len = meetings.Length;
            var result = new bool[len];
            var sl = new SortedList<int,int>(len);
            result[0] = true;
            sl.Add(meetings[0].start, meetings[0].end);

            for (var i = 1; i < len; ++i)
            {
                result[i] = VerifyAndUpdate(meetings[i].start, meetings[i].end);
            }

            return result;

            bool VerifyAndUpdate(int start, int end)
            {
                var closestStartIdx = BinSearch(start, true);
                var closestEndIdx = BinSearch(end, false);
                var closestStartKey = closestStartIdx == -1 ? -1 : sl.GetKeyAtIndex(closestStartIdx);
                var closestStartValue = closestStartIdx == -1 ? -1 : sl.GetValueAtIndex(closestStartIdx);
                var closestEndKey = closestEndIdx == -1 ? -1 : sl.GetKeyAtIndex(closestEndIdx);
                var closestEndValue = closestEndIdx == -1 ? -1 : sl.GetValueAtIndex(closestEndIdx);

                if (closestStartIdx == -1 && closestEndIdx == -1)
                {
                    sl.Add(start, end);
                    return true;
                }

                if (closestStartIdx == -1)
                {
                    while (sl.First().Key != closestEndKey)
                    {
                        sl.Remove(sl.First().Key);
                    }

                    sl.Remove(sl.First().Key);

                    sl.Add(start, Math.Max(end, closestEndValue));
                    return false;
                }

                if (closestStartIdx == closestEndIdx)
                {
                    if (start < closestEndValue)
                    {
                        sl.SetValueAtIndex(closestStartIdx, Math.Max(end, closestEndValue));
                        return false;
                    }

                    sl.Add(start, end);
                    return true;
                }
                else
                {
                    var idx = closestStartIdx;
                    var newStart = closestStartKey;
                    if (start >= closestStartValue)
                    {
                        ++idx;
                        newStart = start;
                    }

                    while (sl.GetKeyAtIndex(idx) != closestEndKey)
                    {
                        sl.RemoveAt(idx);
                    }

                    sl.RemoveAt(idx);
                    sl.Add(newStart, Math.Max(end, closestEndValue));
                    return false;
                }
            }

            int BinSearch(int num, bool isStart)
            {
                var ans = -1;
                for (var (s,e) = (0, sl.Count - 1); s <= e; )
                {
                    var m = s + (e - s) / 2;
                    bool cond = isStart ? sl.GetKeyAtIndex(m) > num : sl.GetKeyAtIndex(m) >= num;
                    if (cond) e = m - 1;
                    else
                    {
                        ans = m;
                        s = m + 1;
                    }
                }

                return ans;
            }
        }
    }
}
