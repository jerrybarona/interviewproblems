using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Amazon
{
    public class P007LongestStringMadeUpOfOnlyVowels
    {
        public int Longest(string str)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            var s = str.Select((val, idx) => (val, idx)).FirstOrDefault(x => !vowels.Contains(x.val));
            if (s == default) return str.Length;
            var e = str.Select((val, idx) => (val, idx)).Last(x => !vowels.Contains(x.val));
            
            var result = s.idx + str.Length - e.idx - 1;
            var max = 0;
            var curr = 0;
            for (var i = s.idx; i <= e.idx; ++i)
            {
                if (vowels.Contains(str[i]))
                {
                    ++curr;
                    max = Math.Max(max, curr);
                }
                else curr = 0;
            }

            return result + max;
        }
    }
}
