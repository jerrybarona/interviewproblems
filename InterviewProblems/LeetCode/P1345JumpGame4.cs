using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P1345JumpGame4 : ITestable
    {
        public void RunTest()
        {
            foreach (var arr in new int[][]
            {
                new[]{ 100, -23, -23, 404, 100, 23, 23, 23, 3, 404 }
            })
            {
                Console.WriteLine(MinJumps(arr));
            }
        }

        public int MinJumps(int[] arr)
        {
            var len = arr.Length;
            if (len == 1) return 0;

            var map = Enumerable.Range(0, len).Aggregate(new Dictionary<int, List<int>>(), (dict, idx) =>
            {
                if (!dict.ContainsKey(arr[idx])) dict.Add(arr[idx], new List<int>());
                dict[arr[idx]].Add(idx);

                return dict;
            });

            var visited = new bool[len];
            visited[0] = true;
            var result = 0;

            for (var queue = new Queue<int>(new[] { 0 }); queue.Count > 0; ++result)
            {
                for (var count = queue.Count; count > 0; --count)
                {
                    var idx = queue.Dequeue();
                    if (idx + 1 < len && !visited[idx + 1])
                    {
                        if (idx + 1 == len - 1) return result + 1;
                        visited[idx + 1] = true;
                        queue.Enqueue(idx + 1);
                    }

                    if (idx - 1 >= 0 && !visited[idx - 1])
                    {
                        //if (idx - 1 == len - 1) return result + 1;
                        visited[idx - 1] = true;
                        queue.Enqueue(idx - 1);
                    }

                    if (map.ContainsKey(arr[idx]) && map[arr[idx]].Count > 1)
                    {
                        foreach (var i in map[arr[idx]])
                        {
                            if (i == idx || visited[i]) continue;
                            if (i == len - 1) return result + 1;
                            visited[i] = true;
                            queue.Enqueue(i);
                        }

                        map.Remove(arr[idx]);
                    }
                }
            }

            return -1;
        }
    }
}
