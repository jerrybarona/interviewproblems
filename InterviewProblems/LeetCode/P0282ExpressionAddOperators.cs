using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0282ExpressionAddOperators
    {
        public void AddOperatorsTest()
        {
            var num = "2147483648";
            var target = -2147483648;

            Console.WriteLine($"[{string.Join(", ", AddOperators(num, target))}]");
        }

        public IList<string> AddOperators(string num, int target)
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(num))
            {
                sb.Append(num[0]);
                //sb.Append("214748364");
                Process(1, num[0] - '0', num[0] - '0', num[0] - '0');
                //Process(9, 214748364, 214748364, 214748364);
            }

            return result;

            void Process(int idx, int currSum, int currProd, int lastNum)
            {
                if (idx == num.Length)
                {
                    if (currSum == target) result.Add(sb.ToString());
                    return;
                }

                var n = num[idx] - '0';

                sb.Append($"+{num[idx]}");
                Process(idx + 1, currSum + n, n, n);
                sb.Length -= 2;

                sb.Append($"-{num[idx]}");
                Process(idx + 1, currSum - n, -n, n);
                sb.Length -= 2;

                if (n == 0 || (currProd <= int.MaxValue / n && currProd >= int.MinValue / n))
                {
                    sb.Append($"*{num[idx]}");
                    var prod = currProd * n;
                    var sum = currSum - currProd + prod;
                    Process(idx + 1, sum, prod, n);
                    sb.Length -= 2;
                }

                if (lastNum != 0 && (lastNum < int.MaxValue / 10 - n))
                {
                    sb.Append(n);
                    var num2 = lastNum * 10 + n;
                    var prod2 = (currProd / lastNum) * num2;
                    var sum2 = currSum - currProd + prod2;
                    Process(idx + 1, sum2, prod2, num2);
                    --sb.Length;
                }
            }
        }
    }
}
