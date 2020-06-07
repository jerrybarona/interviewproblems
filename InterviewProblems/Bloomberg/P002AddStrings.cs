using System;
using System.Collections.Generic;

namespace InterviewProblems.Bloomberg
{
    public class P002AddStrings
    {
        // https://leetcode.com/discuss/interview-question/613396/Bloomberg-or-Phone-or-Add-Strings

        public void AddStringsTest()
        {
            var s1 = "245";
            var s2 = "50";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "245";
            s2 = "-50";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "-245";
            s2 = "-50";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "-245";
            s2 = "50";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "0";
            s2 = "-50";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "0";
            s2 = "50";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "245";
            s2 = "0";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "-245";
            s2 = "0";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");
        }

        public string AddStrings(string s1, string s2)
        {
            var isS1Pos = s1[0] != '-';
            var isS2Pos = s2[0] != '-';
            var isSubtraction = isS1Pos ^ isS2Pos;
            var isResultPos = true;

            var stack = new Stack<char>();
            for (var (i1, i2, carry) = (s1.Length - 1, s2.Length - 1, 0);
                 i1 >= 0 && char.IsDigit(s1[i1]) || i2 >= 0 && char.IsDigit(s2[i2]);
                 --i1, --i2)
            {
                var op2 = carry;
                if (i2 >= 0 && char.IsDigit(s2[i2])) op2 += s2[i2] - '0';

                var op1 = 0;
                if (i1 >= 0 && char.IsDigit(s1[i1])) op1 += s1[i1] - '0';

                var sum = isSubtraction ? op1 - op2 : op1 + op2;
                carry = sum < 0 || sum >= 10 ? 1 : 0;

                isResultPos = sum >= 0;
                stack.Push((char) ((sum + 10) % 10 + '0'));
            }

            if (!isSubtraction && !isS1Pos || isSubtraction && isResultPos && !isS1Pos ||
                isSubtraction && !isResultPos && isS1Pos) stack.Push('-');
            return string.Join(string.Empty, stack);
        }
    }
}
