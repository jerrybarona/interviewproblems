using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P022CandyCrush1d
    {
        // https://leetcode.com/discuss/interview-question/380650/

        public void CandyCrushTest()
        {
            var input = "aaabbbc";
            Console.WriteLine($"\nInput: \"{input}\"\nOutput: \"{CandyCrush(input)}\"");

            input = "aabbbacd";
            Console.WriteLine($"\nInput: \"{input}\"\nOutput: \"{CandyCrush(input)}\"");

            input = "aabbccddeeedcba";
            Console.WriteLine($"\nInput: \"{input}\"\nOutput: \"{CandyCrush(input)}\"");

            input = "aaabbbacd";
            Console.WriteLine($"\nInput: \"{input}\"\nOutput: \"{CandyCrush(input)}\"");
        }

        public string CandyCrush(string s)
        {
            var stack = new Stack<Node>();

            foreach (var c in s)
            {
                if (stack.Count > 0 && c != stack.Peek().Ch && stack.Peek().Count >= 3) stack.Pop();
                if (stack.Count > 0 && c == stack.Peek().Ch) stack.Peek().Count++;
                else stack.Push(new Node(c, 1));
            }
            if (stack.Count > 0 && stack.Peek().Count >= 3) stack.Pop();

            return string.Join(string.Empty, stack.Reverse().Select(x => new string(x.Ch, x.Count)));
        }

        class Node
        {
            public char Ch { get; set; }
            public int Count { get; set; }

            public Node(char ch, int count)
            {
                Ch = ch;
                Count = count;
            }
        }
    }
}
