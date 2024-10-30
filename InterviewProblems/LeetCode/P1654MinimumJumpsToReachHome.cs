using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P1654MinimumJumpsToReachHome : ITestable
    {
        public void RunTest()
        {
            foreach (var (forbidden, a, b, x) in new (int[], int, int, int)[]
            {
                (new [] { 8,3,16,6,12,20 }, 15, 13, 11)
            })
            {
                Console.WriteLine(MinimumJumps(forbidden, a, b, x));
            }
        }

        public int MinimumJumps(int[] forbidden, int a, int b, int x)
        {
            if (x == 0) return 0;

            var seen = new HashSet<int>(new[] { 0 });
            var fb = new HashSet<int>(forbidden);
            var result = 0;

            for (var queue = new Queue<(int, bool)>(new[] { (0, true) }); queue.Count > 0; ++result)
            {
                for (var count = queue.Count; count > 0; --count)
                {
                    var (pos, wasLastJumpForward) = queue.Dequeue();
                    var fw = pos + a;
                    var bw = pos - b;

                    if (fw == x || (wasLastJumpForward && bw == x)) return result + 1;

                    if (pos < x)
                    {
                        if (fw <= x + 2 * a && !fb.Contains(fw) && seen.Add(fw)) queue.Enqueue((fw, true));
                        if (wasLastJumpForward && bw >= 0 && !fb.Contains(bw) && seen.Add(bw)) queue.Enqueue((bw, false));
                    }
                    else
                    {
                        if (wasLastJumpForward && bw >= 0 && !fb.Contains(bw) && seen.Add(bw)) queue.Enqueue((bw, false));
                        if (fw <= x + 2 * a && !fb.Contains(fw) && seen.Add(fw)) queue.Enqueue((fw, true));
                    }
                }
            }

            return -1;
        }
    }
}
