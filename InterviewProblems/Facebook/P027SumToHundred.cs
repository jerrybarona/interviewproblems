using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P027SumToHundred
    {
        // https://leetcode.com/discuss/interview-question/357345/Facebook-or-Phone-Screen-or-Sum-to-100

        public void SumToHundredTest()
        {
            Console.WriteLine(string.Join('\n', SumToHundred()));
        }

        public List<string> SumToHundred()
        {
            var result = new List<string>();
            var sb = new StringBuilder();

            Process(0, 1, 0);

            return result;

            void Process(int total, int num, int last)
            {
                if (num == 10)
                {
                    if (total == 100) result.Add(sb.ToString());
                    return;
                }

                var added = sb.Length == 0 ? 1 : 2;
                sb.Append(added == 1 ? $"{num}" : $"+{num}");
                Process(total + num, num + 1, num);
                sb.Length -= added;

                sb.Append($"-{num}");
                Process(total - num, num + 1, -num);
                sb.Length -= 2;

                if (sb.Length == 0) return;
                sb.Append($"{num}");
                var prod = 10 * last + (last >= 0 ? num : -num);
                Process(total - last + prod, num + 1, prod);
                --sb.Length;
            }
        }
    }
}
