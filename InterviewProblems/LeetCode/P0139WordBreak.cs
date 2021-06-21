using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
	public class P0139WordBreak
	{
        public void WordBreakTest()
        {
            var s = "aebbbbs";
            var wordDict = new[] { "a", "aeb", "ebbbb", "s", "eb" };

            Console.WriteLine(WordBreak(s, wordDict));
        }

        public bool WordBreak(string s, IList<string> wordDict)
        {
            var min = wordDict.Select(w => w.Length).DefaultIfEmpty(0).Min();
            var max = wordDict.Select(w => w.Length).DefaultIfEmpty(0).Max();

            var words = wordDict.ToHashSet();

            for (var queue = new Queue<int>(new[] { 0 }); queue.Count > 0;)
            {
                var curr = queue.Dequeue();
                if (curr >= s.Length) return true;
                for (var end = curr + min; end <= s.Length && end <= curr + max; ++end)
                {
                    Console.WriteLine(s[curr..end]);
                    if (words.Contains(s[curr..end]))
                    {
                        queue.Enqueue(end);
                    }
                }
            }

            return false;
        }
    }
}
