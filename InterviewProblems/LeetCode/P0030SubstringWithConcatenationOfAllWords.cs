using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0030SubstringWithConcatenationOfAllWords : ITestable
    {
        public void RunTest()
        {
            foreach (var (s, words) in new (string s, string[] words)[]
            { 
                (s: "abaababbaba", words: new []{"ab","ba","ab","ba"}),

            })
            {
                Console.WriteLine($"s = \"{s}\"");
                Console.WriteLine($"words = [{string.Join(", ", words)}]");
                Console.WriteLine($"output: [{string.Join(", ", FindSubstring(s, words))}]");
            }
        }
        public IList<int> FindSubstring(string s, string[] words)
        {
            var result = new List<int>();
            var wordsLen = words.Length;
            var wordLen = words[0].Length;
            var sLen = s.Length;

            var wordsDict = words.Aggregate(new Dictionary<string, int>(), (dict, w) =>
            {
                if (!dict.ContainsKey(w)) dict.Add(w, 0);
                ++dict[w];

                return dict;
            });

            for (var i = 0; i < wordLen; ++i) Run(i);

            return result;

            void Run(int startIdx)
            {
                var count = 0;
                var dict = wordsDict.Keys.ToDictionary(k => k, k => 0);
                var queue = new Queue<string>();

                for (var (start, end) = (startIdx, startIdx); end <= sLen - wordLen; end += wordLen)
                {
                    var str = s[end..(end + wordLen)];
                    if (dict.ContainsKey(str))
                    {
                        queue.Enqueue(str);
                        ++dict[str];
                        ++count;

                        var wordTotalWinCount = wordsDict[str];
                        if (dict[str] == wordTotalWinCount && count == wordsLen)
                        {
                            dict[queue.Dequeue()]--;
                            --count;
                            result.Add(start);
                            start += wordLen;

                            continue;
                        }

                        for (; dict[str] > wordTotalWinCount; start += wordLen)
                        {
                            dict[queue.Dequeue()]--;
                            --count;
                        }

                        continue;
                    }

                    queue.Clear();
                    count = 0;
                    foreach (var key in dict.Keys) dict[key] = 0;
                    start = end + wordLen;
                }
            }
        }
    }
}
