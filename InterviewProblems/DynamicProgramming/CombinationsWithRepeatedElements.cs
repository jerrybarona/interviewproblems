using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InterviewProblems.DynamicProgramming
{
    public class CombinationsWithRepeatedElements
    {
        public List<string> Combinations(string str)
        {
            var dict = str
                .Select(c => c)
                .Aggregate(new Dictionary<char, int>(), (res, x) =>
                {
                    if (!res.ContainsKey(x)) res.Add(x, 0);
                    ++res[x];
                    return res;
                })
                .Select(y => (letter: y.Key, count: y.Value))
                .ToList();

            var result = new List<string>();
            Combine(0, new StringBuilder());
            return result;

            void Combine(int idx, StringBuilder sb)
            {
                result.Add(sb.ToString());

                for (var i = idx; i < dict.Count; ++i)
                {
                    var count = dict[i].count;
                    if (count > 0)
                    {
                        dict[i] = (dict[i].letter, dict[i].count - 1);
                        sb.Append(dict[i].letter);
                        Combine(i, sb);
                        sb.Length--;
                        dict[i] = (dict[i].letter, dict[i].count + 1);
                    }
                }
            }
        }
    }
}
