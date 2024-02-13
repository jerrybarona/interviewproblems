using System;
using System.Linq;

namespace InterviewProblems.TikTok
{
    public class P002MessageEncryption : ITestable
    {
        // https://leetcode.com/discuss/interview-question/4066495/TikTok-OA

        public void RunTest()
        {
            foreach (var message in new string[][]
            {
                new [] { "aba", "cbd" },
                new [] { "abc", "cby", "ebz" },
            })
            {
                Console.WriteLine($"I: ['{string.Join("','", message)}']");
                Console.WriteLine($"O: '{Subencrypt(message)}'");
            }
        }

        public string Subencrypt(string[] message)
        {
            var n = message.Length;
            var k = message[0].Length;
            var map = Enumerable.Repeat(0, (k+1)/2).Select(x => new int[26]).ToArray();

            foreach (var word in message)
            {
                for (var (s,e) = (0, k-1); s <= e; ++s, --e)
                {
                    ++map[s][word[s] - 'a'];

                    if (s == e)
                    {
                        continue;
                    }
                    
                    ++map[s][word[e] - 'a'];
                }
            }

            char[] result = new char[k];
            for (var i = 0; i < map.Length; ++i)
            {
                var arr = map[i];
                var pivot = i == map.Length - 1 && k % 2 == 1 ? n / 2: n;

                var count = 0;
                var j = 0;
                for ( ; j < 26; ++j)
                {
                    count += arr[j];
                    if (count >= pivot) break;
                }

                result[i] = (char)(j + 'a');
                result[k - i - 1] = (char)(j + 'a');
            }

            return new string(result);
        }
    }
}
