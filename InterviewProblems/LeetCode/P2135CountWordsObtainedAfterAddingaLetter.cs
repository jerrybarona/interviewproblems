using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P2135CountWordsObtainedAfterAddingaLetter
    {
        public const uint one = 1;
        public const uint zero = 0;

        public void WordCountTest()
        {
            foreach (var (startWords, endWords) in new (string[], string[])[]
            {
                (new string[] {"ab"}, new string[] {"cba"})
            })
            {
                Console.WriteLine(WordCount(startWords, endWords));
            }
        }

        public int WordCount(string[] startWords, string[] targetWords)
        {
            var set = new HashSet<uint>(startWords.Select(w => GetCode(w)));
            var result = 0;
            foreach (var targetWord in targetWords)
            {
                var code = GetCode(targetWord);
                Console.WriteLine($"code: {code}");
                foreach (var letter in targetWord)
                {
                    uint x = code & ~(one << (int)(letter - 'a'));
                    Console.WriteLine($"x: {x}");
                    if (set.Contains(x)) ++result;
                }
            }

            return result;

            uint GetCode(string word)
            {
                return word.Aggregate(zero, (letter, code) => code | (one << (int)(letter - 'a')));
            }
        }
    }
}
