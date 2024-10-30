using System;

namespace InterviewProblems.Amazon
{
    internal class P018MaximumConsecutiveOnServers : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5443769/Amazon-OA-Hard-Question

        public void RunTest()
        {
            foreach (var (serverStates, k) in new (string, int)[]
            {
                ("1001", 2), // 4
                ("11101010110011", 2), // 8
            })
            {
                Console.WriteLine(GetMaxConsecutiveOn(serverStates, k));
            }
        }

        public int GetMaxConsecutiveOn(string serverStates, int k)
        {
            int flips = 0;
            int result = 0;

            for (var (s, e) = (0, 0); e < serverStates.Length; e++)
            {
                if (serverStates[e] == '0')
                {
                    while (e < serverStates.Length && serverStates[e] == '0')
                    {
                        ++e;
                    }
                    --e;
                    flips++;
                }

                while (flips > k)
                {
                    if (serverStates[s] == '0')
                    {
                        --flips;
                        while (serverStates[s] == '0')
                        {
                            ++s;
                        }
                        --s;
                    }
                    ++s;
                }

                result = Math.Max(result, e - s + 1);
            }

            return result;
        }
    }
}
