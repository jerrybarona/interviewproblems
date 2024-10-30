using System;
using System.Text;

namespace InterviewProblems.Stripe
{
    internal class P001MajorMinorPartSplit : ITestable
    {
        // https://leetcode.com/discuss/interview-question/4057853/Stripe-Phone-Interview

        public void RunTest()
        {
            foreach (var (str, countMinorParts) in new (string, int)[]
            {
                ("stripe.com/payments/checkout/customer.john.doe", 2), // s4e.c1m/p6s/c6t/c6r.j5e
                ("www.api.stripe.com/checkout", 3), // w1w.a1i.s7m/c6t
                ("www.api.stripe.com//checkout////www.api.stripe.com.www.api.stripe.com", 3),
            })
            {
                Console.WriteLine(Compress(str, countMinorParts));
            }
        }

        private string Compress(string str, int countMinorParts)
        {
            var majorParts = str.Split("/");
            var sb = new StringBuilder();
            foreach (var majorPart in majorParts)
            {
                if (majorPart == string.Empty) continue;
                if (sb.Length > 0) sb.Append("/");
                var minorParts = majorPart.Split(".");
                var idx = 0;
                for (var count = 0; count < countMinorParts - 1; ++count)
                {
                    if (idx > 0) sb.Append('.');
                    sb.Append(minorParts[idx][0]);
                    sb.Append(minorParts[idx].Length - 2);
                    sb.Append(minorParts[idx][^1]);
                    ++idx;
                    if (idx == minorParts.Length) break;
                }

                if (idx == minorParts.Length) continue;
                if (idx > 0) sb.Append('.');
                sb.Append(minorParts[idx][0]);
                if (idx == minorParts.Length - 1)
                {
                    sb.Append(minorParts[idx].Length - 2);
                    sb.Append(minorParts[idx][^1]);
                }
                else
                {
                    var charCount = minorParts[idx].Length - 1;
                    ++idx;
                    for (; idx < minorParts.Length; ++idx)
                    {
                        if (idx == minorParts.Length - 1)
                        {
                            charCount += minorParts[idx].Length - 1;
                            sb.Append(charCount);
                            sb.Append(minorParts[idx][^1]);
                        }
                        else
                        {
                            charCount += minorParts[idx].Length;
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
