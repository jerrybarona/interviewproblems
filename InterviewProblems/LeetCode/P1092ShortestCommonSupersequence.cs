using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P1092ShortestCommonSupersequence
    {
        public string ShortestCommonSupersequence(string str1, string str2)
        {
            var large = str1.Length >= str2.Length ? str1 : str2;
            var small = str1 == large ? str2 : str1;
            var inserts = new List<(int idx, char c)>();

            var map = large.Select((val, index) => (val, index))
                    .Aggregate(new Dictionary<char, List<int>>(), (res, x) =>
                    {
                        if (!res.ContainsKey(x.val)) res.Add(x.val, new List<int>());
                        res[x.val].Add(x.index);
                        return res;
                    });
            var memo = new Dictionary<(int, int), string>();
            var lcs = Lcs(0, 0);
            var lcsMap = lcs
                    .Aggregate(new Dictionary<char, int>(), (res, x) =>
                    {
                        if (!res.ContainsKey(x)) res.Add(x, 0);
                        ++res[x];
                        return res;
                    });
            var idx = -1;
            foreach (var c in small)
            {
                if (lcsMap.ContainsKey(c))
                {
                    lcsMap[c]--;
                    if (lcsMap[c] == 0) lcsMap.Remove(c);
                    var lIdx = Search(map[c], 0, map[c].Count - 1, idx + 1);
                    if (lIdx != -1)
                    {
                        idx = map[c][lIdx];
                        continue;
                    }
                }
                inserts.Add((idx + 1, c));                
            }

            var sb = new StringBuilder(large);
            for (var i = inserts.Count - 1; i >= 0; --i)
            {
                sb.Insert(inserts[i].idx, inserts[i].c);
            }

            return sb.ToString();

            
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

            int Search(List<int> list, int start, int end, int target)
            {
                var result = -1;
                for (var mid = start + (end - start)/2; start <= end; mid = start + (end - start) / 2)
                {
                    if (list[mid] >= target)
                    {
                        if (list[mid] == target) return mid;
                        result = mid;
                        end = mid - 1;
                    }
                    else start = mid + 1;
                }
                return result;
            }
        }
    }
}
