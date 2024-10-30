using System;
using System.Collections.Generic;

namespace InterviewProblems.Amazon
{
    public class P014MaxPointsFromSprints : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5482816/Amazon-OA
        // https://www.reddit.com/r/leetcode/comments/1besxh0/comment/kuxotet/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button

        public void RunTest()
        {
            foreach (var (d, k) in new(int[], int)[]
            {
                (new[] { 7, 4, 3, 7, 2 }, 8), //32
            })
            {
                Console.WriteLine(MaxPoints(d, k));
            }
        }

        private int MaxPoints(int[] d, int k)
        {            
            var sw = new LinkedList<(int start, int end)>();
            int points = 0;
            int result = 0;

            // init
            var j = 0;
            if (k <= d[0])
            {
                sw.AddFirst((d[0] - k + 1, d[0]));
                points += Points(d[0] - k + 1, d[0]);
                sw.AddLast((1, 0));
                ++j;
            }
            else
            {
                sw.AddFirst((1, d[0]));
                points += Points(1, d[0]);
                ++j;
                
                for (var rem = k - d[0]; rem > 0; ++j)
                {
                    var nextEnd = Math.Min(rem, d[j]);
                    sw.AddLast((1, nextEnd));
                    points += Points(1, nextEnd);
                    rem -= nextEnd;

                    if (rem == 0)
                    {
                        if (nextEnd == d[j])
                        {
                            sw.AddLast((1, 0));
                            continue;
                        }

                        break;
                    }
                }
            }

            result = Math.Max(result, points);

            var len = d.Length;
            for (var i = 0; i < len; ++j)
            {
                j %= len;
                var rem = d[j] - sw.Last.Value.end;

                while (sw.Count > 0 && rem > 0)
                {
                    var (prevStart, prevEnd) = sw.First.Value;
                    sw.RemoveFirst();
                    var days = prevEnd - prevStart + 1;
                    var diff = Math.Min(days, rem);
                    if (diff < days)
                    {
                        var nextStart = prevStart + diff;
                        sw.AddFirst((nextStart, prevEnd));
                        points -= Points(prevStart, nextStart - 1);
                    }
                    else
                    {
                        points -= Points(prevStart, prevEnd);
                        ++i;
                    }

                    rem -= diff;
                }

                var newStart = 1 + rem;
                var newEnd = d[j];
                
                if (sw.Count > 0)
                {
                    var (oldStart, oldEnd) = sw.Last.Value;
                    sw.RemoveLast();
                    points -= Points(oldStart, oldEnd);
                    newStart = oldStart;
                }

                sw.AddLast((newStart, newEnd));
                points += Points(newStart, newEnd);
                sw.AddLast((1, 0));

                result = Math.Max(result, points);
            }

            return result;

            int Points(int s, int e)
            {
                return (e - s + 1) * (s + e) / 2;
            }
        }
    }
}
