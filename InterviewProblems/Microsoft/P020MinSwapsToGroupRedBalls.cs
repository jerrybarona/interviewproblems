using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P020MinSwapsToGroupRedBalls
    {
        public int MinSwaps(string str)
        {
            var numR = str.Count(b => b == 'R');
            var countR = 0;
            
            for (var i = 0; i < numR; ++i) countR += str[i] == 'R' ? 1 : 0;

            var idx = 0;
            for (var (start, end, curr) = (0, numR, countR); end < str.Length; ++end,++start)
            {
                if (str[end] == 'R') ++curr;
                if (str[start] == 'R') --curr;

                if (curr > countR)
                {
                    countR = curr;
                    idx = start + 1;
                }
            }

            var slots = new Queue<int>();
            for (var i = 0; i < numR; ++i)
            {
                if (str[idx + i] == 'W') slots.Enqueue(idx + i);
            }

            var result = 0;
            for (var i = 0; i < str.Length; ++i)
            {
                if (i >= idx && i < idx + numR) continue;
                if (str[i] == 'R') result += Math.Abs(i - slots.Dequeue());
            }

            return result;
        }
    }
}
