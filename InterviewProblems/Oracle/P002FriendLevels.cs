using System;
using System.Collections.Generic;

namespace InterviewProblems.Oracle
{
    public class P002FriendLevels
    {
        // https://leetcode.com/discuss/interview-experience/475709/Oracle-OCI-or-SDE-2-or-Pleasanton-or-Jan-2020-Reject

        public void FriendLevelsTest()
        {
            var dict = new Dictionary<string, string[]>
            {
                { "Bob", new[] { "Sandra", "Alice", "Eric" } },
                { "Sandra", new[] { "Bob", "Don" } },
                { "Alice", new[] { "Bob" } },
                { "Eric", new[] { "Bob" } },
                { "Don", new[] { "Sandra", "Tim" } },
                { "Tim", new[] { "Don" } }
            };

            var result = FriendLevels(dict, "Bob");
            for (var i = 0; i < result.Count; i++)
            {
                Console.WriteLine($"Level {i}: '{string.Join("','", result[i])}'");
            }
        }

        public List<List<string>> FriendLevels(Dictionary<string, string[]> friendMap, string person)
        {
            var seen = new HashSet<string> { person };
            var levels = new List<List<string>>();

            Dfs(0, person);

            return levels;

            void Dfs(int level, string node)
            {
                if (level == levels.Count) levels.Add(new List<string>());
                levels[level].Add(node);

                if (friendMap.ContainsKey(node))
                {
                    foreach (var child in friendMap[node])
                    {
                        if (!seen.Contains(child))
                        {
                            seen.Add(child);
                            Dfs(level + 1, child);
                        }
                    }
                }
            }
        }

    }
}
