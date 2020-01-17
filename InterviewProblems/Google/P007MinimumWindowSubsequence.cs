using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P007MinimumWindowSubsequence
    {
        // LC

        public string MinWindow(string S, string T)
        {
            var memo = Enumerable.Repeat(0, S.Length).Select(x => new List<int>[T.Length]).ToArray();
            var ans = MinWin(0, 0);
            return ans.Count == 0 || ans.Last() == -1 ? string.Empty : S.Substring(ans[0], ans[ans.Count - 1] - ans[0] + 1);

            List<int> MinWin(int sidx, int tidx)
            {
                if (tidx == T.Length) return new List<int>();
                if (sidx == S.Length) return new List<int> { -1 };

                if (memo[sidx][tidx] != null) return memo[sidx][tidx];

                var result = MinWin(sidx + 1, tidx);
                if (S[sidx] == T[tidx])
                {
                    var matched = MinWin(sidx + 1, tidx + 1);
                    if (matched.Count == 0 || matched.Count > 0 && matched.Last() != -1) matched.Insert(0, sidx);
                    if (result.Count > 0 && matched.Count > 0 && result.Last() == -1 && matched.Last() == -1) memo[sidx][tidx] = result;
                    // else if (result.Count > 0 && result.Last() == -1 || matched.Count > 0 && matched.Last() == -1)
                    // {
                    //     memo[sidx][tidx] = result.Last() == -1 ? matched : result;
                    // }

                    if (matched.Count > 0 && matched.Last() != -1 &&
                        (result.Count == 0 ||
                         result.Last() == -1 ||
                         LenCompare(matched, result))) result = matched;
                }

                memo[sidx][tidx] = result;
                return result;
            }

            bool LenCompare(List<int> list1, List<int> list2)
            {
                var len1 = list1.Count == 0 ? int.MaxValue : list1[list1.Count - 1] - list1[0] + 1;
                var len2 = list2.Count == 0 ? int.MaxValue : list2[list2.Count - 1] - list2[0] + 1;

                if (len1 != len2) return len1 < len2;
                return list1.Count > 0 && list2.Count > 0 && list1.First() < list2.First();
            }
        }
    }
}
