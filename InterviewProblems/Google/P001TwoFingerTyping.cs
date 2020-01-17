using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P001TwoFingerTyping
    {
        public int MinimunDistance(string str)
        {
            var charMap = new (int i, int j)[]
            {
                (0,0), (0,1), (0,2), (0,3), (0,4), (0,5), (1,0), (1,1), (1,2), (1,3), (1,4), (1,5), (2,0), (2,1), (2,2), (2,3), (2,4), (2,5),(3,0), (3,1), (3,2), (3,3), (3,4), (3,5), (4,0), (4,1)
            };

            var memo = Enumerable.Repeat(0, str.Length).Select(y => Enumerable.Repeat(0, 26).Select(x => Enumerable.Repeat(-1, 26).ToArray()).ToArray()).ToArray();

            var result = MinDist(0, str[0] - 'A', str[1] - 'A');
            for (var i = 2; i < str.Length; ++i)
            {
                result = Math.Min(result, MinDist(0, str[0] - 'A', str[i] - 'A'));
            }

            return result;

            int MinDist(int idx, int i, int j)
            {
                if (idx == str.Length) return 0;
                if (memo[idx][i][j] > -1) return memo[idx][i][j];

                var currPos = charMap[str[idx] - 'A'];
                var distFromI = Dist(charMap[i], currPos);
                var distFromJ = Dist(charMap[j], currPos);

                var result = Math.Min(distFromI + MinDist(idx + 1, str[idx] - 'A', j),
                    distFromJ + MinDist(idx + 1, i, str[idx] - 'A'));

                memo[idx][i][j] = result;
                return result;
            }

            int Dist((int i, int j) pos1, (int i, int j) pos2)
            {
                return Math.Abs(pos1.i - pos2.i) + Math.Abs(pos1.j - pos2.j);
            }
        }
    }
}
