using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P031ShortestNonSubsequenceOfBinaryString
    {
        // https://leetcode.com/discuss/interview-question/525170/Facebook-or-Phone-or-Shortest-string-that-is-not-a-subsequence-of-a-given-string

        public string ShortestNonSubsequenceOfBinaryString(string s)
        {
            var sb = new StringBuilder();
            var count0 = 0;
            var count1 = 0;

            for (var i = 0; i <= s.Length; ++i)
            {
                if (i < s.Length)
                {
                    if (s[i] == '0') ++count0;
                    else ++count1;
                    if (i > 0 && s[i] == s[i-1]) continue;
                    
                }
            }


            return sb.ToString();
        }

    }
}
