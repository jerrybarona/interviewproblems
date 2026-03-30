using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Google
{
    internal class P018StringTemplateSubstitutions : ITestable
    {
        // https://leetcode.com/discuss/interview-question/6223096/Google-Onsite
        public void RunTest()
        {
            foreach (var (text, subs) in new (string, Dictionary<string, string>)[]
            {
                ("%X%_%Y%", new() { { "X", "123" }, { "Y", "456" }, { "Z", "abc" } }), // 123_456
                ("%X%_%Y%_%Z%", new() { { "X", "123" }, { "Y", "456" }, { "Z", "abc" } }), // 123_456_abc
                ("%X%_%Y%_%Z%", new() { { "X", "123" }, { "Y", "456" }, { "Z", "abc" }, { "A", "def" } }), // 123_456_abc
                ("%X%_%Y%_%Z%", new() { { "X", "123" }, { "Y", "456" }, { "Z", "abc%Y%" } }), // 123_456_abc456
                ("key 1 = %k1%, key 3 = %k3%", new() { { "k1", "v1"}, { "k2", "%k1% v2" }, { "k3", "%k2%, v3" } }) // key 1 = v1, key 3 = v1, v2, v3
            })
            {
                Console.WriteLine(FormatText(text, subs));
            }
        }

        private string FormatText(string text, Dictionary<string,string> subs)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < text.Length; ++i)
            {
                if (text[i] != '%')
                {
                    sb.Append(text[i]);
                    continue;
                }

                var key = new StringBuilder();
                while (++i < text.Length && text[i] != '%')
                {
                    key.Append(text[i]);
                }
                sb.Append(FormatText(subs[key.ToString()], subs));
            }

            return sb.ToString();
        }
    }
}
