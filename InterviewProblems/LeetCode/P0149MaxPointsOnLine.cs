using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0149MaxPointsOnLine
    {
        public void MaxPointsTest()
        {
            var points = new[] { new[] { 3, 10 }, new[] { 0, 2 }, new[] { 0, 2 }, new[] { 3, 10 } };
            Console.WriteLine($"\nPoints: [{string.Join(',', points.Select(p => $"[{p[0]},{p[1]}]"))}]");
            Console.WriteLine($"Output: {MaxPoints(points)}");

            points = new[] { new[] { 3, 1 }, new[] { 12, 3 }, new[] { 3, 1 }, new[] { -6, -1 } };
            Console.WriteLine($"\nPoints: [{string.Join(',', points.Select(p => $"[{p[0]},{p[1]}]"))}]");
            Console.WriteLine($"Output: {MaxPoints(points)}");

            points = new[] { new[] { 4, 0 }, new[] { 4, -1 }, new[] { 4, 5 } };
            Console.WriteLine($"\nPoints: [{string.Join(',', points.Select(p => $"[{p[0]},{p[1]}]"))}]");
            Console.WriteLine($"Output: {MaxPoints(points)}");

            points = new[] { new[] { 0, 0 }, new[] { 1, 1 }, new[] { 0, 0 } };
            Console.WriteLine($"\nPoints: [{string.Join(',', points.Select(p => $"[{p[0]},{p[1]}]"))}]");
            Console.WriteLine($"Output: {MaxPoints(points)}");

            points = new[] { new[] { 84, 250 }, new[] { 42, 90 }, new[] { -42, -230 } };
            Console.WriteLine($"\nPoints: [{string.Join(',', points.Select(p => $"[{p[0]},{p[1]}]"))}]");
            Console.WriteLine($"Output: {MaxPoints(points)}");

            points = new[]
            {
                new[] { 84, 250 }, new[] { 0, 0 }, new[] { 1, 0 }, new[] { 0, -70 }, new[] { 0, -70 }, new[] { 1, -1 },
                new[] { 21, 10 }, new[] { 42, 90 }, new[] { -42, -230 }
            };
            Console.WriteLine($"\nPoints: [{string.Join(',', points.Select(p => $"[{p[0]},{p[1]}]"))}]");
            Console.WriteLine($"Output: {MaxPoints(points)}");
        }


        private const double Eps = 0.01;
        private const int Ips = 100000;

        public int MaxPoints(int[][] points)
        {
            if (points.Length <= 2) return points.Length;
            var map = new Dictionary<(double, double), int>();

            for (var i = 0; i < points.Length - 1; ++i)
            {
                var point1 = points[i];
                for (var j = i + 1; j < points.Length; ++j)
                {
                    var point2 = points[j];
                    var lineKey = GetKey(point1, point2);
                    if (!map.ContainsKey(lineKey)) map.Add(lineKey, 0);
                }
            }

            

            foreach (var point in points)
            {
                foreach (var line in map.Keys.Where(k => Belongs(k, point)).ToList()) ++map[line];
            }

            //Console.WriteLine(string.Join(',', map.Select(i => $"{i.Key} => {i.Value}")));

            return map.Values.Max();

            (double, double) GetKey(int[] p1, int[] p2)
            {
                var (x1, y1, x2, y2) = (p1[0], p1[1], p2[0], p2[1]);
                if (x1 == x2) return (double.PositiveInfinity, x1);
                
                var slope = (double)(y2 - y1) / (x2 - x1);
                var interc = y2 - slope * x2;

                return (Floor(slope), Floor(interc));
            }

            double Floor(double n) => Math.Truncate(n * Ips) / Ips;

            bool Belongs((double, double) line, int[] point)
            {
                var (slope, interc) = line;
                return double.IsPositiveInfinity(slope)
                    ? Math.Abs(point[0] - interc) <= Eps
                    : Math.Abs(slope * point[0] + interc - point[1]) <= Eps;
            }
        }
    }
}
