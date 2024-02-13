using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P1610MaximumNumberOfVisiblePoints : ITestable
    {
        public void RunTest()
        {
            foreach (var (points, angle, location) in new[]
            {
                (new[] { new[] {2,1}, new[] {2,2}, new[] {3,3} }, 90, new[] {1,1}),
                (new[] { new[] {2,1}, new[] {2,2}, new[] {3,4}, new[] {1,1} }, 90, new[] {1,1}),
                (new[] { new[] {1,0}, new[] {2,1} }, 13, new[] {1,1})
            })
            {
                Console.WriteLine($"points = [{string.Join(",", points.Select(p => $"[{p[0]},{p[1]}]"))}]");
                Console.WriteLine($"angle = {angle}");
                Console.WriteLine($"location = [{location[0]},{location[1]}]");
                Console.WriteLine($"Max Visible Points = {VisiblePoints(points, angle, location)}\n");
            }
        }

        public int VisiblePoints(IList<IList<int>> points, int angle, IList<int> location)
        {
            var colocatedCount = points.Where(p => IsColocated(p)).Count();

            var angles = points
                .Where(p => !IsColocated(p))
                .Select(q => GetNormalizedAngle(q))
                .OrderBy(r => r)
                .ToArray();

            var maxVisible = 0;

            for (var i = 0; i < angles.Length; ++i)
            {
                var start = angles[i];
                var end = start + angle;
                if (end >= 360) end -= 360;

                var closestIdx = FindClosestIndex(end);
                var visible = closestIdx - i + 1;
                if (visible <= 0) visible += angles.Length;

                maxVisible = Math.Max(maxVisible, visible);

                if (maxVisible == angles.Length) break;
            }

            return maxVisible + colocatedCount;

            int FindClosestIndex(double num)
            {
                var result = -1;
                for (var (s, e, m) = (0, angles.Length-1, (angles.Length - 1)/2); s <= e; m = s + (e-s)/2)
                {
                    if (angles[m] <= num)
                    {
                        result = m;
                        s = m + 1;
                        continue;
                    }

                    e = m - 1;
                }

                return result;
            }

            bool IsColocated(IList<int> point) => point[0] == location[0] && point[1] == location[1];

            double GetNormalizedAngle(IList<int> point)
            {
                var (x, y) = (point[0] - location[0], point[1] - location[1]);

                if (x == 0) return y > 0 ? 90 : 270;

                var atan = Math.Atan2(y, x);
                return atan >= 0 ? atan * 180 / Math.PI : (atan + 2 * Math.PI) * 180 / Math.PI;
            }
        }
    }
}
