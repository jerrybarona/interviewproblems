using System;
using System.Text;

namespace InterviewProblems.TikTok
{
    public class P005CharacterOccurrences : ITestable
    {
        // https://leetcode.com/discuss/interview-question/3541811/TikTok-or-Round-1-or-Backend-Engineer

        public void RunTest()
        {
            foreach (var s in new[] { "GeeeEEKKKss" })
            {
                Console.WriteLine(CharacterOccurrences(s));
            }
        }

        public string CharacterOccurrences(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            var sb = new StringBuilder();
            var letter = s[0];
            var count = 1;

            for (var i = 1; i < s.Length; ++i)
            {
                if (s[i] == letter)
                {
                    ++count;
                    continue;
                }

                sb.Append($"{letter}{count}");
                letter = s[i];
                count = 1;
            }

            sb.Append($"{letter}{count}");

            return sb.ToString();
        }
    }
}
