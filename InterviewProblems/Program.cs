using InterviewProblems.Cracking;
using InterviewProblems.LeetCode;
using InterviewProblems.Tushar;
using System;
using System.Collections.Generic;

namespace InterviewProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            var sln = new P0140WordBreakII();
            var result = sln.WordBreak("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", new List<string> { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa" });
            Console.WriteLine("Result:");
            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
            Console.ReadLine();
        }
    }
}
