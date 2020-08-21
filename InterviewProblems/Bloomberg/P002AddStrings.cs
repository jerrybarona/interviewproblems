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
            var s2 = "950";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");

            s1 = "245";
            s2 = "-950";
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

            s1 = "-245";
            s2 = "245";
            Console.WriteLine($"\"{s1}\" + \"{s2}\" -> \"{AddStrings(s1, s2)}\"");
        }

        public string AddStrings(string s1, string s2)
        {
            var large = IsGreaterOrEqual(s1, s2) ? s1 : s2;
            var small = large == s1 ? s2 : s1;
            var isLargePos = large[0] != '-';
            var isSmallPos = small[0] != '-';
            var isSubtraction = isLargePos ^ isSmallPos;
            var carry = 0;

            var stack = new Stack<char>();
            for (var (lidx, sidx) = (large.Length - 1, small.Length - 1);
                 lidx >= 0 && char.IsDigit(large[lidx]) || sidx >= 0 && char.IsDigit(small[sidx]);
                 --lidx, --sidx)
            {
                var smallop = carry;
                if (sidx >= 0 && char.IsDigit(small[sidx])) smallop += small[sidx] - '0';

                var largeop = 0;
                if (lidx >= 0 && char.IsDigit(large[lidx])) largeop += large[lidx] - '0';

                var sum = isSubtraction ? largeop - smallop : largeop + smallop;
                carry = sum < 0 || sum >= 10 ? 1 : 0;

                stack.Push((char) ((sum + 10) % 10 + '0'));
            }

            if (carry > 0) stack.Push((char) (carry + '0'));
            var result = ReduceZeroes(string.Join(string.Empty, stack));
            if (result.Length > 0 && !isLargePos) result = "-" + result;

            return result.Length == 0 ? "0" : result;

            string ReduceZeroes(string str)
            {
                var idx = 0;
                for (; idx < str.Length && str[idx] == '0'; ++idx)
                {
                }

                return str.Substring(idx);
            }
                
            bool IsGreaterOrEqual(string str1, string str2)
            {
                str1 = char.IsDigit(str1[0]) ? str1 : str1.Substring(1);
                str2 = char.IsDigit(str2[0]) ? str2 : str2.Substring(1);

                if (str1.Length != str2.Length) return str1.Length > str2.Length;
                for (var i = 0; i < str1.Length; ++i)
                {
                    if (str1[i] != str2[i]) return str1[i] > str2[i];
                }

                return true;
            }
        }
    }
}
