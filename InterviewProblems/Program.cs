using InterviewProblems.CodeForces;
using InterviewProblems.Cracking;
using InterviewProblems.LeetCode;
using InterviewProblems.DynamicProgramming;
using System;
using System.Collections.Generic;
using InterviewProblems.Amazon;
using InterviewProblems.Utilities;
using System.Linq;
using InterviewProblems.Concurrency;
using InterviewProblems.Google;
using InterviewProblems.Microsoft;

namespace InterviewProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            var sln = new P010PlaceFlagsOnPeaks();
            var result = sln.PlaceFlags(new[] { 0, 1, 4, 2, 5, 7, 6, 5, 6, 2, 11, 0 });


            Console.WriteLine("Result:");
            Console.WriteLine(result);
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //} 
            Console.ReadLine();
        }
    }
}
