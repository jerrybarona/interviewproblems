using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0722RemoveComments
    {
        public void RemoveCommentsTest()
        {
            var input = new[] { "a//*b//*c", "blank", "d/*/e*//f" };
            Console.WriteLine(string.Join(',', RemoveComments(input)));
        }

        public IList<string> RemoveComments(string[] source)
        {
            var idx = 0;
            var code = new List<string>();

            while (idx < source.Length)
            {
                var line = Process(false);
                if (!string.IsNullOrEmpty(line)) code.Add(line);
            }

            return code;

            string Process(bool isComment)
            {
                var sb = new StringBuilder();
                var commentStart = 0;
                for (var i = 0; i < source[idx].Length; ++i)
                {
                    if (!isComment)
                    {
                        sb.Append(source[idx][i]);
                        if (i > 0 && source[idx][i - 1] == '/' && source[idx][i] == '/')
                        {
                            sb.Length -= 2;
                            break;
                        }
                        else if (i > 0 && source[idx][i - 1] == '/' && source[idx][i] == '*')
                        {
                            sb.Length -= 2;
                            commentStart = i + 1;
                            isComment = true;
                        }

                    }
                    else
                    {
                        if (i > commentStart && source[idx][i - 1] == '*' && source[idx][i] == '/') isComment = false;
                    }
                }

                ++idx;
                if (isComment) sb.Append(Process(true));
                return sb.ToString();
            }
        }
    }
}
