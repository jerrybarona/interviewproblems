using System;

namespace InterviewProblems.Amazon
{
    internal class P020NumberOfRequestsAfterServerReplacement : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5379922/Amazon-OA-oror-SDE-2-2024-(Code-Question-1)

        public void RunTest()
        {
            foreach (var (server, replaceId, newId) in new(int[], int[], int[])[]
            {
                (new [] {20, 10}, new[]{10, 20}, new[]{20, 1 }), // [40, 2]
                (new [] {3, 3}, new[]{3, 1}, new[]{1, 5 }), // [2, 10]
                (new [] {2, 5, 2}, new[]{2, 5, 3}, new[]{3, 1, 5}), // [11, 7, 11]
            })
            {
                Console.WriteLine(string.Join(", ", GetTotalRequests(server, replaceId, newId)));
            }
        }

        private int[] GetTotalRequests(int[] server, int[] replaceId, int[] newId)
        {
            var map = new int[106];
            var sum = 0;

            for (var i = 0; i < server.Length; i++)
            {
                sum += server[i];
                ++map[server[i]];
            }

            var result = new int[replaceId.Length];

            for (var i = 0; i < replaceId.Length; i++)
            {
                sum -= map[replaceId[i]] * replaceId[i];
                sum += map[replaceId[i]] * newId[i];

                map[newId[i]] += map[replaceId[i]];
                map[replaceId[i]] = 0;

                result[i] = sum;
            }

            return result;
        }
    }
}
