using System;

namespace InterviewProblems.Microsoft
{
    public class P015LongestSemialternatingString
    {
        public int Longest(string str)
        {
            var result = 1;
            for (var (start, end, count) = (0, 1, 1); end < str.Length; ++end)
            {
                if (str[end] == str[end - 1]) ++count;
                else count = 1;

                if (count > 2)
                {
                    start = end - 1;
                    count = 2;
                }

                result = Math.Max(result, end - start + 1);
            }

            return result;
        }
    }
}
