using System;
using System.Text;

namespace InterviewProblems.Google
{
    public class P008EncodeNumber
    {
        // https://leetcode.com/discuss/interview-question/453156/Google-or-Onsite-or-Encode-number

        public string Encode(int number)
        {
            var result = new StringBuilder();
            for (var k = 100000; k > 0; k /= 10)
            {
                var quotient = number / k;
                if (quotient < 10)
                {
                    result.Insert(0, number.ToString());
                    break;
                }

                var c = (char) (Math.Min(quotient, 35) - 10 + 'A');
                result.Insert(0, c);

                number -= Math.Min(quotient, 35) * k;
            }

            while (result.Length < 6) result.Insert(0, 0);

            return result.ToString();
        }
    }
}
