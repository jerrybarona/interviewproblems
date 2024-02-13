using System;

namespace InterviewProblems.LeetCode
{
    public class P2127MaximumEmployeesToBeInvitedToAMeeting : ITestable
    {
        public void RunTest()
        {
            foreach (var f in new[]
            {
                new[] { 2, 2, 1, 2 }
            })
            {
                Console.WriteLine(MaximumInvitations(f));
            }
        }

        public int MaximumInvitations(int[] favorite)
        {
            var max = 0;

            for (var i = 0; i < favorite.Length; ++i)
            {
                if (favorite[i] >= 0) max = Math.Max(max, Count(i));
            }

            return max;

            int Count(int idx)
            {
                var slow = idx;
                var fast = idx;

                do
                {
                    if (favorite[slow] < 0) return 0;
                    MarkVisited(slow);
                    slow = Next(slow);
                    fast = Next(Next(fast));
                } while (slow != fast);

                var result = 1;
                slow = Next(slow);
                MarkVisited(slow);

                for (; slow != fast; ++result)
                {
                    slow = Next(slow);
                    MarkVisited(slow);
                }

                return result;
            }

            void MarkVisited(int idx)
            {
                if (favorite[idx] < 0) return;

                favorite[idx] = -favorite[idx] - 1;
            }

            int Next(int idx)
            {
                return favorite[idx] >= 0 ? favorite[idx] : -(favorite[idx] + 1);
            }
        }
    }
}
