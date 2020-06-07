using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P006ZenDecoder
    {
        // https://leetcode.com/discuss/interview-question/513369/Facebook-or-Phone-or-interesting-string-decode-question.-How-to-solve

        // https://leetcode.com/discuss/interview-question/525170/Facebook-or-Phone-or-Shortest-string-that-is-not-a-subsequence-of-a-given-string

        public void ZenDecoderTest()
        {
            var r1 = ZenDecoder("li1eli0eee");
            Console.WriteLine(Printer(r1));

            var r2 = ZenDecoder("lli456eee");
            Console.WriteLine(Printer(r2));

            var r3 = ZenDecoder("i10e");
            Console.WriteLine(Printer(r3));

            var r4 = ZenDecoder("4:word");
            Console.WriteLine(Printer(r4));

            var r5 = ZenDecoder("li187eli0eee");
            Console.WriteLine(Printer(r5));
        }

        private string Printer(object obj)
        {
            if (obj is List<object> list)
            {
                var res = new List<string>();
                foreach (var item in list)
                {
                    switch (item)
                    {
                        case int i:
                            res.Add(i.ToString());
                            break;
                        case string s:
                            res.Add(s);
                            break;
                        default:
                            res.Add(Printer(item));
                            break;
                    }
                }

                return $"[{string.Join(',', res)}]";
            }
            return obj.ToString();
        }

        public object ZenDecoder(string str)
        {
            var idx = 0;
            var ans = Process();
            return ans.Count > 0 ? ans[0] : ans;

            List<object> Process()
            {
                var result = new List<object>();
                while (idx < str.Length && str[idx] != 'e')
                {
                    if (str[idx] == 'i')
                    {
                        ++idx;
                        var n = 0;
                        while (str[idx] != 'e') n = 10 * n + str[idx++] - '0';
                        result.Add(n);
                    }
                    else if (str[idx] == 'l')
                    {
                        ++idx;
                        result.Add(Process());
                        ++idx;
                    }
                    else if (char.IsDigit(str[idx]))
                    {
                        var n = 0;
                        while (char.IsDigit(str[idx])) n = 10 * n + str[idx++] - '0';
                        ++idx;
                        var sb = new StringBuilder();
                        for (var i = 0; i < n; ++i) sb.Append(str[idx++]);
                        result.Add(sb.ToString());
                    }

                    ++idx;
                }

                return result;
            }
        }
    }
}
