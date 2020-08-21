using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.AlgoExpert
{
    public class LongestStringChain
    {
        public void LongestStringChainTest()
        {
            var strings = new List<string>
            { "abde", "abc", "abd", "abcde",
                "ade",
                "ae",
                "1abde",
                "abcdef"
            };
            Console.WriteLine(longeststringChain(strings));
        }

		public static List<string> longeststringChain(List<string> strings)
        {
            strings = strings.OrderByDescending(s => s.Length).ToList();
            var stringSet = new HashSet<string>(strings);
            var memo = new Dictionary<string, List<string>>();


            return strings.Aggregate(new List<string>(), (res, str) =>
            {
                var next = Longest(str, stringSet, memo);
                return next.Count > 1 && next.Count > res.Count ? next : res;
            });
        }

        private static List<string> Longest(string str, HashSet<string> stringSet, Dictionary<string, List<string>> memo)
        {
            if (memo.ContainsKey(str)) return memo[str];

            var result = new List<string> { str };
            List<string> next = null;
            var count = 0;

            for (var i = 0; i < str.Length; ++i)
            {
                var shorter = str.Substring(0, i) + str.Substring(i + 1);
                if (stringSet.Contains(shorter))
                {
                    var elems = Longest(shorter, stringSet, memo);
                    if (elems.Count > count)
                    {
                        count = elems.Count;
                        next = elems;
                    }
                }
            }

            if (next != null) result.AddRange(next);
            memo.Add(str, result);

            return result;
        }
    }
}

