using System;
using System.Collections.Generic;

namespace InterviewProblems.Microsoft
{
    public class P033NumberOfIdenticalPairsOfIndices
    {
        // https://leetcode.com/discuss/interview-question/651142/Microsoft-Online-Assesment-Question

        public void NumberOfPairsTest()
        {
            var a = new[] { 3, 5, 6, 3, 3, 5 };
            Console.WriteLine($"\nInput: [{string.Join(", ", a)}]\nOutput: {NumberOfPairs(a)}");
        }

        public int NumberOfPairs(int[] a)
        {
            var map = new Dictionary<int,int>();
            var result = 0;
            foreach (var num in a)
            {
                if (map.ContainsKey(num)) result += map[num]++;
                else map.Add(num, 1);
            }

            return result;
        }
    }
}
