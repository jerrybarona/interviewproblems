using System;
using System.Text;

namespace InterviewProblems.Google
{
    public class P014DecodeString
    {
        // https://leetcode.com/discuss/interview-question/489169/Google-or-Phone-or-Decode-String-and-Collatz-Conjecture

        public void DecodeStringTest()
        {
            Console.WriteLine($"Input: 'ab(cde)[3]', Result: '{DecodeString("ab(cde)[3]")}'");
            Console.WriteLine($"Input: 'a(bc(e)[3])[2]', Result: '{DecodeString("a(bc(e)[3])[2]")}'");
        }

        public string DecodeString(string s)
        {
            var idx = 0;
            return Decode();

            string Decode()
            {
                var sb = new StringBuilder();
                var num = 0;
                while (idx < s.Length && s[idx] != ')')
                {
                    if (char.IsLetter(s[idx]))
                    {
                        sb.Append(s[idx++]);
                    }

                    else if (s[idx] == '(')
                    {
                        ++idx;
                        var next = Decode();
                        idx += 2;
                        while (s[idx] != ']') num = 10 * num + s[idx++] - '0';
                        
                        sb.Append(new StringBuilder(next.Length*num).Insert(0, next, num));
                        ++idx;
                    }
                }

                return sb.ToString();
            }
        }
    }
}
