using System;

namespace InterviewProblems.Microsoft
{
    public class P031BinaryEncodingOperations
    {
        // https://leetcode.com/discuss/interview-question/651142/Microsoft-Online-Assesment-Question

        public void NumberOfOperationsTest()
        {
            var s = "011100";
            Console.WriteLine($"Input: \"{s}\"; Output: {NumberOfOperations(s)}");

            s = "111";
            Console.WriteLine($"Input: \"{s}\"; Output: {NumberOfOperations(s)}");

            s = "1111010101111";
            Console.WriteLine($"Input: \"{s}\"; Output: {NumberOfOperations(s)}");
        }

        public int NumberOfOperations(string s)
        {
            var result = 0;
            var idx = 0;
            while (idx < s.Length && s[idx] == '0') ++idx;
            for (; idx < s.Length; ++idx) result += s[idx] == '1' ? 2 : 1;

            return Math.Max(0, result - 1);
        }
    }
}
