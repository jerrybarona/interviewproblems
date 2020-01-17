using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0675CutOffTreesForGolfEvent
    {
        private static readonly (int r, int c)[] _steps = { (1, 0), (0, 1), (-1, 0), (0, -1) };
        public int CutOffTree(IList<IList<int>> forest)
        {
            if (forest.Count == 0 || forest[0].Count == 0) return 0;
            if (forest[0][0] == 0) return -1;

            var m = forest.Count;
            var n = forest[0].Count;

            var trees = forest.SelectMany((row, ridx) => row.Select((val, cidx) => new { val, ridx, cidx }))
                .Where(x => x.val > 1)
                .OrderBy(y => y.val)
                .Select(z => (r: z.ridx, c: z.cidx));

            var totalSteps = 0;
            var prevTree = (r: 0, c: 0);
            foreach (var tree in trees)
            {
                var steps = Steps(prevTree.r, prevTree.c, tree.r, tree.c);
                if (steps == -1) return -1;
                totalSteps += steps;
                prevTree = tree;
            }

            return totalSteps;

            int Steps(int sr, int sc, int tr, int tc)
            {
                if (sr == tr && sc == tc) return 0;
                
                var queue = new Queue<(int r, int c)>(new [] { (sr,sc) });
                var visited = new HashSet<(int r, int c)>(new[] { (sr, sc) });

                for (var result = 1 ; queue.Count > 0; ++result)
                {
                    for (var count = queue.Count; count > 0; --count)
                    {
                        var location = queue.Dequeue();
                        foreach (var nextLoc in _steps.Select(x => (r: x.r + location.r, c: x.c + location.c)))
                        {
                            if (nextLoc.r < 0 || nextLoc.c < 0 || nextLoc.r >= m || nextLoc.c >= n ||
                                forest[nextLoc.r][nextLoc.c] == 0 || visited.Contains(nextLoc)) continue;

                            if (nextLoc.r == tr && nextLoc.c == tc) return result;
                            visited.Add(nextLoc);
                            queue.Enqueue(nextLoc);
                        }
                    }
                }
                return -1;
            }
        }
    }
}
