using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    internal class P0472ConcatenatedWords : ITestable
    {
        public void RunTest()
        {
            foreach (var words in new string[][]
            {
                new [] { "cat", "dog", "catdog" }            
            })
            {
                Console.WriteLine("[" + string.Join(", ", FindAllConcatenatedWordsInADict(words)) + "]");
            }
        }

        public IList<string> FindAllConcatenatedWordsInADict(string[] words)
        {
            var wordSet = new HashSet<string>(words);
            var memo = new Dictionary<string, bool>();

            return words.Where(word => IsConcat(word)).ToList();

            bool IsConcat(string w, int idx = 0)
            {
                if (idx == w.Length) return true;
                string curr = w[idx..];
                if (memo.ContainsKey(curr)) return memo[curr];
                if (idx != 0 && wordSet.Contains(curr)) return true;

                var result = false;
                for (var i = idx; i < w.Length; ++i)
                {
                    var str = w[idx..(i + 1)];
                    if (str != w && wordSet.Contains(str) && IsConcat(w, i + 1))
                    {
                        result = true;
                        break;
                    }
                }

                memo.Add(curr, result);
                return result;
            }
        }
    }
}
