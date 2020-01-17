using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0207CourseSchedule
    {
        enum Status { Unvisited, Visiting, Visited };

        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            var x = (pac : (bool?)null, atl : (bool?)null);
            var graph = prerequisites.Aggregate(new HashSet<int>[numCourses],
            (nodes, x) =>
            {                
                var course = x[0];
                var prereq = x[1];
                if (nodes[prereq] == null) nodes[prereq] = new HashSet<int>();
                nodes[prereq].Add(course);
                return nodes;
            });

            var state = Enumerable.Repeat(Status.Unvisited, numCourses).ToArray();

            for (var n = 0; n < numCourses; ++n)
            {
                if (state[n] == Status.Unvisited && FoundCycle(n)) return false;
            }

            return true;

            bool FoundCycle(int node)
            {
                state[node] = Status.Visiting;
                var children = graph[node];
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        if (state[child] == Status.Visiting ||
                            (state[child] == Status.Unvisited && FoundCycle(child))) return true;
                    }
                }
                
                state[node] = Status.Visited;
                return false;
            }
        }
    }
}
