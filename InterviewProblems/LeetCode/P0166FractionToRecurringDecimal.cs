using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0166FractionToRecurringDecimal
    {
        public void FractionToDecimalTest()
        {
            var num = 6123;
            var dem = 9900;

            Console.WriteLine(FractionToDecimal(num, dem));
        }

        public string FractionToDecimal(int numerator, int denominator)
        {
            var rems = new HashSet<int>();
            var decimals = new List<(int r, int q)>();
            var quotient = numerator / denominator;
            var remainder = numerator % denominator;

            if (remainder == 0) return quotient.ToString();

            var last = WillDivide(remainder);
            if (last == int.MaxValue) return $"{quotient}.{string.Join("", decimals.Select(d => d.q))}";

            var result = new StringBuilder($"{quotient}.");
            foreach (var d in decimals)
            {
                if (d.r == last) result.Append('(');
                result.Append(d.q);
            }
            result.Append(')');

            return result.ToString();

            int WillDivide(int n)
            {
                var m = 10 * n;
                var q = m / denominator;
                var r = m % denominator;

                decimals.Add((r, q));
                if (r == 0) return int.MaxValue;
                if (rems.Add(r)) return WillDivide(r);
                return r;
            }
        }
    }
}
