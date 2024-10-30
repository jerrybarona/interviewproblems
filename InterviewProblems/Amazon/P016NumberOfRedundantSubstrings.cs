using System;
using System.Collections.Generic;

namespace InterviewProblems.Amazon
{
    internal class P016NumberOfRedundantSubstrings : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5480280/Amazon-OA

        public void RunTest()
        {
            foreach (var (word, a, b) in new (string, int, int)[]
            {
                ("abbacc", -1, 2), // 5
                ("akljfs", -2, 1), // 15
            })
            {
                Console.WriteLine(GetRedundantSubstrings(word, a, b));
            }
        }

        private int GetRedundantSubstrings(string word, int a, int b)
        {
            var map = new Dictionary<int, int>
            {
                { 0, 1 }
            };

            var result = 0;

            int currV = 0;
            int currC = 0;

            foreach (var letter in word)
            {
                if (IsVowel(letter))
                {
                    ++currV;
                }
                else
                {
                    ++currC;
                }

                var key = (a - 1) * currV + (b - 1) * currC;
                if (map.TryGetValue(key, out int value))
                {
                    result += value;
                }
                else
                {
                    map.Add(key, 0);
                }

                ++map[key];
            }

            return result;

            bool IsVowel(char l)
            {
                return l == 'a' || l == 'e' || l == 'i' || l == 'o' || l == 'u';
            }
        }
    }
}
