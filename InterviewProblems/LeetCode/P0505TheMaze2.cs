using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P0505TheMaze2
    {
        public void ShortestDistanceTest()
        {
            var maze = new[] { new[] { 0, 0, 0, 0, 1 }, new[] { 0, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 0 }, new[] { 1, 1, 0, 0, 0 }, new[] { 1, 1, 0, 1, 1 } };
            var start = new[] { 0, 0 };
            var destination = new[] { 4, 2 };

            Console.WriteLine(ShortestDistance(maze, start, destination));
        }

        public int ShortestDistance(int[][] maze, int[] start, int[] destination)
        {
            var m = maze.Length;
            var n = maze[0].Length;
            var memo = new Dictionary<(int, int), int>();
            var distance = MinDist(start[0], start[1]);
            return distance >= 200000 ? -1 : distance;

            int MinDist(int r, int c)
            {

                if (r == destination[0] && c == destination[1]) return 0;
                if (memo.ContainsKey((r, c))) return memo[(r, c)];
                if (maze[r][c] == -1) return 200000;

                maze[r][c] = -1;
                var result = 200000;
                var s = 0;
                while (r + s + 1 < m && maze[r + s + 1][c] != 1) ++s;
                result = Math.Min(result, s + MinDist(r + s, c));

                s = 0;
                while (r - s - 1 >= 0 && maze[r - s - 1][c] != 1) ++s;
                result = Math.Min(result, s + MinDist(r - s, c));

                s = 0;
                while (c + s + 1 < n && maze[r][c + s + 1] != 1) ++s;
                result = Math.Min(result, s + MinDist(r, c + s));

                s = 0;
                while (c - s - 1 >= 0 && maze[r][c - s - 1] != 1) ++s;
                result = Math.Min(result, s + MinDist(r, c - s));
                maze[r][c] = 0;

                memo.Add((r, c), Math.Min(result, 200000));
                return memo[(r, c)];
            }
        }
    }
}
