using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P012RainWaterReachingGround
    {
        private static readonly (int r, int c)[] _steps = { (1, -1), (1, 0), (1, 1), (0, -1), (0, 1) };

        public bool RainWaterReachingGround(int[][] wall)
        {
            var m = wall.Length;
            var n = wall[0].Length;
            return Enumerable.Range(0, n).Any(c => CanReach(0, c));

            bool CanReach(int r, int c)
            {
                if (c < 0 || c >= n || wall[r][c] == 0) return false;
                if (r == m - 1) return true;

                wall[r][c] = 0;
                var reached = _steps.Any(step => CanReach(r + step.r, c + step.c));
                wall[r][c] = reached ? 8 : 1;
                return reached;
            }
        }
    }
}
