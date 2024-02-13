using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P2030SmallestKLengthSubsequenceWithOccurrencesOfALetter : ITestable
    {
        public void RunTest()
        {
            foreach (var (s, k, letter, repetition) in new (string,int, char, int)[]
            {
                ("eeaabe", 4, 'e', 2)
            })
            {
                Console.WriteLine(SmallestSubsequence(s, k, letter, repetition));
            }
        }

        public string SmallestSubsequence(string s, int k, char letter, int repetition)
        {
            var len = s.Length;
            var total = s.Count(x => x == letter);
            var count = 0;
            var stack = new Stack<char>();

            for (var i = 0; i < len; ++i)
            {
                while (stack.Count > 0 && s[i] < stack.Peek())
                {
                    if (s[i] == letter && k - stack.Count < total)
                    {
                        if (stack.Pop() == letter) --count;
                        break;
                    }
                    if (stack.Peek() == letter && count + total == repetition) break;
                    if (stack.Count + len - i == k) break;

                    if (stack.Pop() == letter) --count;
                }

                if (s[i] == letter)
                {
                    ++count;
                    --total;
                }

                stack.Push(s[i]);
            }

            while (stack.Count > k) stack.Pop();

            return string.Concat(stack.Reverse());
        }
    }
}
