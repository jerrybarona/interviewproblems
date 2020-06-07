using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P0871MinimumNumberOfRefuelingStops
    {
        public void MinRefuelingStopsTest()
        {
            var t = 1000;
            var f = 36;
            var s = new[]
            {
                new[] { 2, 76 }, new[] { 25, 160 }, new[] { 32, 161 }, new[] { 36, 161 }, new[] { 45, 128 },
                new[] { 55, 198 }, new[] { 62, 94 }, new[] { 64, 128 }, new[] { 73, 169 }, new[] { 76, 154 },
                new[] { 88, 219 }, new[] { 91, 88 }, new[] { 97, 69 }, new[] { 103, 121 }, new[] { 104, 17 },
                new[] { 109, 64 }, new[] { 110, 7 }, new[] { 112, 44 }, new[] { 117, 29 }, new[] { 121, 218 },
                new[] { 136, 40 }, new[] { 154, 136 }, new[] { 159, 203 }, new[] { 179, 61 }, new[] { 180, 196 },
                new[] { 191, 48 }, new[] { 195, 180 }, new[] { 196, 17 }, new[] { 226, 90 }, new[] { 236, 219 },
                new[] { 237, 95 }, new[] { 239, 19 }, new[] { 241, 88 }, new[] { 275, 133 }, new[] { 276, 85 },
                new[] { 292, 38 }, new[] { 310, 98 }, new[] { 316, 229 }, new[] { 327, 228 }, new[] { 337, 156 },
                new[] { 339, 207 }, new[] { 348, 45 }, new[] { 369, 19 }, new[] { 374, 142 }, new[] { 376, 19 },
                new[] { 382, 164 }, new[] { 384, 33 }, new[] { 392, 159 }, new[] { 433, 84 }, new[] { 436, 135 },
                new[] { 454, 96 }, new[] { 455, 197 }, new[] { 460, 209 }, new[] { 491, 212 }, new[] { 495, 128 },
                new[] { 513, 119 }, new[] { 535, 44 }, new[] { 543, 189 }, new[] { 551, 22 }, new[] { 571, 19 },
                new[] { 582, 6 }, new[] { 584, 53 }, new[] { 598, 141 }, new[] { 603, 93 }, new[] { 605, 4 },
                new[] { 606, 245 }, new[] { 619, 142 }, new[] { 625, 168 }, new[] { 649, 217 }, new[] { 663, 32 },
                new[] { 668, 44 }, new[] { 685, 159 }, new[] { 690, 73 }, new[] { 692, 197 }, new[] { 707, 247 },
                new[] { 711, 14 }, new[] { 713, 103 }, new[] { 729, 142 }, new[] { 735, 243 }, new[] { 764, 249 },
                new[] { 790, 28 }, new[] { 797, 56 }, new[] { 813, 69 }, new[] { 833, 100 }, new[] { 839, 67 },
                new[] { 842, 51 }, new[] { 848, 106 }, new[] { 859, 44 }, new[] { 860, 39 }, new[] { 889, 156 },
                new[] { 901, 124 }, new[] { 903, 160 }, new[] { 916, 21 }, new[] { 921, 114 }, new[] { 925, 121 },
                new[] { 930, 170 }, new[] { 942, 39 }, new[] { 950, 110 }, new[] { 962, 135 }, new[] { 966, 249 }
            };

            Console.WriteLine(MinRefuelStops2(t, f, s));
        }

        public int MinRefuelStops(int target, int startFuel, int[][] stations)
        {
            var memo = new Dictionary<(int, long), int>();

            return MinStops(-1, startFuel);

            int MinStops(int idx, long fuel)
            {
                var pos = idx == -1 ? 0 : stations[idx][0];
                if (fuel >= target - pos) return 0;
                if (memo.ContainsKey((idx, fuel))) return memo[(idx, fuel)];

                var result = 2 * stations.Length;
                for (var i = idx + 1; i < stations.Length; ++i)
                {
                    if (fuel >= stations[i][0] - pos)
                    {
                        var next = MinStops(i, fuel - (stations[i][0] - pos) + stations[i][1]);
                        if (next != -1) result = Math.Min(result, 1 + next);
                    }
                }

                result = result == 2 * stations.Length ? -1 : result;
                memo.Add((idx, fuel), result);
                return result;
            }
        }

        public int MinRefuelStops2(int target, int startFuel, int[][] stations)
        {
            var n = stations.Length;
            var maxLen = new long[n + 1];
            maxLen[0] = startFuel;

            Traverse(startFuel);

            for (var i = 0; i < maxLen.Length; ++i)
            {
                if (maxLen[i] >= target) return i;
            }

            return -1;

            void Traverse(long len, int idx = 0, int stops = 1)
            {
                if (stops >= maxLen.Length) return;
                long max = 0;
                for (var i = idx; i < n; ++i)
                {
                    if (len >= stations[i][0])
                    {
                        max = Math.Max(max, len + stations[i][1]);
                        Traverse(len + stations[i][1], i + 1, stops + 1);
                    }
                    else break;
                }
                maxLen[stops] = Math.Max(maxLen[stops], max);
            }
        }
    }
}
