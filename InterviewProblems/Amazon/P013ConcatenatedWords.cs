using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P013ConcatenatedWords
    {
        // https://leetcode.com/discuss/interview-question/545748/Amazon-subsidiary-or-Phone-or-Concatenated-Words

        public void ConcatenatedWordsTest()
        {
            var words = new[] { "rockstar", "rock", "stars", "rocks", "tar", "star", "rockstars", "super", "highway", "high", "way", "superhighway" };
            var r = ConcatenatedWords(words);
            foreach (var l in r)
            {
                Console.WriteLine($"[{string.Join(", ", l)}]");
            }
        }

        public List<List<string>> ConcatenatedWords(string[] words)
        {
            var wordSet = new HashSet<string>(words);
            var memo = new Dictionary<string, List<List<string>>>();
            memo.Add(string.Empty, new List<List<string>>());

            foreach (var w in words) IsConcatenation(w);

            return memo.Values.Where(v => v != null).SelectMany(x => x).Where(y => y.Count > 1).ToList();

            bool IsConcatenation(string word)
            {
                if (memo.ContainsKey(word)) return memo[word] != null;

                var result = new List<List<string>>();
                for (var i = 1; i <= word.Length; ++i)
                {
                    var prefix = word.Substring(0, i);
                    var suffix = word.Substring(i);
                    if (wordSet.Contains(prefix) && IsConcatenation(suffix))
                    {
                        if (memo[suffix].Count == 0) result.Add(new List<string> { prefix });
                        else result.AddRange(memo[suffix].Select(l => new List<string> { prefix }.Concat(l).ToList()));
                    }
                }

                if (result.Count == 0) memo.Add(word, null);
                else memo.Add(word, result);

                return memo[word] != null;
            }
        }
    }
}
