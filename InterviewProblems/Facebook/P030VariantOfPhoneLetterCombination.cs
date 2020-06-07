using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P030VariantOfPhoneLetterCombination
    {
        // https://leetcode.com/discuss/interview-question/287659/Facebook-or-Onsite-or-Variant-of-phone-letters-combination

        private readonly string[] _letters = { "Z", "ABC", "DEF", "GHI", "JKL", "MNO", "PQR", "ST", "UVW", "XY" };

        public void PhoneLetterCombinationTest()
        {
            var input = new [] { 1, 1, 2 };
            Console.WriteLine($"\nInput: [{string.Join(", ", input)}]");
            Console.WriteLine($"Output: [{string.Join(", ", PhoneLetterCombination(input).Select(x => $"\"{x}\""))}]");

            input = new[] { 1, 1, 1, 2 };
            Console.WriteLine($"\nInput: [{string.Join(", ", input)}]");
            Console.WriteLine($"Output: [{string.Join(", ", PhoneLetterCombination(input).Select(x => $"\"{x}\""))}]");

            input = new[] { 1, 1, 1, 1, 2 };
            Console.WriteLine($"\nInput: [{string.Join(", ", input)}]");
            Console.WriteLine($"Output: [{string.Join(", ", PhoneLetterCombination(input).Select(x => $"\"{x}\""))}]");
        }

        public List<string> PhoneLetterCombination(int[] digits)
        {
            var result = new List<string>();
            var sb = new StringBuilder();

            Combine();

            return result;

            void Combine(int idx = 0)
            {
                if (idx == digits.Length)
                {
                    result.Add(sb.ToString());
                    return;
                }

                var digit = digits[idx];
                var letters = _letters[digit];
                for (var (i, count) = (idx, 0); i < digits.Length && digits[i] == digit; ++i, count = ++count % letters.Length)
                {
                    sb.Append(letters[count]);
                    Combine(i + 1);
                    --sb.Length;
                }
            }
        }
    }
}
