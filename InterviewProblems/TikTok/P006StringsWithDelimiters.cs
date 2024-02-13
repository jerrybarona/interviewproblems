using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.TikTok
{
    public class P006StringsWithDelimiters : ITestable
    {
        // https://leetcode.com/discuss/interview-question/3541811/TikTok-or-Round-1-or-Backend-Engineer

        public void RunTest()
        {
            foreach (var input in new string[][]
            {
                new[]{"one$", "two$three$", "four", "five", "six$", "seven$"}
            })
            {
                Console.WriteLine(string.Join(", ", SeparateStrings(input)));
            }
        }

        public List<string> SeparateStrings(string[] input)
        {
            var result = new List<string>();
            var sb = new StringBuilder();

            foreach (var s in input)
            {
                foreach (var letter in s)
                {
                    if (letter == '$')
                    {
                        result.Add(sb.ToString());
                        sb.Length = 0;
                        continue;
                    }

                    sb.Append(letter);
                }
            }

            if (sb.Length > 0)
            {
                result.Add(sb.ToString());
            }

            return result;
        }
    }
}
