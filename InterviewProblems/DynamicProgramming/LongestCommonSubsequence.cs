using System.Collections.Generic;

namespace InterviewProblems.DynamicProgramming
{
    public class LongestCommonSubsequence
    {
        public string Get(string str1, string str2)
        {
            var memo = new Dictionary<(int, int), string>();
            return Lcs(0, 0);
            
            string Lcs(int idx1, int idx2)
            {
                if (idx1 == str1.Length || idx2 == str2.Length) return string.Empty;
                if (memo.ContainsKey((idx1, idx2))) return memo[(idx1, idx2)];

                var result = string.Empty;
                if (str1[idx1] == str2[idx2]) result = new string(str1[idx1], 1) + Lcs(idx1 + 1, idx2 + 1);

                var nextResults = new[] { Lcs(idx1 + 1, idx2), Lcs(idx1, idx2 + 1), Lcs(idx1 + 1, idx2 + 1) };

                foreach (var nextResult in nextResults)
                {
                    if (nextResult.Length > result.Length) result = nextResult;
                }

                memo.Add((idx1, idx2), result);
                return result;
            }
        }
    }
}
