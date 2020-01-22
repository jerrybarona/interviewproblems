using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0554BrickWall
    {
        public void LeastBricksTest()
        {
            var input = new[] { new[] { 6 }, new[] { 6 }, new[] { 2, 4 }, new[] { 6 }, new[] { 1, 2, 2, 1 }, new[] { 6 }, new[] { 2, 1, 2, 1 }, new[] { 1, 5 }, new[] { 4, 1, 1 }, new[] { 1, 4, 1 }, new[] { 4, 2 }, new[] { 3, 3 }, new[] { 2, 2, 2 }, new[] { 5, 1 }, new[] { 5, 1 }, new[] { 6 }, new[] { 4, 2 }, new[] { 1, 5 }, new[] { 2, 3, 1 }, new[] { 4, 2 }, new[] { 1, 1, 4 }, new[] { 1, 3, 1, 1 }, new[] { 2, 3, 1 }, new[] { 3, 3 }, new[] { 3, 1, 1, 1 }, new[] { 3, 2, 1 }, new[] { 6 }, new[] { 3, 2, 1 }, new[] { 1, 5 }, new[] { 1, 4, 1 } };
            Console.WriteLine(LeastBricks(input));
        }

        public int LeastBricks(IList<IList<int>> wall)
        {
            var edges = new Dictionary<int, int>();
            for (var r = 0; r < wall.Count; ++r) CountCrossedBricks(r);

            return wall.Count - (edges.Count > 0 ? edges.Values.Max() : 0);

            void CountCrossedBricks(int row)
            {
                var curr = 0;
                for (var i = 0; i < wall[row].Count - 1; ++i)
                {
                    curr += wall[row][i];
                    if (!edges.ContainsKey(curr)) edges.Add(curr, 0);
                    ++edges[curr];
                }
            }
        }
    }
}
